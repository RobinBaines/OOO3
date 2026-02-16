//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOOCL;
namespace WOOOF
{
    internal class GDIReferencedBy : GDISQFont
    {
        SensualObject SO;
        GDISO Parent;
        public override string GetString()
        {
            string str = "ReferencedBy => " + SO.Name;
            return str;
        }
        public  void DrawString(float X, float Y, StringFormat format)
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


        public GDIReferencedBy(GDISO _Parent, SensualObject _SO, int _qualityfontsize)
        {
            FontSize = _qualityfontsize;
            Parent = _Parent;
            SO = _SO;
            theFont = new Font("Verdana", FontSize);
        }
        //public GDIReferencedBy(GDISO _Parent, string _name, string _value, string _event, int _qualityfontsize, SensualQuality SQ) : base(_Parent, _name, _value, _event, _qualityfontsize, _Parent.SO, SQ)
        //{
        //    theFont = new Font("Verdana", FontSize);
        //    SOevent = "ReferencedBy";
        //    if (Parent.Parent != null)
        //    {
        //        if (Name != Parent.Parent.Name)
        //        {
        //            theFont = new Font("Arial", FontSize);

        //        }
        //    }
        //}
    }
}
