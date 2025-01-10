<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FcMenu.aspx.cs" Inherits="FcMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faculty Menu</title>
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

        .menu {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 50px;
        }

        .menu-item {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            margin-bottom: 10px;
            text-decoration: none;
            font-size: 18px;
            border-radius: 5px;
        }

        .menu-item:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Faculty Menu</h1>
        <div class="menu">
            <asp:Button ID="Button1" runat="server" Text="Take Attendance" CssClass="menu-item" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Enter Marks Distribution" CssClass="menu-item" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="Evaluate Students" CssClass="menu-item" OnClick="Button3_Click" />
            <asp:Button ID="Button4" runat="server" Text="View Feedbacks" CssClass="menu-item" OnClick="Button4_Click" />
        </div>
    </form>
</body>
</html>
