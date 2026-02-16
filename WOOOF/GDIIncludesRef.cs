//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using OOOCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WOOOF
{
    internal class GDIIncludesRef : GDISQFont
    {
        SensualObject SO;
        GDISO Parent;

        public override string GetString()
        {
            string str = "IncludeRef => " + SO.Name;
            return str;
        }

        public void DrawString(float X, float Y, StringFormat format)
        {
            SolidBrush sb;
            sb = new SolidBrush(Color.Coral);
            Parent?.G?.DrawString(GetString(), theFont, sb, X, Y, format);
            sb.Dispose();
        }

        public GDIIncludesRef(GDISO _Parent, SensualObject _SO, int _qualityfontsize)  
        {
            FontSize = _qualityfontsize;
            Parent = _Parent;
            SO = _SO;
            theFont = new Font("Verdana", FontSize);
        }
    }
}
