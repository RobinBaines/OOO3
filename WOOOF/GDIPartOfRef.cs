using OOOCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WOOOF
{
    internal class GDIPartOfRef : GDISQ
    {
        public new void DrawString(float X, float Y, StringFormat format)
        {
            SolidBrush sb;
            sb = new SolidBrush(Color.CadetBlue);
           
            if (Parent.Parent!= null && Parent.Parent.Name != null && Parent.Parent.Name != "")
            { 
                if (Name != Parent.Parent.Name)
                {
                    sb.Color = Color.Purple;
                }
            }

            Parent?.G?.DrawString(GetString(), theFont, sb, X, Y, format);
            sb.Dispose();
        }

        public GDIPartOfRef(GDISO _Parent, string _name, string _value, string _event, int _qualityfontsize, SensualQuality? SQ) : base(_Parent, _name, _value, _event, _qualityfontsize, _Parent.SOParent, SQ)
        {
            theFont = new Font("Verdana", FontSize);
            SOevent = "PartOf";
            if (Parent.Parent != null)
            {
                if (Name != Parent.Parent.Name)
                {
                    theFont = new Font("Arial", FontSize);
                    
                }
            }
        }
    }
}
