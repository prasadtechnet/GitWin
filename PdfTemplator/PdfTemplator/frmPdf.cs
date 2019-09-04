
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
