using System;
using System.Data.SqlClient;

public partial class Faculty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        // Add your login logic here, for example:
        // 1. Check the provided credentials against your database or authentication system.
        // 2. If the credentials are correct, log the user in (create an authentication cookie, session, etc.).
        // 3. Redirect the user to the appropriate page after successful login.
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-H1J6M6H\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        conn.Open();
        // MessageBox.Show("Connection Open");
        SqlCommand cm;

        string username = this.username.Text.Trim();
        string password = this.password.Text.Trim();
        string query = "select *from [User] where [User].Username = '" + username + "' and [User].Password = '" + password + "' and [User].Role = 'Faculty'";
        cm = new SqlCommand(query, conn);
        //cm.ExecuteNonQuery();
        //cm.Dispose();
        dr = cm.ExecuteReader();
        if (dr.Read())
        {
            dr.Close();
            Response.Redirect("~/FcMenu.aspx?UserName=" + this.username.Text);
        }
        else
        {
            loginError.Text = "Error Login";
        }
        conn.Close();
    }
}
