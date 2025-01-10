<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendenceSheetReport.aspx.cs" Inherits="AttendenceSheetReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendance Sheet Report</title>
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
        <h1>Attendance Sheet Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Registration No.</th>
                    <th>Name</th>
                    <th>Attendance</th>
                    <th>Percentage</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>21i-0391</td>
                    <td>Muhaddis Farooq</td>
                    <td>40/50</td>
                    <td>80%</td>
                </tr>
                
                <tr>
                    <td>21i-0439</td>
                    <td>Osama Fahim</td>
                    <td>0/50</td>
                    <td>0%</td>
                </tr>
                <!-- Add more students and attendance data as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>
