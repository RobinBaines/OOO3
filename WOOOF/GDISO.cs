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
        const int OBJECTSPACING = 10;
        const int PARENTCHILDSPACING = 0;
        internal List<GDIIncludesRef> IncludesRef = new List<GDIIncludesRef>();
        internal List<GDIPartOfRef> IsPartOfRef = new List<GDIPartOfRef>();
        internal List<GDIReferencedBy> IsReferencedBy = new List<GDIReferencedBy>();
        internal List<GDIPreviousSOParents> IsPreviousSOParents = new List<GDIPreviousSOParents>();
        internal List<GDISQ> qualities = new List<GDISQ>();
        internal List<GDISO> ChildGDISOs = new List<GDISO>();
        public Graphics? G { get; set; }

        public GDISO? Parent { get; set; }

        public GDISO? Neighbour { get; set; }

        public string Inherits { get; set; }

        public Color theColour
        {
            get
            {
                if (Ended)
                    return Color.LightGray;
                else return Color.Blue;
            }
        }
     
        public int _x;
        public int X
        {
            get
            {
                _x = OBJECTSPACING;
                try
                {
                    if (Neighbour != null)
                        _x = Neighbour._x + Neighbour.Width + OBJECTSPACING;
                }
                catch (Exception ex) { 

                }

                if (Parent != null)
                    _x += (Parent._x); // + OBJECTSPACING);
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
                //return _width;
                string test;
                if (this.Name == "split")
                {
                    test = "split";
                }
                foreach (GDISO GDISO in ChildGDISOs.ToList())
                {
                    if (GDISO.Width + OBJECTSPACING > _width) _width = GDISO.Width + OBJECTSPACING;
                }

                foreach (GDISQ SQ in qualities)
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIIncludesRef SQ in IncludesRef)
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIPartOfRef SQ in IsPartOfRef)
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIReferencedBy SQ in IsReferencedBy)
                {
                    if ((SQ.Width) > _width) _width = SQ.Width;
                }
                foreach (GDIPreviousSOParents SQ in IsPreviousSOParents)
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
                foreach (GDISQ SQ in qualities)
                {
                    _height += ((int)SQ.TextHeight);
                }
                   
                foreach (GDIIncludesRef SQ in IncludesRef)
                {
                    _height += ((int)SQ.TextHeight);
                }

                foreach (GDIPreviousSOParents SQ in IsPreviousSOParents)
                {
                    _height += ((int)SQ.TextHeight);
                }

                
                foreach (GDIPartOfRef SQ in IsPartOfRef)
                {
                    _height += ((int)SQ.TextHeight);
                }
                foreach (GDIReferencedBy SQ in IsReferencedBy)
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
            Neighbour = _neighbour;
            ObjectFontsize = _objectfontsize;
            QualityFontsize = _qualityfontsize;
            theFont = new Font("Verdana", ObjectFontsize);

            Inherits = "";
        }

        public void AddInherits(SensualObject _SO)
        {
            Inherits = _SO.Name;
        }
        //public void AddSQ(string _name, string _value, SensualObject? SOEvent)
        public void AddSQ(SensualQuality CLSQ)
        {
            string soevent ="";
            if (CLSQ.SOEvent != null)
            {
                soevent = CLSQ.SOEvent.Name;
            }
            GDISQ SQ = new GDISQ(this, CLSQ.Name, CLSQ.Value, soevent, QualityFontsize, CLSQ.SOEvent,CLSQ);
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
            GDIPartOfRef SQ = new GDIPartOfRef(this, _name, "", soevent, QualityFontsize, null);
            IsPartOfRef.Add(SQ);
        }

        public void AddReferencedBy(string _name)
        {
            string soevent = "";
            GDIReferencedBy SQ = new GDIReferencedBy(this, _name, "", soevent, QualityFontsize, null);
            IsReferencedBy.Add(SQ);
        }

        public void AddPreviousSOParents(string _name)
        {
            string soevent = "";
            GDIPreviousSOParents SQ = new GDIPreviousSOParents(this, _name, "", soevent, QualityFontsize, null);

            IsPreviousSOParents.Add(SQ);
        }

        private void CalculateY()
        {
            
            int y = YCOORDINATE;
            //return;

            if ((Neighbour != null || Parent != null) && Parent != this)
            {
                if (Parent != null)
                {
                    Parent.CalculateY();
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
                        y += ((int)SQ.TextHeight);
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
                    foreach (GDIPreviousSOParents SQ in Parent.IsPreviousSOParents.ToList())
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
                if (Name == "Fons")
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
                        X = Parent.X;
                        rect = new RectangleF(_x + AutoScrollPositionX, Y + AutoScrollPositionY, Width - PARENTCHILDSPACING, Height);
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
                    SQ.DrawString(rect.X, rect.Y + i * SQ.TextHeight, format1, this.Name);
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

                foreach (GDIPreviousSOParents SQ in IsPreviousSOParents.ToList())
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
