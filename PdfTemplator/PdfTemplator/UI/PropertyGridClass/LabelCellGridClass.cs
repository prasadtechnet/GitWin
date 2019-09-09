using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class LabelCellGridClass
    {
        [CategoryAttribute("Label Info"), DescriptionAttribute("Table Name")]
        public string Name { get; set; }

        [CategoryAttribute("Label Info"), DescriptionAttribute("ControlType"),ReadOnly(true)]
        public string ControlType { get; set; } = "Label";

        [CategoryAttribute("Label Style"), DescriptionAttribute("label Font"), DisplayName("Font")]
        public Font LabelFont { get; set; } = new Font("Arial", 10f, FontStyle.Regular);
       

       [CategoryAttribute("Label Style"), DescriptionAttribute("label Color"), DisplayName("Color")]
        public Color LabelColor { get; set; } = Color.Black;

        [CategoryAttribute("Label Style"), DescriptionAttribute("label Height"), DisplayName("Height")]
        public float Height { get; set; } = 12f;

    }
}
