
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
            var pdfContentT = new C_TemplateModel
            {
                Name="TestPdfTemplate",
                Type="C",
                NameSpace= "PdfModule",
                bindingModelProps=new List<BindingModel> { },
                ModelVariables=new List<string> { },
                Body=new BodySectionModel {
                    Tables=new List<TableModel>
                    {
                       new TableModel
                       {
                           isDynamicTab=false,
                           isHavingBorder=true,
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
                                       new LabelCell
                                       {
                                           CellNo=2,
                                           ContentType="Label",
                                           Text="Test Value"
                                       }
                                   }
                               }
                           }

                       }
                    }
                }
            };
            IBusinessManager objBM = new BusinessManager(new PdfCore.Business.FileModule.FileModule());
           var resp=objBM.GeneratePdfTemplate(pdfContentT, "D:\\");

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
