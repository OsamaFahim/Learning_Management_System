<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color:antiquewhite
        }

        .login-container {
            width: 300px;
            margin: 0 auto;
            padding: 20px;
            background-color: #F0F0F0;
            border: 1px solid #B0B0B0;
            border-radius: 5px;
        }

        .login-form label {
            display: block;
            margin: 10px 0;
        }

        .login-form input[type="text"],
        .login-form input[type="password"] {
            width: 100%;
            padding: 5px;
        }

        .login-form input[type="submit"] {
            margin-top: 10px;
            padding: 5px 10px;
            background-color: #3399FF;
            border: none;
            color: white;
            cursor: pointer;
        }
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Student Login</h1>
        <div class="login-container">
            <div class="login-form">
                <label for="username">Username:</label>
                <asp:TextBox ID="username" runat="server" type="text"></asp:TextBox>

                <label for="password">Password:</label>
                <asp:TextBox ID="password" runat="server" type="password"></asp:TextBox>

                <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
                <asp:Label ID="loginError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </div>
            <p>
                Don't have an account? <asp:HyperLink ID="registerLink" runat="server" NavigateUrl="~/Register.aspx">Register</asp:HyperLink>
            </p>
        
        </div>
    </form>
</body>
</html>
