<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcMenu.aspx.cs" Inherits="AcMenu" %>

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
        <h1>Academic Office Menu</h1>
        <div class="menu-container">
             <asp:HyperLink ID="OfferCourses" runat="server" NavigateUrl="~/RegisterCourse.aspx" CssClass="menu-item">Offer Courses</asp:HyperLink>
             <asp:HyperLink ID="addnewcourseslink" runat="server" NavigateUrl="~/AddNewCourse.aspx" CssClass="menu-item">Add New Course</asp:HyperLink>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CourseAllocationAC.aspx" CssClass="menu-item">Allocate Courses</asp:HyperLink>
            <asp:HyperLink ID="ManageSections1" runat="server" NavigateUrl="~/SectionsAC.aspx" CssClass="menu-item">Students Sections</asp:HyperLink>
            <asp:HyperLink ID="ManageSections2" runat="server" NavigateUrl="~/SectionsAC(2).aspx" CssClass="menu-item">Teachers Sections</asp:HyperLink>
        </div>
    </form>
</body>
</html>

