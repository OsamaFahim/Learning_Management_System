<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GradeReport.aspx.cs" Inherits="GradeReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grade Report</title>
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
        <h1>Grade Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Registration No.</th>
                    <th>Name</th>
                    <th>Section</th>
                    <th>Marks</th>
                    <th>Grade</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>21i-0391</td>
                    <td>Muhaddis Farooq</td>
                    <td>G</td>
                    <td>76</td>
                    <td>B</td>
                </tr>
                <tr>
                    <td>21i-0439</td>
                    <td>Osama Fahim</td>
                    <td>G</td>
                    <td>100</td>
                    <td>A+</td>
                </tr>
                <!-- Add more students and grade data as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>
