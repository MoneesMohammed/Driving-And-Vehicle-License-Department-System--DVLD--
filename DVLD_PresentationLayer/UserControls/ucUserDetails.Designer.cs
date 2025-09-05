namespace Driving___Vehicle_License_Department__DVLD_
{
    partial class ucUserDetails
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
            this.gbloginInfo = new System.Windows.Forms.GroupBox();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucPersonDetails1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucPersonDetails();
            this.gbloginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbloginInfo
            // 
            this.gbloginInfo.Controls.Add(this.lblIsActive);
            this.gbloginInfo.Controls.Add(this.label5);
            this.gbloginInfo.Controls.Add(this.lblUserName);
            this.gbloginInfo.Controls.Add(this.label3);
            this.gbloginInfo.Controls.Add(this.lblUserID);
            this.gbloginInfo.Controls.Add(this.label2);
            this.gbloginInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbloginInfo.Location = new System.Drawing.Point(9, 431);
            this.gbloginInfo.Name = "gbloginInfo";
            this.gbloginInfo.Size = new System.Drawing.Size(1176, 100);
            this.gbloginInfo.TabIndex = 3;
            this.gbloginInfo.TabStop = false;
            this.gbloginInfo.Text = "login Information";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Location = new System.Drawing.Point(862, 49);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(40, 24);
            this.lblIsActive.TabIndex = 5;
            this.lblIsActive.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(750, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Is Active :";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(563, 49);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(40, 24);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "UserName :";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(267, 49);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(40, 24);
            this.lblUserID.TabIndex = 1;
            this.lblUserID.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "User ID :";
            // 
            // ucPersonDetails1
            // 
            this.ucPersonDetails1.Location = new System.Drawing.Point(7, 3);
            this.ucPersonDetails1.Name = "ucPersonDetails1";
            this.ucPersonDetails1.Size = new System.Drawing.Size(1178, 412);
            this.ucPersonDetails1.TabIndex = 2;
            // 
            // ucUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbloginInfo);
            this.Controls.Add(this.ucPersonDetails1);
            this.Name = "ucUserDetails";
            this.Size = new System.Drawing.Size(1192, 556);
            this.Load += new System.EventHandler(this.ucUserDetails_Load);
            this.gbloginInfo.ResumeLayout(false);
            this.gbloginInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private UserControls.ucPersonDetails ucPersonDetails1;
        private System.Windows.Forms.GroupBox gbloginInfo;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label2;
    }
}
