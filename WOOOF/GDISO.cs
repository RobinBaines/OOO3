//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using OOOCL;
namespace WOOOF
{
   /// <summary>
   /// Display a SensualObject.
   /// </summary>
    internal class GDISO : BaseClass
    {
        const int YCOORDINATE = 0;
        const int OBJECTSPACING = 2;
        internal List<GDISQ> qualities = new List<GDISQ>();
        internal List<GDISO> ChildGDISOs = new List<GDISO>();

        private Graphics? _G;

        public Graphics? G
        {
            get { return _G; }
            set { _G = value; }
        }

        private GDISO? _Parent;

        public GDISO? Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        private GDISO? _Neighbour;

        public GDISO? Neighbour
        {
            get { return _Neighbour; }
            set { _Neighbour = value; }
        }


        private string _Inherits;

        public string Inherits
        {
            get { return _Inherits; }
            set { _Inherits = value; }
        }

        private Color _theColour;
        public Color theColour
        {
            get { return _theColour; }
            set { _theColour = value; }
        }

     

        public int _x;
        public int X
        {
            get
            {
                if (Neighbour != null)
                    _x = Neighbour.X + Neighbour.Width + OBJECTSPACING;
                else _x = OBJECTSPACING;
                if (Parent != null)
                    _x += (Parent.X + OBJECTSPACING);
                return _x;
            }
            set { _x = value; }
        }

        private int _y;

        public int Y
        {
            get
            {
                return _y;
            }
            set { _y = value; }
        }

        int _width = 300;
        public int Width
        {
            get
            {
                _width = TextWidth + 15;
                foreach (GDISO GDISO in ChildGDISOs)
                {
                    if (GDISO.Width + GDISO._x - _x + OBJECTSPACING > _width) _width = GDISO.Width + GDISO._x - _x + OBJECTSPACING;
                }
                foreach (GDISQ SQ in qualities.ToList())
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                return _width;
            }
        }

        private int _height;
        public int Height
        {
            get {
                _height = (int)TextHeight;
                foreach (GDISO GDISO in ChildGDISOs)
                {
                    _height += (GDISO.Height + 10);
                }
                foreach (GDISQ SQ in qualities.ToList())
                    _height += ((int)SQ.TextHeight);
                return _height; 
            }
        }

        public int ObjectFontsize { get; set; }
        public int QualityFontsize { get; set; }
        private Font _theFont;
        public Font theFont
        {
            get { return _theFont; }
            set { _theFont = value; }
        }

        public SensualObject SO;

        private float _textHeight;
        public float TextHeight
        {
            get { return theFont.SizeInPoints + 10; }
        }

        public int TextWidth
        {
            get
            {
                string _name = Name;
                if (Inherits != null)
                {
                    if (Inherits.Length > 0)
                    {
                        _name = _name + ":" + Inherits;
                    }
                }
                SizeF stringSize = new SizeF();
                Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));
                stringSize = gfx.MeasureString(_name, theFont);
               
                return (int)stringSize.Width;
            } 
        }

        public GDISO(GDISO? _neighbour, SensualObject _SO, int _objectfontsize, int _qualityfontsize)
        {
            SO = _SO;
            Name = SO.Name;
            if (Name == "InitEvent")
                Neighbour = _neighbour;
            Neighbour = _neighbour;
            GDISO? GDISOParent = null;
            if (SO.SOParent != null)
            {
                int index = BuildSOs.TheSOs.IndexOf(SO.SOParent);
                if (index >= 0)
                {
                    GDISOParent = Form1.GDISOs[BuildSOs.TheSOs[index].Name];
                }
            }
          
            Parent = GDISOParent;
            ObjectFontsize = _objectfontsize;
            QualityFontsize = _qualityfontsize;
            theFont = new Font("Verdana", ObjectFontsize);
           // CalculateY();
            if (Parent != null)
            {
                Parent.ChildGDISOs.Add(this);
            }
            theColour = Color.Blue;
        }
       

        public void AddInherits(SensualObject _SO)
        {
            Inherits = _SO.Name;
        }
        public void AddSQ(string _name, string _value, SensualObject SOEvent)
        {
            string soevent ="";
            if(SOEvent != null)
            {
                soevent = SOEvent.Name;
            }
            GDISQ SQ = new GDISQ(this, _name, _value, soevent, QualityFontsize);
            qualities.Add(SQ);
        }

        private void CalculateY()
        {
            int y = YCOORDINATE;
            if (Neighbour != null || Parent != null)
            {
                if (Parent != null)
                {
                    y = (Parent.Y);
                    y += ((int)TextHeight + 5); 
                    foreach (GDISO GDISO in Parent.ChildGDISOs)
                    {
                        if(GDISO != this)
                            y += (GDISO.Height + 10);
                        else break;
                    }
                    foreach (GDISQ SQ in Parent.qualities.ToList())
                    {
                        y += ((int)SQ.TextHeight); // + 10);
                    }
                }
            }
            Y = y;
        }

public void DrawGDISO(Graphics _g)
        {
            G = _g;
            RectangleF rect;
            if (_G != null)
            {
                string test;
                if (Name == "InitEvent")
                    test = Name;

                CalculateY();

                if (Neighbour != null)
                {
                    rect = new RectangleF(X, Neighbour.Y, Width, Height);
                }
                else
                {
                    if (Parent != null)
                    {
                        X = Parent.X + 1;
                        //rect = new RectangleF(Parent.X + 5, Y, Width - 10, Height);
                        rect = new RectangleF(X, Y, Width - 10, Height);
                    }
                    else
                    {
                        rect = new RectangleF(X, Y, Width, Height);
                    }
                }

                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
                SolidBrush sb = new SolidBrush(Color.Black);
                format1.Alignment = StringAlignment.Near;
                string _name = Name;
                if (Inherits != null)
                {
                    if (Inherits.Length > 0)
                    {
                        _name = _name + ":" + Inherits;
                    }
                }
                G.DrawString(_name, theFont, sb, rect, format1);

                Pen pn = new Pen(theColour);
                G.DrawRectangle(pn, rect);

                int i = 1;
                foreach (GDISQ SQ in qualities.ToList())
                {
                    SQ.DrawString(rect.X, rect.Y + i * SQ.TextHeight, format1);
                    i++;
                }
            }
        }
    }
}
