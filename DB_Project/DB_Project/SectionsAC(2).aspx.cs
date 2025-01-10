using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SectionsAC_2_ : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;

    private void LoadTeacherIDs()
    {
        connection.Open();

        SqlCommand cmd = new SqlCommand("select  distinct FacultyCourses.facultyid from facultycourses\r\njoin Section on Section.OfferedCourseID = FacultyCourses.courseid;", connection);

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FacultyID.Items.Add(new ListItem(reader["FacultyID"].ToString()));
                }
            }
        }

        connection.Close();
    }
    protected void FacultyID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (FacultyID.SelectedValue != "")
        {
            FID = int.Parse(FacultyID.SelectedValue);
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
            int SectionID = -1;

            string query2 = "select distinct Section.OfferedCourseID,Section.SectionID from Section\r\njoin FacultyCourses on FacultyCourses.courseid = Section.OfferedCourseID\r\n" +
                            "where FacultyCourses.courseid = @CourseID\r\nand not exists (select 1 from FacultyCourses where FacultyCourses.SectionID = Section.SectionID);";

            using (SqlCommand cmd2 = new SqlCommand(query2, connection))
            {
                cmd2.Parameters.AddWithValue("@CourseID", CourseID.Text);

                SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    if (reader2["SectionID"] != DBNull.Value) // Check if SectionID is not null
                    {
                        SectionID = int.Parse(reader2["SectionID"].ToString());
                        SelectSections.Items.Add(new ListItem(SectionID.ToString()));
                    }
                }

                reader2.Close();
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
            if (eventTarget == "FacultyID")
                Postback = true;
        }

        if (Postback || eventTarget == null)
        {
            if (FacultyID.Items.Count == 0)
                LoadTeacherIDs();

            FacultyID_SelectedIndexChanged(sender,e);

            connection.Open();

            string query = "select distinct Courses.CourseID , Courses.CourseCode,Courses.CourseName,Courses.CreditHours,FacultyCourses.SectionID " +
                           "from FacultyCourses\r\njoin Courses on Courses.CourseID = FacultyCourses.courseid\r\n" +
                           "join Section on Section.OfferedCourseID = FacultyCourses.courseid \r\nwhere Section.OfferedCourseID is not null and FacultyCourses.facultyid = @FID;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FID", FID);

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
        FacultyID_SelectedIndexChanged(sender, e);
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

                            string query3 = "UPDATE FacultyCourses SET SectionID = @SectionID WHERE facultyid = @FacultyID and CourseID = @courseID;";
                            using (SqlCommand cmd3 = new SqlCommand(query3, con))
                            {
                                cmd3.Parameters.AddWithValue("@SectionID", selectedSection);
                                cmd3.Parameters.AddWithValue("@FacultyID", FID);
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

