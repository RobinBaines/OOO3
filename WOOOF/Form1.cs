//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
namespace WOOOF
{
    using OOOCL;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {

        internal static Dictionary<string, GDISO> GDISOs = new Dictionary<string, GDISO>();
        Point bottom_right_point;
        bool blnScriptBeingProcessed = false;
        int ticks = 0;
        Thread? newThread;
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.White;
            timer1.Interval = 100;
            timer1.Enabled = true;
            timer1.Start();
            textBox1.Text = "c:\\Projects\\OOO3\\Scripts\\Script.txt";
           
            panel1.AutoScroll = true;
            
            
            BindingList<SensualObject> TheSOs = BuildSOs.TheSOs;
            TheSOs.ListChanged += HandleSOChanged;

        }

        GDISO? LastNeighbourGDISO = null;
        GDISO? gDISO = null;

        /// <summary>
        /// Return the top GDISO parent of the SO.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        GDISO? RedrawFrom(string Name)
        {
            GDISO RedrawFromGDISO = GDISOs[Name];
            if (RedrawFromGDISO != null)
            {
                while (RedrawFromGDISO.SO.SOParent != null)
                    RedrawFromGDISO = GDISOs[RedrawFromGDISO.SO.SOParent.Name];
            }
            return RedrawFromGDISO;
        }

