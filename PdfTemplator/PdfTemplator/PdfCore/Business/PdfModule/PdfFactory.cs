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
       
        protected string ProcessTable(TableModel objMainTab,out List<string> requiredModels)
        {
            var strRes = "";
            requiredModels = new List<string>();
            try
            {
                var objTabVarname = "obj"+objMainTab.TableName;
                var lsModels = new List<string>();
                var lsTableCells = new List<string>();

                foreach (var row in objMainTab.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        switch (cell.ContentType.ToUpper())
                        {
                            case "LABEL":
                                lsTableCells.Add(objTabVarname+".AddCell("+ProcessLabel(cell as LabelCell)+");");
                                break;
                            case "FIELD":
                                {
                                    lsModels.Add((cell as FieldCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessField(cell as FieldCell) + ");");
                                }                               
                                break;
                            case "IMAGEURL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrl(cell as ImageUrlCell) + ");");
                                break;
                            case "IMAGEBYTE":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByte(cell as ImageByteCell) + ");");
                                break;
                            case "IMAGEURLSUB":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrlSubHeading(cell as ImageUrlSubHeaderCell) + ");");
                                break;
                            case "IMAGEBYTESUB":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell) + ");");
                                break;
                            case "EMPTYCELL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessEmpty(cell as EmptyCell) + ");");
                                break;
                            case "TABLECELL":                                
                                 lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessTableCell(cell as TableCell) + ");");                                
                                break;
                        }
                    }
                }

                var strCells = String.Join("\r\n", lsTableCells.ToArray());
                var strDictModels = "";
                if (lsModels != null && lsModels.Count > 0)
                {
                    strDictModels = string.Join(",", lsModels.Distinct().Select(x => x + " obj" + x).ToArray()) + ",";
                    requiredModels = lsModels.Distinct().ToList();
                }
                var strTab = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.TableMethod);

                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableMethodName"], objMainTab.TableName + "Method");
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableVariableName"], objTabVarname);
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableMethodParams"], strDictModels);
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnNo"], objMainTab.noofClmns.ToString());
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableWidth"], objMainTab.width.ToString()+"f");
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnWidths"],String.Join(",",objMainTab.Colwidth.Select(x=>x.ToString()+"f").ToArray()));
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableCells"], strCells);

                strRes = strTab;

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
                //string strValue{0}, string fontFamilyName{1},float fontSize{2}, int fontweight{3}, iTextSharp.text.Color fontColor{4}, int h_align{5}, int v_align{6}, float height{7}, float p_left{8},
                //float p_right{9}, float p_top{10}, float p_btm{11}, string borderPattren{12}, int rowspan{13}, int colspan{14}, iTextSharp.text.Color bgColor{15} = null

                strRes= String.Format(PdfTemplateManager.GetCellCallString("LABELCELL"),new object[] {
                   "\""+objLabel.Text+"\"",
                    "\""+objLabel.Font.FontFamily+"\"",
                    objLabel.Font.FontSize+"f",
                    GetFontWeight(objLabel.Font.FontWeight),
                    GetColor(objLabel.Color),
                    GetAlignment(objLabel.HAlign),
                    GetAlignment(objLabel.VAlign),
                    objLabel.Height+"f",
                    objLabel.PLeft+"f",
                    objLabel.PRight+"f",
                    objLabel.PTop+"f",
                    objLabel.PBottom+"f",
                    "\""+objLabel.BorderPattren+"\"",
                    objLabel.RowSpan,
                    objLabel.ColSpan,                    
                    "null"
              });
            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private string ProcessEmpty(EmptyCell objEmpty)
        {
            var strRes = "";
            try
            {
                strRes= String.Format(PdfTemplateManager.GetCellCallString("EMPTYCELL"), new object[] {
                    "\"\"",
                   "Arial",
                    "10f",
                    GetFontWeight(0),
                    GetColor(new ColorModel{Type="PDF",pdfColor=iTextSharp.text.Color.BLACK }),
                    GetAlignment(objEmpty.HAlign),
                    GetAlignment(objEmpty.VAlign),
                    objEmpty.Height+"f",
                    objEmpty.PLeft+"f",
                    objEmpty.PRight+"f",
                    objEmpty.PTop+"f",
                    objEmpty.PBottom+"f",
                    objEmpty.BorderPattren,
                    objEmpty.RowSpan,
                    objEmpty.ColSpan,
                    "null"
              });
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
                strRes = String.Format(PdfTemplateManager.GetCellCallString("FIELDCELL"), new object[] {
                   "obj"+objField.ModelName+"."+objField.DataFieldName,
                    "\""+objField.Font.FontFamily+"\"",
                    objField.Font.FontSize+"f",
                    GetFontWeight(objField.Font.FontWeight),
                    GetColor(objField.Color),
                    GetAlignment(objField.HAlign),
                    GetAlignment(objField.VAlign),
                    objField.Height+"f",
                    objField.PLeft+"f",
                    objField.PRight+"f",
                    objField.PTop+"f",
                    objField.PBottom+"f",
                    "\""+objField.BorderPattren+"\"",
                    objField.RowSpan,
                    objField.ColSpan,
                    "null"
              });
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

        protected string ProcessModelGeneration(List<string> lsModelName, List<BindingModel> lsProps,string Namespace)
        {
            var strRes = "";
            try
            {
                var modelNameSpaceTemplate = "namespace <#ModelsNamespace#> \r\n{\r\n <#ModelClasss#> \r\n}";
                var modelTemplate = "public class <#ModelClassName#> \r\n{\r\n <#ModelClassProps#> \r\n}";
                var propTemplate = "public string <#ModelProps#> {get; set;}";

                if (lsModelName.Count > 0)
                {
                    var lsModels = new List<string>();
                    foreach (var model in lsModelName)
                    {
                        var modelProps = lsProps.Where(x => x.ModelVariable == model).ToList();
                        if (modelProps.Count > 0)
                        {

                            if (modelProps.Count > 0)
                            {
                                var lsPropModel = String.Join("\r\n", modelProps.Select(x => propTemplate.Replace("<#ModelProps#>", x.PropName)).ToArray());

                                modelTemplate = modelTemplate.Replace("<#ModelClassName#>", model);
                                modelTemplate = modelTemplate.Replace("<#ModelClassProps#>", lsPropModel);
                                lsModels.Add(modelTemplate);
                            }
                            
                        }
                    }

                    if (lsModels.Count > 0)
                    {
                        var lsClasses = string.Join("\r\n\r\n", lsModels.ToArray());

                        modelNameSpaceTemplate= modelNameSpaceTemplate.Replace("<#ModelsNamespace#>", Namespace);
                        modelNameSpaceTemplate = modelNameSpaceTemplate.Replace("<#ModelClasss#>", lsClasses);

                        strRes = modelNameSpaceTemplate;
                    }


                }

               
            }
            catch (Exception ex)
            {
            }
            return strRes;
        }

        #region local
        private string GetColor(ColorModel objColor)
        {
            var strRes = "";
            switch (objColor.Type)
            {
                case "PDF":
                    strRes = "new iTextSharp.text.Color(" + objColor.pdfColor.R + "," + objColor.pdfColor.G + "," + objColor.pdfColor.B + ")";
                    break;
                case "HEX":
                    strRes = "new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml(\"" + objColor.HexString + "\"))";
                    break;
                case "RGB":                   
                    strRes = "new iTextSharp.text.Color("+objColor.Red+","+ objColor.Green+","+objColor.Blue+")";
                    break;
            }
            return strRes;
        }
        private string GetFontWeight(int Value)
        {
            var strRes = "iTextSharp.text.Font.";
            
            switch (Value)
            {
               
                case 0:
                    strRes = strRes + "NORMAL";
                    break;
                case 1:
                    strRes = strRes + "BOLD";
                    break;               
                case 2:
                    strRes = strRes + "ITALIC";
                    break;
                case 3:
                    strRes = strRes + "BOLDITALIC";
                    break;

            }


            return strRes;
        }
        private string GetAlignment(int Value)
        {
            var strRes = "iTextSharp.text.pdf.PdfPCell.";

            switch (Value)
            {
                case 0:
                    strRes = strRes + "ALIGN_LEFT";
                    break;
                case 1:
                    strRes = strRes + "ALIGN_CENTER";
                    break;
                case 2:
                    strRes = strRes + "ALIGN_RIGHT";
                    break;
                case 4:
                    strRes = strRes + "ALIGN_TOP";
                    break;
                case 5:
                    strRes = strRes + "ALIGN_MIDDLE";
                    break;
                case 6:
                    strRes = strRes + "ALIGN_BOTTOM";
                    break;
            }
         

            return strRes;

        }
        #endregion
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
                            List<string> lsTableCalling = new List<string>();
                            List<string> lsPdfTableModelInputs = new List<string>();
                            var strParams = "";
                            foreach (var tab in objInput.Body.Tables)
                            {
                                lsPdfTableModelInputs = new List<string>();
                                strParams = "";

                                lsTables.Add(ProcessTable(tab,out lsPdfTableModelInputs));

                                if(lsPdfTableModelInputs.Count>0)
                                    strParams = string.Join(",", lsPdfTableModelInputs.Select(x => "obj" + x).ToArray()) + ",";
                               // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                                lsTableCalling.Add("lsPTables.Add("+tab.TableName+"Method(" + strParams + " ref sbLog));");
                            }

                            var reportParams = "";
                            var listOfmethods = "";
                            var listOfmethodCalling = "";

                            if (objTemplate.ModelVariables.Count>0)
                                reportParams =String.Join(",", objTemplate.ModelVariables.Select(x => x + " obj" + x).ToArray()) + ",";

                            if (lsTables.Count>0)
                                listOfmethods = String.Join("\r\n", lsTables.ToArray());

                            if (lsTableCalling.Count > 0)
                                listOfmethodCalling = String.Join("\r\n", lsTableCalling.ToArray());


                            //Prepare Document
                            var strDocMethod=PdfTemplateManager.GetFileContent(TemplatePhysicalFile.Template);
                            var strCoreMethods = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PdfCore);

                            //Page Events
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["DocumentPageEvent"], "");
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfDocumentPageEventClass"], "");
                            //Namespace
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleNameSpace"], objTemplate.NameSpace);
                            //Module Name
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleName"], objTemplate.Name);                          
                            //Core Abstract 
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfCoreClass"], strCoreMethods);
                                                        


                            //Module Pramaeters
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportParams"], reportParams);
                            //List of Table Calling Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTableCallingList"], listOfmethodCalling);

                            //List of Table Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTablesList"], listOfmethods);



                            //Model
                            var strModelFileContent = "";
                            if (objTemplate.ModelVariables.Count>0)
                             strModelFileContent = ProcessModelGeneration(objTemplate.ModelVariables,objTemplate.bindingModelProps,objTemplate.NameSpace);
                          


                            var objResp = new TemplateResponse();
                            //Template File 
                            objResp.PdfTemplateFileName = objTemplate.Name+".cs";
                            objResp.PdfTemplateFileContent = strDocMethod;

                            //Model File
                            objResp.PdfModelsFileName = objTemplate.Name+"Models.cs";
                            objResp.PdfModelsFileContent = strModelFileContent;

                            objResp.Status = true;

                            return objResp;
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
