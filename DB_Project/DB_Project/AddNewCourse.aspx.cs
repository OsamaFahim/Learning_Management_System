using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataReader drr;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        conn.Open();
        SqlCommand cm;
        int Course_ID = 0;
        int.TryParse(this.CID.Text, out Course_ID);

        string Course_code = this.CCode.Text;
        string Course_name = this.CName.Text;

        int Credit_Hours = 0;
        int.TryParse(this.CHS.Text, out Credit_Hours);
        int PreReqID = 0;
        int.TryParse(this.PREID.Text, out PreReqID);

        string query = "SELECT courses.CourseID from courses where Courses.CourseID = '" + PreReqID + "'";
        cm = new SqlCommand(query, conn);
        drr = cm.ExecuteReader();
        if (drr.Read())
        {
            drr.Close();
            query = "insert into Courses  (CourseID,CourseCode,CourseName,CreditHours,PrerequisiteCourseID) values ('" + Course_ID + "','" + Course_code + "','" + Course_name + "','" + Credit_Hours + "','" + PreReqID + "');";
            cm = new SqlCommand(query, conn);
            int RowsAffected = cm.ExecuteNonQuery();
            if (RowsAffected > 0)
            {
                Response.Redirect("AcMenu.aspx");
            }
            else 
            {
               
            }
        }
        else
        {

        }
    }
}