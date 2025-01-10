# Learning Management System (LMS)

## Overview

The Learning Management System (LMS) is a web-based application built using the .NET Framework, C#, and SQL Server. It is designed to provide a comprehensive platform for managing educational content, student registrations, course materials, quizzes, grades, and communication between instructors and students.

### Features
- **User Authentication**: Admin, Instructor, and Student roles with personalized dashboards.
- **Course Management**: Admin can create, update, and delete courses and materials.
- **Student Enrollment**: Students can enroll in courses and access course content.
- **Quizzes and Assignments**: Instructors can create quizzes, assignments, or any evaluation that they want and track student performance.
- **Grade Tracking**: Students can view their grades and progress in various courses.
- **Discussion Boards**: Forums for students and instructors to discuss course content.

## Tech Stack

- **Backend**: .NET Framework, C#
- **Frontend**: HTML, CSS, JavaScript (using Bootstrap for responsive design)
- **Database**: SQL Server (used for storing user data, courses, quizzes, and grades)
- **Authentication**: ASP.NET Identity for secure user authentication

## Installation

To run the LMS application locally, follow these steps:

### Prerequisites
- .NET Framework (version 3.8 or higher)
- SQL Server (or SQL Server Express) installed locally or remotely
- Visual Studio 2019 or later

### Steps
1. Clone this repository to your local machine:
   ```bash
   git clone https://github.com/OsamaFahim/Learning-Management-System.git

2. Open the Project in visual studio and change the connection strings in the project to your connection strings

**Note**: The database schema is currently missing, so the application may not run as intended. To use this project, recreate the necessary tables based on the code logic.


