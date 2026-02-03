//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using OOOCL;
using System.Xml;
using System.Xml.Linq;

namespace WOOOF
{
    /// <summary>
    /// Display a SensualQuality.
    /// </summary>
    internal class GDISQ : BaseClass
    {
        const int RIGHTSPACING = 15;
        const int VERTICALSPACING = 10;

        public int FontSize { get; set; }
        public string Value { get; set; }

        public Font theFont { get; set; }
      
        public SensualQuality SQ { get; set; }

        public bool SQSetsOtherSQ {
            get
            {
                if (SQ != null && SQ.SOEvent != null)
                {
                    if (SQ.SOParent.Name != SQ.SOEvent.Name)
                    {
                        if (SQ.SOEvent.Name == SOName)
                        {
                            return true;
                        }
                        
                    }
                }
                return false;
            }
        }

        public bool SQSetByOtherSO
        {
            get
            {
                if (SQ != null && SQ.SOEvent != null)
                {
                    if (SQ.SOParent.Name != SQ.SOEvent.Name)
                    {
                        //
                        if (SQ.SOEvent.Name != SOName)
                        {
                            return true;
                        }

                    }
                }
                return false;
            }
        }


        //the name of the SO which is displaying the SQ
        public string SOName { get; set; }

        public float TextHeight
        {

            get { return theFont.SizeInPoints + VERTICALSPACING; }
        }

        int _textWidth;
        public int TextWidth
        {
            get
            {
                string _name = GetString(false);
                SizeF stringSize = new SizeF();
                Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));
                stringSize = gfx.MeasureString(_name, theFont);
                _textWidth = (int)stringSize.Width;
                return _textWidth;
            }
            set { _textWidth = value; }
        }

        public int Width
        {
            get
            {
                return TextWidth + RIGHTSPACING;
            }
        }

        public GDISO? Parent { get; set; }

        public string GetString(bool blnHideEvents, string _SOName)
        {
            SOName = _SOName;
            return GetString(blnHideEvents);
        }
        

        public string GetString(bool blnHideEvents)
        {
            string str = SOevent;
            string test2;
            if (Value != null)
            {
                if (this.Name == "move1") // && Name == "Running" && Value == "false")
                {
                    test2 = "test";
                }
            }

            if (SQSetsOtherSQ)
            {
            str = SQ.SOParent.Name + " > ";
            }
            if (SQSetByOtherSO)
            {
            str = " => " + SQ.SOEvent.Name + ".";
            }

            if (SOevent.Length > 0 && ! str.Contains(" => "))
            {
               str += " => ";
            }

            if (Value.Length > 0) // && str.Length > 0)
            {
                return str + Name + " = " + Value;
            }

            return str += Name;
        }
        public void DrawString(float X, float Y, StringFormat format, string _SOName)
        {
            Color theColor = Color.Black;
            //if(SOParent != null && SOParent.EndedBySO != null)
            if (SQSetsOtherSQ)
                theColor = Color.DarkGray;
            if(Parent.Ended)
                theColor = Color.DarkGray;

            SOName = _SOName;
            SolidBrush sb;
            if (SOevent.Length > 0)
            {
                sb = new SolidBrush(theColor);
            }
            else
            {
                sb = new SolidBrush(theColor);
                if (SQ != null && SQ.SOEvent != null)
                {
                    if (SQ.SOParent.Name != SQ.SOEvent.Name)
                    {
                        if (SQ.SOEvent.Name == SOName)
                            sb = new SolidBrush(theColor);
                        //showing that this Object has set this Quality in another Object.
                    }
                }
            }

            string str = GetString(false);
            if (str.Length > 0) 
                Parent?.G?.DrawString(str, theFont, sb, X, Y, format);
        }


        public string _SOevent = "";
        public string SOevent
        {
            get { 
               
                return _SOevent; 
            }
            set { _SOevent = value; }
        }

        public GDISQ(GDISO _Parent, string _name, string _value, string _event,  int _qualityfontsize, SensualObject? _SOParent, SensualQuality _SQ)
        {
            Parent = _Parent;
            Name = _name;
            Value = _value;
            FontSize = _qualityfontsize;
            theFont = new Font("Verdana", FontSize);
            SOParent = _SOParent;
            SQ = _SQ;
            SOevent = "";
            if (Parent.Name != _event )
            {
                if (Parent.Parent != null)
                {
                    if (Parent.Parent.Name != _event)
                        SOevent = _event;
                }
                else
                {

                    SOevent = _event;
                }
            }
        }
    }
}
