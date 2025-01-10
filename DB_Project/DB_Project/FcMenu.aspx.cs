using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FcMenu : System.Web.UI.Page
{
    string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        name = Request.QueryString["UserName"];
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/Attendance.aspx?UserName=" + name);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/MarksDistributionFc.aspx?UserName=" + name);

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/EvaluationMarksFc.aspx?UserName=" + name);

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (name != "")
            Response.Redirect("~/ViewFeedback.aspx?UserName=" + name);

    }
}