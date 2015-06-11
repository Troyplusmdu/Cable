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
using System.Data.OleDb;
using DataAccessLayer;

public partial class ReconDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            lnkBtnUpdate.Enabled = false;
        } 

        if (!Page.IsPostBack)
        {
            DBManager manager = new DBManager(DataProvider.OleDb);
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

            try
            {
                manager.Open();
                object reconDate = manager.ExecuteScalar(CommandType.Text, "Select recon_date from last_recon");

                if (reconDate != null)
                    txtReconDate.Text = DateTime.Parse(reconDate.ToString()).ToString("dd/MM/yyyy");

                manager.Dispose();
            }
            catch(Exception ex)
            {
                errorDisplay.AddItem("Exception : " + ex.Message.ToString(), DisplayIcons.Error, false);
            }
        }
    }
    protected void lnkBtnUpdate_Click(object sender, EventArgs e)
    {
            DBManager manager = new DBManager(DataProvider.OleDb);
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

            try
            {
                manager.Open();
                manager.ExecuteNonQuery(CommandType.Text, "Update last_recon Set recon_date=Format('"+ txtReconDate.Text +"', 'dd/MM/yyyy')");
                manager.Dispose();

                errorDisplay.AddItem("Recon Date updated successfully." , DisplayIcons.GreenTick,false);
            }
            catch(Exception ex)
            {
                errorDisplay.AddItem("Exception : " + ex.Message.ToString(), DisplayIcons.Error, false);
            }

    }
}
