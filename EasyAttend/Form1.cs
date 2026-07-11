using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace EasyAttend
{ 
    public partial class Form1 : Form
    {
        // متغير لحفظ مسار ملف الإكسيل المختار
        private string excelFilePath = "";

        public Form1()
        {
            InitializeComponent();
            // تفعيل الـ KeyDown للـ TextBox برمجياً لو مش مفعلها من التصميم
            txtStudentId.KeyDown += txtStudentId_KeyDown;
        }

        // 1. زرار اختيار ملف الإكسيل وعرضه فوراً في الـ DataGrid
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.Title = "اختر ملف كشف العسكرية للعميد";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = openFileDialog.FileName;

                    // تحديث الجدول وعرض البيانات فوراً
                    RefreshDataGridView();
                }
            }
        }

        // 2. دالة لقراءة الإكسيل وتحويله لـ DataTable لعرضه في الـ DataGridView
        private void RefreshDataGridView()
        {
            if (string.IsNullOrEmpty(excelFilePath) || !File.Exists(excelFilePath)) return;

            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    DataTable dt = new DataTable();

                    // قراءة العناوين (الصف الأول) وإنشاء الأعمدة في الـ DataTable
                    var firstRow = worksheet.FirstRowUsed();
                    foreach (var cell in firstRow.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString().Trim());
                    }

                    // قراءة صفوف الطلاب
                    var rows = worksheet.RowsUsed();
                    bool isFirstRow = true;
                    foreach (var row in rows)
                    {
                        if (isFirstRow) { isFirstRow = false; continue; } // تخطي صف العناوين

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = row.Cell(i + 1).Value.ToString();
                        }
                        dt.Rows.Add(dr);
                    }

                    // ربط الـ DataTable بالـ DataGridView
                    dgvAttendance.DataSource = dt;

                    // تظبيط شكل الجدول ليظهر بشكل احترافي عربي
                    dgvAttendance.RightToLeft = RightToLeft.Yes;
                    dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حصل خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. حدث الضغط على Enter داخل الـ TextBox للتحضير السريع
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // منع صوت الويندوز المزعج عند ضغط Enter
                e.SuppressKeyPress = true;

                string inputId = txtStudentId.Text.Trim();

                if (string.IsNullOrEmpty(excelFilePath))
                {
                    MessageBox.Show("من فضلك اختر ملف الإكسيل أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(inputId)) return;

                // استدعاء دالة التحضير والكتابة في الملف
                MarkAttendance(inputId);

                // مسح التكست بوكس وتجهيزه للطالب التالي فوراً
                txtStudentId.Clear();
                txtStudentId.Focus();
            }
        }

        // 4. دالة التحضير الذكي والكتابة في الإكسيل
        private void MarkAttendance(string studentId)
        {
            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    // الحصول على تاريخ النهارده بصيغة يوم/شهر (مثال: 11/7)
                    string todayDate = DateTime.Today.ToString("d/M");

                    int targetColumn = -1;
                    int totalColumns = worksheet.LastColumnUsed().ColumnNumber();

                    // البحث عن عمود النهارده في الصف الأول
                    for (int col = 3; col <= totalColumns; col++)
                    {
                        if (worksheet.Cell(1, col).Value.ToString().Trim() == todayDate)
                        {
                            targetColumn = col;
                            break;
                        }
                    }

                    // لو مش موجود، بنعمل عمود جديد للنهارده قبل عمود الملحوظات
                    if (targetColumn == -1)
                    {
                        targetColumn = totalColumns; // عمود الملحوظات هيترحّل خطوة
                        worksheet.Column(targetColumn).InsertColumnsBefore(1);
                        worksheet.Cell(1, targetColumn).Value = todayDate;
                    }

                    // البحث عن الطالب بالرقم (عمود "م" وهو العمود رقم 1)
                    int totalRows = worksheet.LastRowUsed().RowNumber();
                    bool studentFound = false;

                    for (int row = 2; row <= totalRows; row++)
                    {
                        if (worksheet.Cell(row, 1).Value.ToString().Trim() == studentId)
                        {
                            // تسجيل الحضور بـ "حاضر" أو علامة صح
                            worksheet.Cell(row, targetColumn).Value = "حاضر";

                            string studentName = worksheet.Cell(row, 2).Value.ToString();

                            // حفظ الملف فوراً
                            workbook.Save();

                            // تحديث الشاشة والجدول تلقائياً بدون الحاجة لإعادة اختيار الملف
                            RefreshDataGridView();

                            studentFound = true;
                            break;
                        }
                    }

                    if (!studentFound)
                    {
                        MessageBox.Show($"الرقم ({studentId}) مش موجود في الكشف!", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("الملف مفتوح حالياً في برنامج Excel! من فضلك اقفله علشان البرنامج يقدر يعدل عليه.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ غير متوقع: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}