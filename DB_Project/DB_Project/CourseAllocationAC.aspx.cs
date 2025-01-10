using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class CourseAllocationAC : System.Web.UI.Page
{
    Table courseTable = new Table();
    int allocated_count = 0;
    int FID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a new SqlConnection object with the connection string
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        //Opening connection
        con.Open();

        SqlCommand cmd2 = new SqlCommand("select [user].userid,[user].UserName from [user] where [User].Role = 'Faculty'", con);

        // Execute the SqlCommand and store the results in a SqlDataReader object
        SqlDataReader reader2 = cmd2.ExecuteReader();

        if (reader2.HasRows)
        {
            // Loop through each row in the SqlDataReader object
            while (reader2.Read())
            {
                // Add a new ListItem to the dropdown list for each row in the SqlDataReader
                string username = reader2["Username"].ToString();
                string userId = reader2["UserID"].ToString();
                string listItemText = $"{userId} ( {username})";
                InstructorID.Items.Add(new ListItem(listItemText));
            }
        }

        // Get the selected value from the InstructorID control
        string selectedValue = InstructorID.SelectedValue;

        // Split the selected value on the first space character
        string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Parse the first part of the string as an integer
        FID = int.Parse(parts[0]);

        reader2.Close();

        SqlCommand cmd4 = new SqlCommand("select count(*) from FacultyCourses where FacultyCourses.facultyid = '" + FID + "';", con);
        SqlDataReader reader4 = cmd4.ExecuteReader();

        if (reader4.HasRows)
        {
            if (reader4.Read())
                allocated_count = reader4.GetInt32(0);
        }

        reader4.Close();

        if (FID != -1)
        {

            courseTable = new Table();

            // Create a new table row for the header row
            TableRow headerRow = new TableRow();

            // Create a new table cell for each column in the result set
            TableCell idHeaderCell = new TableCell();
            idHeaderCell.Text = "Course ID";
            idHeaderCell.Font.Bold = true;
            headerRow.Cells.Add(idHeaderCell);

            TableCell codeHeaderCell = new TableCell();
            codeHeaderCell.Text = "Course Code";
            codeHeaderCell.Font.Bold = true;
            headerRow.Cells.Add(codeHeaderCell);

            TableCell nameHeaderCell = new TableCell();
            nameHeaderCell.Text = "Course Name";
            nameHeaderCell.Font.Bold = true;
            headerRow.Cells.Add(nameHeaderCell);

            TableCell hoursHeaderCell = new TableCell();
            hoursHeaderCell.Text = "Credit Hours";
            hoursHeaderCell.Font.Bold = true;
            headerRow.Cells.Add(hoursHeaderCell);

            // Add the header row to the table
            courseTable.Rows.Add(headerRow);


            // Execute the query and create a new table row for each result
            using (SqlCommand cmd = new SqlCommand("SELECT StudentCourse.CourseID,CourseCode,CourseName,CreditHours,count(*) as TEN_OR_MORE_ENROLLMENTS\r\nFROM StudentCourse \r\nJOIN Courses ON Courses.CourseID = StudentCourse.CourseID \r\nWHERE StudentCourse.enrolled IS NOT NULL \r\nGROUP BY StudentCourse.CourseID, Courses.CourseCode, Courses.CourseName, Courses.CreditHours \r\nHAVING COUNT(*) > 10 \r\n;", con))
            {
                // Execute the SqlCommand and store the results in a SqlDataReader object
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new table row for this result
                        TableRow row = new TableRow();

                        // Create a new table cell for each column in the result set
                        TableCell idCell = new TableCell();
                        idCell.Text = reader["CourseID"].ToString();
                        row.Cells.Add(idCell);

                        TableCell codeCell = new TableCell();
                        codeCell.Text = reader["CourseCode"].ToString();
                        row.Cells.Add(codeCell);

                        TableCell nameCell = new TableCell();
                        nameCell.Text = reader["CourseName"].ToString();
                        row.Cells.Add(nameCell);

                        TableCell hoursCell = new TableCell();
                        hoursCell.Text = reader["CreditHours"].ToString();
                        row.Cells.Add(hoursCell);

                        // Add a checkbox column to this row
                        TableCell checkboxCell = new TableCell();
                        CheckBox checkbox = new CheckBox();
                        // Set the AutoPostBack property to true
                        checkbox.AutoPostBack = true;
                        checkbox.ID = reader["CourseID"].ToString();
                        checkboxCell.Controls.Add(checkbox);
                        row.Cells.Add(checkboxCell);

                        // Add the row to the table
                        courseTable.Rows.Add(row);
                    }
                }
            }

        // Handle the CheckedChanged event for each checkbox to update the checkbox state and count
        foreach (TableRow row in courseTable.Rows)
        {
            if (row.Cells.Count > 0 && row.Cells[row.Cells.Count - 1].Controls.Count > 0)
            {
                CheckBox checkbox = (CheckBox)row.Cells[row.Cells.Count - 1].Controls[0];
                checkbox.CheckedChanged += new EventHandler(Checkbox_CheckedChanged);
            }
        }

        // Add the table to the form
        phTable.Controls.Add(courseTable);
    }
}

    protected void Checkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;

        // If the checkbox was checked, increment the count and check if we've hit the limit
        if (checkbox.Checked == true)
        {
            allocated_count++;

            if (allocated_count > 3)
            {
                lblErrorMessage.Text = "An Instructor cannot be allocated to more than three Courses at one time.";
                checkbox.Checked = false;
                allocated_count--;
                return;
            }
        }
        // If the checkbox was unchecked, decrement the count
        else
        {
            allocated_count--;
            lblErrorMessage.Text = "";
        }

        // Check if the total number of checked checkboxes exceeds the maximum allowed value
        int checkedCount = 0;
        foreach (TableRow row in courseTable.Rows)
        {
            if (row.Cells.Count > 0 && row.Cells[row.Cells.Count - 1].Controls.Count > 0)
            {
                CheckBox cb = (CheckBox)row.Cells[row.Cells.Count - 1].Controls[0];
                if (cb.Checked)
                {
                    checkedCount++;
                }
            }
        }

        if (checkedCount > 3)
        {
            lblErrorMessage.Text = "An Instructor cannot be allocated to more than three Courses at one time.";
            checkbox.Checked = false;
            allocated_count--;
            return;
        }
        else
            lblErrorMessage.Text = "";
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        int RowsAffected = 0;
        if (FID != -1)
        {
            // Create a new SqlConnection object with the connection string
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

            // Open the SqlConnection
            con.Open();

            // Loop through each row in the CourseTable
            foreach (TableRow row in courseTable.Rows)
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

                            SqlCommand cmd = new SqlCommand("insert into FacultyCourses(facultyid,courseid) values ('" + FID + "','" + CID + "');", con);
                            RowsAffected = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Close the SqlConnection
            con.Close();

            Response.Redirect("~/CourseAllocationAC.aspx");
           
            lblErrorMessage.Text = "";
        }
    }
}