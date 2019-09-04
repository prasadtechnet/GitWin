using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Business.PdfModule
{
    public class PdfDictionaryModel
    {
        public static Dictionary<string,string> GetPdfDictionary()
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
