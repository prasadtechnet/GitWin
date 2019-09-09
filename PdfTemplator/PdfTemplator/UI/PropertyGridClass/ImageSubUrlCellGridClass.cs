using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class ImageSubUrlCellGridClass
    {
        [CategoryAttribute("Image Info"), DescriptionAttribute("Table Name")]
        public string Name { get; set; }
        [CategoryAttribute("Image Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "table";


        [CategoryAttribute("Image Info"), DescriptionAttribute("Source path like http or local"), DisplayName("Path")]
        public string Src { get; set; }

        [CategoryAttribute("Image Info"), DescriptionAttribute("Scale of Image")]
        public float Scale { get; set; } = 30f;

        [CategoryAttribute("Image Info"), DescriptionAttribute("Absoulte Width"), DisplayName("Absoulte Width")]
        public float AbWidth { get; set; } = 210f;
        [CategoryAttribute("Image Info"), DescriptionAttribute("Table Total Width"), DisplayName("Table Total Width")]
        public float TableTotalWidth { get; set; } = 250f;

        //label
        [CategoryAttribute("Label Info"), DescriptionAttribute("Table Name")]
        public string LabelName { get; set; }
      

        [CategoryAttribute("Label Style"), DescriptionAttribute("label Font"), DisplayName("Font")]
        public Font LabelFont { get; set; } = new Font("Arial", 10f, FontStyle.Regular);


        [CategoryAttribute("Label Style"), DescriptionAttribute("label Color"), DisplayName("Color")]
        public Color LabelColor { get; set; } = Color.Black;

        [CategoryAttribute("Label Style"), DescriptionAttribute("label Height"), DisplayName("Height")]
        public float LabelHeight { get; set; } = 12f;
    }
}
