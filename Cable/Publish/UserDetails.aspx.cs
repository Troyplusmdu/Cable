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
using System.Text;

public partial class UserDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
			string connStr = string.Empty; 

			if (Session["Company"] != null) 
				connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString(); 
			else 
				Response.Redirect("~/Login.aspx"); 
 
			string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9); 
			dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info")); 
			BusinessLogic objChk = new BusinessLogic(); 

			if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline"))) 
			{
				lnkBtnSave.Enabled = false; 
			}

            lnkBtncancel.PostBackUrl = Page.Request.UrlReferrer.ToString();
            
            GetUserDetials();
        }
    }

    private void GetUserDetials()
    {
        string userid = string.Empty;
        string connection = string.Empty;

        if(Request.QueryString["ID"] != null)
            userid = Request.QueryString["ID"].ToString();

        if (userid == "AddNew")
            txtUserName.Enabled = true;
        
        if(Session["Company"] != null)
            connection = Session["Company"].ToString();

        BusinessLogic objBus = new BusinessLogic();

        DataSet ds = objBus.GetUserInfo(userid, connection);

		DataSet dsRoles = objBus.GetMasterRoles(connection);

        DataView dvMaster = new DataView(dsRoles.Tables[0]);
        DataView dvBilling = new DataView(dsRoles.Tables[0]);
        DataView dvReport = new DataView(dsRoles.Tables[0]);
        DataView dvMobile = new DataView(dsRoles.Tables[0]);

        dvMaster.RowFilter = "Section = 'MASTER'";
        chckMaster.DataSource = dvMaster;
		chckMaster.DataTextField = "RoleDesc";
		chckMaster.DataValueField = "Role";
		chckMaster.DataBind();

        dvBilling.RowFilter = "Section = 'ACCSYS'";
        chkBilling.DataSource = dvBilling;
        chkBilling.DataTextField = "RoleDesc";
        chkBilling.DataValueField = "Role";
        chkBilling.DataBind();

        dvMobile.RowFilter = "Section = 'MOBILE'";
        chkMobile.DataSource = dvMobile;
        chkMobile.DataTextField = "RoleDesc";
        chkMobile.DataValueField = "Role";
        chkMobile.DataBind();

        dvReport.RowFilter = "Section = 'REPORT'";
        chkReport.DataSource = dvReport;
        chkReport.DataTextField = "RoleDesc";
        chkReport.DataValueField = "Role";
        chkReport.DataBind();


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                txtUserName.Text = ds.Tables[0].Rows[0]["username"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                chkAccLocked.Checked = bool.Parse(ds.Tables[0].Rows[0]["Locked"].ToString());
            }
        }
        else
        {
            return;
        }

        string[] roles = objBus.GetRoles(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, userid);
		
		foreach(string role in roles)
		{
            if (chckMaster.Items.FindByValue(role) != null)
			    chckMaster.Items.FindByValue(role).Selected = true;
            if (chkBilling.Items.FindByValue(role) != null)
			    chkBilling.Items.FindByValue(role).Selected = true;
            if (chkReport.Items.FindByValue(role) != null)
			    chkReport.Items.FindByValue(role).Selected = true;
            if (chkMobile.Items.FindByValue(role) != null)
                chkMobile.Items.FindByValue(role).Selected = true;

		}

    }

    private bool CheckManageUserExists(CheckBoxList chkBilling, string user)
    {
        bool exists = false;
        string connection = string.Empty;

        if (Session["Company"] != null)
            connection = Session["Company"].ToString();


        foreach (ListItem item in chkBilling.Items)
        {
            if (item.Selected && (item.Value == "MNGUSRS"))
                exists = true;
        }        

        if (!exists)
        {
            BusinessLogic objBus = new BusinessLogic();
            exists = objBus.checkUserRoleExists("MNGUSRS",connection, user);
        }

        return exists;

    }

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {
        string userName = string.Empty;
        string Email = string.Empty;
        string connection = string.Empty;

        bool Locked = chkAccLocked.Checked;
        
        if (txtUserName.Text != string.Empty)
            userName = txtUserName.Text;
        if (txtEmail.Text != string.Empty)
            Email = txtEmail.Text;

        if(Session["Company"] != null)
            connection = Session["Company"].ToString();

        ArrayList roles = new ArrayList();

        foreach(ListItem item in chckMaster.Items)
        {
            if(item.Selected)
                roles.Add(item.Value);
        }
        foreach (ListItem item in chkMobile.Items)
        {
            if (item.Selected)
                roles.Add(item.Value);
        }
		foreach(ListItem item in chkBilling.Items)
		{
			if(item.Selected)
				roles.Add(item.Value);
		}
		foreach(ListItem item in chkReport.Items)
		{
			if(item.Selected)
				roles.Add(item.Value);
		}

        BusinessLogic objBus = new BusinessLogic();

        if (!CheckManageUserExists(chkBilling, userName))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Atleast one user should be assigned to Manage Users Role.');", true);
            return;
        }
        
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["ID"].ToString() != "AddNew")
                {
                    objBus.UpdateUserInfo(connection, userName, Email, Locked, roles);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User details updated sucessfully.');", true);
                    return;
                    //errorDisplay.AddItem("User details updated sucessfully.", DisplayIcons.GreenTick, false);
                }
                else
                {
                    if (objBus.InsertUserInfo(connection, userName, Email, Locked, roles))
                    {
                        //errorDisplay.AddItem("User Added sucessfully. User set to Default Password.", DisplayIcons.GreenTick, false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User details saved sucessfully. User Password set to abc123.');", true);
                        txtUserName.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        chkAccLocked.Checked = false;

                        foreach (ListItem item in chckMaster.Items)
                        {
                            if (item.Selected)
                                item.Selected = false;
                        }
                        foreach (ListItem item in chkMobile.Items)
                        {
                            if (item.Selected)
                                item.Selected = false;
                        }

                        foreach (ListItem item in chkBilling.Items)
                        {
                            if (item.Selected)
                                item.Selected = false;
                        }
                        foreach (ListItem item in chkReport.Items)
                        {
                            if (item.Selected)
                                item.Selected = false;
                        }

                    }
                    else
                    {
                        //errorDisplay.AddItem("User already exists. Please try again.", DisplayIcons.Information, false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User already exists. Please try again.');", true);
                        return;
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured."+ ex.Message +"');", true);
        }
        

    }
    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserMaintenance.aspx");
    }
}
