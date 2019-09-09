
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
        private TemplateModel GetTemplate(string type,BodySectionModel objBody=null,PageHeaderSection pageHdr=null,PageFooterSection pageFooter=null)
        {
            var dict = new Dictionary<string, TemplateModel>
            {
                {"HCF",new HCF_TemplateModel(){ Body=objBody,PageHeader=pageHdr,PageFooter=pageFooter } },
                {"HC",new HC_TemplateModel(){Body=objBody,PageHeader=pageHdr } },
                {"CF",new CF_TemplateModel(){Body=objBody,PageFooter=pageFooter } },
                {"CUSTOM",new C_TemplateModel(){Body=objBody } }
            };

            return dict[type.ToUpper()];
        }

        public TemplateModel PrepareMainTemplate(TreeView tvSource,List<GridModelProperty> lsModelProps)
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
                objTemplate.Type = objDocprops.Template.ToUpper()=="CUSTOM"?"C":objDocprops.Template;

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
           
           var tabObj= (tNode.Tag as ControlPropertyModel).Properties as TableGridClass;
            var objTable = new TableModel() {
                TableName=tabObj.Name,
                isChildTab=tabObj.IsChildTable,
                isDynamicTab=tabObj.IsDynamic,
                isSeparateMethod=tabObj.IsHavingSeparateMethod,
                noofClmns=tabObj.NoOfColumn,
                spaceAfter=tabObj.spaceAfter,
                spaceBefore=tabObj.spaceBefore,
                Colwidth=tabObj.ColumnWidths.ToList(),
                width=tabObj.width                
            };


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
                            for (int intCChild = 0; intCChild < tNode.Nodes[intR].Nodes[intC].Nodes.Count; intCChild++)
                            {
                                objRow.Cells.Add(PrepareCellModel(tNode.Nodes[intR].Nodes[intC].Nodes[intCChild]));
                            }
                           
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
                            BorderPattren=parentLabel.BorderPattren.ToString().Replace("_",","),
                            ContentType= "LABEL",
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
                            BorderPattren = parentField.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "FIELD",
                            HAlign = Master.Master.GetAlignmentNumber(parentField.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentField.VAlign),
                            Height = objfld.Height
                        };
                        break;
                    case "EMPTY":
                        var objempty = contM.Properties as EmptyCellGridClass;
                        var parentEmpty = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new EmptyCell
                        {
                            Text=objempty.Name,                           
                            ColSpan = parentEmpty.ColSpan,
                            RowSpan = parentEmpty.RowSpan,
                            PTop = parentEmpty.PTop,
                            PBottom = parentEmpty.PBottom,
                            PLeft = parentEmpty.PLeft,
                            PRight = parentEmpty.PRight,
                            BorderPattren = parentEmpty.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "EMPTYCELL",
                            HAlign = Master.Master.GetAlignmentNumber(parentEmpty.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentEmpty.VAlign)                            
                        };
                        break;
                     
                    case "TABLE":
                        var objtbl = contM.Properties as TableGridClass;

                        //PrepareMainTable(tNode.Nodes[0]);
                        break;
                    case "IMAGEURL":
                        var objimgUrl = contM.Properties as ImageUrlCellGridClass;
                        var parentIU = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new ImageUrlCell
                        {                            
                            Src = objimgUrl.Src,
                            Scale = objimgUrl.Scale,                          
                            ColSpan = parentIU.ColSpan,
                            RowSpan = parentIU.RowSpan,
                            PTop = parentIU.PTop,
                            PBottom = parentIU.PBottom,
                            PLeft = parentIU.PLeft,
                            PRight = parentIU.PRight,
                            BorderPattren = parentIU.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "IMAGEURL",
                            HAlign = Master.Master.GetAlignmentNumber(parentIU.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentIU.VAlign),
                            Height = objimgUrl.Height
                        };
                        break;
                   
                    case "IMAGEBYTE":
                        var objImgByte = contM.Properties as ImageByteCellGridClass;
                        var parentIB = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new ImageByteCell
                        {
                            ImageFieldName = objImgByte.Name,
                            ModelName = objImgByte.ImageFieldModel,                            
                            Scale = objImgByte.Scale,
                            ColSpan = parentIB.ColSpan,
                            RowSpan = parentIB.RowSpan,
                            PTop = parentIB.PTop,
                            PBottom = parentIB.PBottom,
                            PLeft = parentIB.PLeft,
                            PRight = parentIB.PRight,
                            BorderPattren = parentIB.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "IMAGEBYTE",
                            HAlign = Master.Master.GetAlignmentNumber(parentIB.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentIB.VAlign),
                            Height = objImgByte.Height
                        };
                        break;
                    case "IMAGESUBURL":
                        var objImgSubUrl = contM.Properties as ImageSubUrlCellGridClass;
                        var parentISU = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new ImageUrlSubHeaderCell
                        {
                            Src = objImgSubUrl.Src,
                            Scale = objImgSubUrl.Scale,
                            ColSpan = parentISU.ColSpan,
                            RowSpan = parentISU.RowSpan,
                            PTop = parentISU.PTop,
                            PBottom = parentISU.PBottom,
                            PLeft = parentISU.PLeft,
                            PRight = parentISU.PRight,
                            BorderPattren = parentISU.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "IMAGEURLSUB",
                            HAlign = Master.Master.GetAlignmentNumber(parentISU.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentISU.VAlign),
                            Height = objImgSubUrl.Height
                        };
                        break;
                    case "IMAGESUBBYTE":
                        var objImgSubByte = contM.Properties as ImageSubByteCellGridClass;
                        var parentISB = (tNode.Parent.Tag as ControlPropertyModel).Properties as CellGridCalss;
                        cell = new ImageByteSubHeaderCell
                        {
                            ImageFieldName = objImgSubByte.Name,
                            ModelName = objImgSubByte.ImageFieldModel,
                            Scale = objImgSubByte.Scale,
                            ColSpan = parentISB.ColSpan,
                            RowSpan = parentISB.RowSpan,
                            PTop = parentISB.PTop,
                            PBottom = parentISB.PBottom,
                            PLeft = parentISB.PLeft,
                            PRight = parentISB.PRight,
                            BorderPattren = parentISB.BorderPattren.ToString().Replace("_", ","),
                            ContentType = "IMAGEBYTESUB",
                            HAlign = Master.Master.GetAlignmentNumber(parentISB.HAlign),
                            VAlign = Master.Master.GetAlignmentNumber(parentISB.VAlign),
                            Height = objImgSubByte.Height
                        };
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
