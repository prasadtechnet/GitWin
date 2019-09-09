using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class ImageByteCellGridClass
    {
        [CategoryAttribute("Image Info"), DescriptionAttribute("Field Name")]
        public string Name { get; set; }
        [CategoryAttribute("Image Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "table";


        [CategoryAttribute("Image Info"), DescriptionAttribute("Scale of Image")]
        public float Scale { get; set; } = 30f;

        [CategoryAttribute("Binding Model"), DescriptionAttribute("Field Color"), DisplayName("Model Name")]
        public string ImageFieldModel { get; set; }

    }
}
