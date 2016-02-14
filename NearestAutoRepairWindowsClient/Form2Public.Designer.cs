namespace NearestAutoRepairWindowsClient
{
    partial class Form2Public
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
            this.btnGetPublic = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetPublic
            // 
            this.btnGetPublic.Location = new System.Drawing.Point(93, 59);
            this.btnGetPublic.Name = "btnGetPublic";
            this.btnGetPublic.Size = new System.Drawing.Size(75, 23);
            this.btnGetPublic.TabIndex = 0;
            this.btnGetPublic.Text = "Get Public";
            this.btnGetPublic.UseVisualStyleBackColor = true;
            this.btnGetPublic.Click += new System.EventHandler(this.btnGetPublic_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(116, 106);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(35, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "label1";
            // 
            // Form2Public
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnGetPublic);
            this.Name = "Form2Public";
            this.Text = "Form2Public";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetPublic;
        private System.Windows.Forms.Label lbl1;
    }
}