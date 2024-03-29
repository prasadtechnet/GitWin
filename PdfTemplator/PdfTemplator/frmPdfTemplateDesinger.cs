﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfTemplator.PdfCore.Business;
using PdfTemplator.UI.Business;
using PdfTemplator.UI.Master;
using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;

namespace PdfTemplator
{
    public partial class frmPdfTemplateDesinger : Form
    {
        #region Varaibles
        TreeNode _tempNode = null;
        bool _blIsNewControl = false;
        List<GridModelProperty> lsModels = null;
        TreeView tv2 = new TreeView();
        ContextMenu cmCell = null;

        #endregion

        #region Constructor & Load   
        public frmPdfTemplateDesinger()
        {
            InitializeComponent();
        }

        private void frmPdfTemplateDesinger_Load(object sender, EventArgs e)
        {
            EnableTreeView(false);
            cmCell = new ContextMenu { Name="CellMenu" };
            cmCell.MenuItems.Add(new MenuItem("MergeUp", new EventHandler(MergeCellWithTop)));
            cmCell.MenuItems.Add(new MenuItem("MergeDown", new EventHandler(MergeCellWithDown)));
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
                        if (txtLocation.Text != "")
                        {
                            TreeViewInitilise(lbTemplate.SelectedItem.ToString());
                            EnableTreeView(true);

                            lsModels = new List<GridModelProperty>();
                            dgvModels.Rows.Clear();

                            btnProceed.Enabled = false;
                        }
                        else
                            MessageBox.Show("Please provide location to save");
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
                lsModels = new List<GridModelProperty>();

                if(dgvModels.Rows.Count>0)
                    dgvModels.Rows.Clear();

                tvDocument.Nodes.Clear();
           
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
                    lsTN.Add(new TreeNode(sec) { Tag = new ControlPropertyModel { ControlType = "SECTION",Properties=new SectionModel { Name=sec} } });
                }

