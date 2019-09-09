namespace PdfTemplator
{
    partial class frmPdf
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
            this.lbControls = new System.Windows.Forms.ListBox();
            this.tvTreeView = new System.Windows.Forms.TreeView();
            this.btnProceed = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnReset = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbControls
            // 
            this.lbControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbControls.FormattingEnabled = true;
            this.lbControls.ItemHeight = 24;
            this.lbControls.Items.AddRange(new object[] {
            "Table",
            "Label",
            "Field",
            "Image"});
            this.lbControls.Location = new System.Drawing.Point(385, 79);
            this.lbControls.Name = "lbControls";
            this.lbControls.Size = new System.Drawing.Size(230, 148);
            this.lbControls.TabIndex = 0;
            // 
            // tvTreeView
            // 
            this.tvTreeView.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvTreeView.Location = new System.Drawing.Point(68, 255);
            this.tvTreeView.Name = "tvTreeView";
            this.tvTreeView.Size = new System.Drawing.Size(606, 371);
            this.tvTreeView.TabIndex = 1;
            // 
            // btnProceed
            // 
            this.btnProceed.Location = new System.Drawing.Point(48, 204);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Size = new System.Drawing.Size(86, 23);
            this.btnProceed.TabIndex = 2;
            this.btnProceed.Text = "Proceed";
            this.btnProceed.UseVisualStyleBackColor = true;
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(643, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 34);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(1100, 79);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(252, 528);
            this.propertyGrid1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(643, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(853, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(230, 271);
            this.dataGridView1.TabIndex = 8;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(198, 204);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Items.AddRange(new object[] {
            "Table",
            "Label",
            "Field",
            "Image"});
            this.listBox1.Location = new System.Drawing.Point(48, 68);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 124);
            this.listBox1.TabIndex = 11;
            // 
            // frmPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnProceed);
            this.Controls.Add(this.tvTreeView);
            this.Controls.Add(this.lbControls);
            this.Name = "frmPdf";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmPdf_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbControls;
        private System.Windows.Forms.TreeView tvTreeView;
        private System.Windows.Forms.Button btnProceed;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ListBox listBox1;
    }
}

