<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvaluationReport.aspx.cs" Inherits="EvaluationReport" %>


<!--FIRST WE WILL MAKE MARKS DISTRITBUTION PAGE LOGIC PHIR IS MEI DAALEIN GE ABHI SIRF FRONTEND HAI YE HARDCODED--->



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluation Report</title>
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
        <h1>Evaluation Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Assignment</th>
                    <th>Final Exam</th>
                    <th>Quizzes</th>
                    <th>Sessional-I</th>
                    <th>Sessional-II</th>
                    <th>Total</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Muhaddis Farooq</td>
                    <td>13</td>
                    <td>25</td>
                    <td>10</td>
                    <td>10</td>
                    <td>12</td>
                    <td>70</td>
                </tr>
               
                <!-- Add more students and evaluation data as needed -->
            </tbody>
        </table>
    </form>
</body>
</html>

