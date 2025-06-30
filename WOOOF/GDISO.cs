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
        internal List<GDIIncludesRef> IncludesRef = new List<GDIIncludesRef>();
        internal List<GDIPartOfRef> IsPartOfRef = new List<GDIPartOfRef>();
        internal List<GDIReferencedBy> IsReferencedBy = new List<GDIReferencedBy>();

        internal List<GDISQ> qualities = new List<GDISQ>();
        internal List<GDISO> ChildGDISOs = new List<GDISO>();

        public Graphics? G { get; set; }

        public GDISO? Parent { get; set; }

        public GDISO? Neighbour { get; set; }

        public string Inherits { get; set; }
      
        public Color theColour { get; set; }
     
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

        public int Y { get; set; }

        int _width = 300;
        public int Width
        {
            get
            {
                _width = TextWidth + 15;
                foreach (GDISO GDISO in ChildGDISOs.ToList())
                {
                    if (GDISO.Width + GDISO._x - _x + OBJECTSPACING > _width) _width = GDISO.Width + GDISO._x - _x + OBJECTSPACING;
                }
                foreach (GDISQ SQ in qualities.ToList())
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIIncludesRef SQ in IncludesRef.ToList())
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIPartOfRef SQ in IsPartOfRef.ToList())
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIReferencedBy SQ in IsReferencedBy.ToList())
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
                foreach (GDISO GDISO in ChildGDISOs.ToList())
                {
                    _height += (GDISO.Height + 10);
                }
                foreach (GDISQ SQ in qualities.ToList())
                    _height += ((int)SQ.TextHeight);
                foreach (GDIIncludesRef SQ in IncludesRef.ToList())
                {
                    _height += ((int)SQ.TextHeight);
                }
                foreach (GDIPartOfRef SQ in IsPartOfRef.ToList())
                {
                    _height += ((int)SQ.TextHeight);
                }
                foreach (GDIReferencedBy SQ in IsReferencedBy.ToList())
                {
                    _height += ((int)SQ.TextHeight);
                }
                return _height; 
            }
        }

        public int ObjectFontsize { get; set; }
        public int QualityFontsize { get; set; }
       
        public Font theFont { get; set; }

        public SensualObject SO;

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
            if (Parent != null)
            {
                Parent.ChildGDISOs.Add(this);
            }
            theColour = Color.Blue;
            Inherits = "";
        }
       

        public void AddInherits(SensualObject _SO)
        {
            Inherits = _SO.Name;
        }
        public void AddSQ(string _name, string _value, SensualObject? SOEvent)
        {
            string soevent ="";
            if (SOEvent != null)
            {
                soevent = SOEvent.Name;
            }
            GDISQ SQ = new GDISQ(this, _name, _value, soevent, QualityFontsize);
            qualities.Add(SQ);
        }

        public void AddIncludesRef(string _name)
        {
            string soevent = "";
            GDIIncludesRef SQ = new GDIIncludesRef(this, _name, "", soevent, QualityFontsize);

            IncludesRef.Add(SQ);
        }

        public void AddPartOfRef(string _name)
        {
            string soevent = "";
            GDIPartOfRef SQ = new GDIPartOfRef(this, _name, "", soevent, QualityFontsize);
            IsPartOfRef.Add(SQ);
        }

        public void AddReferencedBy(string _name)
        {
            string soevent = "";
            GDIReferencedBy SQ = new GDIReferencedBy(this, _name, "", soevent, QualityFontsize);
            IsReferencedBy.Add(SQ);
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
                    foreach (GDIIncludesRef SQ in Parent.IncludesRef.ToList())
                    {
                        y += ((int)SQ.TextHeight);
                    }
                    foreach (GDIPartOfRef SQ in Parent.IsPartOfRef.ToList())
                    {
                        y += ((int)SQ.TextHeight);
                    }
                    foreach (GDIReferencedBy SQ in Parent.IsReferencedBy.ToList())
                    {
                        y += ((int)SQ.TextHeight);
                    }
                }
            }
            Y = y;
        }

public Point DrawGDISO(Graphics _g, int AutoScrollPositionX, int AutoScrollPositionY)
        {
            Point point = new Point(0,0);
            G = _g;
            RectangleF rect;
            if (G != null)
            {
                string test;
                if (Name == "swimming")
                    test = Name;

                CalculateY();

                if (Neighbour != null)
                {
                    rect = new RectangleF(X + AutoScrollPositionX, Y + AutoScrollPositionY, Width, Height);
                }
                else
                {
                    if (Parent != null)
                    {
                        X = Parent.X + 1;
                        //rect = new RectangleF(Parent.X + 5, Y, Width - 10, Height);
                        rect = new RectangleF(X + AutoScrollPositionX, Y + AutoScrollPositionY, Width - 10, Height);
                    }
                    else
                    {
                        rect = new RectangleF(X + AutoScrollPositionX, Y + AutoScrollPositionY, Width, Height);
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

                foreach (GDIIncludesRef SQ in IncludesRef.ToList())
                {
                    SQ.DrawString(rect.X, rect.Y + i * SQ.TextHeight, format1);
                    i++;
                }

                foreach (GDIPartOfRef SQ in IsPartOfRef.ToList())
                {
                    SQ.DrawString(rect.X, rect.Y + i * SQ.TextHeight, format1);
                    i++;
                }

                foreach (GDIReferencedBy SQ in IsReferencedBy.ToList())
                {
                    SQ.DrawString(rect.X, rect.Y + i * SQ.TextHeight, format1);
                    i++;
                }

                point.X = (int)X + AutoScrollPositionX + (int)Width;
                point.Y = (int)Y + AutoScrollPositionY + (int)Height;
            }
            return point;
        }
    }
}
