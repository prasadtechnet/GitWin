using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class ImageUrlCellGridClass
    {
        [CategoryAttribute("Image Info"), DescriptionAttribute("Image Name")]
        public string Name { get; set; }
        [CategoryAttribute("Image Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "ImageUrl";

        [CategoryAttribute("Image Info"), DescriptionAttribute("Source path like http or local"),DisplayName("Path")]
        public string Src { get; set; }

        [CategoryAttribute("Image Info"), DescriptionAttribute("Scale of Image")]
        public float Scale { get; set; } = 30f;
        [CategoryAttribute("Image Info"), DescriptionAttribute("Image Height"), DisplayName("Height")]
        public float Height { get; set; } = 50f;
    }
}
