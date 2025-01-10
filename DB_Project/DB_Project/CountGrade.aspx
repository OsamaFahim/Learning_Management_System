<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CountGrade.aspx.cs" Inherits="CountGrade" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Count Grade</title>
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
        <h1>Count Grade</h1>
        <table>
            <thead>
                <tr>
                    <th>Section</th>
                    <th>Grade</th>
                    <th>Count</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>A</td>
                    <td>A+</td>
                    <td>5</td>
                </tr>
                <tr>
                    <td>A</td>
                    <td>A</td>
                    <td>10</td>
                </tr>
                <tr>
                    <td>A</td>
                    <td>B+</td>
                    <td>15</td>
                </tr>
                <!-- Add more grade count data as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>
