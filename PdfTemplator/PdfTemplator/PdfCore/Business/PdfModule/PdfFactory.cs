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
       
        protected string ProcessTable(TableModel objMainTab,List<ModelInfoModel> lsMainModelsInfo,out List<string> requiredModels)
        {
            var strRes = "";
            var strResChild = "";
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
                                {
                                    lsModels.Add((cell as ImageByteCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByte(cell as ImageByteCell) + ");");
                                }
                                break;
                            case "IMAGEURLSUB":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrlSubHeading(cell as ImageUrlSubHeaderCell) + ");");
                                break;
                            case "IMAGEBYTESUB":
                                {
                                    lsModels.Add((cell as ImageByteSubHeaderCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell) + ");");
                                }
                                break;
                            case "EMPTYCELL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessEmpty(cell as EmptyCell) + ");");
                                break;
                            case "TABLECELL":
                                {
                                    var lsChildModels = new List<string>();
                                    var lsTabRes = ProcessTableCell(cell as TableCell,lsMainModelsInfo,out lsChildModels);
                                    if (lsChildModels != null && lsChildModels.Count > 0)
                                        lsModels.AddRange(lsChildModels);

                                    if (lsTabRes != null && lsTabRes.Count > 0)
                                    {
                                        lsTableCells.Add(lsTabRes[1]);
                                        lsTableCells.Add(objTabVarname + ".AddCell(" + lsTabRes[0] + ");");

                                        strResChild = strResChild+ lsTabRes[2]+"\r\n";
                                    }
                                }
                                break;
                        }
                    }
                }

                var strCells = String.Join("\r\n", lsTableCells.ToArray());
                var strDictModels = "";

                if (lsModels != null && lsModels.Count > 0)
                {
                    lsModels = lsModels.Distinct().ToList();
                    var localParams = new List<string>();
                    foreach (var item in lsModels)
                    {
                        localParams.Add(lsMainModelsInfo.Find(x => x.ModelName == item).ModelType == "L" ? "List<"+ item + "> ls" + item : item+ " obj" + item);
                    }
                    strDictModels = string.Join(",", localParams.ToArray()) + ",";
                    //  strDictModels = string.Join(",", lsModels.Distinct().Select(x => x + " obj" + x).ToArray()) + ",";
                    //strDictModels = string.Join(",", lsModels.Distinct().Select(x => x + " obj" + x).ToArray()) + ",";
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

                strRes = strTab+"\r\n" +( strResChild!=""? strResChild:"");

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        protected string ProcessDynamicTable(TableModel objMainTab, List<ModelInfoModel> lsMainModelsInfo, out List<string> requiredModels)
        {
            var strRes = "";
            var strResChild = "";
            requiredModels = new List<string>();
            try
            {
                var objTabVarname = "obj" + objMainTab.TableName;
                var lsModels = new List<string>();
                var lsTableHeaderCells = new List<string>();
                var lsTableCells = new List<string>();

                if (objMainTab.isDynamicTab && objMainTab.HeaderRows.Count > 0)
                {
                    foreach (var row in objMainTab.HeaderRows)
                    {
                        foreach (var cell in row.Cells)
                        {
                            if (cell.ContentType.ToUpper() == "LABEL")
                            {
                                lsTableHeaderCells.Add(objTabVarname + ".AddCell(" + ProcessLabel(cell as LabelCell) + ");");
                            }
                        }
                    }
                }

                foreach (var row in objMainTab.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        switch (cell.ContentType.ToUpper())
                        {
                            case "LABEL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessLabel(cell as LabelCell) + ");");
                                break;
                            case "FIELD":
                                {
                                    lsModels.Add((cell as FieldCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessField(cell as FieldCell) + ");");
                                }
                                break;
                            //case "IMAGEURL":
                            //    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrl(cell as ImageUrlCell) + ");");
                            //    break;
                            //case "IMAGEBYTE":
                            //    {
                            //        lsModels.Add((cell as ImageByteCell).ModelName);
                            //        lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByte(cell as ImageByteCell) + ");");
                            //    }
                            //    break;
                            //case "IMAGEURLSUB":
                            //    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrlSubHeading(cell as ImageUrlSubHeaderCell) + ");");
                            //    break;
                            //case "IMAGEBYTESUB":
                            //    {
                            //        lsModels.Add((cell as ImageByteSubHeaderCell).ModelName);
                            //        lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell) + ");");
                            //    }
                            //    break;
                            //case "EMPTYCELL":
                            //    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessEmpty(cell as EmptyCell) + ");");
                            //    break;
                            //case "TABLECELL":
                            //    {
                            //        var lsChildModels = new List<string>();
                            //        var lsTabRes = ProcessTableCell(cell as TableCell, out lsChildModels);
                            //        if (lsChildModels != null && lsChildModels.Count > 0)
                            //            lsModels.AddRange(lsChildModels);

                            //        if (lsTabRes != null && lsTabRes.Count > 0)
                            //        {
                            //            lsTableCells.Add(lsTabRes[1]);
                            //            lsTableCells.Add(objTabVarname + ".AddCell(" + lsTabRes[0] + ");");

                            //            strResChild = strResChild + lsTabRes[2] + "\r\n";
                            //        }
                            //    }
                            //    break;
                        }
                    }
                }


                var strCells = String.Join("\r\n", lsTableHeaderCells.ToArray());
                var temp = String.Join("\r\n", lsTableCells.ToArray());
                strCells = strCells + "\r\n #region Items \r\nfor(intChild=0;intChild<ls"+ lsModels[0]+".Count;intChild++)\r\n{\r\n "+ temp + "  \r\n}\r\n#endregion\r\n";

                var strDictModels = "";
                if (lsModels != null && lsModels.Count > 0)
                {
                    lsModels = lsModels.Distinct().ToList();
                    var localParams = new List<string>();
                    foreach (var item in lsModels)
                    {
                        localParams.Add(lsMainModelsInfo.Find(x => x.ModelName == item).ModelType == "L" ? "List<" + item + "> ls" + item : item + " obj" + item);
                    }
                    strDictModels = string.Join(",", localParams.ToArray()) + ",";
                  //  strDictModels = string.Join(",", lsModels.Distinct().Select(x => x + " obj" + x).ToArray()) + ",";
                    requiredModels = lsModels.Distinct().ToList();
                }
                var strTab = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.TableMethod);

                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableMethodName"], objMainTab.TableName + "Method");
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableVariableName"], objTabVarname);
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableMethodParams"], strDictModels);
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnNo"], objMainTab.noofClmns.ToString());
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableWidth"], objMainTab.width.ToString() + "f");
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnWidths"], String.Join(",", objMainTab.Colwidth.Select(x => x.ToString() + "f").ToArray()));
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableCells"], strCells);

                strRes = strTab ;

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }

        private string ProcessTableVariable(TableModel objMainTab, out List<string> requiredModels)
        {
            var strRes = "";
            requiredModels = new List<string>();
            try
            {
                var objTabVarname = "obj" + objMainTab.TableName;
                var lsModels = new List<string>();
                var lsTableCells = new List<string>();

                foreach (var row in objMainTab.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        switch (cell.ContentType.ToUpper())
                        {
                            case "LABEL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessLabel(cell as LabelCell) + ");");
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
                                {
                                    lsModels.Add((cell as ImageByteCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByte(cell as ImageByteCell) + ");");
                                }
                                break;
                            case "IMAGEURLSUB":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageUrlSubHeading(cell as ImageUrlSubHeaderCell) + ");");
                                break;
                            case "IMAGEBYTESUB":
                                {
                                    lsModels.Add((cell as ImageByteSubHeaderCell).ModelName);
                                    lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessImageByteSubHeading(cell as ImageByteSubHeaderCell) + ");");
                                }
                                break;
                            case "EMPTYCELL":
                                lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessEmpty(cell as EmptyCell) + ");");
                                break;
                            case "TABLECELL":
                                {
                                   // var lsTabRes = ProcessTableCell(cell as TableCell);
                                    //  lsTableCells.Add(objTabVarname + ".AddCell(" + ProcessTableCell(cell as TableCell) + ");");
                                }
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
                var strTab = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.TableVariable);

               
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableVariableName"], objTabVarname);       
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnNo"], objMainTab.noofClmns.ToString());
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableWidth"], objMainTab.width.ToString() + "f");
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableColumnWidths"], String.Join(",", objMainTab.Colwidth.Select(x => x.ToString() + "f").ToArray()));
                strTab = strTab.Replace(PdfTemplateManager.GetPdfDictionary()["TableCells"], strCells);

                strRes = strTab;

            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
        private List<string> ProcessTableCell(TableCell objTabChild,List<ModelInfoModel> lsMainModelInfo,out List<string> lsMethodModel)
        {
            var lsRes = new List<string>();
            lsMethodModel = new List<string>();
            try
            {
                var lsModels = new List<string>();
                var tabVarData = "";
                var tabVarMethod = "";
                if (objTabChild.tableModel.isSeparateMethod)
                {
                    tabVarMethod = ProcessTable(objTabChild.tableModel, lsMainModelInfo, out lsModels);
                    var strParams = "";
                    if (lsModels.Count > 0)
                        strParams = string.Join(",", lsModels.Select(x => "obj" + x).ToArray()) + ",";
                    // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";
                    lsMethodModel = lsModels;
                    tabVarData = "var obj" + objTabChild.tableModel.TableName+"=" + objTabChild.tableModel.TableName + "Method(" + strParams + " ref sbLog));";
                }
                else
                {
                    tabVarData = ProcessTableVariable(objTabChild.tableModel, out lsModels);
                    lsMethodModel = lsModels;
                }

              
                //iTextSharp.text.pdf.PdfPTable table, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color borderColor=null
                var strRes = String.Format(PdfTemplateManager.GetCellCallString("TABLECELL"), new object[] {
                    "obj"+objTabChild.tableModel.TableName,
                    GetAlignment(objTabChild.HAlign),
                    GetAlignment(objTabChild.VAlign),
                    "null"
                });

             
                //2.Child table cell phrase
                lsRes.Add(strRes);
                lsRes.Add(tabVarData);
                lsRes.Add(tabVarMethod);

            }
            catch (Exception ex)
            {
            }
            return lsRes;
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
                   "\"Arial\"",
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
                  (objField.IsDynamicField?  "ls"+objField.ModelName +"[intChild]." +objField.DataFieldName: "obj"+objField.ModelName +"." +objField.DataFieldName),
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
                ////string path, float scale, float height, int hAlign,
                ///int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, float p_left=0f, float p_right = 0f, float p_top = 0f, float p_btm = 0f, string borderPattren="T,R,B")
                strRes = String.Format(PdfTemplateManager.GetCellCallString("IMAGEURLCELL"), new object[] {
                   "\""+ objImage.Src+"\"",
                    objImage.Scale+"f",
                    objImage.Height+"f",
                    GetAlignment(objImage.HAlign),
                     GetAlignment(objImage.VAlign),
                    objImage.PLeft+"f",
                    objImage.PRight+"f",
                    objImage.PTop+"f",
                    objImage.PBottom+"f",
                    "\""+objImage.BorderPattren+"\""
                });

            }
            catch (Exception EX)
            {
            }
            return strRes;
        }
        private string ProcessImageByte(ImageByteCell objImage)
        {
            var strRes = "";
            try
            {
                //byte[] path, float scale, int hAlign, int vAlign = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color backColor = null
                strRes = String.Format(PdfTemplateManager.GetCellCallString("IMAGEBYTECELL"), new object[] {
                    "obj"+objImage.ModelName+"."+objImage.ImageFieldName,
                    objImage.Scale+"f",
                   GetAlignment(objImage.HAlign),
                   GetAlignment( objImage.VAlign),
                    "null"
                });
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
                ///string path, string SubHeading, string FontFamily, float fontSize = 10f, float scale = 50f, float Img_Height = 50f, float lbl_Height = 20f, 
                ///int fontWeight = iTextSharp.text.Font.NORMAL, float totalWidth = 250f,float abWidth = 210, int rowSpan = 1, int colSpan = 1
                strRes = String.Format(PdfTemplateManager.GetCellCallString("IMAGEURLSUBCELL"), new object[] {
                    "\""+ objImage.Src+"\"",
                      "\""+ objImage.label.Text+"\"",
                    "\""+ objImage.label.Font.FontFamily+"\"",
                     objImage.label.Font.FontSize+"f",
                     objImage.Scale+"f",
                     objImage.Height+"f",
                     objImage.label.Height+"f",
                     GetFontWeight(objImage.label.Font.FontWeight),
                     objImage.TableTotalWidth+"f",
                     objImage.AbWidth+"f",
                     objImage.RowSpan,
                     objImage.ColSpan
                });
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
                ///byte[] path,string SubHeading,string FontFamily,float fontSize=10f, float scale=50f,
                ///float Img_Height=50f, float lbl_Height=20f , int fontWeight= iTextSharp.text.Font.NORMAL,
                ///float totalWidth = 250f, float abWidth = 210, int rowSpan = 1, int colSpan = 1

                strRes = String.Format(PdfTemplateManager.GetCellCallString("IMAGEBYTESUBCELL"), new object[] {
                    "obj"+objImage.ModelName+"."+objImage.ImageFieldName,
                      "\""+ objImage.label.Text+"\"",
                    "\""+ objImage.label.Font.FontFamily+"\"",
                     objImage.label.Font.FontSize+"f",
                     objImage.Scale+"f",
                     objImage.Height+"f",
                     objImage.label.Height+"f",
                     GetFontWeight(objImage.label.Font.FontWeight),
                     objImage.TableTotalWidth+"f",
                     objImage.AbWidth+"f",
                     objImage.RowSpan,
                     objImage.ColSpan
                });
            }
            catch (Exception ex)
            {
            }
            return strRes;
        }

        protected string ProcessModelGeneration(List<ModelInfoModel> lsModelName, List<BindingModel> lsProps,string Namespace)
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
                        var modelProps = lsProps.Where(x => x.ModelVariable == model.ModelName).ToList();
                        if (modelProps.Count > 0)
                        {

                            if (modelProps.Count > 0)
                            {
                                var lsPropModel = String.Join("\r\n", modelProps.Select(x => propTemplate.Replace("<#ModelProps#>", x.PropName)).ToArray());

                                modelTemplate = modelTemplate.Replace("<#ModelClassName#>", model.ModelName);
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

            try
            {
                var objInput = objTemplate as HCF_TemplateModel;
                if (objInput.Name != "" && objInput.NameSpace != "")
                {
                    if (objInput.Body != null)
                    {
                        if (objInput.Body.Tables.Count > 0)
                        {

                            var lsHeader = GetHeaderInfo(objInput); //0-con params,1-varaible,2-constr assignment,3- object passing varaibles,4-local table code,5-local methods calling
                            var lsBody = GetBodyContent(objInput);

                            var objPageEvent = ",new PdfPageEvent(" + lsHeader[3] + ")";


                            //Header events
                            var strHeader = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageHeader);

                            strHeader = strHeader.Replace(PdfTemplateManager.GetPdfDictionary()["PageHeaderTablesCalling"], lsHeader[5]);
                            var strFooter = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageFooter);
                            var strHeaderPageEvent = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageEvent);


                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventLocalMethods"], lsHeader[4]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraibles"], lsHeader[1]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventConstructorParameters"], lsHeader[0]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraiblesParameter"], lsHeader[2]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageHeaderMethod"], strHeader);

                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageFooterMethod"], strFooter);


                            //Prepare Document
                            var strDocMethod = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.Template);
                            var strCoreMethods = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PdfCore);

                            //Page Events
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventObject"], objPageEvent);
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfDocumentPageEventClass"], strHeaderPageEvent);
                            //Namespace
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleNameSpace"], objTemplate.NameSpace);
                            //Module Name
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleName"], objTemplate.Name);
                            //Core Abstract 
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfCoreClass"], strCoreMethods);



                            //Module Pramaeters
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportParams"], lsBody[0]);
                            //List of Table Calling Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTableCallingList"], lsBody[2]);

                            //List of Table Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTablesList"], lsBody[1]);



                            //Model
                            var strModelFileContent = "";
                            if (objTemplate.ModelVariables.Count > 0)
                                strModelFileContent = ProcessModelGeneration(objTemplate.ModelVariables, objTemplate.bindingModelProps, objTemplate.NameSpace);



                            var objResp = new TemplateResponse();
                            //Template File 
                            objResp.PdfTemplateFileName = objTemplate.Name + ".cs";
                            objResp.PdfTemplateFileContent = strDocMethod;

                            //Model File
                            objResp.PdfModelsFileName = objTemplate.Name + "Models.cs";
                            objResp.PdfModelsFileContent = strModelFileContent;

                            objResp.Status = true;

                            return objResp;
                        }
                    }
                    else
                        return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };
                }
                else
                    return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };


            }
            catch (Exception ex)
            {

                return new TemplateResponse { Status = false, Message = "Exception:" + ex.Message };

            }
            return null;
        }
        private List<string> GetHeaderInfo(HCF_TemplateModel objInput)
        {
            try
            {
                //PdfPageEventObject
                //PageFooterMethod-empty
                //PageHeaderMethod
                //PageEventVaraiblesParameter
                //PageEventConstructorParameters
                //PageEventVaraibles


                var lsResult = new List<string>();

                try
                {

                    List<string> lsTables = new List<string>();
                    List<string> lsTableCalling = new List<string>();
                    List<string> lsPdfTableModelInputs = new List<string>();
                    List<string> lsPdfHeaderTableModelInputs = new List<string>();
                    var strParams = "";
                    foreach (var tab in objInput.PageHeader.Tables)
                    {
                        lsPdfTableModelInputs = new List<string>();
                        strParams = "";

                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                        if (lsPdfTableModelInputs.Count > 0)
                        {
                            lsPdfHeaderTableModelInputs.AddRange(lsPdfTableModelInputs);
                            var localParams = new List<string>();
                            foreach (var item in lsPdfTableModelInputs)
                            {
                                localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "_ls" + item : "_obj" + item);
                            }
                            strParams = string.Join(",", localParams.ToArray()) + ",";
                        }

                        // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                        lsTableCalling.Add("document.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                    }


                    lsResult = new List<string>();
                    if (lsPdfHeaderTableModelInputs.Count > 0)
                    {
                        strParams = "";
                        lsPdfHeaderTableModelInputs = lsPdfHeaderTableModelInputs.Distinct().ToList();

                        var localObjectParams = new List<string>();
                        var localParams = new List<string>();
                        var localConVariable = new List<string>();
                        var localConVariableAssign = new List<string>();
                        foreach (var item in lsPdfHeaderTableModelInputs)
                        {
                            localObjectParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "ls" + item : "obj" + item);
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "List<" + item + "> ls" + item : item + " obj" + item);
                            localConVariable.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "private List<" + item + "> _ls" + item + "=null;" : "private " + item + " _obj" + item + "=null;");
                            localConVariableAssign.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "_ls" + item + "=ls" + item + ";\r\n" : "_obj" + item + "=obj" + item + ";");
                        }

                        //params- required models
                        lsResult.Add(string.Join(",", localParams.ToArray()));
                        //variable
                        lsResult.Add(string.Join("\r\n", localConVariable.ToArray()));
                        //Constructor asssignment
                        lsResult.Add(string.Join("\r\n", localConVariableAssign.ToArray()));
                        //Page Event object passing varaibles
                        lsResult.Add(string.Join("\r\n", localObjectParams.ToArray()));

                    }
                    else
                    {
                        lsResult.Add("");
                        lsResult.Add("");
                        lsResult.Add("");
                        lsResult.Add("");
                    }
                    //table content

                    if (lsTables.Count > 0)
                        lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                    else
                        lsResult.Add("");

                    //table Calling in header
                    if (lsTableCalling.Count > 0)
                        lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                    else
                        lsResult.Add("");




                }
                catch (Exception ex)
                {
                }


                return lsResult;
                // strFooterPageEvent = strFooterPageEvent.Replace("PdfPageEventObject", "");
            }
            catch (Exception ex)
            {

            }


            return null;
        }
        private List<string> GetBodyContent(HCF_TemplateModel objInput)
        {
            List<string> lsResult = null;
            try
            {

                List<string> lsTables = new List<string>();
                List<string> lsTableCalling = new List<string>();
                List<string> lsPdfTableModelInputs = new List<string>();
                var strParams = "";
                foreach (var tab in objInput.Body.Tables)
                {
                    lsPdfTableModelInputs = new List<string>();
                    strParams = "";
                    if (tab.isDynamicTab)
                        lsTables.Add(ProcessDynamicTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));
                    else
                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                    if (lsPdfTableModelInputs.Count > 0)
                    {
                        var localParams = new List<string>();
                        foreach (var item in lsPdfTableModelInputs)
                        {
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "ls" + item : "obj" + item);


                        }
                        strParams = string.Join(",", localParams.ToArray()) + ",";
                    }

                    // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                    lsTableCalling.Add("lsPTables.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                }


                lsResult = new List<string>();
                if (objInput.ModelVariables.Count > 0)
                {
                    var xt = String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "N").Select(x => x.ModelName + " obj" + x.ModelName).ToArray());
                    if (objInput.ModelVariables.Where(y => y.ModelType == "L").Count() > 0)
                        xt = xt + (xt != "" ? "," : "") + String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "L").Select(x => "List<" + x.ModelName + "> ls" + x.ModelName).ToArray());
                    lsResult.Add(xt + ",");
                }

                else
                    lsResult.Add("");

                if (lsTables.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                else
                    lsResult.Add("");

                if (lsTableCalling.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                else
                    lsResult.Add("");

            }
            catch (Exception ex)
            {
            }

            return lsResult;
        }

        private List<string> GetFooterInfo(CF_TemplateModel objInput)
        {
            try
            {
                //PdfPageEventObject
                //PageFooterMethod
                //PageHeaderMethod-empty
                //PageEventVaraiblesParameter-empty
                //PageEventConstructorParameters-empty
                //PageEventVaraibles-empty


                var lsFooter = new List<string>();

                if (objInput.PageFooter.FooterType == "N")
                {

                }
                var strFooter = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageFooter);
              

                return lsFooter;
                // strFooterPageEvent = strFooterPageEvent.Replace("PdfPageEventObject", "");
            }
            catch (Exception ex)
            {

            }


            return null;
        }
    }
    public class HC : PdfTemplate
    {

        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {

            try
            {
                var objInput = objTemplate as HC_TemplateModel;
                if (objInput.Name != "" && objInput.NameSpace != "")
                {
                    if (objInput.Body != null)
                    {
                        if (objInput.Body.Tables.Count > 0)
                        {

                            var lsHeader = GetHeaderInfo(objInput); //0-con params,1-varaible,2-constr assignment,3- object passing varaibles,4-local table code,5-local methods calling
                            var lsBody = GetBodyContent(objInput);

                            var objPageEvent = ",new PdfPageEvent("+ lsHeader[3] + ")";


                            //Header events
                            var strHeader = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageHeader);

                            strHeader = strHeader.Replace(PdfTemplateManager.GetPdfDictionary()["PageHeaderTablesCalling"],lsHeader[5]);
                            var strHeaderPageEvent = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageEvent);

                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventLocalMethods"],lsHeader[4]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraibles"], lsHeader[1]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventConstructorParameters"], lsHeader[0]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraiblesParameter"], lsHeader[2]);
                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageHeaderMethod"], strHeader);

                            strHeaderPageEvent = strHeaderPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageFooterMethod"], "");
                           

                            //Prepare Document
                            var strDocMethod = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.Template);
                            var strCoreMethods = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PdfCore);

                            //Page Events
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventObject"], objPageEvent);
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfDocumentPageEventClass"], strHeaderPageEvent);
                            //Namespace
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleNameSpace"], objTemplate.NameSpace);
                            //Module Name
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleName"], objTemplate.Name);
                            //Core Abstract 
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfCoreClass"], strCoreMethods);



                            //Module Pramaeters
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportParams"], lsBody[0]);
                            //List of Table Calling Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTableCallingList"], lsBody[2]);

                            //List of Table Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTablesList"], lsBody[1]);



                            //Model
                            var strModelFileContent = "";
                            if (objTemplate.ModelVariables.Count > 0)
                                strModelFileContent = ProcessModelGeneration(objTemplate.ModelVariables, objTemplate.bindingModelProps, objTemplate.NameSpace);



                            var objResp = new TemplateResponse();
                            //Template File 
                            objResp.PdfTemplateFileName = objTemplate.Name + ".cs";
                            objResp.PdfTemplateFileContent = strDocMethod;

                            //Model File
                            objResp.PdfModelsFileName = objTemplate.Name + "Models.cs";
                            objResp.PdfModelsFileContent = strModelFileContent;

                            objResp.Status = true;

                            return objResp;
                        }
                    }
                    else
                        return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };
                }
                else
                    return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };


            }
            catch (Exception ex)
            {

                return new TemplateResponse { Status = false, Message = "Exception:" + ex.Message };

            }
            return null;
        }

        private List<string> GetHeaderInfo(HC_TemplateModel objInput)
        {
            try
            {
                //PdfPageEventObject
                //PageFooterMethod-empty
                //PageHeaderMethod
                //PageEventVaraiblesParameter
                //PageEventConstructorParameters
                //PageEventVaraibles


                var lsResult = new List<string>();

                try
                {

                    List<string> lsTables = new List<string>();
                    List<string> lsTableCalling = new List<string>();
                    List<string> lsPdfTableModelInputs = new List<string>();
                    List<string> lsPdfHeaderTableModelInputs = new List<string>();
                    var strParams = "";
                    foreach (var tab in objInput.PageHeader.Tables)
                    {
                        lsPdfTableModelInputs = new List<string>();
                        strParams = "";
                        
                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                        if (lsPdfTableModelInputs.Count > 0)
                        {
                            lsPdfHeaderTableModelInputs.AddRange(lsPdfTableModelInputs);
                            var localParams = new List<string>();
                            foreach (var item in lsPdfTableModelInputs)
                            {
                                localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "_ls" + item : "_obj" + item);
                            }
                            strParams = string.Join(",", localParams.ToArray()) + ",";
                        }

                        // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                        lsTableCalling.Add("document.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                    }


                     lsResult = new List<string>();
                    if (lsPdfHeaderTableModelInputs.Count > 0)
                    {
                        strParams = "";
                        lsPdfHeaderTableModelInputs = lsPdfHeaderTableModelInputs.Distinct().ToList();

                        var localObjectParams = new List<string>();
                        var localParams = new List<string>();
                        var localConVariable = new List<string>();
                        var localConVariableAssign = new List<string>();
                        foreach (var item in lsPdfHeaderTableModelInputs)
                        {
                            localObjectParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ?"ls" + item : "obj" + item);
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "List<" + item + "> ls" + item : item + " obj" + item);
                            localConVariable.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "private List<" + item + "> _ls" + item + "=null;" : "private " + item + " _obj" + item + "=null;");
                            localConVariableAssign.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "_ls" + item + "=ls" + item + ";\r\n" : "_obj" + item + "=obj" + item + ";");
                        }
                       
                        //params- required models
                        lsResult.Add(string.Join(",", localParams.ToArray()) );
                        //variable
                        lsResult.Add(string.Join("\r\n", localConVariable.ToArray()));
                        //Constructor asssignment
                        lsResult.Add(string.Join("\r\n", localConVariableAssign.ToArray()));
                        //Page Event object passing varaibles
                        lsResult.Add(string.Join("\r\n", localObjectParams.ToArray()) );

                    }
                    else
                    {
                        lsResult.Add("");
                        lsResult.Add("");
                        lsResult.Add("");
                        lsResult.Add("");
                    }
                    //table content

                    if (lsTables.Count > 0)
                        lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                    else
                        lsResult.Add("");

                    //table Calling in header
                    if (lsTableCalling.Count > 0)
                        lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                    else
                        lsResult.Add("");

                  


                }
                catch (Exception ex)
                {
                }

               
                return lsResult;
                // strFooterPageEvent = strFooterPageEvent.Replace("PdfPageEventObject", "");
            }
            catch (Exception ex)
            {

            }


            return null;
        }
        private List<string> GetBodyContent(HC_TemplateModel objInput)
        {
            List<string> lsResult = null;
            try
            {

                List<string> lsTables = new List<string>();
                List<string> lsTableCalling = new List<string>();
                List<string> lsPdfTableModelInputs = new List<string>();
                var strParams = "";
                foreach (var tab in objInput.Body.Tables)
                {
                    lsPdfTableModelInputs = new List<string>();
                    strParams = "";
                    if (tab.isDynamicTab)
                        lsTables.Add(ProcessDynamicTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));
                    else
                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                    if (lsPdfTableModelInputs.Count > 0)
                    {
                        var localParams = new List<string>();
                        foreach (var item in lsPdfTableModelInputs)
                        {
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "ls" + item : "obj" + item);


                        }
                        strParams = string.Join(",", localParams.ToArray()) + ",";
                    }

                    // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                    lsTableCalling.Add("lsPTables.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                }


                lsResult = new List<string>();
                if (objInput.ModelVariables.Count > 0)
                {
                    var xt = String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "N").Select(x => x.ModelName + " obj" + x.ModelName).ToArray());
                    if (objInput.ModelVariables.Where(y => y.ModelType == "L").Count() > 0)
                        xt = xt + (xt != "" ? "," : "") + String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "L").Select(x => "List<" + x.ModelName + "> ls" + x.ModelName).ToArray());
                    lsResult.Add(xt + ",");
                }

                else
                    lsResult.Add("");

                if (lsTables.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                else
                    lsResult.Add("");

                if (lsTableCalling.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                else
                    lsResult.Add("");

            }
            catch (Exception ex)
            {
            }

            return lsResult;
        }
    }
    public class CF : PdfTemplate
    {
        public override TemplateResponse ProcessTemplate(TemplateModel objTemplate)
        {

            try
            {
                var objInput = objTemplate as CF_TemplateModel;
                if (objInput.Name != "" && objInput.NameSpace != "")
                {
                    if (objInput.Body != null)
                    {
                        if (objInput.Body.Tables.Count > 0)
                        {

                            var lsFooter = GetFooterInfo(objInput);
                            var lsBody = GetBodyContent(objInput);

                            var objPageEvent = ",new PdfPageEvent()";


                            //Prepare Document
                            var strDocMethod = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.Template);
                            var strCoreMethods = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PdfCore);

                            //Page Events
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventObject"], objPageEvent);
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfDocumentPageEventClass"], lsFooter[0]);
                            //Namespace
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleNameSpace"], objTemplate.NameSpace);
                            //Module Name
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleName"], objTemplate.Name);
                            //Core Abstract 
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfCoreClass"], strCoreMethods);



                            //Module Pramaeters
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportParams"], lsBody[0]);
                            //List of Table Calling Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTableCallingList"], lsBody[2]);

                            //List of Table Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTablesList"], lsBody[1]);



                            //Model
                            var strModelFileContent = "";
                            if (objTemplate.ModelVariables.Count > 0)
                                strModelFileContent = ProcessModelGeneration(objTemplate.ModelVariables, objTemplate.bindingModelProps, objTemplate.NameSpace);



                            var objResp = new TemplateResponse();
                            //Template File 
                            objResp.PdfTemplateFileName = objTemplate.Name + ".cs";
                            objResp.PdfTemplateFileContent = strDocMethod;

                            //Model File
                            objResp.PdfModelsFileName = objTemplate.Name + "Models.cs";
                            objResp.PdfModelsFileContent = strModelFileContent;

                            objResp.Status = true;

                            return objResp;
                        }
                    }
                    else
                        return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };
                }
                else
                    return new TemplateResponse { Status = false, Message = "Invalid Name / Namespace" };


            }
            catch (Exception ex)
            {

                return new TemplateResponse { Status = false, Message = "Exception:" + ex.Message };

            }
            return null;
        }


        private List<string> GetBodyContent(CF_TemplateModel objInput)
        {
            List<string> lsResult = null;
            try
            {

                List<string> lsTables = new List<string>();
                List<string> lsTableCalling = new List<string>();
                List<string> lsPdfTableModelInputs = new List<string>();
                var strParams = "";
                foreach (var tab in objInput.Body.Tables)
                {
                    lsPdfTableModelInputs = new List<string>();
                    strParams = "";
                    if (tab.isDynamicTab)
                        lsTables.Add(ProcessDynamicTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));
                    else
                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                    if (lsPdfTableModelInputs.Count > 0)
                    {
                        var localParams = new List<string>();
                        foreach (var item in lsPdfTableModelInputs)
                        {
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "ls" + item : "obj" + item);


                        }
                        strParams = string.Join(",", localParams.ToArray()) + ",";
                    }

                    // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                    lsTableCalling.Add("lsPTables.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                }


                lsResult = new List<string>();
                if (objInput.ModelVariables.Count > 0)
                {
                    var xt = String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "N").Select(x => x.ModelName + " obj" + x.ModelName).ToArray());
                    if (objInput.ModelVariables.Where(y => y.ModelType == "L").Count() > 0)
                        xt = xt + (xt != "" ? "," : "") + String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "L").Select(x => "List<" + x.ModelName + "> ls" + x.ModelName).ToArray());
                    lsResult.Add(xt + ",");
                }

                else
                    lsResult.Add("");

                if (lsTables.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                else
                    lsResult.Add("");

                if (lsTableCalling.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                else
                    lsResult.Add("");

            }
            catch (Exception ex)
            {
            }

            return lsResult;
        }
        private List<string> GetFooterInfo(CF_TemplateModel objInput)
        {
            try
            {
                //PdfPageEventObject
                //PageFooterMethod
                //PageHeaderMethod-empty
                //PageEventVaraiblesParameter-empty
                //PageEventConstructorParameters-empty
                //PageEventVaraibles-empty


                var lsFooter = new List<string>();
               
                if( objInput.PageFooter.FooterType == "N")
                {

                }
                var strFooter = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageFooter);
                var strFooterPageEvent = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PageEvent);

                strFooterPageEvent = strFooterPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraibles"], "");
                strFooterPageEvent = strFooterPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventConstructorParameters"], "");
                strFooterPageEvent = strFooterPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageEventVaraiblesParameter"], "");
                strFooterPageEvent = strFooterPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageHeaderMethod"], "");

                strFooterPageEvent = strFooterPageEvent.Replace(PdfTemplateManager.GetPdfDictionary()["PageFooterMethod"], strFooter);
                lsFooter.Add(strFooterPageEvent);

                return lsFooter;
               // strFooterPageEvent = strFooterPageEvent.Replace("PdfPageEventObject", "");
            }
            catch (Exception ex)
            {

            }
           

            return null;
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
                            //List<string> lsTables = new List<string>();
                            //List<string> lsTableCalling = new List<string>();
                            //List<string> lsPdfTableModelInputs = new List<string>();
                            //var strParams = "";
                            //foreach (var tab in objInput.Body.Tables)
                            //{
                            //    lsPdfTableModelInputs = new List<string>();
                            //    strParams = "";

                            //    lsTables.Add(ProcessTable(tab,out lsPdfTableModelInputs));

                            //    if(lsPdfTableModelInputs.Count>0)
                            //        strParams = string.Join(",", lsPdfTableModelInputs.Select(x => "obj" + x).ToArray()) + ",";
                            //   // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                            //    lsTableCalling.Add("lsPTables.Add("+tab.TableName+"Method(" + strParams + " ref sbLog));");
                            //}

                            //var reportParams = "";
                            //var listOfmethods = "";
                            //var listOfmethodCalling = "";

                            //if (objTemplate.ModelVariables.Count>0)
                            //    reportParams =String.Join(",", objTemplate.ModelVariables.Select(x => x + " obj" + x).ToArray()) + ",";

                            //if (lsTables.Count>0)
                            //    listOfmethods = String.Join("\r\n", lsTables.ToArray());

                            //if (lsTableCalling.Count > 0)
                            //    listOfmethodCalling = String.Join("\r\n", lsTableCalling.ToArray());

                           var lsBody= GetBodyContent(objInput);


                            //Prepare Document
                            var strDocMethod=PdfTemplateManager.GetFileContent(TemplatePhysicalFile.Template);
                            var strCoreMethods = PdfTemplateManager.GetFileContent(TemplatePhysicalFile.PdfCore);

                            //Page Events
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfPageEventObject"], "");
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfDocumentPageEventClass"], "");
                            //Namespace
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleNameSpace"], objTemplate.NameSpace);
                            //Module Name
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["ModuleName"], objTemplate.Name);                          
                            //Core Abstract 
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfCoreClass"], strCoreMethods);
                                                        


                            //Module Pramaeters
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportParams"], lsBody[0]);
                            //List of Table Calling Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTableCallingList"], lsBody[2]);

                            //List of Table Code
                            strDocMethod = strDocMethod.Replace(PdfTemplateManager.GetPdfDictionary()["PdfReportPTablesList"], lsBody[1]);



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

        private List<string> GetBodyContent(C_TemplateModel objInput)
        {
            List<string> lsResult = null;
            try
            {

                List<string> lsTables = new List<string>();
                List<string> lsTableCalling = new List<string>();
                List<string> lsPdfTableModelInputs = new List<string>();
                var strParams = "";
                foreach (var tab in objInput.Body.Tables)
                {
                    lsPdfTableModelInputs = new List<string>();
                    strParams = "";
                    if(tab.isDynamicTab)
                        lsTables.Add(ProcessDynamicTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));
                    else
                        lsTables.Add(ProcessTable(tab, objInput.ModelVariables, out lsPdfTableModelInputs));

                    if (lsPdfTableModelInputs.Count > 0)
                    {
                        var localParams = new List<string>();
                        foreach (var item in lsPdfTableModelInputs)
                        {
                            localParams.Add(objInput.ModelVariables.Find(x => x.ModelName == item).ModelType == "L" ? "ls" + item : "obj" + item);

                          
                        }
                        strParams = string.Join(",", localParams.ToArray()) + ",";
                    }
                      
                    // strParams = string.Join(",", lsPdfTableModelInputs.Select(x => x + " obj" + x).ToArray())+",";

                    lsTableCalling.Add("lsPTables.Add(" + tab.TableName + "Method(" + strParams + " ref sbLog));");
                }


                lsResult = new List<string>();
                if (objInput.ModelVariables.Count > 0)
                {
                    var xt = String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "N").Select(x => x.ModelName + " obj" + x.ModelName).ToArray());
                    if(objInput.ModelVariables.Where(y => y.ModelType == "L").Count()>0)
                        xt = xt+(xt !=""?",":"")+ String.Join(",", objInput.ModelVariables.Where(y => y.ModelType == "L").Select(x => "List<"+x.ModelName +"> ls" + x.ModelName).ToArray());
                    lsResult.Add(xt + ",");
                }
                  
                else
                    lsResult.Add("");

                if (lsTables.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTables.ToArray()));
                else
                    lsResult.Add("");

                if (lsTableCalling.Count > 0)
                    lsResult.Add(String.Join("\r\n", lsTableCalling.ToArray()));
                else
                    lsResult.Add("");

            }
            catch (Exception ex)
            {
            }

            return lsResult;
        }
    }

    #endregion
}
