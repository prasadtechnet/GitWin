
using PdfTemplator.PdfCore.Business.PdfModule;
using PdfTemplator.PdfCore.Models.Pdf;
using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTemplator.UI.Business
{
    public class TemplateInputGenerator
    {
        public TemplateModel GetTemplate(string type,BodySectionModel objBody=null,PageHeaderSection pageHdr=null,PageFooterSection pageFooter=null)
        {
            var dict = new Dictionary<string, TemplateModel>
            {
                {"HCF",new HCF_TemplateModel(){ Body=objBody,PageHeader=pageHdr,PageFooter=pageFooter } },
                {"HC",new HC_TemplateModel(){Body=objBody,PageHeader=pageHdr } },
                {"CF",new CF_TemplateModel(){Body=objBody,PageFooter=pageFooter } },
                {"CUSTOM",new C_TemplateModel(){Body=objBody } }
            };

            return dict[type];
        }

        public TemplateModel GetHTMLTemplate(TreeView tvSource,List<GridModelProperty> lsModelProps)
        {           
            try
            {

                var body = new BodySectionModel();
                var Header = new PageHeaderSection();
                var Footer = new PageFooterSection();


                foreach (TreeNode item in tvSource.Nodes[0].Nodes)
                {
                    switch (item.Text.ToUpper())
                    {
                        case "CONTENT":
                            body.Tables = new List<TableModel>();
                            foreach (TreeNode tab in item.Nodes)
                            {
                                body.Tables.Add(PrepareMainTable(tab));
                            }
                            break;
                        case "PAGEHEADER":
                            Header.Tables = new List<TableModel>();
                            foreach (TreeNode tab in item.Nodes)
                            {
                                body.Tables.Add(PrepareMainTable(tab));
                            }
                            break;
                        case "PAGEFOOTER":
                            Footer = new PageFooterSection() {FooterType="N" };
                            break;
                    }
                }
                var objDocprops = (tvSource.Nodes[0].Tag as ControlPropertyModel).Properties as DocumentGridModel;
                var objTemplate = GetTemplate(objDocprops.Template, body, Header, Footer);

                objTemplate.ModelVariables = lsModelProps.Select(x => new ModelInfoModel {ModelName=x.ModelName,ModelType=x.ModelType }).Distinct().ToList();

                objTemplate.bindingModelProps = lsModelProps.Select(x => new BindingModel {ModelVariable=x.ModelName,PropName=x.FieldName}).Distinct().ToList();

                objTemplate.Name = objDocprops.Name;
                objTemplate.NameSpace = objDocprops.Namespace;
                objTemplate.Type = objDocprops.Template=="CUSTOM"?"C":objDocprops.Template;

                objTemplate.Location = objDocprops.Location;

                return objTemplate;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private TableModel PrepareMainTable(TreeNode tNode)
        {
            var objTable = new TableModel();
            if (tNode.Nodes.Count > 0)
            {
                objTable.Rows = new List<RowModel>();
                for (int intR = 0; intR < tNode.Nodes.Count; intR++)
                {
                    var objRow = new RowModel();
                    if (tNode.Nodes[intR].Nodes.Count > 0)
                    {
                        objRow.Cells = new List<CellModel>();
                        for (int intC = 0; intC < tNode.Nodes[intR].Nodes.Count; intC++)
                        {
                            objRow.Cells.Add(PrepareCellModel(tNode.Nodes[intR].Nodes[intC]));
                        }
                    }
                    objTable.Rows.Add(objRow);
                }                
            }
            
            return objTable;
        }

        private CellModel PrepareCellModel(TreeNode tNode)
        {
            var cell = new CellModel { };
            try
            {
                
                var contM = (tNode.Tag as ControlPropertyModel);
                switch (contM.ControlType.ToUpper())
                {
                  
                    case "LABEL":
                       var objlbl = contM.Properties as LabelCellGridClass;
                        var parentLabel = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new LabelCell {
                            Text=objlbl.Name,
                            Font=new FontModel {FontFamily=objlbl.LabelFont.FontFamily.Name,FontSize=objlbl.LabelFont.Size },
                            Color=new ColorModel { Type="RGB",Blue=objlbl.LabelColor.B,Green=objlbl.LabelColor.G,Red=objlbl.LabelColor.R},
                            ColSpan=parentLabel.ColSpan,
                            RowSpan=parentLabel.RowSpan,
                            PTop=parentLabel.PTop,
                            PBottom = parentLabel.PBottom,
                            PLeft = parentLabel.PLeft,
                            PRight = parentLabel.PRight,
                            BorderPattren=parentLabel.BorderPattren.ToString().Replace("-",","),
                            ContentType="",
                            HAlign= Master.Master.GetAlignmentNumber(parentLabel.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentLabel.VAlign),
                            Height=objlbl.Height                           
                        };
                       
                        break;
                    case "FIELD":
                        var objfld = contM.Properties as FieldCellGridClass;
                        var parentField = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new FieldCell
                        {
                            DataFieldName = objfld.Name,
                            ModelName=objfld.FieldModel,
                            Font = new FontModel { FontFamily = objfld.FieldFont.FontFamily.Name, FontSize = objfld.FieldFont.Size },
                            Color = new ColorModel { Type = "RGB", Blue = objfld.FieldColor.B, Green = objfld.FieldColor.G, Red = objfld.FieldColor.R },
                            ColSpan = parentField.ColSpan,
                            RowSpan = parentField.RowSpan,
                            PTop = parentField.PTop,
                            PBottom = parentField.PBottom,
                            PLeft = parentField.PLeft,
                            PRight = parentField.PRight,
                            BorderPattren = parentField.BorderPattren.ToString().Replace("-", ","),
                            ContentType = "",
                            HAlign = Master.Master.GetAlignmentNumber(parentField.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentField.VAlign),
                            Height = objlbl.Height
                        };
                        break;
                    case "EMPTY":
                        var objempty = contM.Properties as EmptyCellGridClass;
                        break;
                    case "TABLE":
                        //var objtbl = contM.Properties as TableGridClass;
                        //PrepareMainTable(tNode.Nodes[0]);
                        break;
                    case "IMAGEURL":
                        var objimgUrl = contM.Properties as ImageUrlCellGridClass;
                        break;
                    case "IMAGEBYTE":
                        var objImgByte = contM.Properties as ImageByteCellGridClass;
                        break;
                    case "IMAGESUBURL":
                        var objImgSubUrl = contM.Properties as ImageSubUrlCellGridClass;
                        break;
                    case "IMAGESUBBYTE":
                        var objImgSubByte = contM.Properties as ImageSubByteCellGridClass;
                        break;
                
            }
                
            }
            catch (Exception ex)
            {
            }
            return cell;
        }
    }
}
