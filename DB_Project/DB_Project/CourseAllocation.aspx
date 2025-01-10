<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseAllocation.aspx.cs" Inherits="CourseAllocation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Allocation Report</title>
    <style>
        body {
            font-family: Roboto;
            margin: 0;
            padding: 0;
        }

        h1 {
            text-align: center;
            padding: 20px;
            background-color: #4CAF50;
            color: white;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            text-align: left;
            padding: 8px;
            border: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
            font-weight: bold;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr:hover {
            background-color: #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Course Allocation Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Course Code</th>
                    <th>Course Name</th>
                    <th>CHs</th>
                    <th>Section</th>
                    <th>Instructor</th>
                    <th>Coordinator</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>CS-XXX</td>
                    <td>Database Systems</td>
                    <td>3</td>
                    <td>CS-2B</td>
                    <td>Dr. Shujaat Hussain</td>
                    <td>Dr. Shujaat Hussain</td>
                </tr>
                 <tr>
                    <td>CS-XXX</td>
                    <td>Database Systems</td>
                    <td>3</td>
                    <td>CS-2C</td>
                    <td>Dr. Shujaat Hussain</td>
                    <td>Dr. Shujaat Hussain</td>
                </tr>
                <tr>
                    <td>CS-XXX</td>
                    <td>Database Systems</td>
                    <td>3</td>
                    <td>CS-2G</td>
                    <td>Ms. Noor ul Ain</td>
                    <td>Dr. Shujaat Hussain</td>
                </tr>
                 <tr>
                    <td>CS-XXX</td>
                    <td>Database Systems</td>
                    <td>3</td>
                    <td>CS-2D</td>
                    <td>Dr. Amna Basharat</td>
                    <td>Dr. Shujaat Hussain</td>
                </tr>
                <!-- Add more courses, sections, and instructors as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>


