namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    partial class ucFilterPerson
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
            this.gbFind = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnSearchPerson = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ucPersonDetails1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucPersonDetails();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFind
            // 
            this.gbFind.Controls.Add(this.label1);
            this.gbFind.Controls.Add(this.btnAddNewPerson);
            this.gbFind.Controls.Add(this.btnSearchPerson);
            this.gbFind.Controls.Add(this.txtFindBy);
            this.gbFind.Controls.Add(this.cbFindBy);
            this.gbFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFind.Location = new System.Drawing.Point(12, 12);
            this.gbFind.Name = "gbFind";
            this.gbFind.Size = new System.Drawing.Size(1178, 100);
            this.gbFind.TabIndex = 3;
            this.gbFind.TabStop = false;
            this.gbFind.Text = "Filter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewPerson.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.add_user2;
            this.btnAddNewPerson.Location = new System.Drawing.Point(917, 41);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(66, 43);
            this.btnAddNewPerson.TabIndex = 20;
            this.btnAddNewPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnSearchPerson
            // 
            this.btnSearchPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchPerson.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.user_avatar;
            this.btnSearchPerson.Location = new System.Drawing.Point(795, 41);
            this.btnSearchPerson.Name = "btnSearchPerson";
            this.btnSearchPerson.Size = new System.Drawing.Size(66, 43);
            this.btnSearchPerson.TabIndex = 19;
            this.btnSearchPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchPerson.UseVisualStyleBackColor = true;
            this.btnSearchPerson.Click += new System.EventHandler(this.btnSearchPerson_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindBy.Location = new System.Drawing.Point(418, 47);
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(283, 29);
            this.txtFindBy.TabIndex = 19;
            this.txtFindBy.TextChanged += new System.EventHandler(this.txtFindBy_TextChanged);
            // 
            // cbFindBy
            // 
            this.cbFindBy.BackColor = System.Drawing.SystemColors.Window;
            this.cbFindBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Items.AddRange(new object[] {
            "NationalNo",
            "Person ID"});
            this.cbFindBy.Location = new System.Drawing.Point(122, 47);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(278, 28);
            this.cbFindBy.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "Find By :";
            // 
            // ucPersonDetails1
            // 
            this.ucPersonDetails1.Location = new System.Drawing.Point(12, 128);
            this.ucPersonDetails1.Name = "ucPersonDetails1";
            this.ucPersonDetails1.Size = new System.Drawing.Size(1182, 417);
            this.ucPersonDetails1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 21;
            this.label1.Text = "Find By :";
            // 
            // ucFilterPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucPersonDetails1);
            this.Controls.Add(this.gbFind);
            this.Name = "ucFilterPerson";
            this.Size = new System.Drawing.Size(1204, 561);
            this.Load += new System.EventHandler(this.ucFilterPerson_Load);
            this.gbFind.ResumeLayout(false);
            this.gbFind.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbFind;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnSearchPerson;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label label3;
        private ucPersonDetails ucPersonDetails1;
        private System.Windows.Forms.Label label1;
    }
}
