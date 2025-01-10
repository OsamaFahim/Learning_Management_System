<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SectionsAC(2).aspx.cs" Inherits="SectionsAC_2_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Sections AC(2)</title>
    <style>
       body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f0f0f0;
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: white;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #4CAF50;
            font-size: 2em;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
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

        .input-field {
            margin: 0 10px;
            display: inline-block;
        }

        .input-field input {
            padding: 5px;
            border-radius: 3px;
            border: 1px solid #ddd;
        }

        .submit-button {
            margin-top: 20px;
            display: flex;
            justify-content: center;
        }

        .submit-button input[type="submit"] {
            background-color: #4CAF50;
            color: white;
            padding: 8px 20px;
            font-size: 1em;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            text-transform: uppercase;
        }

                /* Grid View Styles */
        .gridViewStyle {
            font-size: 14px;
            font-family: Arial, sans-serif;
        }

        .tableLabel {
    font-size: 14px;
    font-family: Arial, sans-serif;
    background-color: #f2f2f2;
} 

        /* Table Drop-Down Styles */
        .tableDropDown {
            padding: 5px;
            border-radius: 3px;
            border: 1px solid #ddd;
            font-size: 14px;
            font-family: Arial, sans-serif;
        }

        
        .rowStyle {
    background-color: white;
    }
    .gridViewStyle tr:hover {
        background-color: #ddd;
    }

    .gridViewStyle {
    font-size: 14px;
    font-family: Arial, sans-serif;
    }

    .gridViewStyle tr:nth-child(even) {
        background-color: #f2f2f2;
    }

    .gridViewStyle tr:nth-child(odd) {
        background-color: white;
    }

    .gridViewStyle th {
        background-color: white;
    }

    .gridViewStyle caption {
    background-color: #f2f2f2;
    color: #4CAF50;
    font-size: 2em;
    margin-bottom: 20px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="container">
        <h2>Select Faculty</h2>

            <div class="select-Faculty">
                <table>
                    <tr>
                        <td>FacultyID</td>
                        <td>
                          
                         <asp:DropDownList ID="FacultyID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FacultyID_SelectedIndexChanged"></asp:DropDownList>

                    </td>
                  
                </tr>
                </table>
            </div>

         <h2>Allocated Courses</h2>

  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
    BorderColor="#DEDFDE" BorderWidth="1px"   
    CellPadding="4" ForeColor="Black" Width="100%" GridLines="None" 
    CssClass="gridViewStyle">
    <AlternatingRowStyle BackColor="#f2f2f2" />  
    <HeaderStyle BackColor="#0066ff" ForeColor="Black" />
    <RowStyle CssClass="rowStyle" />
    <Columns>
        <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="CourseID" runat="server" Text='<%# Bind("Courseid") %>' CssClass="tableLabel"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("CourseCode") %>' CssClass="tableLabel"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CourseName") %>' CssClass="tableLabel"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Credit Hours" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("CreditHours") %>' CssClass="tableLabel"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Current Section" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SectionID") %>' CssClass="tableLabel"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Available Sections" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:DropDownList ID="DropDown1" runat="server" CssClass="tableDropDown"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


       <div class="submit-button">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </div>

        </div>
    </form>
</body>
</html>