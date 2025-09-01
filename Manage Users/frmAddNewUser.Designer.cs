namespace Driving___Vehicle_License_Department__DVLD_
{
    partial class frmAddNewUser
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
            this.components = new System.ComponentModel.Container();
            this.lblMode = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPersonalInfo = new System.Windows.Forms.TabPage();
            this.ucFilterPerson1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucFilterPerson();
            this.btnNext = new System.Windows.Forms.Button();
            this.tpLoginInfo = new System.Windows.Forms.TabPage();
            this.btnShowHidePassword = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMode.Location = new System.Drawing.Point(403, 34);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(259, 42);
            this.lblMode.TabIndex = 2;
            this.lblMode.Text = "Add New User";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpPersonalInfo);
            this.tabControl1.Controls.Add(this.tpLoginInfo);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(12, 122);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1220, 680);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.Controls.Add(this.ucFilterPerson1);
            this.tpPersonalInfo.Controls.Add(this.btnNext);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 29);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(1212, 647);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            this.tpPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // ucFilterPerson1
            // 
            this.ucFilterPerson1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucFilterPerson1.Location = new System.Drawing.Point(7, 8);
            this.ucFilterPerson1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucFilterPerson1.Name = "ucFilterPerson1";
            this.ucFilterPerson1.Size = new System.Drawing.Size(1204, 562);
            this.ucFilterPerson1.TabIndex = 21;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.next;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1066, 578);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(94, 43);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tpLoginInfo
            // 
            this.tpLoginInfo.Controls.Add(this.btnShowHidePassword);
            this.tpLoginInfo.Controls.Add(this.btnBack);
            this.tpLoginInfo.Controls.Add(this.cbIsActive);
            this.tpLoginInfo.Controls.Add(this.lblUserID);
            this.tpLoginInfo.Controls.Add(this.txtConfirmPassword);
            this.tpLoginInfo.Controls.Add(this.txtPassword);
            this.tpLoginInfo.Controls.Add(this.txtUserName);
            this.tpLoginInfo.Controls.Add(this.label7);
            this.tpLoginInfo.Controls.Add(this.label5);
            this.tpLoginInfo.Controls.Add(this.label4);
            this.tpLoginInfo.Controls.Add(this.label2);
            this.tpLoginInfo.Location = new System.Drawing.Point(4, 29);
            this.tpLoginInfo.Name = "tpLoginInfo";
            this.tpLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoginInfo.Size = new System.Drawing.Size(1212, 647);
            this.tpLoginInfo.TabIndex = 1;
            this.tpLoginInfo.Text = "LoginInfo";
            this.tpLoginInfo.UseVisualStyleBackColor = true;
            // 
            // btnShowHidePassword
            // 
            this.btnShowHidePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHidePassword.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.close_eye;
            this.btnShowHidePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowHidePassword.Location = new System.Drawing.Point(546, 196);
            this.btnShowHidePassword.Name = "btnShowHidePassword";
            this.btnShowHidePassword.Size = new System.Drawing.Size(40, 37);
            this.btnShowHidePassword.TabIndex = 20;
            this.btnShowHidePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowHidePassword.UseVisualStyleBackColor = true;
            this.btnShowHidePassword.Click += new System.EventHandler(this.btnShowHidePassword_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.arrow;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(26, 582);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(94, 43);
            this.btnBack.TabIndex = 19;
            this.btnBack.Text = "Back";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Location = new System.Drawing.Point(309, 293);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(88, 24);
            this.cbIsActive.TabIndex = 3;
            this.cbIsActive.Text = "Is Active";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(305, 73);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(40, 24);
            this.lblUserID.TabIndex = 6;
            this.lblUserID.Text = "???";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(309, 222);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(196, 26);
            this.txtConfirmPassword.TabIndex = 2;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.AllTextBoxes_TextChanged);
            this.txtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.AllTextBoxes_Validating);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(309, 176);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(196, 26);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.AllTextBoxes_TextChanged);
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.AllTextBoxes_Validating);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(309, 117);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(196, 26);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextChanged += new System.EventHandler(this.AllTextBoxes_TextChanged);
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.AllTextBoxes_Validating);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.profile1;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(58, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(217, 32);
            this.label7.TabIndex = 7;
            this.label7.Text = "User ID";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.reset_password;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(58, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(217, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Confirm Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.reset_password;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(58, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 35);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.business_card;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(58, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "UserName";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.close_1;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(958, 821);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 43);
            this.btnClose.TabIndex = 17;
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
            this.btnSave.Location = new System.Drawing.Point(1111, 821);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 43);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 884);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmAddNewUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New User";
            this.Load += new System.EventHandler(this.frmAddNewUser_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpLoginInfo.ResumeLayout(false);
            this.tpLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLoginInfo;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnShowHidePassword;
        private UserControls.ucFilterPerson ucFilterPerson1;
    }
}