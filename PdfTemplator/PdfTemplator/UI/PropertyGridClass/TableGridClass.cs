using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
    [DefaultPropertyAttribute("Name")]
    public class TableGridClass
    {
        [CategoryAttribute("Table Info"), DescriptionAttribute("Table Name")]
        public string Name { get; set; }
        [CategoryAttribute("Table Info"), DescriptionAttribute("ControlType")]
        public string ControlType { get; set; } = "Table";

        [CategoryAttribute("Table Info"), DescriptionAttribute("Number of Columns"),DisplayName("No Of Columns")]
        public int NoOfColumn  { get; set; }

        [CategoryAttribute("Table Info"), DescriptionAttribute("Column Widths like 0.2,0.5,0.3. total equal to 1 separated by comma"), DisplayName("Column widths")]
        public float[] ColumnWidths { get; set; }


        [CategoryAttribute("Table Info"), DescriptionAttribute("Is it Dynamic Table"), DisplayName("Is Dynamic")]
        public bool IsDynamic { get; set; }
    }
}
