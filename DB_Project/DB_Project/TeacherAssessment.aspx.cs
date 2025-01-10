using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class TeacherAssessment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // TeacherAssessment.aspx.cs
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Perform form validation and save data

        // Redirect to the confirmation page
        Response.Redirect("ConfirmationForm.aspx");
    }

}