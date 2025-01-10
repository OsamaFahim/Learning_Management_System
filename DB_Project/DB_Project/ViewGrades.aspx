<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewGrades.aspx.cs" Inherits="ViewGrades" %>

<!DOCTYPE html>
<html>
<head>
    <title>View Marks</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
      <script>
        function hideErrorMessage() {
            var category = document.getElementById('CategoryID').value;
            var weightage = document.getElementById('WeightageID').value;

            if (category != 'Category' && category != '' && weightage != '' && weightage != 'Weightage') {
                document.getElementById('lblErrorMessage').style.display = 'none';
            }

        }
      </script>
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

         #lblErrorMessage {
               color: darkred; /* light green color */
               font-size: 1.2em; /* increase font size */
                 font-weight: bold; /* make the text bold */
                text-align: center; /* center align the text */
                 margin: 10px 0; /* add some margin around the label */
              margin-top: 10px;
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
        <div class ="container">     

        <h2>Your Grades</h2>


             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
    BorderColor="#DEDFDE" BorderWidth="1px"   
    CellPadding="4" ForeColor="Black" Width="100%" GridLines="None" 
    CssClass="gridViewStyle">
    <AlternatingRowStyle BackColor="#f2f2f2" />  
    <HeaderStyle BackColor="#0066ff" ForeColor="Black" />
    <RowStyle CssClass="rowStyle" />
    <Columns>
       <asp:TemplateField HeaderText="CourseID" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="CourseID" runat="server" Text='<%# Bind("CourseID") %>' CssClass="tableLabel"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      
       <asp:TemplateField HeaderText="CourseName" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="CourseName" runat="server" Text='<%# Bind("CourseName") %>' CssClass="tableLabel"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

        <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Grade") %>' CssClass="tableLabel"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

      
    </Columns>
</asp:GridView>

            </div>
    </form>
</body>
</html>