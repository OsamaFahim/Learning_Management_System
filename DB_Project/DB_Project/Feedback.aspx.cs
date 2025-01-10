using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

public partial class Feedback : System.Web.UI.Page
{

    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    string courseName = "";
    int CID = -1;
    int SID = -1;
    protected void CourseID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CourseID.SelectedValue != "")
        {
            courseName = CourseID.SelectedValue.ToString();
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

    protected void LoadCourses(Object sender, EventArgs e)
    {
        connection.Open();

           CourseID.Items.Add("Select Course");
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
                    CourseID.Items.Add(listItemText);
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
            if (eventTarget == "TeacherID")
                Postback = true;
        }

        if(Postback || eventTarget == null)
        {
            getStudentID();

            if (CourseID.Items.Count == 0)
                LoadCourses(sender,e);
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        connection.Open();
        // Retrieve the selected values
        string qualityValue = "", gradingValue = "", communicationValue = "", reviewValue = "";
         qualityValue = Request.Form["qualityOfTeaching1"];
        gradingValue = Request.Form["qualityOfTeaching2"];
         communicationValue = Request.Form["qualityOfTeaching3"];
        reviewValue = review.Value;

        if (qualityValue != "" && qualityValue != null  && gradingValue != "" && gradingValue != null && communicationValue != "" && communicationValue != null)
        {
            getStudentID();
            CourseID_SelectedIndexChanged(sender, e);

            // Get the selected value from the InstructorID control
            string selectedValue = null;
            selectedValue = CourseID.SelectedValue;

            if (selectedValue != "Select Course" && selectedValue != null)
            {
                lblErrorMessage.Text = "";
                // Split the selected value on the first space character
                string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Parse the first part of the string as an integer
                CID = int.Parse(parts[0]);


                string query = "select facultyid from facultyCourses\r\njoin [user] on [user].UserID = FacultyCourses.facultyid and [user].Role = 'Faculty'\r\n" +
                "join StudentCourse on StudentCourse.CourseID = FacultyCourses.courseid\r\n" +
                "join Courses on Courses.CourseID = FacultyCourses.courseid\r\nwhere StudentCourse.StudentID = @SID and StudentCourse.SectionID = FacultyCourses.SectionID and FacultyCourses.courseid = @CID;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SID", SID);
                    cmd.Parameters.AddWithValue("@CID", CID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    int facultyid = -1;
                    while (reader.Read())
                    {
                        facultyid = int.Parse(reader["facultyid"].ToString());
                    }
                    reader.Close();

                    string query2 = "select *from Feedback where facultyid = @FID and courseid = @CID and Studentid = @SID";
                    SqlCommand cmd2 = new SqlCommand(query2, connection);

                    cmd2.Parameters.AddWithValue("@FID", facultyid);
                    cmd2.Parameters.AddWithValue("@CID", CID);
                    cmd2.Parameters.AddWithValue("@SID", SID);

                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    int max_primary_ID = 0;
                    string query3 = "SELECT MAX(EvaluationID) AS maximum_EvaID FROM Feedback;";

                    using (SqlCommand cmd3 = new SqlCommand(query3, connection))
                    {
                        SqlDataReader reader3 = cmd3.ExecuteReader();
                        if (reader3.Read() && !reader3.IsDBNull(reader3.GetOrdinal("maximum_EvaID")))
                            max_primary_ID = int.Parse(reader3["maximum_EvaID"].ToString()) + 1;
                        else
                            max_primary_ID += 1;

                        reader3.Close();
                    }

                    if (reader2.HasRows)
                    {
                        string query5 = "update Feedback set Quality = @Q, Grading = @G, Communication = @Commu, comment = @comme;";

                        SqlCommand cmd5 = new SqlCommand(query5, connection);

                        cmd5.Parameters.AddWithValue("@Q", qualityValue);
                        cmd5.Parameters.AddWithValue("@G", gradingValue);
                        cmd5.Parameters.AddWithValue("@Commu", communicationValue);
                        cmd5.Parameters.AddWithValue("@comme", reviewValue);


                        cmd5.ExecuteNonQuery();
                    }
                    else
                    {

                        string query4 = "insert into Feedback (EvaluationID,Courseid,StudentID,FacultyID,Quality,Grading,Communication,comment) values (@FPID,@CID,@SID,@FID,@Q,@G,@Commu,@comme);";

                        SqlCommand cmd4 = new SqlCommand(query4, connection);

                        cmd4.Parameters.AddWithValue("@FPID", max_primary_ID);
                        cmd4.Parameters.AddWithValue("@CID", CID);
                        cmd4.Parameters.AddWithValue("@SID", SID);
                        cmd4.Parameters.AddWithValue("@FID", facultyid);
                        cmd4.Parameters.AddWithValue("@Q", qualityValue);
                        cmd4.Parameters.AddWithValue("@G", gradingValue);
                        cmd4.Parameters.AddWithValue("@Commu", communicationValue);
                        cmd4.Parameters.AddWithValue("@comme", reviewValue);

                        cmd4.ExecuteNonQuery();
                    }

                }
            }
        }
        else
        {
            lblErrorMessage.Text = "";
            lblErrorMessage.Text = "Please Fill All Fields";
        }
        connection.Close();
        Response.Redirect(Request.Url.ToString());
    }
}
