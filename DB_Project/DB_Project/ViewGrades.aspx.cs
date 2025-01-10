using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewGrades : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True;MultipleActiveResultSets=True");
    int FID = -1;
    string courseName = "";
    int CID = -1;
    int SID = -1;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        getStudentID();

        string query = "SELECT\r\n    C.CourseID,\r\n    C.CourseName,\r\n    CASE \r\n       " +
            " WHEN SUM(E.Obtained) >= 90 THEN 'A+' \r\n        WHEN SUM(E.Obtained) BETWEEN 86 AND 89 THEN 'A' \r\n        WHEN SUM(E.Obtained) BETWEEN 82 AND 85 THEN 'A-' \r\n   " +
            "     WHEN SUM(E.Obtained) BETWEEN 78 AND 81 THEN 'B+' \r\n        WHEN SUM(E.Obtained) BETWEEN 74 AND 77 THEN 'B' \r\n        WHEN SUM(E.Obtained) BETWEEN 70 AND 73 THEN 'B-' \r\n   " +
            "     WHEN SUM(E.Obtained) BETWEEN 66 AND 69 THEN 'C+' \r\n        WHEN SUM(E.Obtained) BETWEEN 62 AND 65 THEN 'C' \r\n        WHEN SUM(E.Obtained) BETWEEN 58 AND 61 THEN 'C-' \r\n   " +
            "     WHEN SUM(E.Obtained) BETWEEN 54 AND 57 THEN 'D+' \r\n        WHEN SUM(E.Obtained) BETWEEN 50 AND 53 THEN 'D' \r\n        ELSE 'F' \r\n    END AS Grade\r\nFROM StudentCourse SC\r\n" +
            "JOIN Evaluation E ON SC.StudentID = E.StudentID AND SC.CourseID = E.CourseID\r\nJOIN Courses C ON SC.CourseID = C.CourseID\r\nWHERE SC.StudentID = @SID AND SC.enrolled IS NOT NULL\r\n" +
            "GROUP BY C.CourseID, C.CourseName;";

        SqlCommand cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@SID", SID);

        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);

        DataTable CourseTable = new DataTable();

        adapter1.Fill(CourseTable);

        GridView1.DataSource = CourseTable;

        GridView1.DataBind();

        connection.Close();
    }
}