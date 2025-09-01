namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.Local_License
{
    partial class frmNewLocalDrivingLicenseApplication
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPersonalInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ucFilterPerson1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucFilterPerson();
            this.tabApplicationInfo = new System.Windows.Forms.TabPage();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblDLApplicationID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPersonalInfo.SuspendLayout();
            this.tabApplicationInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPersonalInfo);
            this.tabControl1.Controls.Add(this.tabApplicationInfo);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 101);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1227, 679);
            this.tabControl1.TabIndex = 20;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPersonalInfo
            // 
            this.tabPersonalInfo.Controls.Add(this.btnNext);
            this.tabPersonalInfo.Controls.Add(this.ucFilterPerson1);
            this.tabPersonalInfo.Location = new System.Drawing.Point(4, 29);
            this.tabPersonalInfo.Name = "tabPersonalInfo";
            this.tabPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPersonalInfo.Size = new System.Drawing.Size(1219, 646);
            this.tabPersonalInfo.TabIndex = 0;
            this.tabPersonalInfo.Text = "Personal Info";
            this.tabPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.next;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1097, 585);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(94, 43);
            this.btnNext.TabIndex = 21;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ucFilterPerson1
            // 
            this.ucFilterPerson1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucFilterPerson1.Location = new System.Drawing.Point(4, 7);
            this.ucFilterPerson1.Margin = new System.Windows.Forms.Padding(4);
            this.ucFilterPerson1.Name = "ucFilterPerson1";
            this.ucFilterPerson1.Size = new System.Drawing.Size(1208, 540);
            this.ucFilterPerson1.TabIndex = 0;
            // 
            // tabApplicationInfo
            // 
            this.tabApplicationInfo.Controls.Add(this.cbLicenseClass);
            this.tabApplicationInfo.Controls.Add(this.lblCreatedBy);
            this.tabApplicationInfo.Controls.Add(this.lblApplicationFees);
            this.tabApplicationInfo.Controls.Add(this.lblApplicationDate);
            this.tabApplicationInfo.Controls.Add(this.lblDLApplicationID);
            this.tabApplicationInfo.Controls.Add(this.label8);
            this.tabApplicationInfo.Controls.Add(this.label9);
            this.tabApplicationInfo.Controls.Add(this.label4);
            this.tabApplicationInfo.Controls.Add(this.label5);
            this.tabApplicationInfo.Controls.Add(this.label6);
            this.tabApplicationInfo.Controls.Add(this.label7);
            this.tabApplicationInfo.Location = new System.Drawing.Point(4, 29);
            this.tabApplicationInfo.Name = "tabApplicationInfo";
            this.tabApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabApplicationInfo.Size = new System.Drawing.Size(1219, 646);
            this.tabApplicationInfo.TabIndex = 1;
            this.tabApplicationInfo.Text = "Application Info";
            this.tabApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(363, 233);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(395, 28);
            this.cbLicenseClass.TabIndex = 10;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(358, 337);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(49, 29);
            this.lblCreatedBy.TabIndex = 9;
            this.lblCreatedBy.Text = "???";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(358, 300);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(39, 29);
            this.lblApplicationFees.TabIndex = 8;
            this.lblApplicationFees.Text = "15";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.Location = new System.Drawing.Point(358, 177);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(131, 29);
            this.lblApplicationDate.TabIndex = 7;
            this.lblApplicationDate.Text = "01/01/1800";
            // 
            // lblDLApplicationID
            // 
            this.lblDLApplicationID.AutoSize = true;
            this.lblDLApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDLApplicationID.Location = new System.Drawing.Point(358, 140);
            this.lblDLApplicationID.Name = "lblDLApplicationID";
            this.lblDLApplicationID.Size = new System.Drawing.Size(63, 29);
            this.lblDLApplicationID.TabIndex = 6;
            this.lblDLApplicationID.Text = "[???]";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.profile1;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(57, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(284, 29);
            this.label8.TabIndex = 5;
            this.label8.Text = "Created By:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.payment;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(57, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(284, 29);
            this.label9.TabIndex = 4;
            this.label9.Text = "Application Fees:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(57, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.D_L;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(57, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "License Class:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.calendar;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(57, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(284, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = "Application Date:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.business_card;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(57, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(284, 29);
            this.label7.TabIndex = 0;
            this.label7.Text = "D.L.Application ID:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Brown;
            this.lblMode.Location = new System.Drawing.Point(287, 24);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(597, 37);
            this.lblMode.TabIndex = 21;
            this.lblMode.Text = "New Local Driving License Application";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.close_1;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(953, 834);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 43);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.diskette;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1106, 834);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 43);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmNewLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 889);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNewLocalDrivingLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Local Driving License Application";
            this.Load += new System.EventHandler(this.frmNewLocalDrivingLicenseApplication_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPersonalInfo.ResumeLayout(false);
            this.tabApplicationInfo.ResumeLayout(false);
            this.tabApplicationInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ucFilterPerson ucFilterPerson1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPersonalInfo;
        private System.Windows.Forms.TabPage tabApplicationInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblDLApplicationID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}