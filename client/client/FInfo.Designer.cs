namespace client
{
    partial class FInfo
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblName.Location = new System.Drawing.Point(118, 41);
            this.lblName.Name = "lblName";
            this.lblName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblName.Size = new System.Drawing.Size(69, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "שם:  מפלגת ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.AllowDrop = true;
            this.lblInfo.AutoEllipsis = true;
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblInfo.Location = new System.Drawing.Point(53, 98);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblInfo.Size = new System.Drawing.Size(238, 229);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.linkLabel.Location = new System.Drawing.Point(136, 69);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.linkLabel.Size = new System.Drawing.Size(70, 13);
            this.linkLabel.TabIndex = 2;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "קישור למצע";
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(345, 369);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblName);
            this.Name = "FInfo";
            this.Text = "FInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.LinkLabel linkLabel;
    }
}