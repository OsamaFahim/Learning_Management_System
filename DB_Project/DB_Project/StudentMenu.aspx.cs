using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentMenu : System.Web.UI.Page
{
    string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       name =  Request.QueryString["username"];

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/StudentConfirmation.aspx?UserName=" + name);
      
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/Feedback.aspx?UserName=" + name);

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/ViewAttendance.aspx?UserName=" + name);

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/ViewMarks.aspx?UserName=" + name);

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/ViewGrades.aspx?UserName=" + name);

    }
}