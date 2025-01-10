using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewFeedback : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;

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

    protected void CourseNameID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CourseNameID.SelectedValue != "")
        {
            courseName = CourseNameID.SelectedValue.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bool Postback = false;
        string eventTarget = Request.Form["__EVENTTARGET"];

        if (!string.IsNullOrEmpty(eventTarget))
        {
            if (eventTarget == "CourseNameID")
                Postback = true;
        }

        if (Postback || eventTarget == null)
        {
            if (CourseNameID.Items.Count == 0)
                LoadCourseNames();

            CourseNameID_SelectedIndexChanged(sender, e);

            string selectedValue = null;
            selectedValue = CourseNameID.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "Select Course")
            {
                // Split the selected value on the first space character
                string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Parse the first part of the string as an integer
                CID = int.Parse(parts[0]);
            }

            if (CID != -1 && selectedValue != "" && selectedValue != "elect Course")
            {
                getFacultyID();
                string query = "select feedback.StudentID,[user].Username,feedback.Quality,Feedback.Grading,Feedback.Communication,Feedback.comment\r\nfrom Feedback\r\n" +
                    "join StudentCourse on StudentCourse.StudentID = Feedback.StudentID and feedback.Courseid = StudentCourse.CourseID and enrolled is not null\r\n" +
                    "join [user] on [user].UserID = StudentCourse.StudentID and Role = 'Student'\r\n" +
                    "join FacultyCourses on FacultyCourses.courseid = StudentCourse.CourseID and FacultyCourses.SectionID = StudentCourse.SectionID\r\n" +
                    "where FacultyCourses.facultyid = @FID and FacultyCourses.courseid = @CID;";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@FID", FID);
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
}