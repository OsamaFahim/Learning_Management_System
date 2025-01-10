// ConfirmationForm.aspx.cs
using System;
using System.Web.UI;

public partial class ConfirmationForm : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // Redirect the user to the CAPTCHA page
        Response.Redirect("CAPTCHAForm.aspx");
    }
}
