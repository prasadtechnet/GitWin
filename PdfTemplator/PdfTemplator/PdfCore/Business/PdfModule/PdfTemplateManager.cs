using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Business.PdfModule
{
    public enum TemplatePhysicalFile
    {
        PdfCore,
        Template,
        PageFooter,
        PageHeader,
        PageEvent,
        TableMethod,
        TableVariable
    }
  public static  class PdfTemplateManager
    {        
        public static string GetFileContent(TemplatePhysicalFile templatePhysicalFile)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory+"\\Templates\\";

            var dict = new Dictionary<TemplatePhysicalFile, string>
            {
                {TemplatePhysicalFile.PdfCore,basePath+"PdfCore.txt"},
                {TemplatePhysicalFile.PageEvent,basePath+"PdfPageEvent.txt"},
                {TemplatePhysicalFile.PageHeader,basePath+"PdfFooter.txt"},
                {TemplatePhysicalFile.PageFooter,basePath+"PdfHeader.txt"},
                {TemplatePhysicalFile.Template,basePath+"PdfTemplate.txt"},
                {TemplatePhysicalFile.TableMethod,basePath+"PdfTableMethod.txt"} ,
                {TemplatePhysicalFile.TableVariable,basePath+"PdfPTableVariable.txt"}              
            };

            return File.ReadAllText(dict[templatePhysicalFile]);
        }
        public static Dictionary<string, string> GetPdfDictionary()
        {
            return new Dictionary<string, string>
            {
                #region Page Events	           
                //Page Event
                //Header
                {"PageHeaderTablesCalling","/*<#PdfPageEventPageHeaderCallTables#>*/"},
                //Footer                
                //Main
                 {"PageEventVaraibles","/*<#PdfPageEventVaraibles#>*/"},
                 {"PageEventConstructorParameters","/*<#PdfPageEventParameters#>*/"},
                 {"PageEventVaraiblesParameter","/*<#PdfPageEventVaraiblesParameter#>*/"},
                 {"PageFooterMethod","/*<#PdfPageEventPageFooterMethod#>*/"},
                 {"PageHeaderMethod","/*<#PdfPageEventPageHeaderMethod#>*/"},
                #endregion

                #region Template
                { "DocumentPageEvent","/*<#PdfDocumentPageEvent#>*/"},
                { "ModuleNameSpace","/*<#ModuleNameSpace>*/"},
                { "ModuleName","/*<#ModuleName>*/"},
                { "PdfReportParams","/*<#PdfReportParams>*/"},
                { "PdfReportPTableCallingList"," /*<#PdfReportPTableCallingList>*/"},
                { "PdfReportPTablesList","/*<#PdfReportPTablesList>*/"},
                { "PdfDocumentPageEventClass","/*<#PdfDocumentPageEventClass#>*/"},
                { "PdfCoreClass","/*<#PdfCoreClass#>*/"},
              
                #endregion

                #region Table Methods
                {"TableMethodName","/*<#TableMethodName#>*/" },
                   {"TableVariableName","/*<#TableVariableName#>*/" },
                {"TableMethodParams","/*<#TableMethodParams#>*/" },
                {"TableColumnNo","/*<#TableColumnNo#>*/" },
                {"TableWidth","/*<#TableWidth#>*/" },
                {"TableColumnWidths","/*<#TableColumnWidths#>*/" },
                {"TableCells","/*<#TableCells#>*/" },
	            #endregion
            };
        }

        public static string GetCellCallString(string process)
        {
            var strRes = "";
            switch (process.ToUpper())
            {
                case "LABELCELL":
                    //string strValue{0}, string fontFamilyName{1},float fontSize{2}, int fontweight{3}, iTextSharp.text.Color fontColor{4}, int h_align{5}, int v_align{6}, float height{7}, float p_left{8}, float p_right{9}, float p_top{10}, float p_btm{11}, string borderPattren{12}, int rowspan{13}, int colspan{14}, iTextSharp.text.Color bgColor{15} = null
                    strRes = "GetStringCell({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})";
                    break;
                case "FIELDCELL":
                    //string strValue{0}, string fontFamilyName{1},float fontSize{2}, int fontweight{3}, iTextSharp.text.Color fontColor{4}, int h_align{5}, int v_align{6}, float height{7}, float p_left{8}, float p_right{9}, float p_top{10}, float p_btm{11}, string borderPattren{12}, int rowspan{13}, int colspan{14}, iTextSharp.text.Color bgColor{15} = null
                    strRes = "GetStringCell({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})";
                    break;
                case "IMAGEURLCELL":
                    //string path, float scale, float height, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, float p_left=0f, float p_right = 0f, float p_top = 0f, float p_btm = 0f, string borderPattren="T,R,B")
                    strRes = "ImageCell({0}, {1},{2}, {3},{4},{5}, {6},{7},{8},{9})";
                    break;
                case "IMAGEBYTECELL":
                    //byte[] path, float scale, int hAlign, int vAlign = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color backColor = null
                    strRes = "ImageCell({0}, {1}, {2}, {3}, {4})";
                    break;
                case "IMAGEURLSUBCELL":
                    //string path, string SubHeading, string FontFamily, float fontSize = 10f, float scale = 50f, float Img_Height = 50f, float lbl_Height = 20f,  int fontWeight = iTextSharp.text.Font.NORMAL, float totalWidth = 250f, float abWidth = 210, int rowSpan = 1, int colSpan = 1
                    strRes = "ImageCell({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})";
                    break;
                case "IMAGEBYTESUBCELL":
                    //byte[] path,string SubHeading,string FontFamily,float fontSize=10f, float scale=50f, float Img_Height=50f, float lbl_Height=20f , int fontWeight= iTextSharp.text.Font.NORMAL, float totalWidth = 250f, float abWidth = 210, int rowSpan = 1, int colSpan = 1
                    strRes = "ImageCell({0},{1},{2},{3},{4},{5},{6} ,{7},{8},{9},{10},{11})";
                    break;
                case "EMPTYCELL":
                    //string strValue{0}, string fontFamilyName{1},float fontSize{2}, int fontweight{3}, iTextSharp.text.Color fontColor{4}, int h_align{5}, int v_align{6}, float height{7}, float p_left{8}, float p_right{9}, float p_top{10}, float p_btm{11}, string borderPattren{12}, int rowspan{13}, int colspan{14}, iTextSharp.text.Color bgColor{15} = null
                    strRes = "GetStringCell({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})";
                    break;
                case "TABLECELL":
                    //iTextSharp.text.pdf.PdfPTable table, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color borderColor=null
                    strRes = "PhraseCell({0},{1}, {2},{3})";
                    break;

                case "TABLEDYNMCCELL":
                    //iTextSharp.text.pdf.PdfPTable table, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color borderColor=null
                    strRes = "\r\n #region Items \r\nfor(intChild=0;intChild<{0}.Count;intChild++)\r\n{\r\n {1}  \r\n}\r\n#endregion\r\n";
                    break;
            }

            return strRes;
        }
    }

    
}
