using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
public partial class StudentConfirmation : System.Web.UI.Page
{
    string name = "";
    Table courseTable;
    int enrolled_count = 0;
    int SID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        name = Request.QueryString["UserName"];

        // Create a new SqlConnection object with the connection string
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        // Open the SqlConnection
        con.Open();

        //Query to extract user id from the username
        SqlCommand cmd1 = new SqlCommand("select [User].UserID from [User] where [User].Username = '" + name + "';", con);

        SqlDataReader Reader = cmd1.ExecuteReader();


        if (Reader.Read())
        {
            SID = int.Parse(Reader["UserID"].ToString());
        }

        Reader.Close();

        if (SID != -1)
        {
            SqlCommand cmd3 = new SqlCommand("select count(*) as Total_Enrolled from StudentCourse where StudentCourse.StudentID = '" + SID + "' and StudentCourse.enrolled is not null group by StudentCourse.StudentID;", con);
            SqlDataReader Reader2 = cmd3.ExecuteReader();


            if (Reader2.HasRows)
            {
                if (Reader2.Read())
                    enrolled_count = (int)Reader2["Total_Enrolled"];
            }

            courseTable = new Table();

            Reader2.Close();

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
            using (SqlCommand cmd = new SqlCommand("select StudentCourse.CourseID,Courses.CourseCode,Courses.CourseName,Courses.CreditHours from StudentCourse " +
                                                        "join Courses on Courses.CourseID = StudentCourse.CourseID where StudentCourse.StudentID = '" + SID + "' and StudentCourse.enrolled is null;", con))
            {
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
            Reader.Close();
            phTable.Controls.Add(courseTable);

            SqlCommand cmd6 = new SqlCommand("select StudentCourse.CourseID,Courses.CourseCode,Courses.CourseName,Courses.CreditHours from StudentCourse " +
                                     "join Courses on Courses.CourseID = StudentCourse.CourseID where StudentCourse.StudentID = '" + SID + "' and StudentCourse.enrolled is not null;", con);

            // Execute the SqlCommand and store the results in a SqlDataReader object
            SqlDataReader reader6 = cmd6.ExecuteReader();


            Table Show_Courses = new Table();

            TableRow headerRows2 = new TableRow();

            // Create a new table cell for each column in the result set
            TableCell idHeaderCell2 = new TableCell();
            idHeaderCell2.Text = "Course ID";
            idHeaderCell2.Font.Bold = true;
            headerRows2.Cells.Add(idHeaderCell2);

            TableCell codeHeaderCell2 = new TableCell();
            codeHeaderCell2.Text = "Course Code";
            codeHeaderCell2.Font.Bold = true;
            headerRows2.Cells.Add(codeHeaderCell2);

            TableCell nameHeaderCell2 = new TableCell();
            nameHeaderCell2.Text = "Course Name";
            nameHeaderCell2.Font.Bold = true;
            headerRows2.Cells.Add(nameHeaderCell2);

            TableCell hoursHeaderCell2 = new TableCell();
            hoursHeaderCell2.Text = "Credit Hours";
            hoursHeaderCell2.Font.Bold = true;
            headerRows2.Cells.Add(hoursHeaderCell2);

            // Add the header row to the table
            Show_Courses.Rows.Add(headerRows2);


            // Loop through each row in the SqlDataReader object
            while (reader6.Read())
            {
                // Create a new TableRow object for the current row
                TableRow row = new TableRow();

                // Create new TableCell objects for each cell in the current row
                TableCell IDCell = new TableCell();
                IDCell.Text = reader6["CourseID"].ToString();
                row.Cells.Add(IDCell);

                TableCell codeCell = new TableCell();
                codeCell.Text = reader6["CourseCode"].ToString();
                row.Cells.Add(codeCell);

                TableCell nameCell = new TableCell();
                nameCell.Text = reader6["CourseName"].ToString();
                row.Cells.Add(nameCell);

                TableCell creditHoursCell = new TableCell();
                creditHoursCell.Text = reader6["CreditHours"].ToString();
                row.Cells.Add(creditHoursCell);

                Show_Courses.Rows.Add(row);

            }
            phTable1.Controls.Add(Show_Courses);
            reader6.Close();
        }

    }

    protected void Checkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;

        // If the checkbox was checked, increment the count and check if we've hit the limit
        if (checkbox.Checked == true)
        {
            enrolled_count++;

            if (enrolled_count > 6)
            {
                lblErrorMessage.Text = "Cannot Register for more than six courses at a time.";
                checkbox.Checked = false;
                enrolled_count--;
                return;
            }
        }
        // If the checkbox was unchecked, decrement the count
        else
        {
            enrolled_count--;
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

        if (checkedCount > 6)
        {
            lblErrorMessage.Text = "Cannot Register for more than six courses at a time.";
            checkbox.Checked = false;
            enrolled_count--;
            return;
        }
        else
            lblErrorMessage.Text = "";
    }

 protected void SubmitButton_Click(object sender, EventArgs e)
        {
        int RowsAffected = 0;
        if (SID != -1)
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

                            SqlCommand cmd = new SqlCommand("update StudentCourse set Enrolled = '" + 1 + "' where CourseID = '" + CID + "' and StudentID = '" + SID + "';", con);
                            RowsAffected = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Close the SqlConnection
            con.Close();

            // Check if any rows were affected by the update query
            if (name != "")
            {
                Response.Redirect("~/StudentConfirmation.aspx?UserName=" + name);
                lblErrorMessage.Text = "";
            }
        }
    }
}