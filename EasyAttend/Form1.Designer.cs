namespace EasyAttend
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSmartSearchAndAttend = new TextBox();
            dgvAttendance = new DataGridView();
            label1 = new Label();
            btnBrowse = new Button();
            btnShowAbsentees = new Button();
            label2 = new Label();
            dtpAttendanceDate = new DateTimePicker();
            picInstagram = new PictureBox();
            picGitHub = new PictureBox();
            picLinkedIn = new PictureBox();
            picFacebook = new PictureBox();
            picWhatsApp = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInstagram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGitHub).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLinkedIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picFacebook).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picWhatsApp).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtSmartSearchAndAttend
            // 
            txtSmartSearchAndAttend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSmartSearchAndAttend.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSmartSearchAndAttend.Location = new Point(251, 78);
            txtSmartSearchAndAttend.Multiline = true;
            txtSmartSearchAndAttend.Name = "txtSmartSearchAndAttend";
            txtSmartSearchAndAttend.Size = new Size(617, 79);
            txtSmartSearchAndAttend.TabIndex = 0;
            txtSmartSearchAndAttend.TextAlign = HorizontalAlignment.Center;
            txtSmartSearchAndAttend.TextChanged += txtSmartSearchAndAttend_TextChanged;
            txtSmartSearchAndAttend.KeyDown += txtSmartSearchAndAttend_KeyDown;
            // 
            // dgvAttendance
            // 
            dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(40, 191);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.RowHeadersWidth = 51;
            dgvAttendance.Size = new Size(1266, 581);
            dgvAttendance.TabIndex = 1;
            dgvAttendance.CellDoubleClick += dgvAttendance_CellDoubleClick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Highlight;
            label1.Location = new Point(543, 880);
            label1.Name = "label1";
            label1.Size = new Size(261, 28);
            label1.TabIndex = 2;
            label1.Text = "Powered by: Kerolos Farag";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.Location = new Point(920, 78);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(140, 75);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse File";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnShowAbsentees
            // 
            btnShowAbsentees.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowAbsentees.Location = new Point(1075, 78);
            btnShowAbsentees.Name = "btnShowAbsentees";
            btnShowAbsentees.Size = new Size(143, 75);
            btnShowAbsentees.TabIndex = 4;
            btnShowAbsentees.Text = "Absent";
            btnShowAbsentees.UseVisualStyleBackColor = true;
            btnShowAbsentees.Click += btnShowAbsentees_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(267, 34);
            label2.Name = "label2";
            label2.Size = new Size(554, 41);
            label2.TabIndex = 5;
            label2.Text = "Add/Search By Serial Numbers/Name:";
            // 
            // dtpAttendanceDate
            // 
            dtpAttendanceDate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dtpAttendanceDate.CustomFormat = "d/M";
            dtpAttendanceDate.Format = DateTimePickerFormat.Custom;
            dtpAttendanceDate.Location = new Point(776, 158);
            dtpAttendanceDate.Name = "dtpAttendanceDate";
            dtpAttendanceDate.Size = new Size(92, 27);
            dtpAttendanceDate.TabIndex = 6;
            // 
            // picInstagram
            // 
            picInstagram.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            picInstagram.Image = Properties.Resources.instagram;
            picInstagram.Location = new Point(75, 3);
            picInstagram.Name = "picInstagram";
            picInstagram.Size = new Size(106, 96);
            picInstagram.SizeMode = PictureBoxSizeMode.Zoom;
            picInstagram.TabIndex = 7;
            picInstagram.TabStop = false;
            picInstagram.Click += picInstagram_Click;
            // 
            // picGitHub
            // 
            picGitHub.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            picGitHub.Image = Properties.Resources.github;
            picGitHub.Location = new Point(187, 3);
            picGitHub.Name = "picGitHub";
            picGitHub.Size = new Size(66, 96);
            picGitHub.SizeMode = PictureBoxSizeMode.Zoom;
            picGitHub.TabIndex = 8;
            picGitHub.TabStop = false;
            picGitHub.Click += picGitHub_Click;
            // 
            // picLinkedIn
            // 
            picLinkedIn.Image = Properties.Resources.linkedin;
            picLinkedIn.Location = new Point(259, 3);
            picLinkedIn.Name = "picLinkedIn";
            picLinkedIn.Size = new Size(94, 96);
            picLinkedIn.SizeMode = PictureBoxSizeMode.Zoom;
            picLinkedIn.TabIndex = 9;
            picLinkedIn.TabStop = false;
            picLinkedIn.Click += picLinkedIn_Click;
            // 
            // picFacebook
            // 
            picFacebook.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            picFacebook.Image = Properties.Resources.facebook;
            picFacebook.Location = new Point(359, 3);
            picFacebook.Name = "picFacebook";
            picFacebook.Size = new Size(68, 96);
            picFacebook.SizeMode = PictureBoxSizeMode.Zoom;
            picFacebook.TabIndex = 10;
            picFacebook.TabStop = false;
            picFacebook.Click += picFacebook_Click;
            // 
            // picWhatsApp
            // 
            picWhatsApp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            picWhatsApp.Image = Properties.Resources.whatsapp;
            picWhatsApp.Location = new Point(3, 3);
            picWhatsApp.Name = "picWhatsApp";
            picWhatsApp.Size = new Size(66, 96);
            picWhatsApp.SizeMode = PictureBoxSizeMode.Zoom;
            picWhatsApp.TabIndex = 11;
            picWhatsApp.TabStop = false;
            picWhatsApp.Click += picWhatsApp_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Bottom;
            flowLayoutPanel1.Controls.Add(picWhatsApp);
            flowLayoutPanel1.Controls.Add(picInstagram);
            flowLayoutPanel1.Controls.Add(picGitHub);
            flowLayoutPanel1.Controls.Add(picLinkedIn);
            flowLayoutPanel1.Controls.Add(picFacebook);
            flowLayoutPanel1.Location = new Point(451, 778);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.RightToLeft = RightToLeft.No;
            flowLayoutPanel1.Size = new Size(443, 84);
            flowLayoutPanel1.TabIndex = 12;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 925);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(dtpAttendanceDate);
            Controls.Add(label2);
            Controls.Add(btnShowAbsentees);
            Controls.Add(btnBrowse);
            Controls.Add(label1);
            Controls.Add(dgvAttendance);
            Controls.Add(txtSmartSearchAndAttend);
            Name = "Form1";
            Text = "Easy Attend";
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInstagram).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGitHub).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLinkedIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)picFacebook).EndInit();
            ((System.ComponentModel.ISupportInitialize)picWhatsApp).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvAttendance;
        private Label label1;
        private Button btnBrowse;
        private Button btnShowAbsentees;
        private Label label2;
        private TextBox txtSmartSearchAndAttend;
        private DateTimePicker dtpAttendanceDate;
        private PictureBox picInstagram;
        private PictureBox picGitHub;
        private PictureBox picLinkedIn;
        private PictureBox picFacebook;
        private PictureBox picWhatsApp;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}