using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;


public partial class SectionsAC : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int SID = -1;
    private void LoadStudentIDs()
    {
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT StudentCourse.StudentID FROM StudentCourse WHERE enrolled IS NOT NULL;", connection);

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentID.Items.Add(new ListItem(reader["StudentID"].ToString()));
                }
            }
        }

        connection.Close();
    }

    protected void StudentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (StudentID.SelectedValue != "")
        {
            SID = int.Parse(StudentID.SelectedValue);
        }
    }

    protected void AddSection()
    {
        connection.Open();
        foreach (GridViewRow row in GridView1.Rows)
        {
            DropDownList SelectSections = row.Cells[5].FindControl("DropDown1") as DropDownList;
            SelectSections.Items.Add(new ListItem("Select Section"));
            Label CourseID = row.Cells[0].FindControl("CourseID") as Label;
            int max_SectionID = -1;
            int strength = -1;

            string query2 = "SELECT TOP 1 SectionID, MAX(STRENGTH) AS MAX_STRENGTH " +
                "FROM Section " +
                "WHERE OfferedCourseID = @CourseID " +
                "GROUP BY Strength, SectionID " +
                "HAVING Strength < 50 " +
                "ORDER BY Strength DESC;";

            using (SqlCommand cmd2 = new SqlCommand(query2, connection))
            {
                cmd2.Parameters.AddWithValue("@CourseID", CourseID.Text);

                SqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.Read())
                {
                    max_SectionID = int.Parse(reader2["SectionID"].ToString());
                    strength = Convert.ToInt32(reader2["MAX_STRENGTH"]);
                }

                reader2.Close();
            }

            int max_section = 0;
            if (max_SectionID == -1)
            {
                string query3 = "SELECT MAX(SectionID) AS maximum_secID FROM Section;";
                using (SqlCommand cmd3 = new SqlCommand(query3, connection))
                {
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    if (reader3.Read() && !reader3.IsDBNull(reader3.GetOrdinal("maximum_secID")))
                        max_section = int.Parse(reader3["maximum_secID"].ToString()) + 1;
                    else
                        max_section += 1;

                    reader3.Close();
             }

                string query4 = "INSERT INTO Section (SECTIONID, offeredCourseID, Strength) VALUES (@SectionID, @OfferedCourseID, @Strength);";

                using (SqlCommand cmd4 = new SqlCommand(query4, connection))
                {
                    cmd4.Parameters.AddWithValue("@SectionID", max_section.ToString());
                    cmd4.Parameters.AddWithValue("@OfferedCourseID", CourseID.Text);
                    cmd4.Parameters.AddWithValue("@Strength", 0);

                    int rowsAffected = cmd4.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        SelectSections.Items.Add(new ListItem(max_section.ToString()));
                }

            }
            else
                SelectSections.Items.Add(new ListItem(max_SectionID.ToString()));
        }
        connection.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool Postback = false;
        string eventTarget = Request.Form["__EVENTTARGET"];

        if (!string.IsNullOrEmpty(eventTarget))
        {
            if (eventTarget == "StudentID")
                Postback = true;
        }

        if (Postback || eventTarget == null)
        {
            if(StudentID.Items.Count == 0)
             LoadStudentIDs();

            StudentID_SelectedIndexChanged(sender, e);


            connection.Open();

            string query = "SELECT DISTINCT Courses.CourseID, Courses.CourseCode, Courses.CourseName, Courses.CreditHours, StudentCourse.SectionID " +
                            "FROM StudentCourse " +
                            "JOIN Courses ON Courses.CourseID = StudentCourse.CourseID " +
                            "RIGHT JOIN FacultyCourses ON FacultyCourses.courseid = StudentCourse.CourseID " +
                            "WHERE StudentCourse.enrolled IS NOT NULL AND StudentCourse.StudentID = @SID;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@SID", SID);

            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);

            DataTable CourseTable = new DataTable();

            adapter1.Fill(CourseTable);

            GridView1.DataSource = CourseTable;

            GridView1.DataBind();

            connection.Close();

            AddSection();
        }

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        StudentID_SelectedIndexChanged(sender, e);
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string selectedSection = ((DropDownList)row.FindControl("DropDown1")).SelectedValue;

                if (selectedSection != "" && selectedSection != "Select Section")
                {

                    string courseID = ((Label)row.Cells[0].FindControl("CourseID")).Text;
                    Label label = row.Cells[4].FindControl("Label4") as Label;
                    string currentSectionID = (label != null) ? label.Text : string.Empty;

                    if (currentSectionID != selectedSection)
                    {
                        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True"))
                        {
                            con.Open();

                            string query1 = "UPDATE Section SET Strength = Strength + 1 WHERE SectionID = @SectionID;";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con))
                            {
                                cmd1.Parameters.AddWithValue("@SectionID", selectedSection);
                                cmd1.ExecuteNonQuery();
                            }

                            if (!string.IsNullOrEmpty(currentSectionID))
                            {
                                string query2 = "UPDATE Section SET Strength = Strength - 1 SectionID = @CurrentSectionID; ";
                                using (SqlCommand cmd2 = new SqlCommand(query2, con))
                                {
                                    cmd2.Parameters.AddWithValue("@CurrentSectionID", currentSectionID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }

                            string query3 = "UPDATE StudentCourse SET SectionID = @SectionID WHERE StudentID = @StudentID and CourseID = @courseID;";
                            using (SqlCommand cmd3 = new SqlCommand(query3, con))
                            {
                                cmd3.Parameters.AddWithValue("@SectionID", selectedSection);
                                cmd3.Parameters.AddWithValue("@StudentID", SID);
                                cmd3.Parameters.AddWithValue("@courseID", courseID);
                                cmd3.ExecuteNonQuery();
                            }

                        }
                    }
                }
            }
        }
        Response.Redirect(Request.Url.ToString());
    }
}

