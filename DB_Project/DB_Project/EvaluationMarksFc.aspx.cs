using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class EvaluationMarksFc : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;
    string SID = "";
    string StudentName = "";
    protected void CourseNameID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CourseNameID.SelectedValue != "")
        {
            courseName = CourseNameID.SelectedValue.ToString();
        }
    }

    protected void SectionID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SectionID.SelectedValue != "")
        {
            SID = SectionID.SelectedValue.ToString();
        }
    }

    protected void StudentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (StudentID.SelectedValue != "")
        {
            StudentName = StudentID.SelectedValue.ToString();
        }
    }

    void getFacultyID()
    {
        string name = Request.QueryString["UserName"];

        // Create a new SqlConnection object with the connection string
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        // Open the SqlConnection
        con.Open();

        //Query to extract user id from the username
        SqlCommand cmd1 = new SqlCommand("select [User].UserID from [User] where [User].Username = '" + name + "';", con);
        SqlDataReader Reader = cmd1.ExecuteReader();


        if (Reader.Read())
        {
            FID = int.Parse(Reader["UserID"].ToString());
        }

        Reader.Close();

    }

    protected void LoadCourseNames()
    {
        connection.Open();

        CourseNameID.Items.Add(new ListItem("Select Course"));
        getFacultyID();

        int courseID = -1;
        string Name = "";

        string query = "select FacultyCourses.courseid,Courses.CourseName from FacultyCourses join Courses on FacultyCourses.courseid = Courses.CourseID" +
                        "\r\nwhere FacultyCourses.SectionID is not null and FacultyCourses.facultyid = @FID;";

        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@FID", FID);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                courseID = int.Parse(reader["CourseID"].ToString());
                Name = reader["CourseName"].ToString();
                string listItemText = $"{courseID} ( {Name})";
                CourseNameID.Items.Add(listItemText);
            }
        }

        connection.Close();
    }

    protected void LoadSections(object sender, EventArgs e)
    {
        connection.Open();

        SectionID.Items.Clear();
        CourseNameID_SelectedIndexChanged(sender, e);

        // Get the selected value from the InstructorID control
        string selectedValue = null;
        selectedValue = CourseNameID.SelectedValue;

        if (selectedValue != "Select Course" && selectedValue != null)
        {

            // Split the selected value on the first space character
            string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse the first part of the string as an integer
            CID = int.Parse(parts[0]);

            SectionID.Items.Add(new ListItem("Select Section"));
            getFacultyID();

            string query = "select FacultyCourses.SectionID from FacultyCourses\r\nwhere FacultyCourses.courseid = @CourseID and FacultyCourses.facultyid = @FID;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                string secID = "";

                cmd.Parameters.AddWithValue("@courseid", CID);
                cmd.Parameters.AddWithValue("@FID", FID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    secID = reader["SectionID"].ToString();
                    SectionID.Items.Add(new ListItem(secID));
                }
            }
            // Refresh the grid with the selected course's data
        }
        connection.Close();
    }

    protected void LoadStudents(object sender, EventArgs e)
    {
        connection.Open();

        StudentID.Items.Clear();
        StudentID_SelectedIndexChanged(sender, e);

        // Get the selected value from the InstructorID control
        string selectedValueCourse = null;
        string selectedValueSection = null;

        selectedValueCourse = CourseNameID.SelectedValue;
        selectedValueSection = SectionID.SelectedValue;

        if (selectedValueCourse != "Select Course" && selectedValueSection != "Select Section" && selectedValueCourse != null && selectedValueSection != null)
        {

            // Split the selected value on the first space character
            string[] parts = selectedValueCourse.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse the first part of the string as an integer
            CID = int.Parse(parts[0]);

            StudentID.Items.Add(new ListItem("Select Student"));
            getFacultyID();

            SectionID_SelectedIndexChanged(sender, e);

            string query = "select [user].username, StudentCourse.StudentID from StudentCourse " +
                            "join [user] on [user].userID = studentCourse.StudentID \r\nwhere courseID = @CID and sectionID =@SID and enrolled is not null and [user].role = 'Student'";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@CID", CID);
                cmd.Parameters.AddWithValue("@SID", SID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["StudentID"].ToString();
                    string Name = reader["username"].ToString();
                    string listItemText = $"{studentID} ( {Name})";
                    StudentID.Items.Add(new ListItem(listItemText));
                }
            }
            // Refresh the grid with the selected course's data
        }
        connection.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool Postback = false;
        string eventTarget = Request.Form["__EVENTTARGET"];

        if (!string.IsNullOrEmpty(eventTarget))
        {
            if (eventTarget == "CourseNameID" || eventTarget == "SectionID" || eventTarget == "StudentID")
                Postback = true;
        }

        if (Postback || eventTarget == null)
        {
            if (CourseNameID.Items.Count == 0)
                LoadCourseNames();

            if (eventTarget != "SectionID" && eventTarget != "StudentID")
                LoadSections(sender, e);

            if(eventTarget != "StudentID")
                LoadStudents(sender, e);

            CourseNameID_SelectedIndexChanged(sender, e);

            string selectedValue = null;
            selectedValue = CourseNameID.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "Select Course")
            {
                // Split the selected value on the first space character
                string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Parse the first part of the string as an integer
                CID = int.Parse(parts[0]);

                SectionID_SelectedIndexChanged(sender, e);
                StudentID_SelectedIndexChanged(sender, e);

                string selectedValueStudent = null;
                selectedValueStudent = StudentID.SelectedValue;

                if (!string.IsNullOrEmpty(selectedValueStudent) && selectedValueStudent != "Select Student")
                {
                    string[] parts2 = selectedValueStudent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // Parse the first part of the string as an integer
                    StudentName = parts2[0];
                }

            }

            if (CID != -1 && SID != "" && SID != "Select Section" && StudentName != "Select Student" && StudentName != "")
            {
                connection.Open();

                getFacultyID();
                string query = "select distinct Evaluation.Category,Evaluation.Weightage from StudentCourse\r\njoin Evaluation on Evaluation.StudentID = StudentCourse.StudentID \r\n" +
                 "where StudentCourse.StudentID = @StudID and StudentCourse.SectionID = @SID and StudentCourse.CourseID = @CID and Evaluation.StudentID = @StudID and Evaluation.CourseID = @CID;";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@StudID", int.Parse(StudentName));
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@CID", CID);

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);

                DataTable CourseTable = new DataTable();

                adapter1.Fill(CourseTable);

                GridView1.DataSource = CourseTable;

                GridView1.DataBind();

                connection.Close();
            }
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        connection.Open();
        bool hasValidationError = false;
        getFacultyID();


        CourseNameID_SelectedIndexChanged(sender, e);

        string selectedValue = null;
        selectedValue = CourseNameID.SelectedValue;

        if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "Select Course")
        {
            // Split the selected value on the first space character
            string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse the first part of the string as an integer
            CID = int.Parse(parts[0]);

            SectionID_SelectedIndexChanged(sender, e);
            StudentID_SelectedIndexChanged(sender, e);

            string selectedValueStudent = null;
            selectedValueStudent = StudentID.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValueStudent) && selectedValueStudent != "Select Student")
            {
                string[] parts2 = selectedValueStudent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Parse the first part of the string as an integer
                StudentName = parts2[0];
            }

        }

        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox enterMarksTextBox = (TextBox)row.FindControl("EnterMarks");
                Label categoryNameLabel = (Label)row.FindControl("CategoryName");
                Label weightageLabel = (Label)row.FindControl("Label2");

                string categoryName = categoryNameLabel.Text;
                decimal weightage = Convert.ToDecimal(weightageLabel.Text);
                decimal enteredMarks = -1;

                if (decimal.TryParse(enterMarksTextBox.Text, out enteredMarks))
                {
                    if (enteredMarks <= weightage)
                    {
                        if (enteredMarks != -1)
                        {
                            string query = "update Evaluation set Obtained = @GottenMarks " +
                                            "where Evaluation.Courseid = @CID and Evaluation.FacultyID = @FID and Evaluation.StudentID = @StudID and Evaluation.Category = @category";

                            SqlCommand cmd = new SqlCommand(query, connection);

                            cmd.Parameters.AddWithValue("@GottenMarks", enteredMarks);
                            cmd.Parameters.AddWithValue("@CID", CID);
                            cmd.Parameters.AddWithValue("@FID", FID);
                            cmd.Parameters.AddWithValue("@StudID", StudentName);
                            cmd.Parameters.AddWithValue("@category", categoryNameLabel.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "Invalid Data Entry: Entered marks cannot be greater than weightage.";
                        hasValidationError = true;
                        break; // Stop further processing if there's an error
                    }
                }
            }
        }

        if (!hasValidationError)
        {
            Response.Redirect(Request.Url.ToString());
        }
        connection.Close();
    }
}