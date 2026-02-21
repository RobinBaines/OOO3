//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
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

        public GDISQ? EndedBySO { get; set; }

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

                foreach (GDIIncludesRef SO in IncludesRef)
                {
                    if ((SO.Width) > _width) _width = SO.Width;
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
                if (EndedBySO != null)
                {
                    if ((EndedBySO.Width) > _width) _width = EndedBySO.Width;
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
                    if (SQ.GetString(blnHideEvents, this.Name).Length > 0)
                        _height += ((int)SQ.TextHeight);
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIIncludesRef SO in IncludesRef)
                    {
                        _height += ((int)SO.TextHeight);
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIPreviousSOParents SQ in IsPreviousSOParents)
                    {
                        _height += ((int)SQ.TextHeight);
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIPartOfRef SQ in IsPartOfRef)
                    {
                        _height += ((int)SQ.TextHeight);
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIReferencedBy SQ in IsReferencedBy)
                    {
                        _height += ((int)SQ.TextHeight);
                    }
                }

                if (EndedBySO != null)
                {
                    _height += ((int)EndedBySO.TextHeight);
                }
                    
                return _height; 
            }
        }

        public int ObjectFontsize { get; set; }
        public int QualityFontsize { get; set; }

        public bool blnHideEvents { get; set; }


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

        public GDISO(GDISO? _neighbour, SensualObject _SO, int _objectfontsize, int _qualityfontsize, bool _blnHideEvents)
        {
            blnHideEvents = _blnHideEvents;
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
        public void AddEndedBySO(SensualObject _SO)
        {
            EndedBySO = new(this, "ENDED BY: " + _SO.Name, "", "", QualityFontsize, null, null);
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
                        if(SQ.GetString(blnHideEvents, Parent.Name).Length > 0)
                            y += ((int)SQ.TextHeight);
                    }

                    if (blnHideEvents == false)
                    {
                        foreach (GDIIncludesRef SO in Parent.IncludesRef.ToList())
                        {
                            y += ((int)SO.TextHeight);
                        }
                    }

                    if (blnHideEvents == false)
                    {
                        foreach (GDIPartOfRef SQ in Parent.IsPartOfRef.ToList())
                        {
                            y += ((int)SQ.TextHeight);
                        }
                    }

                    if (blnHideEvents == false)
                    {
                        foreach (GDIReferencedBy SQ in Parent.IsReferencedBy.ToList())
                        {
                            y += ((int)SQ.TextHeight);
                        }
                    }

                    if (blnHideEvents == false)
                    {
                        foreach (GDIPreviousSOParents SQ in Parent.IsPreviousSOParents.ToList())
                        {
                            y += ((int)SQ.TextHeight);
                        }
                    }
                    if (Parent.EndedBySO != null)
                    {
                        y += (int)Parent.EndedBySO.TextHeight;
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
                if (Name == "My_Nida(SEE_NIDA_BARK)") // && Parent.Name == "I_SEE_NIDA_BARKING")
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
                        //if (_name == "Me" && Parent.Neighbour != null)
                        //    _name = _name + ":" + Inherits + " " + X.ToString() + " " + Parent.Neighbour.Name;
                        //else
                            _name = _name + ":" + Inherits;
                    }
                }
                G.DrawString(_name, theFont, sb, rect, format1);

                Pen pn = new Pen(theColour);
                G.DrawRectangle(pn, rect);

      
                float TextPosition = this.TextHeight; 
                foreach (GDISQ SQ in qualities.ToList())
                {
                    string str = SQ.GetString(blnHideEvents, this.Name);
                    if (str.Length > 0)
                    {
                        
                        SQ.DrawString(rect.X, rect.Y + TextPosition, format1, this.Name);
                        TextPosition += SQ.TextHeight;

                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIIncludesRef SO in IncludesRef.ToList())
                    {
                        SO.DrawString(rect.X, rect.Y + TextPosition, format1);
                        TextPosition += SO.TextHeight;
   
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIPartOfRef SQ in IsPartOfRef.ToList())
                    {
                        SQ.DrawString(rect.X, rect.Y + TextPosition, format1);
                        TextPosition += SQ.TextHeight;
          
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIReferencedBy SQ in IsReferencedBy.ToList())
                    {
                        SQ.DrawString(rect.X, rect.Y + TextPosition, format1);
                        TextPosition += SQ.TextHeight;
   
                    }
                }

                if (blnHideEvents == false)
                {
                    foreach (GDIPreviousSOParents SQ in IsPreviousSOParents.ToList())
                    {
                        SQ.DrawString(rect.X, rect.Y + TextPosition, format1);
                        TextPosition += SQ.TextHeight;
         
                    }
                }

                if (EndedBySO != null)
                {
                    EndedBySO.DrawString(rect.X, rect.Y + TextPosition, format1, EndedBySO.Name);
                }

                point.X = (int)X + (int)Width;
                point.Y = (int)Y + (int)Height;
            }
            return point;
        }
    }
}
