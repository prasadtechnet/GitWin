using PdfTemplator.PdfCore.Models;
using PdfTemplator.PdfCore.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Business.PdfModule
{
   public static class PdfFactory
    {
       public static PdfTemplate GetTemplate(string type)
        {
            var dict = new Dictionary<string, PdfTemplate>
            {
                {"HCF",new HCF()},
                {"HC",new HC()},
                {"CF",new CF()},
                {"C",new C()}
            };

            return dict[type];
        }
    }



    #region Pdf Types

 

    public abstract class PdfTemplate
    {
        public abstract TemplateResponse ProcessTemplate(TemplateModel objTemplate);

        protected string ProcessTable(TableModel objMainTab)
        {
            var strRes = "";
            try
            {
                var lsModels = new List<string>();
                var lsTableCells = new List<string>();

                foreach (var row in objMainTab.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        switch (cell.ContentType.ToUpper())
                        {
                            case "LABEL":
                                lsTableCells.Add(ProcessLabel(cell as LabelCell));
                                break;
                            case "FIELD":
                                {
                                    lsModels.Add((cell as FieldCell).ModelVariableName);
                                    lsTableCells.Add(ProcessField(cell as FieldCell));
                                }                               
                                break;
                            case "IMAGEURL":
                                lsTableCells.Add(ProcessImageUrl(cell as ImageUrlCell));
                                break;
                            case "IMAGEBYTE":
                                lsTableCells.Add(ProcessImageByte(cell as ImageByteCell));
                                break;
                            case "IMAGEURLSUB":
                                lsTableCells.Add(ProcessImageUrlSubHeading(cell as ImageUrlSubHeaderCell));
                                break;
                            case "IMAGEBYTESUB":
                                lsTableCells.Add(ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell));
                                break;
                            case "EMPTYCELL":
                                lsTableCells.Add(ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell));
                                break;
                            case "TABLECELL":                                
                                 lsTableCells.Add(ProcessTableCell(cell as TableCell));                                
                                break;
                        }
                    }
                }

                var strCells = String.Join("\r\n", lsTableCells.ToArray());
                var strDictModels =string.Join(",",lsModels.Distinct().Select(x=>x+" obj"+x).ToArray());

                //FileModule.FileModule.ReadFile()

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessTableCell(TableCell objTabChild)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessLabel(LabelCell objLabel)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessEmpty(LabelCell objLabel)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessField(FieldCell objField)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessImageUrl(ImageUrlCell objImage)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessImageByte(ImageByteCell objImage)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessImageUrlSubHeading(ImageUrlSubHeaderCell objImage)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessImageByteSubHeading(ImageByteSubHeaderCell objImage)
        {
            var strRes = "";
            try
            {

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
    }
    public class HCF : PdfTemplate
    {
        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {
            var resp = new TemplateResponse();
            try
            {

            }
            catch (Exception ex)
            {
                resp.Message = "Exception:"+ex.Message;
            }
            return resp;
        }
    }
    public class HC : PdfTemplate
    {
        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {
            var resp = new TemplateResponse();
            try
            {

            }
            catch (Exception ex)
            {
                resp.Message = "Exception:" + ex.Message;
            }
            return resp;
        }
    }
    public class CF : PdfTemplate
    {
        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {
            var resp = new TemplateResponse();
            try
            {

            }
            catch (Exception ex)
            {
                resp.Message = "Exception:" + ex.Message;
            }
            return resp;
        }
    }
    public class C : PdfTemplate
    {
        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {
          
            try
            {
                var objInput = objTemplate as C_TemplateModel;
                if (objInput.Name != "" && objInput.NameSpace != "")
                {
                    if (objInput.Body != null)
                    {
                        if (objInput.Body.Tables.Count > 0)
                        {
                            List<string> lsTables = new List<string>();
                            foreach (var tab in objInput.Body.Tables)
                            {
                                lsTables.Add(ProcessTable(tab));
                            }
                        }
                    }
                    else
                    return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };
                }
                else
                    return new TemplateResponse {Status=false,Message="Invalid Name / Namespace" };


            }
            catch (Exception ex)
            {
               
                return new TemplateResponse { Status = false, Message = "Exception:" + ex.Message };
            
            }
            return null;
        }
    }

    #endregion
}
