<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu_select.aspx.cs" Inherits="Menu_select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /* Set the background color of the body */
        body {
            background-color:antiquewhite
        }

        /* Center the labels */
        .centered-labels {
            text-align: center;
        }

        /* Increase the font size to 60px and change the font family to Roboto */
        #Label1, #Label2, #Label3 {
            font-family: "Roboto", Roboto;
            font-size: 60px;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>&nbsp;</p>
        <div class="centered-labels">
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                <a href="AcademicOffice.aspx"><asp:Label ID="Label1" runat="server" ForeColor="#3399FF" Text="Academic Office"></asp:Label></a>
            </p>
            <p>
                &nbsp;<a href="Faculty.aspx"><asp:Label ID="Label2" runat="server" ForeColor="#3399FF" Text="Faculty"></asp:Label></a>
            </p>
            <p>
                <a href="Student.aspx"><asp:Label ID="Label3" runat="server" ForeColor="#3399FF" Text="Student"></asp:Label></a>
            </p>
        </div>
    </form>
</body>
</html>
