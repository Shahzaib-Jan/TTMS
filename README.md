# Timetable Management System (TTMS)

A modern, robust Windows Forms desktop application developed in C# (.NET Framework) for automated and interactive university timetable scheduling, conflict management, and printable report generation.

## 🚀 Key Features

- **Multi-Tier OOP Architecture**: Cleanly separated into Presentation (`TTMS OOP`), Business Logic (`TTMS_OOP.BLL`), Data Access (`TTMS_OOP.DAL`), and Domain Entities (`TTMS_OOP.Models`).
- **Interactive Drag-and-Drop Timetable Builder**:
  - Drag and drop courses across day/timeslot slots to move or swap classes instantly.
  - Excludes standard 12:00 PM - 1:00 PM break periods automatically.
  - Rounded, color-accented cards with full course names and bottom-right course code badges.
- **Strict Academic Constraint Validation**:
  - Credit-hour validation (e.g., 3-credit hour subjects cannot exceed 3 weekly lecture hours).
  - Conflict detection for teachers, classrooms, and student sections.
- **CRUD Management**:
  - Manage Sections, Subjects, Teachers, and Time Slots.
  - Real-time ascending sorting for clean data overview.
- **High-Resolution Print & Export**:
  - Formatted canvas rendering for printable preview and timetable export.
  - Academic header styling with customized attribution (*By Shahzaib*).
- **Authentication & Security**:
  - Secure login with role management and password change utility.
  - JSON-based persistent repository.

## 🛠️ Tech Stack & Requirements

- **Language**: C# (.NET Framework 4.7.2+)
- **UI Framework**: Windows Forms (WinForms)
- **JSON Serialization**: Newtonsoft.Json
- **IDE**: Visual Studio 2022+ / JetBrains Rider / .NET CLI

## 📂 Project Structure

```
├── TTMS OOP/             # WinForms Presentation Layer (Forms, Views, Helpers)
├── TTMS_OOP.BLL/         # Business Logic Layer (Managers, Validations, Scheduler)
├── TTMS_OOP.DAL/         # Data Access Layer (JSON Repositories)
├── TTMS_OOP.Models/      # Domain Models & Interfaces (Entities)
├── TTMS OOP.slnx         # Solution Configuration
└── .gitignore            # Git ignore rules for .NET/VS
```

## ⚙️ Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Shahzaib-Jan/TTMS.git
   ```
2. **Open the solution:**
   - Double-click `TTMS OOP.slnx` in Visual Studio 2022+, or
   - Build via CLI:
     ```bash
     dotnet build "TTMS OOP.slnx"
     ```
3. **Run the application:**
   - Press `F5` in Visual Studio or launch the built executable in `TTMS OOP/bin/Debug/`.

## 👨‍💻 Author

**Shahzaib Jan**  
GitHub: [@Shahzaib-Jan](https://github.com/Shahzaib-Jan)
