using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOOOF
{
    internal class GDIReferencedBy : GDISQ
    {
        public new void DrawString(float X, float Y, StringFormat format)
        {
            SolidBrush sb;
            sb = new SolidBrush(Color.CadetBlue);

            if (Parent.Parent != null && Parent.Parent.Name != null && Parent.Parent.Name != "")
            {
                if (Name != Parent.Parent.Name)
                {
                    sb.Color = Color.Coral;
                }
            }

            Parent?.G?.DrawString(GetString(), theFont, sb, X, Y, format);
            sb.Dispose();
        }

        public GDIReferencedBy(GDISO _Parent, string _name, string _value, string _event, int _qualityfontsize) : base(_Parent, _name, _value, _event, _qualityfontsize)
        {
            theFont = new Font("Verdana", FontSize);
            SOevent = "ReferencedBy";
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
