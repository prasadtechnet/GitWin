using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Templates
{
    class PdfDocument
    {
        private byte[] GeneratePdf(List<iTextSharp.text.pdf.PdfPTable> lsPTables, ref StringBuilder sbLog)
        {
            byte[] btPdf = null;
            try
            {
                if (lsPTables != null && lsPTables.Count > 0)
                {
                    try
                    {
                        iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 40f, 40f, 30f, 10f);
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {

                            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
                            
                            /*<#PdfDocumentPageEvent#>*/

                            document.Open();

                            foreach (iTextSharp.text.pdf.PdfPTable tab1 in lsPTables)
                            {
                                document.Add(tab1);
                            }

                            document.Close();
                            btPdf = memoryStream.ToArray();
                            memoryStream.Close();

                            sbLog.AppendLine("PdfGen-doc: Success \r\n");
                        }
                    }
                    catch (Exception ex1)
                    {
                        sbLog.AppendLine("PdfGen-doc-Ex:" + ex1.Message + " \r\n");
                    }

                }
            }
            catch (Exception ex)
            {
                sbLog.AppendLine("PdfGen-Ex:" + ex.Message + " \r\n");
            }

            return btPdf;
        }
    }
}
