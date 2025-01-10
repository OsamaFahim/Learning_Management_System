using System;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void registerButton_Click(object sender, EventArgs e)
    {
        // Add your registration logic here, for example:
        // 1. Check if the username is already taken.
        // 2. If the username is available, create a new user in your database or authentication system.
        // 3. Redirect the user to the appropriate page after successful registration.

        // For now, we will just display a success message after registration.
        string username = this.username.Text.Trim();
        string password = this.password.Text.Trim();
        string confirmPassword = this.confirmPassword.Text.Trim();

        if (password != confirmPassword)
        {
            registerError.Text = "Passwords do not match. Please try again.";
        }
        else
        {
            // Add your registration logic here

            // Redirect the user to the appropriate page after successful registration.
            Response.Redirect("AcademicOffice.aspx");
        }
    }
}

