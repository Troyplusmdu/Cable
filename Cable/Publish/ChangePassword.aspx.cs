﻿using System;
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
using System.Text;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtConfirmPass.Attributes.Add("onKeyPress", " return clickButton(event,'" + lnkBtnSave.ClientID + "')");
            txtCurrentPass.Focus();
        }

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
            lnkBtnSave.Enabled = false;
        } 

    }
    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {
        StringBuilder msg = new StringBuilder();

        if (!Page.IsValid)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    msg.Append(" - " + validator.ErrorMessage);
                    msg.Append("\\n");
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('"+ msg.ToString() +"');", true);
            return;
        }
  
        BusinessLogic objBus = new BusinessLogic();

        if (txtNewPass.Text != txtConfirmPass.Text)
        {
            //errorDisplay.AddItem("Your new password and Confirm password dosent match.", DisplayIcons.Information, false);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your new password and Confirm password dosent match.');", true);
            return;
        }

        if (Session["LoggedInUser"] != null && Session["Company"] != null)
        {
            string password = objBus.GetPassword(Session["LoggedInUser"].ToString(), Session["Company"].ToString());

            if (password != txtCurrentPass.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your have entered incorrect password.');", true);
                //errorDisplay.AddItem("Your have entered incorrect password.", DisplayIcons.Information, false);
                return;
            }
            else
            {
                objBus.ChangePassword(Session["LoggedInUser"].ToString(), txtNewPass.Text, Session["Company"].ToString());
                //errorDisplay.AddItem("Your password has been changed successfully.", DisplayIcons.GreenTick, false);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your password has been changed successfully.');", true);
                txtCurrentPass.Text = string.Empty;
                txtNewPass.Text = string.Empty;
                txtConfirmPass.Text = string.Empty;
                //return;
            }
        }
        
    }
}
