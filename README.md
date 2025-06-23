# Grades-Tracking-System

# 📊 Grades Tracking System – Project 1 (INFO-3138)

**Course:** INFO-3138  
**Project:** JSON Grades Tracker  
**Instructor:** *Fanshawe College – Summer 2025*  
**Weight:** 15% of Final Grade

---

## 📌 Objective

This project involves building a JSON-based grade tracking system. It consists of:

- 🗂 A **JSON Schema** to validate course and evaluation data
- 🖥 A **C# Console Application** to manage, view, and update grades with data validation

---

## 🧰 Features

### 🔧 JSON Schema
- Defines a **course object** with a required `Code` and optional `Evaluations`
- Validates evaluation details like `Weight`, `OutOf`, and `EarnedMarks`
- Enforces format:  
  - Course Code: `"INFO-3138"`  
  - Weight: `0-100`  
  - OutOf: `≥ 0`  
  - EarnedMarks: `≥ 0 or null`

### 💻 Console Application
- Reads from and writes to a JSON data file
- Supports:
  - Add/Edit/Delete **Courses**
  - Add/Edit/Delete **Evaluations** under each course
  - Full **validation** against the schema after every change
  - Calculation of:
    - **Percent achieved**
    - **Course marks earned**
    - **Summary and detailed listings**
- Handles file not found by initializing empty data
- Saves all updates on exit

---

## 🧪 Sample Calculations

**Percent Evaluation:**  
`PercentEvaluation = 100 × EarnedMarks / OutOf`  

**Course Marks Evaluation:**  
`CourseMarksEvaluation = PercentEvaluation × Weight / 100`  

**Course Summary:**  
- **CourseMarksTotal** = sum of CourseMarksEvaluation  
- **WeightTotal** = sum of Weight (where EarnedMarks ≠ null)  
- **PercentTotal** = 100 × CourseMarksTotal / WeightTotal

---

## 📝 Schema Rules Summary

| Property     | Type      | Required | Constraints              |
|--------------|-----------|----------|---------------------------|
| Code         | string    | ✅ Yes   | `"UUUU-####"` format      |
| Evaluations  | array     | ❌ No    | List of Evaluation objects |

**Evaluation Object:**

| Property     | Type          | Required | Constraints           |
|--------------|---------------|----------|------------------------|
| Description  | string        | ✅ Yes   | Any                    |
| Weight       | number        | ✅ Yes   | `0 <= Weight <= 100`   |
| OutOf        | integer       | ✅ Yes   | `OutOf >= 0`           |
| EarnedMarks  | number or null| ✅ Yes   | If not null: `>= 0`    |

---

## 📁 Project Structure

Chemistry_YR_Android/
├── grades.json # Grades data file (array of courses)
├── grades-schema.json # JSON Schema file
├── Program.cs # Console UI and logic
├── Course.cs # Course model
├── Evaluation.cs # Evaluation model
└── ...

yaml
Copy
Edit

---

## ▶️ How to Run

1. Clone/download the repo.
2. Open in **Visual Studio** (.NET Core Console App).
3. Run the project.
4. Follow on-screen prompts to manage grades.

> If `grades.json` does not exist, the app will prompt you to create one.

---

## 🛡 Validation Examples

- ❌ **Invalid Course Code**: `info-3138` → ❌ Lowercase not allowed  
- ❌ **Invalid Marks**: `OutOf = -5` → ❌ Must be ≥ 0

The app blocks invalid data and prompts for re-entry.

---

## 🎓 Learning Outcomes

- CLO 2: Understand JSON syntax and constraints  
- CLO 3–5: Create, validate, and use JSON data within an app  
- Apply design for real-time data validation and persistence

---

## 📦 Submission

Submit a `.zip` file containing:
- Visual Studio project
- `grades-schema.json`
- `grades.json` (sample with 3 courses and ≥2 evaluations each)

Only one student should submit if working in pairs. Credit both contributors in source comments.

---

## 🏁 Grading Breakdown

| Task                                          | Marks |
|-----------------------------------------------|-------|
| JSON Schema (valid, fields, constraints)      | 7     |
| Sample JSON file with 3+ courses              | 1     |
| JSON parsing, summary & calculations          | 2     |
| Course & Evaluation add/edit/delete + validate| 5     |
| File writing, error handling                  | 4     |
| Code style, documentation, exceptions         | 1     |
| **Total**                                     | **20**|

---

## ⚠ Academic Integrity

Submit **original** work. Do **not** copy from others or AI tools. Academic misconduct will result in penalties, including zero grade or expulsion.

---

© 2025 Fanshawe College. All rights reserved.
