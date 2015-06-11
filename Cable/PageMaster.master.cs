using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ACCSYS;

public partial class PageMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["UserName"] != null)
        {
            //lblWelcome.Text = "Welcome";
            //lblUser.Text = Session["UserName"].ToString() + " !";
        }

        NavBarBlocks blocks = Navbar2.Blocks;
        
        string connStr = string.Empty;

        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            lblCompany.Text = "Company : " +Session["Company"].ToString();
        }
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic objBus = new BusinessLogic(connStr);

        if (Session["CashMode"] == null)
        {
            Session["CashMode"] = objBus.getCashEntryMode(connStr);
        }

        if (Session["CashMode"] != null)
        {
            if (Session["CashMode"].ToString() != "Book")
            {

                foreach (NavBarBlock block in blocks)
                {
                    NavBarItems items = block.Items;
                    NavBarItems toRemove = new NavBarItems();

                    foreach (NavBarItem item in items)
                    {
                        if (item.Text == "Manage Books" || item.Text == "Cash Adjustments" || item.Text == "Installation Cash Adjustments")
                            toRemove.Add(item);

                        if ((item.Text == "System Configuration") && (System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() != "ONLINE-OFFLINE-CLIENT"))
                            toRemove.Add(item);
                    }

                    foreach (NavBarItem test in toRemove)
                    {
                        items.Remove(test);
                    }

                }
            }
        }

        if (!Page.IsPostBack)
        {
            uiDateTimeLabel.Text = DateTime.Now.ToString("dd MMM yyyy");            
        }

        if (System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() != "ONLINE-OFFLINE-CLIENT")
        {
            foreach (NavBarBlock block in blocks)
            {
                NavBarItems items = block.Items;
                NavBarItems toRemove = new NavBarItems();

                foreach (NavBarItem item in items)
                {
                    if ((item.Text == "System Configuration") && (System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() != "ONLINE-OFFLINE-CLIENT"))
                        toRemove.Add(item);
                }

                foreach (NavBarItem test in toRemove)
                {
                    items.Remove(test);
                }

            }
        }

    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-34);
        Response.Redirect("~/Default.aspx");
    }

    protected override void  OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Session["Company"] == null)
            Response.Redirect("~/Login.aspx");
        
    }
    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        if (e.Exception != null)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
            throw new Exception(e.Exception.Message + e.Exception.StackTrace);
        }
        
        //ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
    }
}
