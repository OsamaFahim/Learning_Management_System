<!-- ConfirmationForm.aspx -->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmationForm.aspx.cs" Inherits="ConfirmationForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirmation Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Enter CAPTCHA" OnClick="Button1_Click" />
            <p>I am a not a robot.</p>
        </div>
    </form>
</body>
</html>
