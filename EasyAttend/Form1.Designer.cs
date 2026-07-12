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
            txtStudentId = new TextBox();
            dgvAttendance = new DataGridView();
            label1 = new Label();
            btnBrowse = new Button();
            btnShowAbsentees = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            SuspendLayout();
            // 
            // txtStudentId
            // 
            txtStudentId.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtStudentId.Location = new Point(506, 77);
            txtStudentId.Multiline = true;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(345, 79);
            txtStudentId.TabIndex = 0;
            txtStudentId.TextAlign = HorizontalAlignment.Center;
            txtStudentId.KeyDown += txtStudentId_KeyDown;
            // 
            // dgvAttendance
            // 
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(40, 191);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.RowHeadersWidth = 51;
            dgvAttendance.Size = new Size(1266, 495);
            dgvAttendance.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Highlight;
            label1.Location = new Point(543, 709);
            label1.Name = "label1";
            label1.Size = new Size(261, 28);
            label1.TabIndex = 2;
            label1.Text = "Powered by: Kerolos Farag";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(878, 81);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(140, 75);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse File";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnShowAbsentees
            // 
            btnShowAbsentees.Location = new Point(1038, 84);
            btnShowAbsentees.Name = "btnShowAbsentees";
            btnShowAbsentees.Size = new Size(143, 72);
            btnShowAbsentees.TabIndex = 4;
            btnShowAbsentees.Text = "Absent";
            btnShowAbsentees.UseVisualStyleBackColor = true;
            btnShowAbsentees.Click += btnShowAbsentees_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 746);
            Controls.Add(btnShowAbsentees);
            Controls.Add(btnBrowse);
            Controls.Add(label1);
            Controls.Add(dgvAttendance);
            Controls.Add(txtStudentId);
            Name = "Form1";
            Text = "Easy Attend";
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtStudentId;
        private DataGridView dgvAttendance;
        private Label label1;
        private Button btnBrowse;
        private Button btnShowAbsentees;
    }
}
