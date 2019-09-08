
using PdfTemplator.PdfCore.Business;
using PdfTemplator.PdfCore.Models.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTemplator
{
    public partial class frmPdf : Form
    {
        public frmPdf()
        {
            InitializeComponent();
        }

        private void frmPdf_Load(object sender, EventArgs e)
        {

            //  var strRes= Enum.GetName(typeof(PdfCore.Business.PdfModule.TemplatePhysicalFile), 1);
            //HCF
            var pdfContentT = new HCF_TemplateModel
            {
                Name = "TestPdfTemplate_HCF",
                Type = "HCF",
                NameSpace = "PdfModule.PDF",
                bindingModelProps = new List<BindingModel> {
                    new BindingModel {ModelVariable= "HeaderDetail", PropName= "TestValue" },
                    //new BindingModel {ModelVariable="ImageDetail",PropName="CustSign" },
                    //new BindingModel {ModelVariable="ImageDetail",PropName="TechSign"  },
                    //new BindingModel {ModelVariable="ItemDetails",PropName="HD1Field" },
                    //new BindingModel {ModelVariable="ItemDetails",PropName="HD2Field"  },
                    //new BindingModel {ModelVariable="ItemDetails",PropName="HD3Field"  },
                },
                ModelVariables = new List<ModelInfoModel> {
                    new ModelInfoModel { ModelName = "HeaderDetail",ModelType="N" },
                    //new ModelInfoModel{ModelName="ImageDetail",ModelType="N" },
                    //new ModelInfoModel{ModelName="ItemDetails",ModelType="L" }
                },
                Body = new BodySectionModel
                {
                    Tables = new List<TableModel>
                    {
                       new TableModel
                       {
                           isDynamicTab=false,
                           isHavingBorder=true,
                           isChildTab=false,
                           isSeparateMethod=true,
                           TableName="Detail",
                           noofClmns=2,
                           width=530f,
                           Colwidth=new List<float>{0.5f,0.5f },
                           spaceAfter=10f,
                           spaceBefore=0f,
                           Rows=new List<RowModel>{
                               new RowModel{
                                   Cells=new List<CellModel>
                                   {
                                       new LabelCell
                                       {
                                           CellNo=1,
                                           ContentType="Label",
                                           Text="Test"
                                       },
                                        new EmptyCell
                                       {
                                           CellNo=2,
                                           ContentType="EMPTYCELL",
                                           Text=""
                                       },
                                        new FieldCell
                                        {
                                            CellNo=3,
                                            ContentType="FIELD",
                                            DataFieldName="TestValue",
                                            ModelName="HeaderDetail"
                                        },
                                        new EmptyCell
                                       {
                                           CellNo=4,
                                           ContentType="EMPTYCELL",
                                           Text=""
                                       }
                                   }
                               }
                           }

                       },

                    }
                },
                PageHeader = new PageHeaderSection
                {
                    Tables = new List<TableModel>
                   {
                                  new TableModel
                                  {
                                      isDynamicTab=false,
                                      isHavingBorder=true,
                                      isChildTab=false,
                                      isSeparateMethod=true,
                                      TableName="PageLogoDetail",
                                      noofClmns=2,
                                      width=530f,
                                      Colwidth=new List<float>{0.5f,0.5f },
                                      spaceAfter=10f,
                                      spaceBefore=0f,
                                      Rows=new List<RowModel>{
                                          new RowModel{
                                              Cells=new List<CellModel>
                                              {

                                                   new ImageUrlCell{
                                                       CellNo=1,
                                                       ContentType="IMAGEURL",
                                                       Src="https://www.gstatic.com/webp/gallery/4.sm.jpg",
                                                       Scale=20f,
                                                       HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT
                                                   },
                                                   new LabelCell
                                                   {
                                                       CellNo=1,
                                                       ContentType="Label",
                                                       Text="Heading"
                                                   },
                                              }
                                          }
                                      }
                                  },
                                  new TableModel
                                   {
                                       isDynamicTab=false,
                                       isHavingBorder=true,
                                       isChildTab=false,
                                       isSeparateMethod=true,
                                       TableName="PageHeaderDetail",
                                       noofClmns=2,
                                       width=530f,
                                       Colwidth=new List<float>{0.5f,0.5f },
                                       spaceAfter=10f,
                                       spaceBefore=0f,
                                       Rows=new List<RowModel>{
                                           new RowModel{
                                               Cells=new List<CellModel>
                                               {
                                                   new LabelCell
                                                   {
                                                       CellNo=1,
                                                       ContentType="Label",
                                                       Text="Test"
                                                   },
                                                    new EmptyCell
                                                   {
                                                       CellNo=2,
                                                       ContentType="EMPTYCELL",
                                                       Text=""
                                                   },
                                                    new FieldCell
                                                    {
                                                        CellNo=3,
                                                        ContentType="FIELD",
                                                        DataFieldName="TestValue",
                                                        ModelName="HeaderDetail"
                                                    },
                                                    new EmptyCell
                                                   {
                                                       CellNo=4,
                                                       ContentType="EMPTYCELL",
                                                       Text=""
                                                   }
                                               }
                                           }
                                       }

                                   },
                   }
                },
                PageFooter=new PageFooterSection
                {
                    FooterType="N"
                }
            };
            //HC-content and Header
            //var pdfContentT = new HC_TemplateModel
            //{
            //    Name = "TestPdfTemplate_HC",
            //    Type = "HC",
            //    NameSpace = "PdfModule.PDF",
            //    bindingModelProps = new List<BindingModel> {
            //        new BindingModel {ModelVariable= "HeaderDetail", PropName= "TestValue" },
            //        //new BindingModel {ModelVariable="ImageDetail",PropName="CustSign" },
            //        //new BindingModel {ModelVariable="ImageDetail",PropName="TechSign"  },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD1Field" },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD2Field"  },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD3Field"  },
            //    },
            //    ModelVariables = new List<ModelInfoModel> {
            //        new ModelInfoModel { ModelName = "HeaderDetail",ModelType="N" },
            //        //new ModelInfoModel{ModelName="ImageDetail",ModelType="N" },
            //        //new ModelInfoModel{ModelName="ItemDetails",ModelType="L" }
            //    },
            //    Body = new BodySectionModel
            //    {
            //        Tables = new List<TableModel>
            //        {
            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=1,
            //                               ContentType="Label",
            //                               Text="Test"
            //                           },
            //                            new EmptyCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           },
            //                            new FieldCell
            //                            {
            //                                CellNo=3,
            //                                ContentType="FIELD",
            //                                DataFieldName="TestValue",
            //                                ModelName="HeaderDetail"
            //                            },
            //                            new EmptyCell
            //                           {
            //                               CellNo=4,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           }
            //                       }
            //                   }
            //               }

            //           },

            //        }
            //    },
            //    PageHeader = new PageHeaderSection
            //    {
            //       Tables=new List<TableModel>
            //       {
            //                      new TableModel
            //                      {
            //                          isDynamicTab=false,
            //                          isHavingBorder=true,
            //                          isChildTab=false,
            //                          isSeparateMethod=true,
            //                          TableName="PageLogoDetail",
            //                          noofClmns=2,
            //                          width=530f,
            //                          Colwidth=new List<float>{0.5f,0.5f },
            //                          spaceAfter=10f,
            //                          spaceBefore=0f,
            //                          Rows=new List<RowModel>{
            //                              new RowModel{
            //                                  Cells=new List<CellModel>
            //                                  {

            //                                       new ImageUrlCell{
            //                                           CellNo=1,
            //                                           ContentType="IMAGEURL",
            //                                           Src="https://www.gstatic.com/webp/gallery/4.sm.jpg",
            //                                           Scale=20f,
            //                                           HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT
            //                                       },
            //                                       new LabelCell
            //                                       {
            //                                           CellNo=1,
            //                                           ContentType="Label",
            //                                           Text="Heading"
            //                                       },
            //                                  }
            //                              }
            //                          }
            //                      },
            //                      new TableModel
            //                       {
            //                           isDynamicTab=false,
            //                           isHavingBorder=true,
            //                           isChildTab=false,
            //                           isSeparateMethod=true,
            //                           TableName="PageHeaderDetail",
            //                           noofClmns=2,
            //                           width=530f,
            //                           Colwidth=new List<float>{0.5f,0.5f },
            //                           spaceAfter=10f,
            //                           spaceBefore=0f,
            //                           Rows=new List<RowModel>{
            //                               new RowModel{
            //                                   Cells=new List<CellModel>
            //                                   {
            //                                       new LabelCell
            //                                       {
            //                                           CellNo=1,
            //                                           ContentType="Label",
            //                                           Text="Test"
            //                                       },
            //                                        new EmptyCell
            //                                       {
            //                                           CellNo=2,
            //                                           ContentType="EMPTYCELL",
            //                                           Text=""
            //                                       },
            //                                        new FieldCell
            //                                        {
            //                                            CellNo=3,
            //                                            ContentType="FIELD",
            //                                            DataFieldName="TestValue",
            //                                            ModelName="HeaderDetail"
            //                                        },
            //                                        new EmptyCell
            //                                       {
            //                                           CellNo=4,
            //                                           ContentType="EMPTYCELL",
            //                                           Text=""
            //                                       }
            //                                   }
            //                               }
            //                           }

            //                       },
            //       }
            //    }
            //};
            //CF-content and Footer

            //var pdfContentT = new CF_TemplateModel
            //{
            //    Name = "TestPdfTemplate_CF",
            //    Type = "CF",
            //    NameSpace = "PdfModule.PDF",
            //    bindingModelProps = new List<BindingModel> {
            //        new BindingModel {ModelVariable= "HeaderDetail", PropName= "TestValue" },
            //        //new BindingModel {ModelVariable="ImageDetail",PropName="CustSign" },
            //        //new BindingModel {ModelVariable="ImageDetail",PropName="TechSign"  },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD1Field" },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD2Field"  },
            //        //new BindingModel {ModelVariable="ItemDetails",PropName="HD3Field"  },
            //    },
            //    ModelVariables = new List<ModelInfoModel> {
            //        new ModelInfoModel { ModelName = "HeaderDetail",ModelType="N" },
            //        //new ModelInfoModel{ModelName="ImageDetail",ModelType="N" },
            //        //new ModelInfoModel{ModelName="ItemDetails",ModelType="L" }
            //    },
            //    Body = new BodySectionModel
            //    {
            //        Tables = new List<TableModel>
            //        {
            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=1,
            //                               ContentType="Label",
            //                               Text="Test"
            //                           },
            //                            new EmptyCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           },
            //                            new FieldCell
            //                            {
            //                                CellNo=3,
            //                                ContentType="FIELD",
            //                                DataFieldName="TestValue",
            //                                ModelName="HeaderDetail"
            //                            },
            //                            new EmptyCell
            //                           {
            //                               CellNo=4,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           }
            //                       }
            //                   }
            //               }

            //           },
                      
            //        }
            //    },
            //    PageFooter=new PageFooterSection
            //    {
            //        FooterType="N"
            //    }
            //};

            //Only Content

            //var pdfContentT = new C_TemplateModel
            //{
            //    Name = "TestPdfTemplate",
            //    Type = "C",
            //    NameSpace = "PdfModule.PDF",
            //    bindingModelProps = new List<BindingModel> {
            //        new BindingModel {ModelVariable= "HeaderDetail", PropName= "TestValue" },
            //        new BindingModel {ModelVariable="ImageDetail",PropName="CustSign" },
            //        new BindingModel {ModelVariable="ImageDetail",PropName="TechSign"  },
            //        new BindingModel {ModelVariable="ItemDetails",PropName="HD1Field" },
            //        new BindingModel {ModelVariable="ItemDetails",PropName="HD2Field"  },
            //        new BindingModel {ModelVariable="ItemDetails",PropName="HD3Field"  },
            //    },
            //    ModelVariables = new List<ModelInfoModel> {
            //        new ModelInfoModel { ModelName = "HeaderDetail",ModelType="N" },
            //        new ModelInfoModel{ModelName="ImageDetail",ModelType="N" },
            //        new ModelInfoModel{ModelName="ItemDetails",ModelType="L" }
            //    },
            //    Body = new BodySectionModel
            //    {
            //        Tables = new List<TableModel>
            //        {
            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=1,
            //                               ContentType="Label",
            //                               Text="Test"
            //                           },
            //                            new EmptyCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           },
            //                            new FieldCell
            //                            {
            //                                CellNo=3,
            //                                ContentType="FIELD",
            //                                DataFieldName="TestValue",
            //                                ModelName="HeaderDetail"
            //                            },
            //                            new EmptyCell
            //                           {
            //                               CellNo=4,
            //                               ContentType="EMPTYCELL",
            //                               Text=""
            //                           }
            //                       }
            //                   }
            //               }

            //           },
            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Tab2Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {

            //                            new ImageUrlCell{
            //                                CellNo=5,
            //                                ContentType="IMAGEURL",
            //                                Src="https://www.gstatic.com/webp/gallery/4.sm.jpg",
            //                                Scale=30f,
            //                                HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER
            //                            },
            //                            new ImageByteCell
            //                            {
            //                                CellNo =6,
            //                                ContentType="IMAGEBYTE",
            //                                ImageFieldName="CustSign",
            //                                ModelName="ImageDetail",
            //                                Scale=30f,
            //                                HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER
            //                            },
            //                            new ImageUrlSubHeaderCell
            //                            {
            //                                 CellNo=7,
            //                                ContentType="IMAGEURLSUB",
            //                                Src="https://www.gstatic.com/webp/gallery/4.sm.jpg",
            //                                Sclae=30f,
            //                                HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER,
            //                                label=new LabelCell
            //                                {
            //                                   Text="Cust Sign"
            //                                }
            //                            },
            //                            new ImageByteSubHeaderCell
            //                            {
            //                                 CellNo=8,
            //                                ContentType="IMAGEBYTESUB",
            //                                ImageFieldName="TechSign",
            //                                ModelName="ImageDetail",
            //                                Scale=30f,
            //                                HAlign=iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER,
            //                                label=new LabelCell
            //                                {
            //                                   Text="Tech Sign"
            //                                }
            //                            }
            //                           //new LabelCell
            //                           //{
            //                           //    CellNo=2,
            //                           //    ContentType="Label",
            //                           //    Text="Test Value"
            //                           //}
            //                       }
            //                   }
            //               }

            //           },
            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Tab3Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="Label",
            //                               Text="Test Value"
            //                           },
            //                           new TableCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="TABLECELL",
            //                               tableModel=new TableModel
            //                               {
            //                                   isChildTab=true,
            //                                   isDynamicTab=false,
            //                                   isHavingBorder=false,
            //                                   noofClmns=2,
            //                                   Colwidth=new List<float>{0.5f,0.5f },
            //                                   isSeparateMethod=false,
            //                                   width=260f,
            //                                   spaceAfter=2f,
            //                                   spaceBefore=0f,
            //                                   TableName="childTab1",
            //                                   Rows=new List<RowModel>
            //                                   {
            //                                       new RowModel
            //                                       {
            //                                           Cells=new List<CellModel>
            //                                           {
            //                                               new LabelCell
            //                                               {
            //                                                   CellNo=2,
            //                                                   ContentType="Label",
            //                                                   Text="Test ch1"
            //                                               },
            //                                                                    new LabelCell
            //                                               {
            //                                                   CellNo=2,
            //                                                   ContentType="Label",
            //                                                   Text="Test ch1 Value"
            //                                               }
            //                                           }
            //                                       }
            //                                   }
            //                               }
            //                           }
            //                       }
            //                   }
            //               }

            //           },

            //           new TableModel
            //           {
            //               isDynamicTab=false,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=true,
            //               TableName="Tab4Detail",
            //               noofClmns=2,
            //               width=530f,
            //               Colwidth=new List<float>{0.5f,0.5f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="Label",
            //                               Text="Test Value"
            //                           },
            //                           new TableCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="TABLECELL",
            //                               tableModel=new TableModel
            //                               {
            //                                   isChildTab=true,
            //                                   isDynamicTab=false,
            //                                   isHavingBorder=false,
            //                                   noofClmns=2,
            //                                   Colwidth=new List<float>{0.5f,0.5f },
            //                                   isSeparateMethod=true,
            //                                   width=260f,
            //                                   spaceAfter=2f,
            //                                   spaceBefore=0f,
            //                                   TableName="Tab4Detail_childTab2",
            //                                   Rows=new List<RowModel>
            //                                   {
            //                                       new RowModel
            //                                       {
            //                                           Cells=new List<CellModel>
            //                                           {
            //                                               new LabelCell
            //                                               {
            //                                                   CellNo=2,
            //                                                   ContentType="Label",
            //                                                   Text="Test ch2"
            //                                               },
            //                                                                    new LabelCell
            //                                               {
            //                                                   CellNo=2,
            //                                                   ContentType="Label",
            //                                                   Text="Test ch2 Value"
            //                                               }
            //                                           }
            //                                       }
            //                                   }
            //                               }
            //                           }
            //                       }
            //                   }
            //               }

            //           },
            //           new TableModel
            //           {
            //               isDynamicTab=true,
            //               isHavingBorder=true,
            //               isChildTab=false,
            //               isSeparateMethod=false,
            //               TableName="Tab5DynamicDetail",
            //               noofClmns=3,
            //               width=530f,
            //               Colwidth=new List<float>{0.2f,0.5f,0.3f },
            //               spaceAfter=10f,
            //               spaceBefore=0f,
            //               HeaderRows=new List<RowModel>{
            //                     new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                           new LabelCell
            //                           {
            //                               CellNo=1,
            //                               ContentType="Label",
            //                               Text="HD1"
            //                           },
            //                           new LabelCell
            //                           {
            //                               CellNo=2,
            //                               ContentType="Label",
            //                               Text="HD2"
            //                           },
            //                            new LabelCell
            //                           {
            //                               CellNo=3,
            //                               ContentType="Label",
            //                               Text="HD3"
            //                           }
            //                       }
            //                     }
            //               },
            //               Rows=new List<RowModel>{
            //                   new RowModel{
            //                       Cells=new List<CellModel>
            //                       {
            //                            new FieldCell
            //                            {
            //                                CellNo=1,
            //                                ContentType="FIELD",
            //                                DataFieldName="HD1Field",
            //                                ModelName="ItemDetails",
            //                                IsDynamicField=true
            //                            },
            //                              new FieldCell
            //                            {
            //                                CellNo=2,
            //                                ContentType="FIELD",
            //                                DataFieldName="HD2Field",
            //                                ModelName="ItemDetails",
            //                                 IsDynamicField=true
            //                            },
            //                              new FieldCell
            //                            {
            //                                CellNo=3,
            //                                ContentType="FIELD",
            //                                DataFieldName="HD3Field",
            //                                ModelName="ItemDetails",
            //                                IsDynamicField=true
            //                            }

            //                       }
            //                   }
            //               }

            //           }
            //        }
            //    }
            //};


            IBusinessManager objBM = new BusinessManager(new PdfCore.Business.FileModule.FileModule());
            var resp = objBM.GeneratePdfTemplate(pdfContentT, "E:\\");

        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            dgvSections.Enabled = false;



            tvTreeView.Nodes.Add("Document");

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
