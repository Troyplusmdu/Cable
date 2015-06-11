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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (!objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                divWarning.Visible = false;
                tblWarning.Visible = false;
                //lblMsg.Text = "";
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                //lblMsg.Font.Size = new FontUnit(FontSize.Small);
                //lblMsg.Font.Italic = true;
                //lblMsg.Font.Bold = false;
            }
        }

    }
}
