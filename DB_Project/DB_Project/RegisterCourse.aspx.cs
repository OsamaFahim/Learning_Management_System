using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegisterCourse : System.Web.UI.Page
{
    // Create a new Table control
    Table CourseTable = new Table();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a new SqlConnection object with the connection string
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        // Create a new SqlCommand object with the query and the SqlConnection object
        SqlCommand cmd = new SqlCommand("select courses.CourseID,CourseCode,CourseName,CreditHours from courses", con);

        // Open the SqlConnection
        con.Open();

        // Execute the SqlCommand and store the results in a SqlDataReader object
        SqlDataReader reader = cmd.ExecuteReader();

        // Create a new TableRow object for the table header
        TableRow headerRow = new TableRow();

        // Create new TableHeaderCell objects for the table header
        TableHeaderCell IDHeader = new TableHeaderCell();
        IDHeader.Text = "Course ID";
        headerRow.Cells.Add(IDHeader);

        // Create new TableHeaderCell objects for the table header
        TableHeaderCell codeHeader = new TableHeaderCell();
        codeHeader.Text = "Course Code";
        headerRow.Cells.Add(codeHeader);

        TableHeaderCell nameHeader = new TableHeaderCell();
        nameHeader.Text = "Course Name";
        headerRow.Cells.Add(nameHeader);

        TableHeaderCell creditHoursHeader = new TableHeaderCell();
        creditHoursHeader.Text = "Credit Hours";
        headerRow.Cells.Add(creditHoursHeader);

        // Add the table header row to the table
        CourseTable.Rows.Add(headerRow);

        // Loop through each row in the SqlDataReader object
        while (reader.Read())
        {
            // Create a new TableRow object for the current row
            TableRow row = new TableRow();

            // Create new TableCell objects for each cell in the current row
            TableCell IDCell = new TableCell();
            IDCell.Text = reader["CourseID"].ToString();
            row.Cells.Add(IDCell);

            TableCell codeCell = new TableCell();
            codeCell.Text = reader["CourseCode"].ToString();
            row.Cells.Add(codeCell);

            TableCell nameCell = new TableCell();
            nameCell.Text = reader["CourseName"].ToString();
            row.Cells.Add(nameCell);

            TableCell creditHoursCell = new TableCell();
            creditHoursCell.Text = reader["CreditHours"].ToString();
            row.Cells.Add(creditHoursCell);

            // Add a new TableCell for the checkbox
            TableCell checkboxCell = new TableCell();

            // Create a new checkbox control and set its ID to the course ID
            CheckBox cb = new CheckBox();
            cb.ID = reader["CourseID"].ToString();

            // Set the AutoPostBack property to true
            cb.AutoPostBack = true;

            // Set the OnCheckedChanged attribute and specify the name of the method to handle the event
            cb.CheckedChanged += new EventHandler(cb_CheckedChanged);


            // Add the checkbox to the cell and the cell to the row
            checkboxCell.Controls.Add(cb);

            row.Cells.Add(checkboxCell);


            // Add the current row to the table
            CourseTable.Rows.Add(row);
        }
        reader.Close();
        SqlCommand cmd2= new SqlCommand("select [user].userid from [user] where [User].Role = 'Student'", con);

        // Execute the SqlCommand and store the results in a SqlDataReader object
        SqlDataReader reader2 = cmd2.ExecuteReader();

        if (reader2.HasRows)
        {
            // Loop through each row in the SqlDataReader object
            while (reader2.Read())
            {
                // Add a new ListItem to the dropdown list for each row in the SqlDataReader
                StudentID.Items.Add(new ListItem(reader2["UserID"].ToString()));
            }
        }
        // Close the SqlDataReader and the SqlConnection
        reader2.Close();
        con.Close();

        // Close the SqlDataReader and the SqlConnection

        // Add the table to the web page
        form1.Controls.Add(CourseTable);

    }

    protected void cb_CheckedChanged(object sender, EventArgs e)
    {
     
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        // Create a new SqlConnection object with the connection string
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        int insertion_counter = 0;

        // Open the SqlConnection
        con.Open();

        // Loop through each row in the CourseTable
        foreach (TableRow row in CourseTable.Rows)
        {
            // Check if there are cells in the current row
            if (row.Cells.Count > 0)
            {
                // Get the last cell in the row
                TableCell lastCell = row.Cells[row.Cells.Count - 1];

                // Check if the last cell contains any controls
                if (lastCell.Controls.Count > 0)
                {
                    // Get the checkbox control for the current row
                    CheckBox cb = (CheckBox)lastCell.Controls[0];

                    // Check if the checkbox is ticked
                    if (cb.Checked)
                    {
                        // Get the course ID for the current row
                        int CID = int.Parse(row.Cells[0].Text);
                        int SID = int.Parse(StudentID.SelectedValue);

                        SqlCommand cmd = new SqlCommand("INSERT INTO StudentCourse (StudentID, CourseID) VALUES ('" + SID + "','" + CID + "');", con);
                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            insertion_counter++;
                        }
                        else
                        {
                            // Handle the case where the query did not execute successfully
                        }

                    }
                }
            }
        }

        if(insertion_counter > 0)
        {
             Response.Redirect("AcMenu.aspx");
        }

        // Close the SqlConnection
        con.Close();
    }

}