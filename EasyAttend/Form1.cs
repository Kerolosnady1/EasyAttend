using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EasyAttend
{
    public partial class Form1 : Form
    {
        private string excelFilePath = "";
        // متغير لمنع تشغيل حدث التغيير TextChanged أثناء مسح التكست بوكس تلقائياً
        private bool isClearing = false;

        public Form1()
        {
            InitializeComponent();

            // ربط الأحداث برمجياً للتأكد من عملها بكفاءة وثبات
            txtSmartSearchAndAttend.TextChanged += txtSmartSearchAndAttend_TextChanged;
            txtSmartSearchAndAttend.KeyDown += txtSmartSearchAndAttend_KeyDown;
            dgvAttendance.CellDoubleClick += dgvAttendance_CellDoubleClick;

            // تظبيط شكل خانة التاريخ عشان تظهر بنفس شكل أعمدة الإكسيل (يوم/شهر)
            dtpAttendanceDate.Format = DateTimePickerFormat.Custom;
            dtpAttendanceDate.CustomFormat = "d/M";
        }

        // 1. زرار اختيار ملف الإكسيل وعرضه في الـ DataGridView
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.Title = "اختر ملف كشف العسكرية للعميد";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = openFileDialog.FileName;
                    RefreshDataGridView();
                }
            }
        }

        // 2. دالة قراءة ملف الإكسيل وتحديث الـ DataGridView
        private void RefreshDataGridView()
        {
            if (string.IsNullOrEmpty(excelFilePath) || !File.Exists(excelFilePath)) return;

            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    DataTable dt = new DataTable();

                    var firstRow = worksheet.FirstRowUsed();
                    foreach (var cell in firstRow.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString().Trim());
                    }

                    // تأكيد وجود عمود الملاحظات في الـ DataTable
                    if (!dt.Columns.Contains("ملاحظات"))
                    {
                        dt.Columns.Add("ملاحظات");
                    }

                    var rows = worksheet.RowsUsed();
                    bool isFirstRow = true;
                    foreach (var row in rows)
                    {
                        if (isFirstRow) { isFirstRow = false; continue; }

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            var cell = row.Cell(i + 1);

                            if (cell.IsEmpty())
                            {
                                dr[i] = "";
                            }
                            else
                            {
                                dr[i] = cell.Value.ToString().Trim();
                            }
                        }
                        dt.Rows.Add(dr);
                    }

                    dgvAttendance.DataSource = dt;
                    dgvAttendance.RightToLeft = RightToLeft.Yes;
                    dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    // تلوين الصفوف تلقائياً بعد التحميل
                    ApplyRowColoring();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حصل خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. دالة تلوين الصفوف السحرية (مستأذن بالأصفر، مشاغب بالأحمر)
        private void ApplyRowColoring()
        {
            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.IsNewRow) continue;

                if (dgvAttendance.Columns.Contains("ملاحظات"))
                {
                    string note = row.Cells["ملاحظات"].Value?.ToString()?.Trim() ?? "";

                    if (note.Contains("مستأذن"))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // أصفر هادئ
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (note.Contains("مشاغب") || note.Contains("ساقط") || note.Contains("تأديب"))
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose; // أحمر وردي خفيف
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        // 4. الفلترة الفورية أثناء الكتابة في الـ TextBox
        private void txtSmartSearchAndAttend_TextChanged(object sender, EventArgs e)
        {
            if (isClearing) return; // لو بننظف التكست بوكس برمجياً بنوقف الفلترة مؤقتاً لتجنب التهنيج

            if (dgvAttendance.DataSource is DataTable dt)
            {
                string filterText = txtSmartSearchAndAttend.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(filterText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    int temp;
                    if (int.TryParse(filterText, out temp))
                    {
                        dt.DefaultView.RowFilter = $"[م] = {temp}";
                    }
                    else
                    {
                        dt.DefaultView.RowFilter = $"[اسم الطالب (رباعــــي)] LIKE '%{filterText}%'";
                    }
                }
                ApplyRowColoring(); // إعادة تلوين الصفوف المفلترة
            }
        }

        // 5. زر الـ Enter الذكي: يحضر الطالب في التاريخ المختار بـ dtpAttendanceDate 🌟
        private void txtSmartSearchAndAttend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // منع صوت الويندوز المزعج
                e.Handled = true;          // منع إضافة سطر جديد داخل التكست بوكس الـ Multiline 🔴

                if (string.IsNullOrEmpty(excelFilePath))
                {
                    MessageBox.Show("من فضلك اختر ملف الإكسيل أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string inputText = txtSmartSearchAndAttend.Text.Trim();
                if (string.IsNullOrEmpty(inputText)) return;

                if (dgvAttendance.Rows.Count > 0 && dgvAttendance.Rows[0].Cells["م"].Value != null)
                {
                    string studentId = dgvAttendance.Rows[0].Cells["م"].Value.ToString().Trim();
                    string studentName = dgvAttendance.Rows[0].Cells["اسم الطالب (رباعــــي)"].Value.ToString().Trim();

                    // قراءة التاريخ المختار من خانة التاريخ الذكية 📅
                    DateTime selectedDate = dtpAttendanceDate.Value;
                    string formattedDate = selectedDate.ToString("d/M");

                    var confirm = MessageBox.Show($"هل تريد تحضير الطالب:\n\n👤 {studentName}\n🔢 رقم مسلسل (م): {studentId}\n📅 في تاريخ: {formattedDate}؟",
                                                  "تأكيد تحضير",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                    if (confirm == DialogResult.Yes)
                    {
                        // 1. تفعيل وضع الـ Clearing لإلغاء تداخل الـ TextChanged فوراً قبل المسح
                        isClearing = true;
                        txtSmartSearchAndAttend.Clear();
                        isClearing = false;

                        // 2. إلغاء الفلترة برمجياً ليعود الجدول كاملاً قبل عملية الحفظ والتحميل لضمان ثبات الـ UI
                        if (dgvAttendance.DataSource is DataTable dt)
                        {
                            dt.DefaultView.RowFilter = "";
                        }

                        // 3. تحديث ملف الإكسيل بالتاريخ المختار وحفظه وإعادة تحميل الـ DataGridView
                        UpdateStudentStatus(studentId, "حاضر", "", selectedDate);

                        // 4. إعادة التركيز للتكست بوكس للطالب التالي
                        txtSmartSearchAndAttend.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("لم يتم العثور على أي طالب بهذا الاسم أو الرقم!", "خطأ في البحث", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 6. دالة موحدة لتحديث حالة الطالب لـ أي تاريخ نحدده (بشكل مرن جداً 📅)
        private void UpdateStudentStatus(string studentId, string attendanceValue, string notesValue, DateTime targetDate)
        {
            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    string formattedDate = targetDate.ToString("d/M"); // هيطلع "16/7" أو "14/7" مثلاً
                    int totalColumns = worksheet.LastColumnUsed().ColumnNumber();

                    int notesColumn = -1;
                    int targetDateColumn = -1;

                    // البحث عن عمود الملاحظات وعمود التاريخ المطلوب
                    for (int col = 1; col <= totalColumns; col++)
                    {
                        string header = worksheet.Cell(1, col).Value.ToString().Trim();
                        if (header == "ملاحظات") notesColumn = col;
                        if (header == formattedDate) targetDateColumn = col;
                    }

                    // لو عمود الملاحظات مش موجود ننشئه في الآخر
                    if (notesColumn == -1)
                    {
                        notesColumn = totalColumns + 1;
                        worksheet.Cell(1, notesColumn).Value = "ملاحظات";
                    }

                    // لو العمود بتاع التاريخ المستهدف مش موجود ننشئه قبل عمود الملاحظات علطول
                    if (targetDateColumn == -1 && attendanceValue != null)
                    {
                        targetDateColumn = notesColumn;
                        worksheet.Column(targetDateColumn).InsertColumnsBefore(1);
                        worksheet.Cell(1, targetDateColumn).Value = formattedDate;
                        notesColumn++; // بنرحل عمود الملاحظات خطوة لليمين لأننا حشرنا عمود قبله
                    }

                    int totalRows = worksheet.LastRowUsed().RowNumber();
                    for (int row = 2; row <= totalRows; row++)
                    {
                        if (worksheet.Cell(row, 1).Value.ToString().Trim() == studentId)
                        {
                            if (attendanceValue != null && targetDateColumn != -1)
                            {
                                worksheet.Cell(row, targetDateColumn).Value = attendanceValue;
                            }

                            if (notesValue != null)
                            {
                                worksheet.Cell(row, notesColumn).Value = notesValue;
                            }

                            workbook.Save();
                            break;
                        }
                    }
                }

                // نعيد تحميل الـ DataGridView تماماً بعد الحفظ لتنعكس التعديلات فوراً
                RefreshDataGridView();
            }
            catch (IOException)
            {
                MessageBox.Show("الملف مفتوح حالياً في برنامج Excel! من فضلك اقفله علشان البرنامج يقدر يعدل عليه.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 7. النقر المزدوج (Double-Click) لتسجيل الحالات الخاصة بالتاريخ المختار أيضاً 🌟
        private void dgvAttendance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvAttendance.Rows[e.RowIndex];
                string studentId = row.Cells["م"].Value?.ToString();
                string studentName = row.Cells["اسم الطالب (رباعــــي)"].Value?.ToString();

                if (string.IsNullOrEmpty(studentId)) return;

                DateTime selectedDate = dtpAttendanceDate.Value;
                string formattedDate = selectedDate.ToString("d/M");

                ContextMenuStrip menu = new ContextMenuStrip();

                ToolStripMenuItem titleItem = new ToolStripMenuItem($"خيارات الطالب: {studentName} (تاريخ: {formattedDate})");
                titleItem.Enabled = false;
                menu.Items.Add(titleItem);
                menu.Items.Add(new ToolStripSeparator());

                menu.Items.Add($"✅ تسجيل حضور (حاضر يوم {formattedDate})", null, (s, ev) => {
                    UpdateStudentStatus(studentId, "حاضر", "", selectedDate);
                });

                menu.Items.Add($"🟡 طالب مستأذن يوم {formattedDate}", null, (s, ev) => {
                    UpdateStudentStatus(studentId, "مستأذن", "مستأذن بعذر رسمي", selectedDate);
                });

                menu.Items.Add($"🔴 طالب مشاغب يوم {formattedDate}", null, (s, ev) => {
                    UpdateStudentStatus(studentId, "غائب", "مشاغب - توصية رسوب ⚠️", selectedDate);
                });

                menu.Items.Add($"🔄 إلغاء حضور يوم {formattedDate}", null, (s, ev) => {
                    UpdateStudentStatus(studentId, "", "", selectedDate);
                });

                menu.Show(Cursor.Position);
            }
        }

        // 8. زرار عرض إجمالي الغياب وتقرير الحضور اليومي
        private void btnShowAbsentees_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.Rows.Count == 0 || dgvAttendance.DataSource == null)
            {
                MessageBox.Show("من فضلك اختر ملف الكشف واعرض البيانات أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // التقرير هيطلع بناءً على التاريخ المختار في الأداة لتسهيل المراجعة لأي يوم فات 📅
            string selectedDateStr = dtpAttendanceDate.Value.ToString("d/M");

            if (!dgvAttendance.Columns.Contains(selectedDateStr))
            {
                MessageBox.Show($"عمود التاريخ المختار ({selectedDateStr}) مش موجود في الجدول! هل قمت بتحضير أي طالب فيه؟", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            System.Collections.Generic.List<string> absentIds = new System.Collections.Generic.List<string>();
            int presentCount = 0;

            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.IsNewRow) continue;

                var attendanceValue = row.Cells[selectedDateStr].Value?.ToString()?.Trim();
                var studentId = row.Cells["م"].Value?.ToString()?.Trim();

                if (attendanceValue == "حاضر")
                {
                    presentCount++;
                }
                else
                {
                    if (!string.IsNullOrEmpty(studentId))
                    {
                        absentIds.Add(studentId);
                    }
                }
            }

            int totalStudents = presentCount + absentIds.Count;

            if (absentIds.Count == 0)
            {
                MessageBox.Show($"الله ينور! الحضور كامل بنسبة 100% يوم {selectedDateStr} 🎉\n\n" +
                                $"إجمالي الطلاب: {totalStudents}\n" +
                                $"عدد الحاضرين: {presentCount}\n" +
                                $"عدد الغائبين: 0",
                                "تقرير الحضور والغياب", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string allAbsentIds = string.Join(", ", absentIds);

                string reportMessage = $"📊 تقرير الحضور والغياب ليوم ({selectedDateStr}):\n" +
                                       $"--------------------------------------------\n" +
                                       $"👤 إجمالي الطلاب في الكشف: {totalStudents}\n" +
                                       $"✅ إجمالي الحاضرين: {presentCount}\n" +
                                       $"❌ إجمالي الغائبين: {absentIds.Count}\n\n" +
                                       $"📌 أرقام المسلسل (م) للغائبين هي:\n{allAbsentIds}";

                MessageBox.Show(reportMessage, "تقرير الحضور والغياب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}