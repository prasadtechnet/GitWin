namespace PdfTemplator
{
    partial class frmPdfTemplateDesinger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wbTemplate = new System.Windows.Forms.WebBrowser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvDocument = new System.Windows.Forms.TreeView();
            this.lbTemplate = new System.Windows.Forms.ListBox();
            this.lbControlTypes = new System.Windows.Forms.ListBox();
            this.btnProceed = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAddControlProperty = new System.Windows.Forms.Button();
            this.txtTemplate_Name = new System.Windows.Forms.TextBox();
            this.txtTemplate_Namespace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tvModels = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.pgControlProps = new System.Windows.Forms.PropertyGrid();
            this.btnPropertyCancel = new System.Windows.Forms.Button();
            this.btnPropertyAdd = new System.Windows.Forms.Button();
            this.btnHtmlPreview = new System.Windows.Forms.Button();
            this.btnSamplePdf = new System.Windows.Forms.Button();
            this.btnGenerateFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.btnRemoveNode = new System.Windows.Forms.Button();
            this.btnCopyNode = new System.Windows.Forms.Button();
            this.pnlTreeViewContainer = new System.Windows.Forms.Panel();
            this.btnNodePaste = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlTreeViewContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvDocument);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1445, 779);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnPropertyCancel);
            this.splitContainer2.Panel1.Controls.Add(this.btnPropertyAdd);
            this.splitContainer2.Panel1.Controls.Add(this.pgControlProps);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.tvModels);
            this.splitContainer2.Panel1.Controls.Add(this.btnAddControlProperty);
            this.splitContainer2.Panel1.Controls.Add(this.lbControlTypes);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.wbTemplate);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(985, 779);
            this.splitContainer2.SplitterDistance = 424;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.btnGenerateFile);
            this.panel1.Controls.Add(this.btnSamplePdf);
            this.panel1.Controls.Add(this.btnHtmlPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 81);
            this.panel1.TabIndex = 0;
            // 
            // wbTemplate
            // 
            this.wbTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbTemplate.Location = new System.Drawing.Point(0, 81);
            this.wbTemplate.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbTemplate.Name = "wbTemplate";
            this.wbTemplate.Size = new System.Drawing.Size(557, 698);
            this.wbTemplate.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.pnlTreeViewContainer);
            this.panel2.Controls.Add(this.btnBrowsePath);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTemplate_Namespace);
            this.panel2.Controls.Add(this.txtTemplate_Name);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnProceed);
            this.panel2.Controls.Add(this.lbTemplate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(456, 204);
            this.panel2.TabIndex = 0;
            // 
            // tvDocument
            // 
            this.tvDocument.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDocument.Location = new System.Drawing.Point(0, 204);
            this.tvDocument.Name = "tvDocument";
            this.tvDocument.Size = new System.Drawing.Size(456, 575);
            this.tvDocument.TabIndex = 1;
            // 
            // lbTemplate
            // 
            this.lbTemplate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTemplate.FormattingEnabled = true;
            this.lbTemplate.ItemHeight = 16;
            this.lbTemplate.Items.AddRange(new object[] {
            "Custom",
            "HCF",
            "HC",
            "CF"});
            this.lbTemplate.Location = new System.Drawing.Point(3, 8);
            this.lbTemplate.Name = "lbTemplate";
            this.lbTemplate.Size = new System.Drawing.Size(187, 148);
            this.lbTemplate.TabIndex = 0;
            // 
            // lbControlTypes
            // 
            this.lbControlTypes.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbControlTypes.FormattingEnabled = true;
            this.lbControlTypes.ItemHeight = 16;
            this.lbControlTypes.Items.AddRange(new object[] {
            "Table",
            "Row",
            "Empty",
            "Label",
            "Field",
            "ImageUrl",
            "ImageByte",
            "ImageSubUrl",
            "ImageSubByte",
            "Cell"});
            this.lbControlTypes.Location = new System.Drawing.Point(17, 8);
            this.lbControlTypes.Name = "lbControlTypes";
            this.lbControlTypes.Size = new System.Drawing.Size(180, 196);
            this.lbControlTypes.TabIndex = 1;
            // 
            // btnProceed
            // 
            this.btnProceed.Location = new System.Drawing.Point(298, 128);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Size = new System.Drawing.Size(68, 32);
            this.btnProceed.TabIndex = 1;
            this.btnProceed.Text = "Proceed";
            this.btnProceed.UseVisualStyleBackColor = true;
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(372, 128);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(68, 32);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAddControlProperty
            // 
            this.btnAddControlProperty.Location = new System.Drawing.Point(17, 207);
            this.btnAddControlProperty.Name = "btnAddControlProperty";
            this.btnAddControlProperty.Size = new System.Drawing.Size(180, 32);
            this.btnAddControlProperty.TabIndex = 2;
            this.btnAddControlProperty.Text = "Properties";
            this.btnAddControlProperty.UseVisualStyleBackColor = true;
            this.btnAddControlProperty.Click += new System.EventHandler(this.btnAddControlProperty_Click);
            // 
            // txtTemplate_Name
            // 
            this.txtTemplate_Name.Location = new System.Drawing.Point(218, 29);
            this.txtTemplate_Name.Name = "txtTemplate_Name";
            this.txtTemplate_Name.Size = new System.Drawing.Size(222, 20);
            this.txtTemplate_Name.TabIndex = 3;
            this.txtTemplate_Name.Text = "PdfTemplate";
            // 
            // txtTemplate_Namespace
            // 
            this.txtTemplate_Namespace.Location = new System.Drawing.Point(218, 70);
            this.txtTemplate_Namespace.Name = "txtTemplate_Namespace";
            this.txtTemplate_Namespace.Size = new System.Drawing.Size(222, 20);
            this.txtTemplate_Namespace.TabIndex = 4;
            this.txtTemplate_Namespace.Text = "Test.Pdf";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Namespace";
            // 
            // tvModels
            // 
            this.tvModels.Location = new System.Drawing.Point(227, 29);
            this.tvModels.Name = "tvModels";
            this.tvModels.Size = new System.Drawing.Size(182, 210);
            this.tvModels.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Models";
            // 
            // pgControlProps
            // 
            this.pgControlProps.Location = new System.Drawing.Point(29, 290);
            this.pgControlProps.Name = "pgControlProps";
            this.pgControlProps.Size = new System.Drawing.Size(362, 477);
            this.pgControlProps.TabIndex = 8;
            // 
            // btnPropertyCancel
            // 
            this.btnPropertyCancel.Location = new System.Drawing.Point(323, 269);
            this.btnPropertyCancel.Name = "btnPropertyCancel";
            this.btnPropertyCancel.Size = new System.Drawing.Size(68, 32);
            this.btnPropertyCancel.TabIndex = 8;
            this.btnPropertyCancel.Text = "Cancel";
            this.btnPropertyCancel.UseVisualStyleBackColor = true;
            this.btnPropertyCancel.Click += new System.EventHandler(this.btnPropertyCancel_Click);
            // 
            // btnPropertyAdd
            // 
            this.btnPropertyAdd.Location = new System.Drawing.Point(249, 269);
            this.btnPropertyAdd.Name = "btnPropertyAdd";
            this.btnPropertyAdd.Size = new System.Drawing.Size(68, 32);
            this.btnPropertyAdd.TabIndex = 7;
            this.btnPropertyAdd.Text = "Add";
            this.btnPropertyAdd.UseVisualStyleBackColor = true;
            this.btnPropertyAdd.Click += new System.EventHandler(this.btnPropertyAdd_Click);
            // 
            // btnHtmlPreview
            // 
            this.btnHtmlPreview.Location = new System.Drawing.Point(7, 29);
            this.btnHtmlPreview.Name = "btnHtmlPreview";
            this.btnHtmlPreview.Size = new System.Drawing.Size(68, 32);
            this.btnHtmlPreview.TabIndex = 8;
            this.btnHtmlPreview.Text = "Preview";
            this.btnHtmlPreview.UseVisualStyleBackColor = true;
            this.btnHtmlPreview.Click += new System.EventHandler(this.btnHtmlPreview_Click);
            // 
            // btnSamplePdf
            // 
            this.btnSamplePdf.Location = new System.Drawing.Point(98, 29);
            this.btnSamplePdf.Name = "btnSamplePdf";
            this.btnSamplePdf.Size = new System.Drawing.Size(79, 32);
            this.btnSamplePdf.TabIndex = 9;
            this.btnSamplePdf.Text = "Pdf Preview";
            this.btnSamplePdf.UseVisualStyleBackColor = true;
            this.btnSamplePdf.Click += new System.EventHandler(this.btnSamplePdf_Click);
            // 
            // btnGenerateFile
            // 
            this.btnGenerateFile.Location = new System.Drawing.Point(207, 29);
            this.btnGenerateFile.Name = "btnGenerateFile";
            this.btnGenerateFile.Size = new System.Drawing.Size(79, 32);
            this.btnGenerateFile.TabIndex = 10;
            this.btnGenerateFile.Text = "Generate File";
            this.btnGenerateFile.UseVisualStyleBackColor = true;
            this.btnGenerateFile.Click += new System.EventHandler(this.btnGenerateFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Location";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(218, 108);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "D:\\";
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(412, 105);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(27, 23);
            this.btnBrowsePath.TabIndex = 9;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            // 
            // btnRemoveNode
            // 
            this.btnRemoveNode.Location = new System.Drawing.Point(194, 2);
            this.btnRemoveNode.Name = "btnRemoveNode";
            this.btnRemoveNode.Size = new System.Drawing.Size(84, 32);
            this.btnRemoveNode.TabIndex = 9;
            this.btnRemoveNode.Text = "Remove";
            this.btnRemoveNode.UseVisualStyleBackColor = true;
            this.btnRemoveNode.Click += new System.EventHandler(this.btnRemoveNode_Click);
            // 
            // btnCopyNode
            // 
            this.btnCopyNode.Location = new System.Drawing.Point(282, 2);
            this.btnCopyNode.Name = "btnCopyNode";
            this.btnCopyNode.Size = new System.Drawing.Size(84, 32);
            this.btnCopyNode.TabIndex = 10;
            this.btnCopyNode.Text = "Copy";
            this.btnCopyNode.UseVisualStyleBackColor = true;
            this.btnCopyNode.Click += new System.EventHandler(this.btnCopyNode_Click);
            // 
            // pnlTreeViewContainer
            // 
            this.pnlTreeViewContainer.BackColor = System.Drawing.Color.White;
            this.pnlTreeViewContainer.Controls.Add(this.btnNodePaste);
            this.pnlTreeViewContainer.Controls.Add(this.btnCopyNode);
            this.pnlTreeViewContainer.Controls.Add(this.btnRemoveNode);
            this.pnlTreeViewContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTreeViewContainer.Location = new System.Drawing.Point(0, 168);
            this.pnlTreeViewContainer.Name = "pnlTreeViewContainer";
            this.pnlTreeViewContainer.Size = new System.Drawing.Size(456, 36);
            this.pnlTreeViewContainer.TabIndex = 10;
            // 
            // btnNodePaste
            // 
            this.btnNodePaste.Location = new System.Drawing.Point(369, 2);
            this.btnNodePaste.Name = "btnNodePaste";
            this.btnNodePaste.Size = new System.Drawing.Size(84, 32);
            this.btnNodePaste.TabIndex = 11;
            this.btnNodePaste.Text = "Paste";
            this.btnNodePaste.UseVisualStyleBackColor = true;
            this.btnNodePaste.Click += new System.EventHandler(this.btnNodePaste_Click);
            // 
            // frmPdfTemplateDesinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 779);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmPdfTemplateDesinger";
            this.Text = "frmPdfTemplateDesinger";
            this.Load += new System.EventHandler(this.frmPdfTemplateDesinger_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlTreeViewContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvDocument;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser wbTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbTemplate;
        private System.Windows.Forms.ListBox lbControlTypes;
        private System.Windows.Forms.TextBox txtTemplate_Namespace;
        private System.Windows.Forms.TextBox txtTemplate_Name;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProceed;
        private System.Windows.Forms.Button btnAddControlProperty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvModels;
        private System.Windows.Forms.Button btnPropertyCancel;
        private System.Windows.Forms.Button btnPropertyAdd;
        private System.Windows.Forms.PropertyGrid pgControlProps;
        private System.Windows.Forms.Button btnGenerateFile;
        private System.Windows.Forms.Button btnSamplePdf;
        private System.Windows.Forms.Button btnHtmlPreview;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnRemoveNode;
        private System.Windows.Forms.Panel pnlTreeViewContainer;
        private System.Windows.Forms.Button btnNodePaste;
        private System.Windows.Forms.Button btnCopyNode;
    }
}