# 🎖️ Military Attendance Kiosk System

A smart, fast, and secure desktop application built with **C# Windows Forms** designed to manage and register student attendance for military training courses in universities. The application directly reads and updates data in real-time within the Brigadier General's **Excel** file without requiring a complex database setup, cutting down traditional roll-call time from hours to just a few minutes! ⏱️

---

## ✨ Key Features

* **📁 Direct Excel Integration:** Select the official attendance spreadsheet (`.xlsx`) via a file dialog and modify it directly using the lightweight `ClosedXML` library.
* **🚫 Anti-Cheating Mechanism:** Designed around physical ID collection at the registration desk. Students cannot log attendance for absent peers since the original ID card must be handed over physically.
* **⚡ Ultra-Fast Entry:** Simply type the student's serial number (`m`) and press **Enter**. The application automatically marks them present, saves the file, and clears the input box for the next student in under a second!
* **🔄 Live Dashboard:** A real-time `DataGridView` instantly reflects attendance updates across all 15 days as soon as a student is logged.
* **📅 Smart Date Detection:** The system automatically detects the current system date (e.g., `11/7`) and matches it with the corresponding column header in Excel to mark the student as "**Present**" (حاضر).
* **💾 Auto-Save:** The application automatically saves changes to the Excel file after every single entry, ensuring zero data loss if the computer loses power or encounters an unexpected shutdown.

---

## 🛠️ Technologies Used

* **Language:** C#
* **Application Type:** Windows Forms App (.NET Framework / .NET Core)
* **Excel Manipulation:** `ClosedXML` library installed via NuGet Package Manager.
* **Data Binder:** `DataTable` to bridge the Excel sheet and the DataGridView smoothly.

---

## 📸 UI & Workflow

1. **Step 1:** Click the **"Browse"** button to locate and select the Brigadier General's Excel sheet.
2. **Step 2:** The grid view instantly populates with the student roster and the 15-day columns.
3. **Step 3:** The administrator collects student IDs inside the lecture hall, inputs the student's serial number (`m`), and hits **Enter**.
4. **Step 4:** The word "**حاضر**" appears instantly under the current date column, and the file saves in the background.

---

## 🚀 Setup & Installation

1. Clone the repository to your local machine:
   ```bash
	git clone [https://github.com/YOUR_USERNAME/REPOSITORY_NAME.git](https://github.com/YOUR_USERNAME/REPOSITORY_NAME.git)

Open the project using Visual Studio.

Install the required ClosedXML library using the NuGet Package Manager Console:

	Install-Package ClosedXML
```
Build and run the application immediately!

📝 Required Excel File Structure
For the application to function perfectly out of the box, the Excel file layout must be formatted as follows (configured Right-to-Left for Arabic data):

Column A: Titled "م" (Contains the student's serial or identification number).

Column B: Titled "اسم الطالب" (Student Name).

Subsequent Columns (C to Q): Header names must represent the course dates (e.g., 11/7, 12/7, 13/7, etc.).

The Last Column: Titled "ملحوظات" (Notes).

⚙️ Developed & Maintained by: Kerolos Nady - Computer Science Student.