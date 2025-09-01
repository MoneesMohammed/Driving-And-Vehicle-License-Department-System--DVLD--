namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Written_Test_Appointments
{
    partial class frmWrittenTestAppointments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAllAppointmentTest = new System.Windows.Forms.DataGridView();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecodes = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucDrivingLicenseApplicationInfo1 = new Driving___Vehicle_License_Department__DVLD_.UserControls.ucDrivingLicenseApplicationInfo();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTakeTest = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointmentTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAllAppointmentTest
            // 
            this.dgvAllAppointmentTest.AllowUserToAddRows = false;
            this.dgvAllAppointmentTest.AllowUserToDeleteRows = false;
            this.dgvAllAppointmentTest.AllowUserToOrderColumns = true;
            this.dgvAllAppointmentTest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllAppointmentTest.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvAllAppointmentTest.BackgroundColor = System.Drawing.Color.DarkSlateGray;
            this.dgvAllAppointmentTest.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllAppointmentTest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAllAppointmentTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllAppointmentTest.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllAppointmentTest.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAllAppointmentTest.GridColor = System.Drawing.SystemColors.Menu;
            this.dgvAllAppointmentTest.Location = new System.Drawing.Point(17, 757);
            this.dgvAllAppointmentTest.Name = "dgvAllAppointmentTest";
            this.dgvAllAppointmentTest.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllAppointmentTest.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAllAppointmentTest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllAppointmentTest.Size = new System.Drawing.Size(1024, 194);
            this.dgvAllAppointmentTest.TabIndex = 48;
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.add_event_2;
            this.btnAddAppointment.Location = new System.Drawing.Point(968, 679);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(73, 72);
            this.btnAddAppointment.TabIndex = 47;
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 717);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 25);
            this.label3.TabIndex = 46;
            this.label3.Text = "Appointment:";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.close_1;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(913, 966);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 43);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecodes
            // 
            this.lblRecodes.AutoSize = true;
            this.lblRecodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecodes.Location = new System.Drawing.Point(149, 967);
            this.lblRecodes.Name = "lblRecodes";
            this.lblRecodes.Size = new System.Drawing.Size(24, 25);
            this.lblRecodes.TabIndex = 44;
            this.lblRecodes.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 967);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 43;
            this.label2.Text = "#Recodes :";
            // 
            // ucDrivingLicenseApplicationInfo1
            // 
            this.ucDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(10, 253);
            this.ucDrivingLicenseApplicationInfo1.Name = "ucDrivingLicenseApplicationInfo1";
            this.ucDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(1031, 420);
            this.ucDrivingLicenseApplicationInfo1.TabIndex = 42;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.exam_1;
            this.pictureBox1.Location = new System.Drawing.Point(384, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(242, 198);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(279, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 37);
            this.label1.TabIndex = 40;
            this.label1.Text = "Written Test Appointments";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEdit,
            this.tsmTakeTest});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 96);
            // 
            // tsmEdit
            // 
            this.tsmEdit.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.edit;
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(204, 46);
            this.tsmEdit.Text = "Edit";
            this.tsmEdit.Click += new System.EventHandler(this.tsmEdit_Click);
            // 
            // tsmTakeTest
            // 
            this.tsmTakeTest.Image = global::Driving___Vehicle_License_Department__DVLD_.Properties.Resources.test;
            this.tsmTakeTest.Name = "tsmTakeTest";
            this.tsmTakeTest.Size = new System.Drawing.Size(204, 46);
            this.tsmTakeTest.Text = "Take Test";
            this.tsmTakeTest.Click += new System.EventHandler(this.tsmTakeTest_Click);
            // 
            // frmWrittenTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 1021);
            this.Controls.Add(this.dgvAllAppointmentTest);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecodes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWrittenTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Written Test Appointments";
            this.Load += new System.EventHandler(this.frmWrittenTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointmentTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllAppointmentTest;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecodes;
        private System.Windows.Forms.Label label2;
        private UserControls.ucDrivingLicenseApplicationInfo ucDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmTakeTest;
    }
}