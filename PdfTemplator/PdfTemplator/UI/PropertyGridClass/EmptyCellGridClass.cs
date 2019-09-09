using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class EmptyCellGridClass
    {
        [CategoryAttribute("Empty Cell Info"), DescriptionAttribute("Empty Cell Name")]
        public string Name { get; set; } = "";
        [CategoryAttribute("Empty Cell Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "Empty";
    }
}
