using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewMarks : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;
    int SID = -1;
    protected void CourseNameID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CourseNameID.SelectedValue != "")
        {
            courseName = CourseNameID.SelectedValue.ToString();
        }
    }

    void getStudentID()
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
            SID = int.Parse(Reader["UserID"].ToString());
        }

        Reader.Close();

    }

    protected void LoadCourseNames(Object sender, EventArgs e)
    {
        connection.Open();

        CourseNameID.Items.Add("Select Course");
        string query = "select FacultyCourses.courseid,CourseName from facultyCourses" +
        "\r\njoin [user] on [user].UserID = FacultyCourses.facultyid and [user].Role = 'Faculty'\r\n" +
        "join StudentCourse on StudentCourse.CourseID = FacultyCourses.courseid\r\n" +
        "join Courses on Courses.CourseID = FacultyCourses.courseid\r\nwhere StudentCourse.StudentID = @SID and StudentCourse.SectionID = FacultyCourses.SectionID;";

        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@SID", SID);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string course_ki_id = reader["courseid"].ToString();
                string Name = reader["courseName"].ToString();
                string listItemText = $"{course_ki_id} ( {Name})";
                CourseNameID.Items.Add(listItemText);
            }
        }

        connection.Close();
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
            getStudentID();

            if (CourseNameID.Items.Count == 0)
                LoadCourseNames(sender, e);

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
                getStudentID();
                string query = "select Evaluation.Courseid,Evaluation.StudentID,Evaluation.Category,Evaluation.Weightage,Evaluation.Obtained\r\n" +
                                "from  evaluation\r\njoin StudentCourse on Evaluation.Courseid = StudentCourse.CourseID and StudentCourse.StudentID = Evaluation.StudentID" +
                                "\r\nwhere StudentCourse.StudentID = @SID and StudentCourse.CourseID = @CID;";

                SqlCommand cmd = new SqlCommand(query, connection);

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
}
