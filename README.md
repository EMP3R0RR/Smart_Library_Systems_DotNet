SmartLibraryMVC is a web-based library management system built with ASP.NET MVC 5, Entity Framework, and SQL Server, using a clean three-tier architecture (DAL–BLL–MVC). It supports role-based login, 
CRUD operations for books, users, and categories, ensuring data integrity and a responsive Razor-based interface.

What it does:
Users: Login, browse, borrow/return books (max 3 active), view profile & borrows
Admins: Full CRUD for books/users, manage all borrows, delete records, role-based dashboards
Real-time availability, fine tracking, due dates, duplicate borrow prevention
RESTful Web API for mobile apps or integrations

Tech Stack:
ASP.NET MVC5 + Web API 2
Entity Framework 6 (DB-First)
3-Layer Architecture (DAL → BLL → Presentation)
AutoMapper (Entity ↔ DTO)
Bootstrap + jQuery
Session & API Key Security

Key Features:
Dual Interface: MVC Views + JSON API
Borrow Rules: Max 3 books, 1-month due, Fine at missing return date.
Admin Oversight: Filter borrows by user/book
Ready for Google Books API integration (ISBN lookup)

Perfect for:
Learning layered architecture
Building internal tools
Extending into mobile/enterprise systems

![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Login.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Dashboard_User.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/book_details_user.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/BorrowedBooks.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Profile.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Change_password.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Admin_dashboard.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/BorrowRecords.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Book_details_admin.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/Edit_book_admin.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/All_users.PNG)
![image alt](https://github.com/EMP3R0RR/Smart_Library_Systems_DotNet/blob/26f591e0d541df0656f5e2a8a1f3905af68c9cb0/SmartLibrarySystems/users_per_book_admin.PNG)

