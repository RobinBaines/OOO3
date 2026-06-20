//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using OOOCL;

namespace WOOOF
{
    internal class GDIPartOfRef : GDISQFont
    {
        SensualObject SO;
        GDISO Parent;

        public override string GetString()
        {
            string str = "PartOf => " + SO.Name;
            return str;
        }
        public void DrawString(float X, float Y, StringFormat format)
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
        public GDIPartOfRef(GDISO _Parent, SensualObject _SO, int _qualityfontsize)
        {
            FontSize = _qualityfontsize;
            Parent = _Parent;
            SO = _SO;
            theFont = new Font("Verdana", FontSize);
        }

    }
}
