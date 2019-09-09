using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
   public class RowGridClass
    {
        [CategoryAttribute("Row Info"), DescriptionAttribute("Row Name")]
        public string Name { get; set; } = "tr";
        [CategoryAttribute("Row Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "Row";
    }
}
