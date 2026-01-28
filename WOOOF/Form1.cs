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
    using System.Xml.Linq;

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
            ScriptName.Text = "c:\\Projects\\OOO3\\Scripts\\sc.txt";

            panel1.AutoScroll = true;


            BindingList<SensualObject> TheSOs = BuildSOs.TheSOs;
            TheSOs.ListChanged += HandleSOChanged;

        }

        GDISO? LastNeighbourGDISO = null;
        GDISO? gDISO = null;
        bool blnPaintActive = false;
        void HandleSOChanged(object? sender, ListChangedEventArgs? e)
        {
            HandleSOChanged(e.ListChangedType);
        }
        void HandleSOChanged(ListChangedType ListChangedType)
        {
            if (blnPaintActive == false)
            {
                blnPaintActive = true;
                //if (e != null)
                {
                    if (ListChangedType == ListChangedType.Reset || ListChangedType == ListChangedType.ItemAdded)
                    {
                        GDISOs.Clear();
                        foreach (SensualObject _SO in BuildSOs.TheSOs.ToList())
                        {
                            string test;
                            if (_SO.Name == "I_SEE_NIDA_BARKING")
                            {
                                test = "test";
                            }
                            if (GDISOs.TryGetValue(_SO.Name, out gDISO) == false)
                            {
                                //create new GDISO.
                                gDISO = new GDISO(null, _SO, (int)numObjectFontsize.Value, (int)numQualityFontsize.Value, cbHideEvents.Checked);
                                GDISOs.Add(_SO.Name, gDISO);
                            }

                            if (_SO.ptrDerivedFrom != null)
                                GDISOs[_SO.Name].AddInherits(_SO.ptrDerivedFrom);

                            if (_SO.EndedBySO != null)
                                GDISOs[_SO.Name].AddEndedBySO(_SO.EndedBySO);

                            GDISOs[_SO.Name].Ended = _SO.Ended;
                        }
                        foreach (SensualObject _SO in BuildSOs.TheSOs.ToList())
                        {
                            if (_SO.SOParent != null)
                            {
                                if (GDISOs.ContainsKey(_SO.SOParent.Name) == true)
                                {
                                    GDISOs[_SO.Name].Parent = GDISOs[_SO.SOParent.Name];
                                    GDISOs[_SO.SOParent.Name].ChildGDISOs.Add(GDISOs[_SO.Name]);
                                }
                            }
                        }

                        GDISO GDISONeighbour = null;
                        foreach (GDISO GDISO in GDISOs.Values.ToList())
                        {
                            if (GDISO.Parent == null)
                            {
                                GDISO.Neighbour = GDISONeighbour;
                                GDISONeighbour = GDISO;
                            }
                            GDISO.IncludesRef.Clear();
                            GDISO.IsPartOfRef.Clear();
                            GDISO.IsReferencedBy.Clear();
                            GDISO.IsPreviousSOParents.Clear();
                            GDISO.qualities.Clear();

                            //add the SO.qualities to this GDISO.qualities
                            foreach (OOOCL.SensualQuality SQ in GDISO.SO.qualities.ToList())
                            {
                                //if (SQ.SOParent == GDISO.SO)
                                string test;
                                if (GDISO.SO.Name == "Nida")
                                    test = "";
                                if (SQ.Name == "Running" && SQ.Value == "false")
                                {
                                    test = "";
                                }

                                GDISO.AddSQ(SQ);
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
                            foreach (OOOCL.SensualObject SO in GDISO.SO.PreviousSOParents.ToList())
                            {
                                // if (SO.SOParent == GDISO.SO)
                                GDISO.AddPreviousSOParents(SO.Name);
                            }
                        }
                    }
                }
                //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                //panel1.Invalidate(rect);
                panel1.Invalidate();
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
                        blnPaintActive = false;
                        BuildSOs.TheSOs.ResetBindings();
                       
                        panel1.Invalidate();
                        btnRunScript.Enabled = true;
                    }
                }
            }
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            if (ScriptName.Text.Length > 0)
            {
                if (File.Exists(ScriptName.Text))
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
                    newThread.Start(ScriptName.Text);
                }
                else
                    MessageBox.Show(ScriptName.Text + " not found.");
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
            fileDlg.DefaultExt = "txt|scr";
            fileDlg.InitialDirectory = "c:\\Projects\\OOO3\\Scripts\\";
            if (fileDlg.InitialDirectory.Length == 0)
            {
                fileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            if (fileDlg.ShowDialog() == DialogResult.OK)
                ScriptName.Text = fileDlg.FileName;
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
            if (blnScriptBeingProcessed)
            {
                gDISO = null;
                blnPaintActive = true;
                panel1.Invalidate();
            }
        }

        bool blnStartDrawing = false;

        private void GDISO_Paint(GDISO GDISO, Graphics g)
        {
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
                    if (blnScriptBeingProcessed == false)
                        button1.Location = bottom_right_point;
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (blnStartDrawing == true) return;
            blnStartDrawing = true;
            Graphics g = e.Graphics;

            //Paint the GDISO which have no parent first.
            foreach (GDISO GDISO in GDISOs.Values.ToList())
            {
                if (GDISO.Parent == null)
                {
                    GDISO_Paint(GDISO, g);
                }
            }
            foreach (GDISO GDISO in GDISOs.Values.ToList())
            {
                if (GDISO.Parent != null)
                {
                    GDISO_Paint(GDISO, g);
                }
            }
            g.Dispose();
            blnPaintActive = false;
            blnStartDrawing = false;
        }

        private void cbHideEvents_CheckedChanged(object sender, EventArgs e)
        {
            HandleSOChanged(ListChangedType.Reset);
            panel1.Invalidate();
        }
    }
}
