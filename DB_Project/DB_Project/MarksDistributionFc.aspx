<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarksDistributionFc.aspx.cs" Inherits="MarksDistributionFc" %>

<!DOCTYPE html>
<html>
<head>
    <title>Add Marks Distribution</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
    color: darkred;
    font-size: 1.2em;
    font-weight: bold;
    text-align: center;
    margin: 10px 0;
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

        <h2>Select CourseName</h2>

            <div class="select-Faculty">
                <table>
                    <tr>
                        <td>CourseNameID</td>
                        <td>
                          
                         <asp:DropDownList ID="CourseNameID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CourseNameID_SelectedIndexChanged"></asp:DropDownList>

                    </td>
                  
                </tr>
                </table>
            </div>

             <h2>Select Section</h2>

                <div class="select-Faculty">
                <table>
                    <tr>
                        <td>SelectSectionID</td>
                        <td>
                          
                         <asp:DropDownList ID="SectionID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SectionID_SelectedIndexChanged"></asp:DropDownList>

                    </td>
                  
                </tr>
                </table>
            </div>


            <h2>Current Distributions</h2>

  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
    BorderColor="#DEDFDE" BorderWidth="1px"   
    CellPadding="4" ForeColor="Black" Width="100%" GridLines="None" 
    CssClass="gridViewStyle">
    <AlternatingRowStyle BackColor="#f2f2f2" />  
    <HeaderStyle BackColor="#0066ff" ForeColor="Black" />
    <RowStyle CssClass="rowStyle" />
    <Columns>
       <asp:TemplateField HeaderText="CategoryName" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="CategoryName" runat="server" Text='<%# Bind("Category") %>' CssClass="tableLabel"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      
       <asp:TemplateField HeaderText="Weightage" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Weightage") %>' CssClass="tableLabel"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

       <asp:TemplateField HeaderText="Option" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:DropDownList ID="DropDown1" runat="server" CssClass="tableDropDown"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
      
    </Columns>
</asp:GridView>

            <h2>Enter New Distribution</h2>

             <b>Enter Category : </b>
             <asp:TextBox ID="CategoryID" runat="server" Height="16px" OnTextChanged="TextBox1_TextChanged" Width="147px" onchange="hideErrorMessage()">Category</asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;
            <b>Enter Weightage : </b>
        <asp:TextBox ID="WeightageID" runat="server" Height="16px" OnTextChanged="TextBox2_TextChanged" Width="152px" onchange="hideErrorMessage()">Weightage</asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
              <div >
                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            </div>

             <div class="submit-button">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </div>

        </div>
    </form>
</body>
</html>