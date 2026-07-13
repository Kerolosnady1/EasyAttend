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

        private void btnShowAbsentees_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.Rows.Count == 0 || dgvAttendance.DataSource == null)
            {
                MessageBox.Show("من فضلك اختر ملف الكشف واعرض البيانات أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string todayDate = DateTime.Today.ToString("d/M");

            if (!dgvAttendance.Columns.Contains(todayDate))
            {
                MessageBox.Show($"عمود تاريخ النهارده ({todayDate}) مش موجود في الجدول! هل قمت بتحضير أي طالب النهارده؟", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            System.Collections.Generic.List<string> absentIds = new System.Collections.Generic.List<string>();
            int presentCount = 0; // متغيّر لحساب عدد الحاضرين

            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.IsNewRow) continue;

                var attendanceValue = row.Cells[todayDate].Value?.ToString()?.Trim();
                var studentId = row.Cells["م"].Value?.ToString()?.Trim();

                if (attendanceValue == "حاضر")
                {
                    presentCount++; // زيادة الحاضرين بمقدار 1
                }
                else // لو غايب أو فاضي
                {
                    if (!string.IsNullOrEmpty(studentId))
                    {
                        absentIds.Add(studentId);
                    }
                }
            }

            int totalStudents = presentCount + absentIds.Count; // إجمالي الطلاب الفعلي في الكشف

            if (absentIds.Count == 0)
            {
                MessageBox.Show($"الله ينور! الحضور كامل بنسبة 100% 🎉\n\n" +
                                $"إجمالي الطلاب: {totalStudents}\n" +
                                $"عدد الحاضرين: {presentCount}\n" +
                                $"عدد الغائبين: 0",
                                "تقرير الحضور والغياب اليومي", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string allAbsentIds = string.Join(", ", absentIds);

                string reportMessage = $"📊 تقرير الحضور والغياب لليوم ({todayDate}):\n" +
                                       $"--------------------------------------------\n" +
                                       $"👤 إجمالي الطلاب في الكشف: {totalStudents}\n" +
                                       $"✅ إجمالي الحاضرين: {presentCount}\n" +
                                       $"❌ إجمالي الغائبين: {absentIds.Count}\n\n" +
                                       $"📌 أرقام المسلسل (م) للغائبين هي:\n{allAbsentIds}";

                MessageBox.Show(reportMessage, "تقرير الحضور والغياب اليومي", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemoveAttendance(string studentId)
        {
            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    string todayDate = DateTime.Today.ToString("d/M");
                    int targetColumn = -1;
                    int totalColumns = worksheet.LastColumnUsed().ColumnNumber();

                    for (int col = 3; col <= totalColumns; col++)
                    {
                        if (worksheet.Cell(1, col).Value.ToString().Trim() == todayDate)
                        {
                            targetColumn = col;
                            break;
                        }
                    }

                    if (targetColumn == -1) return; // مفيش عمود للنهارده أصلاً

                    int totalRows = worksheet.LastRowUsed().RowNumber();
                    for (int row = 2; row <= totalRows; row++)
                    {
                        if (worksheet.Cell(row, 1).Value.ToString().Trim() == studentId)
                        {
                            // مسح كلمة "حاضر" من الإكسيل
                            worksheet.Cell(row, targetColumn).Value = "";
                            workbook.Save();
                            RefreshDataGridView(); // تحديث الجدول فوراً
                            MessageBox.Show("تم إلغاء حضور الطالب بنجاح وتم تأديبه! 🫡", "تم الإلغاء");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء إلغاء الحضور: {ex.Message}");
            }
        }

        private void dgvAttendance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // التأكد إن الضغطة مش على صف العناوين
            if (e.RowIndex < 0) return;

            var row = dgvAttendance.Rows[e.RowIndex];
            string studentId = row.Cells["م"].Value?.ToString();
            string studentName = row.Cells["اسم الطالب (رباعــــي)"].Value?.ToString();
            string todayDate = DateTime.Today.ToString("d/M");

            if (string.IsNullOrEmpty(studentId)) return;

            // التأكد إن الطالب أصلاً متحضر النهارده قبل ما نلغي
            var attendanceStatus = row.Cells[todayDate].Value?.ToString();
            if (attendanceStatus != "حاضر")
            {
                MessageBox.Show("الطالب ده مش متحضر النهارده أصلاً عشان تلغي حضوره!", "تنبيه");
                return;
            }

            var confirmResult = MessageBox.Show($"هل أنت متأكد من إلغاء حضور الطالب: {studentName} (رقم م: {studentId}) للشرر والمشاغبة؟",
                                                 "إلغاء تحضير طالب",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                RemoveAttendance(studentId); // دالة بتمسح القيمة من الإكسيل (هكتبهالك تحت)
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // التأكد إن الـ DataGridView مربوطة بـ DataTable
            if (dgvAttendance.DataSource is DataTable dt)
            {
                string filterText = txtSearch.Text.Trim().Replace("'", "''"); // لحماية الكود من الرموز الخاصة

                if (string.IsNullOrEmpty(filterText))
                {
                    dt.DefaultView.RowFilter = ""; // إلغاء الفلتر وعرض الكل لو الخانة فاضية
                }
                else
                {
                    // فلترة ذكية: تبحث في عمود "م" بالرقم، أو في عمود الاسم بأي جزء من الكلمة
                    dt.DefaultView.RowFilter = $"[م] = '{filterText}' OR [اسم الطالب (رباعــــي)] LIKE '%{filterText}%'";
                }
            }
        }
    }
}