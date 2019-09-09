namespace PdfTemplator
{
    partial class Form2
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
            this.pgTest = new System.Windows.Forms.PropertyGrid();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnGetback = new System.Windows.Forms.Button();
            this.cbControl = new System.Windows.Forms.ComboBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgTest
            // 
            this.pgTest.Location = new System.Drawing.Point(106, 128);
            this.pgTest.Name = "pgTest";
            this.pgTest.Size = new System.Drawing.Size(222, 266);
            this.pgTest.TabIndex = 4;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(106, 69);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 5;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnGetback
            // 
            this.btnGetback.Location = new System.Drawing.Point(221, 69);
            this.btnGetback.Name = "btnGetback";
            this.btnGetback.Size = new System.Drawing.Size(75, 23);
            this.btnGetback.TabIndex = 6;
            this.btnGetback.Text = "GetBack";
            this.btnGetback.UseVisualStyleBackColor = true;
            this.btnGetback.Click += new System.EventHandler(this.btnGetback_Click);
            // 
            // cbControl
            // 
            this.cbControl.FormattingEnabled = true;
            this.cbControl.Items.AddRange(new object[] {
            "Label",
            "Field",
            "Table",
            "ImageUrl",
            "ImageByte"});
            this.cbControl.Location = new System.Drawing.Point(106, 22);
            this.cbControl.Name = "cbControl";
            this.cbControl.Size = new System.Drawing.Size(121, 21);
            this.cbControl.TabIndex = 7;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(233, 20);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(51, 23);
            this.btnSet.TabIndex = 8;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 507);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.cbControl);
            this.Controls.Add(this.btnGetback);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.pgTest);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PropertyGrid pgTest;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnGetback;
        private System.Windows.Forms.ComboBox cbControl;
        private System.Windows.Forms.Button btnSet;
    }
}