        void HandleSOChanged(object? sender, ListChangedEventArgs? e)
        {
            if (e != null)
            {
                if (e.ListChangedType == ListChangedType.Reset)
                {
                }

                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    //called from NotifyPropertyChanged(); if an SQ is added to SO.qualities.
                    SensualObject SO = BuildSOs.TheSOs[e.NewIndex];

                    //work out from which Parent GDISO needs to be updated.
                    gDISO = RedrawFrom(SO.Name);
                    if (gDISO != null)
                    {
                        Rectangle rect = new Rectangle(gDISO.X - 1, 0, this.Width, this.Height);
                        panel1.Invalidate(rect);

                        //Force the Update otherwise the next Invalidate() may be invoked 
                        //before this OnPaint has been executed.
                        try
                        {
                            Invoke(delegate
                            {
                                Update();
                            });
                        }
                        catch
                        { }
                    }
                }

                //Called if an SO is added to the lists of SOs. 
                //The rewrite can be slow so don't bother reacting to creation of a new SO because changes to the
                //property lists will reqrite it anyway.
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    SensualObject SO = BuildSOs.TheSOs[e.NewIndex];
                    //string test;
                    //if (SO.Name == "Dino Bos")
                    //    test = SO.Name;
                    //if (SO.Name == "Deer")
                    //    test = SO.Name;

                    //get the GDISOParent of this GDISO.
                    GDISO? GDISOParent = null;
                    if (SO.SOParent != null)
                    {
                        int index = BuildSOs.TheSOs.IndexOf(SO.SOParent);
                        if (index >= 0)
                        {
                            GDISOParent = GDISOs[BuildSOs.TheSOs[index].Name];
                        }
                    }

                    //If this GDI has no GDISOParent then set GDINeighbour to the last Neighbour so that the X position
                    //is correct.
                    GDISO? GDINeighbour = null;
                    if (GDISOParent == null)
                        GDINeighbour = LastNeighbourGDISO;

                    //create the new gDISO.
                    gDISO = new GDISO(GDINeighbour, SO, (int)numObjectFontsize.Value, (int)numQualityFontsize.Value);

                    //If this  GDI has no GDISOParent then save it. 
                    if (GDISOParent == null)
                        LastNeighbourGDISO = gDISO;

                    GDISOs.Add(SO.Name, gDISO);

                    ////_SO.ptrDerivedFrom is set after the SO is added to BuildSOs.TheSOs.
                    ////so update everytime an SO is added to BuildSOs.TheSOs.
                    //foreach (SensualObject _SO in BuildSOs.TheSOs.ToList())
                    //{
                    //    if (_SO.ptrDerivedFrom != null)
                    //        GDISOs[_SO.Name].AddInherits(_SO.ptrDerivedFrom);
                    //}
                    //Rectangle rect;

                    ////work out from which Parent GDISO needs to be updated.
                    //gDISO = RedrawFrom(SO.Name);
                    //if (gDISO != null)
                    //{
                    //    rect = new Rectangle(gDISO.X - 1, 0, this.Width, this.Height);
                    //    panel1.Invalidate(rect);
                    //    try
                    //    {
                    //        Invoke(delegate
                    //        {
                    //            Update();
                    //        });
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
            }
        }


        /// <summary>
        /// Check if the Thread has finished and Invalidate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (blnScriptBeingProcessed)
            {
                ticks++;

                //re-write once more when thread finishes.
                if (newThread != null)
                {
                    if (newThread.IsAlive == false)
                    {
                        gDISO = null;
                        button1.Location = bottom_right_point;
                        blnScriptBeingProcessed = false;
                        panel1.Invalidate();
                        btnRunScript.Enabled = true;
                    }
                }
            }
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (File.Exists(textBox1.Text))
                {
                    BuildSOs.TheSOs.Clear();
                    GDISOs.Clear();
                    bottom_right_point.X = 0;
                    bottom_right_point.Y = 0;
                    button1.Location = new Point(panel1.Height - button1.Height, panel1.Width - button1.Width);
                    LastNeighbourGDISO = null;
                    BuildSOs.SleepTime = (int)SleepTimer.Value;
                    btnRunScript.Enabled = false;
                    blnScriptBeingProcessed = true;
                    newThread = new Thread(DoWork);
                    newThread.Start(textBox1.Text);
                    
                }
                else
                    MessageBox.Show(textBox1.Text + " not found.");
            }
        }

        /// <summary>
        /// Run the script processing from a Thread so that it can be delayed when SOs and SQs are
        /// added without interferring with the Form updates.
        /// </summary>
        /// <param name="data"></param>
        public static void DoWork(object? data)
        {
            //redirect std output
            if (data != null)
            {
                string output_file = (string)data;
                output_file = output_file.Replace(".", "Output.");
                FileStream fs = new FileStream(output_file, FileMode.Create);
                TextWriter tmp = Console.Out;
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);

                //process the script
                BuildSOs.CreateOutput((string)data);
                sw.Close();
            }
        }
        private void btnSelectScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Txt File txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDlg.DefaultExt = "txt";
            fileDlg.InitialDirectory = "c:\\Projects\\OOO3\\Scripts\\";
            if (fileDlg.InitialDirectory.Length == 0)
            {
                fileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            if (fileDlg.ShowDialog() == DialogResult.OK)
                textBox1.Text = fileDlg.FileName;
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
            if (blnScriptBeingProcessed)
            {
                gDISO = null;
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            bool blnStartDrawing = false;
            if (gDISO == null)
                blnStartDrawing = true;

            foreach (GDISO GDISO in GDISOs.Values.ToList())
            {
                GDISO.IncludesRef.Clear();
                GDISO.IsPartOfRef.Clear();
                GDISO.IsReferencedBy.Clear();
                GDISO.qualities.Clear();

                //add the SO.qualities to this GDISO.qualities
                foreach (OOOCL.SensualQuality SQ in GDISO.SO.qualities.ToList())
                {
                    if (SQ.SOParent == GDISO.SO)
                        GDISO.AddSQ(SQ.Name, SQ.Value, SQ.SOEvent);
                }
                foreach (OOOCL.SensualObject SO in GDISO.SO.IncludesReference.ToList())
                {
                    // if (SO.SOParent == GDISO.SO)
                    GDISO.AddIncludesRef(SO.Name);
                }
                foreach (OOOCL.SensualObject SO in GDISO.SO.IsPartOfReference.ToList())
                {
                    // if (SO.SOParent == GDISO.SO)
                    GDISO.AddPartOfRef(SO.Name);
                }
                foreach (OOOCL.SensualObject SO in GDISO.SO.ReferencedBy.ToList())
                {
                    // if (SO.SOParent == GDISO.SO)
                    GDISO.AddReferencedBy(SO.Name);
                }

                if (gDISO != null)
                {
                    if (gDISO.Name == GDISO.Name)
                    {
                        blnStartDrawing = true;
                    }
                }
                if (blnStartDrawing)
                {
                    Point point = GDISO.DrawGDISO(g, panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
                    if (point.X > bottom_right_point.X)
                        bottom_right_point.X = point.X;
                    if (point.Y > bottom_right_point.Y)
                    {
                        bottom_right_point.Y = point.Y;
                        if(blnScriptBeingProcessed == false)
                            button1.Location = bottom_right_point;
                    }
                }
            }
            g.Dispose();
        }
    }
}
