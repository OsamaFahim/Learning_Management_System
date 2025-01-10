<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentFeedbackReport.aspx.cs" Inherits="StudentFeedbackReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students Feedback Report</title>
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
        <h1>Students Feedback Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Student Name</th>
                     <th>Teacher Name</th>
                    <th>Feedback</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Muhaddis Farooq</td>
                    <td>Sir Aleem</td>
                    <td>Great teacher, very helpful and knowledgeable.</td>
                </tr>
                <tr>
                    <td>Osama Fahim</td>
                    <td>Mam Parisa Salma</td>
                    <td>Provides clear explanations and is best oop teacher my favourite.</td>
                </tr>
                <!-- Add more student feedback as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>