                tvDocument.Nodes.Add(new TreeNode("Document",lsTN.ToArray()) { Tag = new ControlPropertyModel { ControlType = "DOCUMENT",Properties=new DocumentGridModel {Template=lbTemplate.SelectedItem.ToString(),Name=txtTemplate_Name.Text,Namespace=txtTemplate_Namespace.Text,Location=txtLocation.Text} } });
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
            _blIsNewControl = true;
            btnPropertyAdd.Text = "Add";
            var lsDirectAddControls = Master.DirectAddingControls();
            if (lbControlTypes.SelectedItem != null)
            {
                if (!lsDirectAddControls.Contains(lbControlTypes.SelectedItem.ToString()))
                {
                    //Open Property grid for inputs
                    
                    pgControlProps.SelectedObject = PropertyGridManager.GetPropertyGridObject(lbControlTypes.SelectedItem.ToString());

                    ShowPropertyGrid(true);

                }
                else
                {
                    //directly add to treeview
                    if (tvDocument.SelectedNode != null)
                    {
                        if (lbControlTypes.SelectedItem.ToString() == "Empty")
                        {
                            AddTreeNodeToDocument(PrepareTreeNode("Empty", new ControlPropertyModel { ControlType = "Row",Properties=new EmptyCellGridClass {Name=""} }));
                        }
                        else if (lbControlTypes.SelectedItem.ToString() == "Row")
                        {
                            if (tvDocument.SelectedNode.Tag != null)
                            {
                                var objTab = ((tvDocument.SelectedNode.Tag as ControlPropertyModel).Properties as TableGridClass);
                                if (objTab.NoOfColumn > 0)
                                {
                                    var lsTds = new List<TreeNode>();
                                    for (int i = 0; i < objTab.NoOfColumn; i++)
                                    {
                                        lsTds.Add(new TreeNode("Cell") { Tag = new ControlPropertyModel { ControlType = "Cell",Properties=new CellGridCalss { Width = objTab.ColumnWidths[i] } },ContextMenu=cmCell });
                                    }

                                    AddTreeNodeToDocument(PrepareTreeNode("Row", new ControlPropertyModel { ControlType = "Row",Properties=new RowGridClass { } },lsTds));
                                }
                                else
                                    MessageBox.Show("Parent table should have atleast one column");

                            }
                            else
                                MessageBox.Show("Parent should be table");
                        }


                    }

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
            if (_blIsNewControl)
            {

                if (lbControlTypes.SelectedItem.ToString() == "Table")
                {
                    //insitilise row with cells
                    var objTab = objControl as TableGridClass;
                    if (objTab.NoOfColumn > 0)
                    {
                       
                       
                        var tRows = new List<TreeNode>();
                        if (objTab.IsDynamic)
                        {
                            var lsRowTds = new List<TreeNode>();
                            var lsHeadTds = new List<TreeNode>();
                            for (int i = 0; i < objTab.NoOfColumn; i++)
                            {
                                lsHeadTds.Add(new TreeNode("Cell",new TreeNode[] { new TreeNode("Label") {Tag=new ControlPropertyModel { ControlType="Label",Properties=PropertyGridManager.GetPropertyGridObject("Label")} } }) { Tag = new ControlPropertyModel { ControlType = "Cell", Properties = new CellGridCalss { Width=objTab.ColumnWidths[i] } } });
                                lsRowTds.Add(new TreeNode("Cell", new TreeNode[] { new TreeNode("Field") { Tag = new ControlPropertyModel { ControlType = "Field", Properties = PropertyGridManager.GetPropertyGridObject("Field") } } }) { Tag = new ControlPropertyModel { ControlType = "Cell", Properties = new CellGridCalss { Width = objTab.ColumnWidths[i] } } });
                            }
                            tRows.Add(PrepareTreeNode("Header", new ControlPropertyModel { ControlType = "Row", Properties = new RowGridClass { } }, lsHeadTds));
                            tRows.Add(PrepareTreeNode("Items", new ControlPropertyModel { ControlType = "Row", Properties = new RowGridClass { } }, lsRowTds));
                        }
                        else
                        {
                            var lsTds = new List<TreeNode>();
                            for (int i = 0; i < objTab.NoOfColumn; i++)
                            {
                                lsTds.Add(new TreeNode("Cell") { Tag = new ControlPropertyModel { ControlType = "Cell", Properties = new CellGridCalss { Width = objTab.ColumnWidths[i] } },ContextMenu=cmCell });
                            }
                            tRows.Add(PrepareTreeNode("Row", new ControlPropertyModel { ControlType = "Row", Properties = new RowGridClass { } }, lsTds));
                        }

                      
                            
                        var tab = PrepareTreeNode("Table", new ControlPropertyModel { ControlType = "Table", Properties = objTab }, tRows);


                        AddTreeNodeToDocument(tab);
                    }
                    else
                        MessageBox.Show("Parent table should have atleast one column");
                }
                else
                {
                  

                    AddTreeNodeToDocument(PrepareTreeNode(lbControlTypes.SelectedItem.ToString(), new ControlPropertyModel { ControlType = lbControlTypes.SelectedItem.ToString(), Properties = objControl }));

                    var modelFieldInfo = PropertyGridManager.GetModelFieldName(new ControlPropertyModel { ControlType = lbControlTypes.SelectedItem.ToString(), Properties = objControl });
                    if (modelFieldInfo != "")
                    {
                        var flds = modelFieldInfo.Split('-');
                        if(lsModels.Where(x=>x.ModelName==flds[0] && x.FieldName == flds[1]).FirstOrDefault() == null)
                        {
                            lsModels.Add(new GridModelProperty {ModelName=flds[0],FieldName=flds[1]});
                            RefreshModelGrid();
                        }
                    }

                }

                // AddTreeNodeToDocument(lbControlTypes.SelectedItem.ToString(), objControl);
            }
            else
            {
                //Update properties
                if (tvDocument.SelectedNode != null)
                {
                    var objControlUpdateObject = (tvDocument.SelectedNode.Tag as ControlPropertyModel);

                    

                    objControlUpdateObject.Properties=pgControlProps.SelectedObject;

                    tvDocument.SelectedNode.Tag = objControlUpdateObject;

                    var modelFieldInfo = PropertyGridManager.GetModelFieldName(new ControlPropertyModel { ControlType = objControlUpdateObject.ControlType, Properties = objControl });
                    if (modelFieldInfo != "")
                    {
                        var flds = modelFieldInfo.Split('-');
                        if (lsModels.Where(x => x.ModelName == flds[0] && x.FieldName == flds[1]).FirstOrDefault() == null)
                        {
                            lsModels.Add(new GridModelProperty { ModelName = flds[0], FieldName = flds[1] });
                            RefreshModelGrid();
                        }
                    }

                    ShowPropertyGrid(false);
                }

            }

            lbControlTypes.ClearSelected();

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

        private void RefreshModelGrid()
        {
            if(dgvModels.Rows.Count>0)
               dgvModels.Rows.Clear();

            dgvModels.DataSource = lsModels;
            dgvModels.Refresh();
        }
        #endregion

        #region View and Final Process

        private void btnHtmlPreview_Click(object sender, EventArgs e)
        {
            HTMLPreviewModule objHtml = new HTMLPreviewModule();
           var strRes=objHtml.GetHTMLTemplate(tvDocument);
            wbTemplate.DocumentText = strRes;
        }

        private void btnSamplePdf_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerateFile_Click(object sender, EventArgs e)
        {
            TemplateInputGenerator objGen = new TemplateInputGenerator();
            var objTemplate= objGen.PrepareMainTemplate(tvDocument,lsModels);

            IBusinessManager objBM = new BusinessManager(new PdfCore.Business.FileModule.FileModule());
           var respModel= objBM.GeneratePdfTemplate(objTemplate, objTemplate.Location);
            if (respModel.Status)
            {
                MessageBox.Show("Created successfully");
            }
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
                   // tv2.Nodes.Add((TreeNode)tvDocument.SelectedNode.Clone());

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
                    //create New Nodes
                    

                    tvDocument.SelectedNode.Nodes.Add(CopyNewNode(_tempNode));
                 

                  tvDocument.SelectedNode = null;

                   _tempNode = null;
                }
                else
                    MessageBox.Show("Please copy node first");
            }
            else
                MessageBox.Show("Please select parent node");
        }

