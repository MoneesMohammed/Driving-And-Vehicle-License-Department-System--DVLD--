namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    partial class ucFilterDriverLicenseInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterByID = new System.Windows.Forms.TextBox();
            this.btnSearchLicense = new System.Windows.Forms.Button();
            this.ucDriverLicenseInfo1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucDriverLicenseInfo();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnSearchLicense);
            this.gbFilter.Controls.Add(this.txtFilterByID);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(802, 111);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "LicenseID:";
            // 
            // txtFilterByID
            // 
            this.txtFilterByID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterByID.Location = new System.Drawing.Point(206, 44);
            this.txtFilterByID.Name = "txtFilterByID";
            this.txtFilterByID.Size = new System.Drawing.Size(411, 31);
            this.txtFilterByID.TabIndex = 1;
            this.txtFilterByID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterByID_KeyDown);
            // 
            // btnSearchLicense
            // 
            this.btnSearchLicense.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.preview;
            this.btnSearchLicense.Location = new System.Drawing.Point(698, 25);
            this.btnSearchLicense.Name = "btnSearchLicense";
            this.btnSearchLicense.Size = new System.Drawing.Size(81, 72);
            this.btnSearchLicense.TabIndex = 2;
            this.btnSearchLicense.UseVisualStyleBackColor = true;
            this.btnSearchLicense.Click += new System.EventHandler(this.btnSearchLicense_Click);
            // 
            // ucDriverLicenseInfo1
            // 
            this.ucDriverLicenseInfo1.Location = new System.Drawing.Point(0, 119);
            this.ucDriverLicenseInfo1.Name = "ucDriverLicenseInfo1";
            this.ucDriverLicenseInfo1.Size = new System.Drawing.Size(1089, 471);
            this.ucDriverLicenseInfo1.TabIndex = 0;
            // 
            // ucFilterDriverLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ucDriverLicenseInfo1);
            this.Name = "ucFilterDriverLicenseInfo";
            this.Size = new System.Drawing.Size(1094, 593);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucDriverLicenseInfo ucDriverLicenseInfo1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchLicense;
        private System.Windows.Forms.TextBox txtFilterByID;
    }
}
