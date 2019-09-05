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
        Document,
        PageFooter,
        PageHeader,
        PageEvent,
        TableMethod
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
                {TemplatePhysicalFile.Document,basePath+"PdfDocument.txt"},
                {TemplatePhysicalFile.TableMethod,basePath+"PdfTableMethod.txt"}                
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

                #region Document
                   {"DocumentPageEvent","/*<#PdfDocumentPageEvent#>*/"},
                #endregion

                #region Table Methods
                {"TableMethodName","/*<#TableMethodName#>*/" },
                {"TableMethodParams","/*<#TableMethodParams#>*/" },
                {"TableColumnNo","/*<#TableColumnNo#>*/" },
                {"TableWidth","/*<#TableWidth#>*/" },
                {"TableColumnWidths","/*<#TableColumnWidths#>*/" },
                {"TableCells","/*<#TableCells#>*/" },
	            #endregion
            };
        }
    }

    
}
