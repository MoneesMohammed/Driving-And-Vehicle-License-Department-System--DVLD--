namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    partial class frmReleaseDetainedLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReleaseDetainedLicense));
            this.ucFilterDriverLicenseInfo1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucFilterDriverLicenseInfo();
            this.llblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.llblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.gbDetainInfo = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.lblFineFees = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbDetainInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFilterDriverLicenseInfo1
            // 
            this.ucFilterDriverLicenseInfo1.Location = new System.Drawing.Point(0, 74);
            this.ucFilterDriverLicenseInfo1.Name = "ucFilterDriverLicenseInfo1";
            this.ucFilterDriverLicenseInfo1.Size = new System.Drawing.Size(1081, 593);
            this.ucFilterDriverLicenseInfo1.TabIndex = 0;
            // 
            // llblShowLicenseInfo
            // 
            this.llblShowLicenseInfo.AutoSize = true;
            this.llblShowLicenseInfo.Enabled = false;
            this.llblShowLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblShowLicenseInfo.Location = new System.Drawing.Point(270, 975);
            this.llblShowLicenseInfo.Name = "llblShowLicenseInfo";
            this.llblShowLicenseInfo.Size = new System.Drawing.Size(164, 24);
            this.llblShowLicenseInfo.TabIndex = 53;
            this.llblShowLicenseInfo.TabStop = true;
            this.llblShowLicenseInfo.Text = "Show License Info";
            this.llblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowLicenseInfo_LinkClicked);
            // 
            // llblShowLicenseHistory
            // 
            this.llblShowLicenseHistory.AutoSize = true;
            this.llblShowLicenseHistory.Enabled = false;
            this.llblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblShowLicenseHistory.Location = new System.Drawing.Point(15, 975);
            this.llblShowLicenseHistory.Name = "llblShowLicenseHistory";
            this.llblShowLicenseHistory.Size = new System.Drawing.Size(191, 24);
            this.llblShowLicenseHistory.TabIndex = 52;
            this.llblShowLicenseHistory.TabStop = true;
            this.llblShowLicenseHistory.Text = "Show License History";
            this.llblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowLicenseHistory_LinkClicked);
            // 
            // gbDetainInfo
            // 
            this.gbDetainInfo.Controls.Add(this.lblApplicationID);
            this.gbDetainInfo.Controls.Add(this.label13);
            this.gbDetainInfo.Controls.Add(this.lblFineFees);
            this.gbDetainInfo.Controls.Add(this.label9);
            this.gbDetainInfo.Controls.Add(this.lblTotalFees);
            this.gbDetainInfo.Controls.Add(this.label7);
            this.gbDetainInfo.Controls.Add(this.lblApplicationFees);
            this.gbDetainInfo.Controls.Add(this.lblCreatedBy);
            this.gbDetainInfo.Controls.Add(this.lblLicenseID);
            this.gbDetainInfo.Controls.Add(this.label10);
            this.gbDetainInfo.Controls.Add(this.label12);
            this.gbDetainInfo.Controls.Add(this.lblDetainDate);
            this.gbDetainInfo.Controls.Add(this.lblDetainID);
            this.gbDetainInfo.Controls.Add(this.label5);
            this.gbDetainInfo.Controls.Add(this.label3);
            this.gbDetainInfo.Controls.Add(this.label2);
            this.gbDetainInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetainInfo.Location = new System.Drawing.Point(12, 660);
            this.gbDetainInfo.Name = "gbDetainInfo";
            this.gbDetainInfo.Size = new System.Drawing.Size(1069, 271);
            this.gbDetainInfo.TabIndex = 54;
            this.gbDetainInfo.TabStop = false;
            this.gbDetainInfo.Text = "Detain Info";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(798, 90);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(72, 25);
            this.lblCreatedBy.TabIndex = 27;
            this.lblCreatedBy.Text = "[????]";
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseID.Location = new System.Drawing.Point(798, 41);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(72, 25);
            this.lblLicenseID.TabIndex = 26;
            this.lblLicenseID.Text = "[????]";
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainDate.Location = new System.Drawing.Point(265, 90);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(144, 25);
            this.lblDetainDate.TabIndex = 20;
            this.lblDetainDate.Text = "[??/???/????]";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainID.Location = new System.Drawing.Point(265, 41);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(72, 25);
            this.lblDetainID.TabIndex = 19;
            this.lblDetainID.Text = "[????]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(314, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 42);
            this.label1.TabIndex = 55;
            this.label1.Text = "Release Detained License";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(265, 139);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(72, 25);
            this.lblApplicationFees.TabIndex = 28;
            this.lblApplicationFees.Text = "[$$$$]";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFees.Location = new System.Drawing.Point(265, 197);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(72, 25);
            this.lblTotalFees.TabIndex = 30;
            this.lblTotalFees.Text = "[$$$$]";
            // 
            // lblFineFees
            // 
            this.lblFineFees.AutoSize = true;
            this.lblFineFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFineFees.Location = new System.Drawing.Point(798, 139);
            this.lblFineFees.Name = "lblFineFees";
            this.lblFineFees.Size = new System.Drawing.Size(72, 25);
            this.lblFineFees.TabIndex = 32;
            this.lblFineFees.Text = "[$$$$]";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationID.Location = new System.Drawing.Point(798, 188);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(72, 25);
            this.lblApplicationID.TabIndex = 34;
            this.lblApplicationID.Text = "[????]";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(494, 188);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(288, 25);
            this.label13.TabIndex = 33;
            this.label13.Text = "Application ID:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(498, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(284, 25);
            this.label9.TabIndex = 31;
            this.label9.Text = "Fine Fees:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(29, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(214, 25);
            this.label7.TabIndex = 29;
            this.label7.Text = "Total Fees:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(498, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 25);
            this.label10.TabIndex = 24;
            this.label10.Text = "Created By:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(498, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(284, 25);
            this.label12.TabIndex = 23;
            this.label12.Text = "License ID:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(29, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Application Fees:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(29, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Detain Date:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(29, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Detain ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRelease
            // 
            this.btnRelease.Enabled = false;
            this.btnRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.Release_Detained_License_48px;
            this.btnRelease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelease.Location = new System.Drawing.Point(934, 949);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(147, 50);
            this.btnRelease.TabIndex = 51;
            this.btnRelease.Text = "Release";
            this.btnRelease.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(768, 949);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 50);
            this.btnClose.TabIndex = 50;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmReleaseDetainedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 1011);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbDetainInfo);
            this.Controls.Add(this.llblShowLicenseInfo);
            this.Controls.Add(this.llblShowLicenseHistory);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucFilterDriverLicenseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReleaseDetainedLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release Detained License";
            this.Load += new System.EventHandler(this.frmReleaseDetainedLicense_Load);
            this.gbDetainInfo.ResumeLayout(false);
            this.gbDetainInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ucFilterDriverLicenseInfo ucFilterDriverLicenseInfo1;
        private System.Windows.Forms.LinkLabel llblShowLicenseInfo;
        private System.Windows.Forms.LinkLabel llblShowLicenseHistory;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbDetainInfo;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblFineFees;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label label13;
    }
}