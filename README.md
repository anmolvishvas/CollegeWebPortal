# College Web Portal

A complete **ASP.NET Web Forms (.NET Framework 4.8)** mini-project for a university
syllabus. It demonstrates Web Forms, server controls, validation, state management,
master pages, themes, ADO.NET, LINQ, AJAX, stored procedures and role-based
authentication with a clean, layered architecture.

> **Project type:** ASP.NET **Web Site** project (uses `App_Code` + `CodeFile`).
> No Entity Framework — data access is **ADO.NET only**.

---

## Technology Stack

| Layer            | Technology                                   |
|------------------|----------------------------------------------|
| Backend          | ASP.NET Web Forms, C# (.NET Framework 4.8)   |
| Frontend         | HTML5, CSS3, Bootstrap 5, JavaScript, jQuery |
| Database         | Microsoft SQL Server                         |
| Data Access      | ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter, DataReader, DataSet, Stored Procedures, parameterized queries) |
| IDE              | Visual Studio 2022                           |

Bootstrap 5, Bootstrap Icons and jQuery are loaded from a CDN, so **no NuGet
packages are required**.

---

## Folder Structure

```
CollegeWebPortal/                 (solution root)
├── CollegeWebPortal.sln
├── README.md
└── CollegeWebPortal/             (web site root)
    ├── Web.config                (connection string, auth, theme, errors, sessions)
    ├── Web.sitemap               (navigation + role security trimming)
    ├── Global.asax               (rebuilds role principal from auth cookie)
    ├── MasterPage.master         (logo, navbar menu, sidebar, breadcrumb, footer)
    ├── Login.aspx / Logout.aspx / Default.aspx / Error.aspx / NoticeDetail.aspx
    ├── App_Code/
    │   ├── BasePage.cs           (role-based page guard)
    │   ├── DAL/DBHelper.cs       (central ADO.NET helper)
    │   ├── BLL/*.cs              (business logic managers)
    │   ├── Models/Entities.cs    (POCO models)
    │   └── Helpers/              (SecurityHelper, CaptchaHelper, SessionHelper)
    ├── App_Themes/CollegeTheme/  (CollegeTheme.skin + CollegeTheme.css)
    ├── CSS/site.css
    ├── JS/site.js
    ├── Images/                   (logo.svg, avatar.svg, uploads/)
    ├── Admin/    (Dashboard, ManageStudents/Faculty/Departments/Courses/Subjects/Notices, ViewAttendance/Marks, Web.config)
    ├── Faculty/  (Dashboard, ManageAttendance, UploadMarks, SearchStudent, ViewNotices, Web.config)
    ├── Student/  (Dashboard, Profile, ViewAttendance, ViewMarks, ViewNotices, FeeStatus, ChangePassword, Web.config)
    └── Database/ (CollegePortalDB.sql, StoredProcedures.sql)
```

---

## Setup Instructions

### 1. Create the database
1. Open **SQL Server Management Studio (SSMS)**.
2. Open and execute `CollegeWebPortal/Database/CollegePortalDB.sql` (creates the
   `CollegePortalDB` database, all tables and sample data).
3. Open and execute `CollegeWebPortal/Database/StoredProcedures.sql` (creates the
   stored procedures).

### 2. Update the connection string
Open `CollegeWebPortal/Web.config` and set `Data Source` to your SQL Server
instance in the `CollegePortalDB` connection string, e.g.:

```xml
<add name="CollegePortalDB"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=CollegePortalDB;Integrated Security=True;TrustServerCertificate=True"
     providerName="System.Data.SqlClient" />
```

Common `Data Source` values: `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`, `localhost`.

### 3. Open and run in Visual Studio 2022
- **Option A:** Double-click `CollegeWebPortal.sln`.
- **Option B:** In Visual Studio: `File > Open > Web Site...` and pick the inner
  `CollegeWebPortal` folder.

Then press **F5** (or **Ctrl+F5**) to launch with IIS Express. The site opens on
`Default.aspx`, which redirects to the login page.

> Requires the **ASP.NET and web development** workload with **.NET Framework 4.8**
> targeting pack installed in Visual Studio 2022.

---

## Demo Logins

| Role    | Username | Password      |
|---------|----------|---------------|
| Admin   | `admin`  | `Admin@123`   |
| Faculty | `jsmith` | `Faculty@123` |
| Faculty | `rkumar` | `Faculty@123` |
| Student | `CS2101` | `Student@123` |
| Student | `CS2102` | `Student@123` |
| Student | `EC2101` | `Student@123` |

A **captcha** is shown on the login page and must be entered to sign in.
Passwords are stored as **SHA-256 hashes** (salted). The C# hashing in
`App_Code/Helpers/SecurityHelper.cs` matches the SQL `HASHBYTES` expression used
when the sample users are inserted.

---

## Syllabus Concepts Demonstrated

- **Server controls:** TextBox, Label, Button, DropDownList, GridView, DetailsView,
  FormView, FileUpload, Calendar, CheckBox, RadioButtonList, HyperLink, Menu,
  SiteMapPath.
- **Validation controls:** RequiredFieldValidator, CompareValidator,
  RegularExpressionValidator, RangeValidator, CustomValidator, ValidationSummary
  (client- and server-side).
- **State management:** ViewState (edit state on manage pages), Session (login),
  Cookies (Remember Me + forms-auth role), QueryString (`NoticeDetail.aspx?id=`).
- **Master page & navigation:** `MasterPage.master`, `Web.sitemap`, Menu control,
  breadcrumb, role-based sidebar.
- **Themes:** `App_Themes/CollegeTheme` with a skin file + CSS, applied globally
  via `styleSheetTheme`.
- **ADO.NET:** `DBHelper` uses SqlConnection, SqlCommand, SqlDataAdapter,
  DataReader, DataSet, stored procedures and parameterized queries. Connection
  string lives in `Web.config`.
- **Stored procedures:** InsertStudent, UpdateStudent, DeleteStudent,
  SearchStudent, InsertFaculty, InsertAttendance, InsertMarks, InsertNotice.
- **LINQ:** searching/sorting students, filtering attendance, searching notices.
- **AJAX:** ScriptManager, UpdatePanel (captcha refresh, student search) and Timer
  (live dashboard clock) for partial page updates.
- **Captcha:** random code generated server-side and validated before login.
- **Error handling:** try/catch throughout, friendly `Error.aspx`, custom errors
  in `Web.config`; raw SQL exceptions are never shown to the user.
- **Role-based authentication:** Forms authentication + per-folder `Web.config`
  authorization (Admin/Faculty/Student) and a `BasePage` guard.

---

## Modules

- **Admin:** dashboard, manage students (add/update/delete/search), faculty,
  departments, courses, subjects, notices; view attendance and marks reports.
- **Faculty:** dashboard, mark attendance (calendar + present/absent), upload
  marks, search students, view notices.
- **Student:** dashboard, profile, attendance, marks, notices, fee status,
  change password.

---

## Notes
- When a student/faculty is added by the Admin, a login is auto-created
  (student username = Roll No, default password `Student@123`; faculty username =
  first-initial + last-name, default `Faculty@123`).
- Uploaded notice attachments are saved under `Images/uploads/`.
- Session timeout is 30 minutes (see `Web.config`).
