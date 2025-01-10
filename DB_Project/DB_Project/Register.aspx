<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color:antiquewhite
        }

        .register-container {
            width: 300px;
            margin: 0 auto;
            padding: 20px;
            background-color: #F0F0F0;
            border: 1px solid #B0B0B0;
            border-radius: 5px;
        }

        .register-form label {
            display: block;
            margin: 10px 0;
        }

        .register-form input[type="text"],
        .register-form input[type="password"] {
            width: 100%;
            padding: 5px;
        }

        .register-form input[type="submit"] {
            margin-top: 10px;
            padding: 5px 10px;
            background-color: #3399FF;
            border: none;
            color: white;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Register</h1>
        <div class="register-container">
            <div class="register-form">
                <label for="username">Username:</label>
                <asp:TextBox ID="username" runat="server" type="text"></asp:TextBox>

                <label for="password">Password:</label>
                <asp:TextBox ID="password" runat="server" type="password"></asp:TextBox>

                <label for="confirmPassword">Confirm Password:</label>
                <asp:TextBox ID="confirmPassword" runat="server" type="password"></asp:TextBox>

                <asp:Button ID="registerButton" runat="server" Text="Register" OnClick="registerButton_Click" />
                <asp:Label ID="registerError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
