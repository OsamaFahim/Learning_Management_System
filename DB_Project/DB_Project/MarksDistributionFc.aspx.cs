using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

public partial class MarksDistributionFc : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;
    string SID = "";

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
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

    private void RefreshGrid()
    {
        // Clear the existing data in the grid
        GridView1.DataSource = null;
        GridView1.DataBind();

        // Check if a course and section are selected
        if (CID != -1 && SID != "" && SID != "Select Section")
        {
            connection.Close();
            connection.Open();

            string query = "select distinct Evaluation.Category, Evaluation.Weightage from Evaluation\r\nwhere Courseid = @CID and facultyid = @FID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CID", CID);
            cmd.Parameters.AddWithValue("@FID", FID);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable courseTable = new DataTable();
            adapter.Fill(courseTable);

            GridView1.DataSource = courseTable;
            GridView1.DataBind();

            connection.Close();

            AddDeleteOption();
        }
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
           /* // Refresh the grid with the selected course's data
            RefreshGrid();*/
        }
        connection.Close();
    }


    protected void AddDeleteOption()
    {
        connection.Open();
        foreach (GridViewRow row in GridView1.Rows)
        {
            DropDownList SelectSections = row.Cells[2].FindControl("DropDown1") as DropDownList;
            SelectSections.Items.Add(new ListItem("Keep Distribution"));
            SelectSections.Items.Add(new ListItem("Delete Distribution"));
        }
        connection.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
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

            if (eventTarget != "SectionID")
                LoadSections(sender, e);

            RefreshGrid();

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

            if (CID != -1 && SID != "" && SID != "Select Section")
            {
                connection.Open();

                getFacultyID();
                string query = "select distinct Evaluation.Category,Evaluation.Weightage from Evaluation\r\nwhere Courseid =@CID and facultyid = @FID;";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@CID", CID);
                cmd.Parameters.AddWithValue("@FID", FID);

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);

                DataTable CourseTable = new DataTable();

                adapter1.Fill(CourseTable);

                GridView1.DataSource = CourseTable;

                GridView1.DataBind();

                connection.Close();

                AddDeleteOption();
            }

            string new_category = this.CategoryID.Text;
            string new_weightage = this.WeightageID.Text;

            string query2 = "SELECT SUM(E.Weightage) AS TotalWeightage\r\nFROM (\r\n    SELECT DISTINCT Category, Weightage\r\n    FROM Evaluation\r\n    WHERE Courseid = @CID AND FacultyID = @FID\r\n) AS E;";

            SqlCommand cmd2 = new SqlCommand(query2, connection);

            cmd2.Parameters.AddWithValue("@CID", CID);
            cmd2.Parameters.AddWithValue("@FID", FID);

            connection.Open();
            SqlDataReader reader = cmd2.ExecuteReader();

        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        connection.Open();
        string selectedValue = CourseNameID.SelectedValue;

        if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "Select Course")
        {
            // Split the selected value on the first space character
            string[] parts = selectedValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse the first part of the string as an integer
            CID = int.Parse(parts[0]);

            SectionID_SelectedIndexChanged(sender, e);
        }

        CourseNameID_SelectedIndexChanged(sender, e);
        getFacultyID();

        string new_category = CategoryID.Text;
        string new_weightage = WeightageID.Text;

        foreach (GridViewRow row in GridView1.Rows)
        {
            // Find the DropDownList in the current row
            DropDownList dropDownList = row.FindControl("DropDown1") as DropDownList;

            if (dropDownList != null)
            {
                // Get the selected value of the DropDownList
                string selectedValueDropDown = dropDownList.SelectedValue;

                
                Label categoryNameLabel = row.FindControl("CategoryName") as Label;
                Label weightageLabel = row.FindControl("Label2") as Label;

                if (categoryNameLabel != null && weightageLabel != null)
                {
                    string categoryName = categoryNameLabel.Text;
                    string weightage = weightageLabel.Text;

                    if(selectedValueDropDown == "Delete Distribution") 
                    {
                        string query = $"DELETE FROM Evaluation " +
                                     $"WHERE Category = @Category AND facultyID = @FID AND CourseID = @CID";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@Category", categoryName);
                        cmd.Parameters.AddWithValue("@FID", FID);
                        cmd.Parameters.AddWithValue("@CID", CID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        decimal totalWeightage = 0;

        if (new_category != "Category" && new_category != "" && new_weightage != "" && new_weightage != "Weightage" && CID != -1 && courseName != "Select Course" && SID  != "Select Section" && decimal.TryParse(new_weightage, out _))
        {
            connection.Close();
            connection.Open();

            string query = "SELECT SUM(E.Weightage) AS TotalWeightage\r\nFROM (\r\n    SELECT DISTINCT Category, Weightage\r\n    FROM Evaluation\r\n    WHERE Courseid = @CID AND FacultyID = @FID\r\n) AS E;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CID", CID);
            cmd.Parameters.AddWithValue("@FID", FID);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader["TotalWeightage"] != DBNull.Value)
                {
                    // Read the value of TotalWeightage from the reader and convert it to decimal
                    totalWeightage = Convert.ToDecimal(reader["TotalWeightage"]);
                    // Do something with the totalWeightage value
                }
            }
            if (new_weightage != null)
                totalWeightage += decimal.Parse(new_weightage);

            if (totalWeightage > 100)
            {
                lblErrorMessage.Text = "Absolutes have exceeded 100";
            }
            else
            {
                lblErrorMessage.Text = "";
                string query2 = "select StudentCourse.StudentID from StudentCourse\r\nwhere StudentCourse.CourseID = @CID and enrolled is not null and StudentCourse.SectionID = @SID;";

                SqlCommand cmd2 = new SqlCommand(query2, connection);

                cmd2.Parameters.AddWithValue("@CID", CID);
                cmd2.Parameters.AddWithValue("@SID", SID);

                SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    int studentID = -1;
                    studentID = Convert.ToInt32(reader2["StudentID"]);

                    if (studentID != -1)
                    {
                        int max_primary_ID = 0;
                        string query3 = "SELECT MAX(EvaluationID) AS maximum_EvaID FROM Evaluation;";

                        using (SqlCommand cmd3 = new SqlCommand(query3, connection))
                        {
                            SqlDataReader reader3 = cmd3.ExecuteReader();
                            if (reader3.Read() && !reader3.IsDBNull(reader3.GetOrdinal("maximum_EvaID")))
                                max_primary_ID = int.Parse(reader3["maximum_EvaID"].ToString()) + 1;
                            else
                                max_primary_ID += 1;

                            reader3.Close();
                        }
                        string query4 = "insert into Evaluation (EvaluationID,Courseid,StudentID,FacultyID,Category,Weightage) values (@EPID,@CID,@StudID,@FID,@Category,@Weightage);";

                        using (SqlCommand cmd4 = new SqlCommand(query4, connection))
                        {
                            cmd4.Parameters.AddWithValue("@EPID", max_primary_ID);
                            cmd4.Parameters.AddWithValue("@CID", CID);
                            cmd4.Parameters.AddWithValue("@StudID", studentID);
                            cmd4.Parameters.AddWithValue("@FID", FID);
                            cmd4.Parameters.AddWithValue("@Category", new_category);
                            cmd4.Parameters.AddWithValue("@Weightage", new_weightage);

                            cmd4.ExecuteNonQuery();
                        }

                    }

                }

                reader.Close();
                connection.Close();


            }
        }

        if (totalWeightage > 100)
            lblErrorMessage.Text = "Absolutes have exceeded 100";
        else
            lblErrorMessage.Text = "";
        Response.Redirect(Request.Url.ToString());
    }
}