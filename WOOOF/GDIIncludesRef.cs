using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOOOF
{
    internal class GDIIncludesRef : GDISQ
    {
        public new void DrawString(float X, float Y, StringFormat format)
        {
            SolidBrush sb;
            sb = new SolidBrush(Color.Coral);
            Parent?.G?.DrawString(GetString(), theFont, sb, X, Y, format);
            sb.Dispose();
        }

        public GDIIncludesRef(GDISO _Parent, string _name, string _value, string _event, int _qualityfontsize) : base(_Parent, _name, _value, _event, _qualityfontsize)
        {

        }
    }
}
