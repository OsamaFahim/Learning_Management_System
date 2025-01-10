<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfferedCourseReport.aspx.cs" Inherits="OfferedCourseReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Offered Courses Report</title>
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
        <h1>Offered Courses Report</h1>
        <table>
            <thead>
                <tr>
                    <th>Course Code</th>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>CS101</td>
                    <td>Introduction to Computer Science</td>
                    <td>3</td>
                </tr>
                <tr>
                    <td>CS102</td>
                    <td>Programming Fundamentals</td>
                    <td>4</td>
                </tr>
               <tr>
                    <td>CS2005</td>
                    <td>Database Systems</td>
                    <td>3</td>
                </tr>
                  <tr>
                    <td>CL2005</td>
                    <td>Database Systems - Lab</td>
                    <td>1</td>
                </tr>
                 <tr>
                    <td>CS2006</td>
                    <td>Operating Systems</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>CS2009</td>
                    <td>Design and Analysis of Algorithm</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>MT2005</td>
                    <td>Probabilty And Statistics</td>
                    <td>3</td>
                </tr>
                  <tr>
                    <td>MG1002</td>
                    <td>Marketing Management</td>
                    <td>3</td>
                </tr>
                <tr>
                    <td>CS2001</td>
                    <td>Data Structures</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>EE2003</td>
                    <td>Computer Organization and Assembly Language</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>MT1004</td>
                    <td>Linear Algebra</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>CS1005</td>
                    <td>Discrete Structures</td>
                    <td>3</td>
                </tr>
                  <tr>
                    <td>CS1004</td>
                    <td>Object Oriented Programming</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>MT1006</td>
                    <td>Differential Equations</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>SS1003</td>
                    <td>Pakistan Studies</td>
                    <td>3</td>
                </tr>
                 <tr>
                    <td>SS1008</td>
                    <td>Communication and Presentation Skills</td>
                    <td>2</td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
