# 📊 EasyAttend - Smart Attendance Management System

**EasyAttend** is a lightweight, high-performance C# Windows Forms desktop application designed to make managing and tracking student attendance incredibly fast and automated. Integrated directly with Excel files using **ClosedXML**, it serves as an ideal solution for instructors, university coordinators, and administrators.

[**English Version**](#key-features) | [**النسخة العربية**](#الميزات-الرئيسية)

---

## Key Features

*   **⚡ Smart Instant Search & Attend:** Enter a student’s serial number (`م`) or type their name, press **Enter**, and they are instantly marked as "Present".
*   **📅 Dynamic Date Column Generation:** If a column for the selected date (e.g., `16/7`) doesn't exist in your Excel sheet, the app automatically creates it before marking attendance.
*   **⏳ Flexible Past-Date Logging:** Easily select any previous date from the interactive `DateTimePicker` to log or edit past attendance in seconds.
*   **🎨 Conditional Row Coloring:** Smart UI feedback that highlights special cases instantly:
    *   🟡 **Yellow:** Authorized absence / excused (`مستأذن`).
    *   🔴 **Red:** Rowdy / disciplinary action (`مشاغب` / `تأديب`).
*   **🖱️ Double-Click Context Menu:** Right-click or double-click on any student row to assign special notes or reset their status without touching the keyboard.
*   **📊 Instant Absence Reports:** Generates a real-time summary report for any selected date showing: total students, present count, absent count, and a list of all absent serial numbers.
*   **🔒 Error & Conflict Handling:** Built-in protection against file-sharing violations (e.g., when the Excel sheet is open in Microsoft Excel) to prevent data loss.

---

## الميزات الرئيسية (Arabic)

*   **⚡ بحث وتحضير ذكي وسريع:** بمجرد كتابة رقم الطالب (م) أو جزء من اسمه والضغط على **Enter**، يتم تسجيله "حاضر" فوراً.
*   **📅 إنشاء تلقائي لأعمدة التواريخ:** إذا كان تاريخ اليوم غير موجود في ملف الإكسيل، يقوم البرنامج بإنشاء العمود ديناميكياً وحفظ البيانات فيه.
*   **⏳ تحضير مرن لتواريخ سابقة:** يمكنك اختيار أي تاريخ سابق من خانة التاريخ الذكية لتحضير الطلاب أو تعديل غيابهم بسهولة.
*   **🎨 تلوين ذكي وتلقائي للجدول:** تمييز بصري فوري للطلاب حسب حالاتهم الخاصة:
    *   🟡 **الأصفر:** طالب مستأذن بعذر رسمي.
    *   🔴 **الأحمر الوردي:** طالب مشاغب أو عليه عقوبة تأديبية.
*   **🖱️ قائمة خيارات سريعة بالـ Double-Click:** اضغط مرتين على أي طالب لفتح قائمة الخيارات الخاصة (تسجيل حضور، إذن، غياب تأديبي، أو تصفير الحالة).
*   **📊 تقارير غياب فورية بنقرة واحدة:** يعرض تقريراً تفصيلياً لأي يوم يوضح إجمالي الحاضرين، الغائبين، وقائمة بأرقام مسلسل الغائبين لسهولة نقلها.

---

## Tech Stack

*   **Language:** C# (.NET Framework)
*   **UI Framework:** Windows Forms (WinForms)
*   **Database/Storage:** Excel Spreadsheet (`.xlsx`)
*   **Libraries used:** 
    *   [ClosedXML](https://github.com/ClosedXML/ClosedXML) (for robust and fast Excel file manipulation without needing MS Office installed).

---

## How to Run & Use the Project

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/EasyAttend.git](https://github.com/your-username/EasyAttend.git)
    ```
2.  **Restore Nuget Packages:** Make sure `ClosedXML` and its dependencies are installed via Nuget Package Manager.
3.  **Run the Application:** Build and run using Visual Studio.
4.  **How to use:**
    *   Click **Browse** (`اختر كشف العسكرية`) and select your Excel sheet.
    *   To record today's attendance, keep the date picker as is, search for the student, and hit **Enter**.
    *   To record attendance for a past date, change the date in the picker, search for the student, and hit **Enter**.
    *   Double-click on any row to open the advanced options.

---

## Project Structure (MVC-like WinForms)

*   `Form1.cs`: Contains the UI event listeners, validation logic, and the smart rendering engine.
*   `Form1.Designer.cs`: Declarations of GUI controls including `dgvAttendance`, `txtSmartSearchAndAttend`, and `dtpAttendanceDate`.
*   `Program.cs`: The main entry point of the application.

---

## License

This project is licensed under the MIT License - feel free to use and modify it!

---
*Created with ❤️ by [Kerolos](https://github.com/your-username)*