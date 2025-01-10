using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class OCR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    private DataTable GetReportData()
    {
        // Your logic to fetch data from database and return as DataTable
        // For example:
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection("Data Source=MUHADDIS\\SQLEXPRESS;Initial Catalog=FlexDB;Integrated Security=True")) // replace with your connection string
        {
            using (SqlCommand cmd = new SqlCommand("select Courses.CourseCode,Courses.CourseName,Courses.CreditHours from Courses\r\n", con)) // replace with your SQL query
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }
}
