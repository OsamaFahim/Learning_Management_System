<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentSection.aspx.cs" Inherits="StudentSection" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students Section Report</title>
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
        <h1>Students Section Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Registration No.</th>
                    <th>Student Name</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>19I-XXXX</td>
                    <td>A</td>
                </tr>
                <tr>
                    <td>19I-XXXX</td>
                    <td>B</td>
                </tr>
                <tr>
                    <td>20I-XXXX</td>
                    <td>C</td>
                </tr>
                  <tr>
                    <td>21I-XXXX</td>
                    <td>D</td>
                </tr>
                <!---Insert more names here if you need-->
            </tbody>
        </table>
    </form>
</body>
</html>
