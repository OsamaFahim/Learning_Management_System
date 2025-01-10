<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewCourse.aspx.cs" Inherits="AddNewCourse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AddNewCourse</title>
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

        .submit-button {
            background-color: green;
            color: white;
            border: none;
            padding: 10px 24px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            transition-duration: 0.4s;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>AddNewCourse</h1>
        <div class ="newcourse-newcourse">
        <table>
            <thead>
                <tr>
                    <th>CourseID</th>
                    <th>CourseCode</th>
                    <th>CourseName</th>
                    <th>CHs</th>
                    <th>PreRequisteID</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><asp:TextBox ID="CID" runat="server" OnTextChanged="TextBox1_TextChanged">Enter CID</asp:TextBox></td>
                    <td><asp:TextBox ID="CCode" runat="server" OnTextChanged="TextBox2_TextChanged">Enter CCode</asp:TextBox></td>
                    <td><asp:TextBox ID="CName" runat="server" OnTextChanged="TextBox3_TextChanged">Enter CName</asp:TextBox></td>
                    <td><asp:TextBox ID="CHS" runat="server">Enter CHs</asp:TextBox></td>
                    <td><asp:TextBox ID="PREID" runat="server">Enter Pre ID</asp:TextBox></td>
                </tr> 
            </tbody>
        </table>
        <br/>
        <asp:Button ID="Button1" runat="server" CssClass="submit-button" OnClick="Button1_Click" Text="Submit" />
    </form>
    </body>
</html>



