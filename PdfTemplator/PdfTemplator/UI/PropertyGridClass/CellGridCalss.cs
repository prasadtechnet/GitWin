using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
    
  
   public class CellGridCalss
    {
        [CategoryAttribute("Cell Info"), DescriptionAttribute("Cell Name"), ReadOnly(true)]
        public string Name { get; set; } = "tr";
        [CategoryAttribute("Cell Info"), DescriptionAttribute("ControlType"),ReadOnly(true)]
        public string ControlType { get; set; } = "Cell";


        //span
        [CategoryAttribute("Span"), DescriptionAttribute("Row Span")]
        public int RowSpan { get; set; } = 1;
        [CategoryAttribute("Span"), DescriptionAttribute("Col Span")]
        public int ColSpan { get; set; } = 1;
        //border

        [CategoryAttribute("Border"), DescriptionAttribute("Border Pattren")]
        public BorderPattren BorderPattren { get; set; } = BorderPattren.L_T_R_B;

        //padding
        [CategoryAttribute("Padding"), DescriptionAttribute("Padding Left")]
        public float PLeft { get; set; } = 0f;
        [CategoryAttribute("Padding"), DescriptionAttribute("Padding Right")]
        public float PRight { get; set; } = 0f;
        [CategoryAttribute("Padding"), DescriptionAttribute("Padding  Top")]
        public float PTop { get; set; } = 0f;
        [CategoryAttribute("Padding"), DescriptionAttribute("Padding Bottom")]
        public float PBottom { get; set; } = 0f;
        //align
        [CategoryAttribute("Cell Align"), DescriptionAttribute("Vertical Alignment")]
        public Alignment VAlign { get; set; } = Alignment.ALIGN_TOP;
        [CategoryAttribute("Cell Align"), DescriptionAttribute("Horizontal Alignment")]
        public Alignment HAlign { get; set; } = Alignment.ALIGN_LEFT;

        public float Width { get; set; }
    }
}
