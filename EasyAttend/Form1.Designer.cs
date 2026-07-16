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
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
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
            // 
            // dgvAttendance
            // 
            dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(40, 191);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.RowHeadersWidth = 51;
            dgvAttendance.Size = new Size(1266, 495);
            dgvAttendance.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 746);
            Controls.Add(label2);
            Controls.Add(btnShowAbsentees);
            Controls.Add(btnBrowse);
            Controls.Add(label1);
            Controls.Add(dgvAttendance);
            Controls.Add(txtSmartSearchAndAttend);
            Name = "Form1";
            Text = "Easy Attend";
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
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
    }
}