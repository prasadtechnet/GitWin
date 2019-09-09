using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfTemplator.UI.Master;
using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;

namespace PdfTemplator
{
    public partial class frmPdfTemplateDesinger : Form
    {
        #region Varaibles
        TreeNode _tempNode = null;
        #endregion

        #region Constructor & Load   
        public frmPdfTemplateDesinger()
        {
            InitializeComponent();
        }

        private void frmPdfTemplateDesinger_Load(object sender, EventArgs e)
        {
            EnableTreeView(false);
        }
        #endregion
        
        #region Template

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (lbTemplate.SelectedItem != null)
            {
                if (txtTemplate_Name.Text != "")
                {
                    if (txtTemplate_Namespace.Text != "")
                    {

                        TreeViewInitilise(lbTemplate.SelectedItem.ToString());
                        EnableTreeView(true);

                        btnProceed.Enabled = false;
                    }
                    else
                        MessageBox.Show("Please provide namesapce");
                }
                else
                    MessageBox.Show("Please provide name");
            }
            else
                MessageBox.Show("Please select template");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Reset?", "Clear all controls", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if(lbTemplate.SelectedItems.Count>0)
                    lbTemplate.SelectedItems.Clear();
                if (lbControlTypes.SelectedItems.Count > 0)
                    lbControlTypes.SelectedItems.Clear();

                ShowPropertyGrid(false);

                EnableTreeView(false);

                tvDocument.Nodes.Clear();
                tvModels.Nodes.Clear();
                wbTemplate.DocumentText = "";
                btnProceed.Enabled = true;
            }
        }


        private void TreeViewInitilise(string template)
        {
            var lsSecs= Master.GetSections(template);
            if (lsSecs.Count > 0)
            {
                var lsTN = new List<TreeNode>();
                foreach (var sec in lsSecs)
                {
                    lsTN.Add(new TreeNode(sec) { Tag = new ControlPropertyModel { ControlType = "SECTION" } });
                }

                tvDocument.Nodes.Add(new TreeNode("Document", lsTN.ToArray()) { Tag = new ControlPropertyModel { ControlType = "DOCUMENT" } });
                tvDocument.ExpandAll();
            }
        }

        private void EnableTreeView(bool blEnable = false)
        {
            btnRemoveNode.Visible = blEnable;
            btnCopyNode.Visible = blEnable;
            btnNodePaste.Visible = blEnable;
            tvDocument.Enabled = blEnable;
        }

        #endregion

        #region Control

        private void btnAddControlProperty_Click(object sender, EventArgs e)
        {
            var lsDirectAddControls = Master.DirectAddingControls();
            if (lbControlTypes.SelectedItem != null)
            {
                if (!lsDirectAddControls.Contains(lbControlTypes.SelectedItem.ToString()))
                {
                    //Open Property grid for inputs

                    TableGridClass objTab = new TableGridClass();
                    objTab.Name = "";

                    pgControlProps.SelectedObject = objTab;

                    ShowPropertyGrid(true);

                }
                else
                {
                    //directly add to treeview
                }
            }
            else
                MessageBox.Show("Please select control");
        }
     
        private void AddTreeNodeToDocument(TreeNode tNode)
        {
            if (tvDocument.SelectedNode != null)
            {
                tvDocument.SelectedNode.Nodes.Add(tNode);

                ShowPropertyGrid(false);
            }
            else
                MessageBox.Show("Please select parent node");
        }
        #endregion

        #region Property Grid
       
        private void btnPropertyAdd_Click(object sender, EventArgs e)
        {
            var objControl=pgControlProps.SelectedObject;
            if (lbControlTypes.SelectedItem.ToString() == "Table")
            {
                //insitilise row with cells
                var objTab = objControl as TableGridClass;
                if (objTab.NoOfColumn >0)
                {
                    var lsTds = new List<TreeNode>();
                    for (int i = 0; i < objTab.NoOfColumn; i++)
                    {
                        lsTds.Add(new TreeNode("td") { Tag = new ControlPropertyModel { ControlType = "td" } });
                    }

                    AddTreeNodeToDocument(PrepareTreeNode("Table", objTab, new List<TreeNode> { PrepareTreeNode("Row", new ControlPropertyModel { ControlType = "tr" }, lsTds) }));
                }
            }
            else
            {
                AddTreeNodeToDocument(PrepareTreeNode(lbControlTypes.SelectedItem.ToString(), new ControlPropertyModel { ControlType = "td",Properties= objControl }  ));
            }

           // AddTreeNodeToDocument(lbControlTypes.SelectedItem.ToString(), objControl);

        }

        private TreeNode PrepareTreeNode(string ControlType, object TagData, List<TreeNode> lsChilds = null)
        {
            var tNode = new TreeNode(ControlType);

            if (lsChilds != null)
                tNode = new TreeNode(ControlType, lsChilds.ToArray());

            tNode.Tag = TagData;
            
            return tNode;
        }

        private void btnPropertyCancel_Click(object sender, EventArgs e)
        {
            pgControlProps.SelectedObject = null;

            ShowPropertyGrid(false);
        }

        private void ShowPropertyGrid(bool blShow = false)
        {
            btnPropertyAdd.Visible = blShow;
            btnPropertyCancel.Visible = blShow;
            pgControlProps.Visible = blShow;
        }
        #endregion

        #region View and Final Process

        private void btnHtmlPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnSamplePdf_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerateFile_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region Treeview Node operations
        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            var lsSec = Master.GetSections(lbTemplate.SelectedItem.ToString());
            if (MessageBox.Show("Are you sure you want to delete?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (tvDocument.SelectedNode != null)
                {
                    if (!lsSec.Contains(tvDocument.SelectedNode.Text))
                    {
                        tvDocument.SelectedNode.Remove();
                    }
                    else
                        MessageBox.Show("Sorry, sections will not delete");
                }
                else
                    MessageBox.Show("Please select parent node");
            }
        }

        private void btnCopyNode_Click(object sender, EventArgs e)
        {
            var lsSec = Master.GetSections(lbTemplate.SelectedItem.ToString());
            
                if (tvDocument.SelectedNode != null)
                {
                    if (!lsSec.Contains(tvDocument.SelectedNode.Text))
                    {
                        _tempNode = (TreeNode)tvDocument.SelectedNode.Clone();
                    }
                    else
                        MessageBox.Show("Sorry, sections will not copy");
                }
                else
                    MessageBox.Show("Please select parent node");
           

         
        }

        private void btnNodePaste_Click(object sender, EventArgs e)
        {
            if (tvDocument.SelectedNode != null)
            {
                if (_tempNode != null)
                {
                    tvDocument.SelectedNode.Nodes.Add(_tempNode);
                    _tempNode = null;
                }
                else
                    MessageBox.Show("Please copy node first");
            }
            else
                MessageBox.Show("Please select parent node");
        }

        #endregion
      
    }
}
