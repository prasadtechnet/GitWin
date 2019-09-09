using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class FieldCellGridClass
    {
        [CategoryAttribute("Binding Model"), DescriptionAttribute("Field Name")]
        public string Name { get; set; }

        [CategoryAttribute("Field Info"), DescriptionAttribute("ControlType"), ReadOnly(true)]
        public string ControlType { get; set; } = "Field";

        [CategoryAttribute("Field Style"), DescriptionAttribute("Field Font"), DisplayName("Font")]
        public Font FieldFont { get; set; }

        [CategoryAttribute("Field Style"), DescriptionAttribute("Field Color"), DisplayName("Color")]
        public Color FieldColor { get; set; }

        [CategoryAttribute("Binding Model"), DescriptionAttribute("Field Color"), DisplayName("Model Name")]
        public string FieldModel { get; set; }

        [CategoryAttribute("Field Style"), DescriptionAttribute("Field Height"), DisplayName("Height")]
        public float Height { get; set; } = 12f;
    }
}
