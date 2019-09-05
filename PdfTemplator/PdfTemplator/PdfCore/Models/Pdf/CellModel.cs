using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Models.Pdf
{

    #region Templates

    public class TemplateModel
    {
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public string Type { get; set; }
        public List<string> ModelVariables { get; set; }
        public List<BindingModel> bindingModelProps { get; set; }
    }

    public class HCF_TemplateModel:TemplateModel
    {     
        public BodySectionModel Body { get; set; }
        public PageHeaderSection PageHeader { get; set; }
        public PageFooterSection PageFooter { get; set; }
    }
    public class HC_TemplateModel : TemplateModel
    {
       
        public BodySectionModel Body { get; set; }
        public PageHeaderSection PageHeader { get; set; }
    }
    public class CF_TemplateModel : TemplateModel
    {
     
        public BodySectionModel Body { get; set; }      
        public PageFooterSection PageFooter { get; set; }

    }
    public class C_TemplateModel : TemplateModel
    {  
        public BodySectionModel Body { get; set; }
    }
    #endregion

    #region Sections
    public class PageHeaderSection
    {
        public List<TableModel> Tables { get; set; }
    }
    public class PageFooterSection
    {
        public string FooterType { get; set; } = "N";  //N-Number only(default),T-Table only,TN- Both Table Number
        public TableModel Table { get; set; }
    }
    public class BodySectionModel
    {
        public List<TableModel> Tables { get; set; }
    }

    #endregion

    #region Table
    public class TableModel
    {
        public string TableName { get; set; }
        public bool isChildTab { get; set; } = false;
        public int noofClmns { get; set; }
        public float width { get; set; } = 530f;
        public List<float> Colwidth { get; set; }
        public float spaceBefore { get; set; }
        public float spaceAfter { get; set; }
        public bool isHavingBorder { get; set; } = true;
        public bool isSeparateMethod { get; set; } = true;
        public bool isDynamicTab { get; set; } = false;
        public List<RowModel> Rows { get; set; } = new List<RowModel>();
    }
    public class RowModel
    {
        public List<CellModel> Cells { get; set; } = new List<CellModel>();
    }

    #endregion

    #region Cell
    public class CellModel
    {
        //cell info
        public int CellNo { get; set; }
        public int RowNo { get; set; }
        public float Height { get; set; } = 12f;
        //span
        public int RowSpan { get; set; } = 1;
        public int ColSpan { get; set; } = 1;
        //border
        // public bool isHavingBorder { get; set; }
        // public int noOfSides { get; set; }
        public string BorderPattren { get; set; } = "L,T,R,B";
        public ColorModel BorderColor { get; set; } = new ColorModel { Type="PDF", pdfColor=iTextSharp.text.Color.BLACK};
        //padding
        public float PLeft { get; set; } = 0f;
        public float PRight { get; set; } = 0f;
        public float PTop { get; set; } = 0f;
        public float PBottom { get; set; } = 0f;
        //align
        public int VAlign { get; set; } = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP;
        public int HAlign { get; set; } = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
        //content
        public string ContentType { get; set; }
        public CellModel CellObject { get; set; }
    }
    public class TableCell : CellModel
    {
        public TableModel tableModel { get; set; }
    }
    public class LabelCell : CellModel
    {
        public FontModel Font { get; set; } = new FontModel { FontFamily = "Arial", FontSize = 10f, FontWeight = iTextSharp.text.Font.NORMAL };
        public ColorModel Color { get; set; } = new ColorModel { Type = "PDF", pdfColor = iTextSharp.text.Color.BLACK };
        public string Text { get; set; }

    }
    public class EmptyCell : CellModel
    {
     
        public string Text { get; set; } = "";

    }
    public class FieldCell : CellModel
    {
        public FontModel Font { get; set; } = new FontModel {FontFamily="Arial",FontSize=10f, FontWeight = iTextSharp.text.Font.NORMAL };
        public ColorModel Color { get; set; } = new ColorModel { Type = "PDF", pdfColor = iTextSharp.text.Color.BLACK };
        public string DataFieldName { get; set; }
        public string ModelName { get; set; }
    }
    public class ImageUrlCell : CellModel
    {
        public string Src { get; set; }
        public float Sclae { get; set; }

    }
    public class ImageByteCell : CellModel
    {
        public byte[] Image { get; set; }
        public float Scale { get; set; }
        public int ScaleAbWidth { get; set; }
        public int ScaleAbHeight { get; set; }
    }
    public class ImageUrlSubHeaderCell : CellModel
    {
        public TableCell TableCell { get; set; }
        public LabelCell label { get; set; }
        public string Src { get; set; }
        public float Sclae { get; set; }
    }
    public class ImageByteSubHeaderCell : CellModel
    {
        public TableCell TableCell { get; set; }
        public LabelCell label { get; set; }
        public byte[] Image { get; set; }
        public float Scale { get; set; }
        public int ScaleAbWidth { get; set; }
        public int ScaleAbHeight { get; set; }
    }
    #endregion

    #region Style Model
    public class FontModel
    {
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
        public int FontWeight { get; set; }
    }

    public class ColorModel
    {
        public string Type { get; set; }
        public string HexString { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public iTextSharp.text.Color pdfColor { get; set; }
    }

    #endregion

    #region Model
    public class BindingModel
    {
        public string PropName { get; set; }
        public string ModelVariable { get; set; }
    }
    #endregion
}
