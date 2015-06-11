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

public partial class UserMaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            txtUserName.Focus();

        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            lnkBtnAdd.Visible = false;
            GrdViewCust.Columns[3].Visible = false;
        } 
    }

    protected void GrdViewCust_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GrdViewCust_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserDetails.aspx?ID=AddNew");
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridSource.SelectParameters.Add(new SessionParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtUserName.UniqueID, "Text"));
    }
    
}
