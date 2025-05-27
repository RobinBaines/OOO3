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
      
        public float TextHeight
        {
            get { return theFont.SizeInPoints + VERTICALSPACING; }
        }

        int _textWidth;
        public int TextWidth
        {
            get
            {
                string _name = GetString();
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

        public string GetString()
        {
            string str = SOevent;
            if (SOevent.Length > 0)
            {
                str += "=>";
            }
            if (Value.Length > 0)
            {
                return str + Name + " = " + Value;
            }
            return str + Name;
        }
        public void DrawString(float X, float Y, StringFormat format)
        {
            SolidBrush sb;
            if (SOevent.Length > 0)
            {
                sb = new SolidBrush(Color.Red);
            }
            else
            {
                sb = new SolidBrush(Color.Black);
            }
            Parent?.G?.DrawString(GetString(), theFont, sb, X, Y, format);
        }


        public string _SOevent = "";
        public string SOevent
        {
            get { 
               
                return _SOevent; 
            }
            set { _SOevent = value; }
        }

        public GDISQ(GDISO _Parent, string _name, string _value, string _event,  int _qualityfontsize)
        {
            Parent = _Parent;
            Name = _name;
            Value = _value;
            FontSize = _qualityfontsize;
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
            theFont = new Font("Verdana", FontSize);
        }
    }
}
