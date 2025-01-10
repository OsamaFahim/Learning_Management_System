using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Attendance : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;
    string SID = "";

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

        string query = "select FacultyCourses.courseid,Courses.CourseName from FacultyCourses\r\njoin Courses on FacultyCourses.courseid = Courses.CourseID" +
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
        CourseNameID_SelectedIndexChanged(sender,  e);

        // Get the selected value from the InstructorID control
        string selectedValue = null;
        selectedValue = CourseNameID.SelectedValue;

        if (selectedValue != "Select Course" &&  selectedValue != null)
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

        }
        connection.Close();
    }

    protected void AddStatus()
    {
        connection.Open();
        foreach (GridViewRow row in GridView1.Rows)
        {
            DropDownList SelectSections = row.Cells[2].FindControl("DropDown1") as DropDownList;
            SelectSections.Items.Add(new ListItem("Select Status"));
            SelectSections.Items.Add(new ListItem("P"));
            SelectSections.Items.Add(new ListItem("A"));
            SelectSections.Items.Add(new ListItem("L"));
        }
        connection.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            // Store the selected date in the hidden field
            hidden_attendance_date.Value = Request.Form["attendance_date"];
        }
        else
        {
            // Set the value of the input date control from the hidden field
            attendance_date.Attributes["value"] = hidden_attendance_date.Value;
        }

        bool Postback = false;
        string eventTarget = Request.Form["__EVENTTARGET"];

        if (!string.IsNullOrEmpty(eventTarget))
        {
            if (eventTarget == "CourseNameID" || eventTarget == "SectionID")
                Postback = true;
        }


        if (Postback || eventTarget == null)
        {
            if (CourseNameID.Items.Count == 0)
                LoadCourseNames();

            if(eventTarget != "SectionID")
                LoadSections(sender,e);

            CourseNameID_SelectedIndexChanged(sender,e);

            string selectedValue = null;
            selectedValue = CourseNameID.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "Select Course")
            {
                // Split the selected value on the first space character
                string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Parse the first part of the string as an integer
                CID = int.Parse(parts[0]);

                SectionID_SelectedIndexChanged(sender, e);
            }

            
            if (CID != -1 && SID != "" && SID != "Select Section")
            {
                connection.Open();

                string query = "select StudentCourse.StudentID,[user].Username from StudentCourse\r\njoin [User] on [user].UserID = StudentCourse.StudentID and role = 'Student' \r\n" +
                                "where StudentCourse.SectionID = @SID and StudentCourse.CourseID = @CID;";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@CID", CID);

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);

                DataTable CourseTable = new DataTable();

                adapter1.Fill(CourseTable);

                GridView1.DataSource = CourseTable;

                GridView1.DataBind();

                connection.Close();

                AddStatus();

            }
        }

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        // Retrieve the selected date from the input date control
        string selectedDate = Request.Form["attendance_date"];


        // Convert the selected date to the desired SQL format
        DateTime parsedDate;
        string sqlFormattedDate = "";
        if (DateTime.TryParse(selectedDate, out parsedDate))
        {
            sqlFormattedDate = parsedDate.ToString("yyyy-MM-dd");
            // Use the SQL formatted date for further processing or storing in a database
        }

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
        }

        getFacultyID();

        if (sqlFormattedDate != "")
        {

                string query = "select *from Attendance\r\nwhere Attendance.attendance_date = @attendanceDate  and Courseid = @CID and FacultyID = @FID ;";

            connection.Open();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@attendanceDate", sqlFormattedDate);
                cmd.Parameters.AddWithValue("@CID", CID);
                cmd.Parameters.AddWithValue("@FID", FID);


                SqlDataReader reader = cmd.ExecuteReader();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    int max_primary_ID = 0;
                    string query3 = "SELECT MAX(AttendanceID) AS maximum_AttID FROM Attendance;";

                    using (SqlCommand cmd3 = new SqlCommand(query3, connection))
                    {
                        SqlDataReader reader3 = cmd3.ExecuteReader();
                        if (reader3.Read() && !reader3.IsDBNull(reader3.GetOrdinal("maximum_AttID")))
                            max_primary_ID = int.Parse(reader3["maximum_AttID"].ToString()) + 1;
                        else
                            max_primary_ID += 1;

                        reader3.Close();
                    }


                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string status = ((DropDownList)row.FindControl("DropDown1")).SelectedValue;
                        if (status != "" && status != "status")
                        {
                            string StudentID = ((Label)row.Cells[0].FindControl("StudentID")).Text;

                            if (reader.HasRows)
                            {
                                string query2 = "update attendance set status = @status where Courseid = @CID and FacultyID = @FID;";
                                using (SqlCommand cmd2 = new SqlCommand(query2, connection))
                                {
                                    cmd2.Parameters.AddWithValue("@CID", CID);
                                    cmd2.Parameters.AddWithValue("@FID", FID);
                                    cmd2.Parameters.AddWithValue("@status", status);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string query2 = "insert into Attendance (AttendanceID,Courseid,StudentID,FacultyID,Attendance_date,status) values (@AttID,@CID,@StuID,@FID,@sqldate,@status);";
                                using (SqlCommand cmd2 = new SqlCommand(query2, connection))
                                {
                                    cmd2.Parameters.AddWithValue("@AttID", max_primary_ID);
                                    cmd2.Parameters.AddWithValue("@CID", CID);
                                    cmd2.Parameters.AddWithValue("@StuID", StudentID);
                                    cmd2.Parameters.AddWithValue("@FID", FID);
                                    cmd2.Parameters.AddWithValue("@sqldate", sqlFormattedDate);
                                    cmd2.Parameters.AddWithValue("@status", status);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }

                    }
                }
            }
            connection.Close(); 
        }
        Response.Redirect(Request.Url.ToString());
    }
}