        private void btnNodeProperties_Click(object sender, EventArgs e)
        {
            _blIsNewControl = false;
            btnPropertyAdd.Text = "Update";

            if (tvDocument.SelectedNode != null)
            {
                var lsSec = Master.GetSections(lbTemplate.SelectedItem.ToString());
                if (!lsSec.Contains(tvDocument.SelectedNode.Text))
                {
                   
                    var objControl = (tvDocument.SelectedNode.Tag as ControlPropertyModel);
                    pgControlProps.SelectedObject = objControl.Properties;
                    ShowPropertyGrid(true);
                }
                else
                    MessageBox.Show("Sorry, sections will not have props");
               
            }
            else
                MessageBox.Show("Please select node");
        }


        private TreeNode CopyNewNode(TreeNode tnSrc)
        {
            var tnNew = new TreeNode();
            try
            {
              
                if (tnSrc.Nodes.Count > 0)
                {
                    var lsTNs = new List<TreeNode>();
                    foreach (TreeNode tn in tnSrc.Nodes)
                    {
                        lsTNs.Add(CopyNewNode(tn));
                    }
                    var oldTag = tnSrc.Tag as ControlPropertyModel;
                    var cp = oldTag.ControlType;
                    var objNewCP = new ControlPropertyModel {ControlType= cp, Properties= PropertyGridManager.GetPropertyGridObject(cp) };

                    tnNew = new TreeNode(tnSrc.Text, lsTNs.ToArray()) { Tag = objNewCP, Name=tnSrc.Text };
                }
                else
                {
                    var oldTag = tnSrc.Tag as ControlPropertyModel;
                    var cp = oldTag.ControlType;
                    var objNewCP = new ControlPropertyModel { ControlType = cp, Properties = PropertyGridManager.GetPropertyGridObject(cp) };
                    tnNew = new TreeNode(tnSrc.Text) { Tag = objNewCP, Name = tnSrc.Text };
                }

               
            }
            catch (Exception ex)
            {

            }

            return tnNew;
        }


        #endregion

        #region Context Menu Event handler

        protected void MergeCellWithTop(object sender, EventArgs e)
        {
            var t1 = tvDocument.SelectedNode;

            if (t1.PrevNode != null)
            {
                var t2 = t1.PrevNode;
                var res = MergeNodes(t1);
                if (res != null)
                {                   
                    tvDocument.SelectedNode = res;
                    tvDocument.SelectedNode.PrevNode.Remove();
                }
            } else
                MessageBox.Show("There is no top node");
            


        }
        protected void MergeCellWithDown(object sender, EventArgs e)
        {
            var t1 = tvDocument.SelectedNode;
            if (t1.PrevNode != null)
            {
                var t2 = t1.NextNode;
                var res = MergeNodes(t1);
                if (res != null)
                {
                    tvDocument.SelectedNode = res;
                    tvDocument.SelectedNode.NextNode.Remove();
                }
            }
            else
                MessageBox.Show("There is no below node");

        }


        private TreeNode MergeNodes(TreeNode current)
        {
            TreeNode tn=null;
            try
            {
               var objNew= (current.Tag as ControlPropertyModel).Properties as CellGridCalss;
                objNew.ColSpan += 1;

                current.Tag = new ControlPropertyModel { ControlType = "Cell", Properties = objNew };

                return current;
            }
            catch (Exception ex)
            {

            }

            return tn;
        }
        #endregion
    }
}
