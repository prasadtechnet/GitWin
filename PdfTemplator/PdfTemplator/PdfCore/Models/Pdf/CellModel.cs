using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Models.Pdf
{

    #region Templates

    public class HCF_TemplateModel
    {
        public string Name { get; set; }
        public List<string> ModelVariables { get; set; }
        public BodySectionModel Body { get; set; }
        public PageHeaderSection PageHeader { get; set; }
        public PageFooterSection PageFooter { get; set; }
        public List<BindingModel> bindingModelProps { get; set; }
    }
    public class HC_TemplateModel
    {
        public string Name { get; set; }
        public List<string> ModelVariables { get; set; }
        public BodySectionModel Body { get; set; }
        public PageHeaderSection PageHeader { get; set; }
        public List<BindingModel> bindingModelProps { get; set; }
    }
    public class CF_TemplateModel
    {
        public string Name { get; set; }
        public List<string> ModelVariables { get; set; }
        public BodySectionModel Body { get; set; }      
        public PageFooterSection PageFooter { get; set; }
        public List<BindingModel> bindingModelProps { get; set; }
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
        public int noofClmns { get; set; }
        public int widthPercent { get; set; }
        public int spaceBefore { get; set; }
        public int spaceAfter { get; set; }
        public bool isHavingBorder { get; set; }
        public bool isDynamicTab { get; set; }
        public List<RowModel> Rows { get; set; }
    }
    public class RowModel
    {
        public List<CellModel> Cells { get; set; }
    }

    #endregion

    #region Cell
    public class CellModel
    {
        //cell info
        public int CellNo { get; set; }
        public int RowNo { get; set; }
        public int Height { get; set; }
        //span
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        //border
       // public bool isHavingBorder { get; set; }
       // public int noOfSides { get; set; }
        public string BorderPattren { get; set; }
        public ColorModel BorderColor { get; set; }
        //padding
        public int PLeft { get; set; }
        public int PRight { get; set; }
        public int PTop { get; set; }
        public int PBottom { get; set; }
        //align
        public string VAlign { get; set; }
        public string HAlign { get; set; }
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
        public FontModel Font { get; set; }
        public ColorModel Color { get; set; }
        public string Text { get; set; }

    }
    public class FieldCell : CellModel
    {
        public FontModel Font { get; set; }
        public ColorModel Color { get; set; }
        public string DataFieldName { get; set; }
        public string ModelVariableName { get; set; }
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
        public int FontSize { get; set; }

    }

    public class ColorModel
    {
        public string Type { get; set; }
        public string HexString { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

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
