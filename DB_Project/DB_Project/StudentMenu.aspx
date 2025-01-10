<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentMenu.aspx.cs" Inherits="StudentMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Office Menu</title>
    <style>
        body {
            font-family: Roboto;
            margin: 0;
            padding: 0;
            background-color: #f0f0f0;
        }

        h1 {
            text-align: center;
            padding: 20px;
            background-color: #4CAF50;
            color: white;
        }

        .menu-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 50px;
        }

        .menu-item {
            background-color: #4CAF50;
            color: white;
            padding: 20px 40px;
            margin: 10px;
            text-align: center;
            text-decoration: none;
            font-size: 18px;
            border-radius: 5px;
            cursor: pointer;
        }

        .menu-item:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Student Menu</h1>
        <div class="menu-container">
             
            <asp:Button ID="Button1" runat="server" Text="Register Courses" CssClass="menu-item" OnClick="Button1_Click" />
             <asp:Button ID="Button2" runat="server" Text="Give Feedback" CssClass="menu-item" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="View Attendance" CssClass="menu-item" OnClick="Button3_Click" />
             <asp:Button ID="Button4" runat="server" Text="View Marks" CssClass="menu-item" OnClick="Button4_Click" />
             <asp:Button ID="Button5" runat="server" Text="View Grade" CssClass="menu-item" OnClick="Button5_Click" />
        </div>
    </form>
</body>
</html>


