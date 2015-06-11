using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MobileDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            if (!Page.User.IsInRole("MOBCASHENT"))
                BtnCashEntry.Visible = false;

            if (!Page.User.IsInRole("MOBCUSTDET"))
                BtnCustomerMaster.Visible = false;

            if (!Page.User.IsInRole("MOBREPORTS"))
                BtnReports.Visible = false;
        }
    }
    protected void BtnCashEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("MobileCashEntry.aspx");
    }
    protected void lnkBtnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-34);
        Response.Redirect("~/MobileLogin.aspx");
    }
    protected void BtnCustomerMaster_Click(object sender, EventArgs e)
    {
        Response.Redirect("MobileCustomerSearch.aspx");
    }
    protected void BtnReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("MobileReports.aspx");
    }
}
