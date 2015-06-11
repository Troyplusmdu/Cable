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
using SMSLibrary;
using System.Text;

public partial class SMSConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            DBManager manager = new DBManager(DataProvider.OleDb);
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;
            DataSet ds = new DataSet();
            string dbQry = string.Empty;
            
            srcArea.ConnectionString = manager.ConnectionString;

            try
            {
                dbQry = "select SMSRequired,CopyRequired,Mobile from tblSMSSettings";

                manager.Open();
                ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
                ddArea.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rdoSMSReq.SelectedValue = ds.Tables[0].Rows[0]["SMSRequired"].ToString();
                    rdoCopyReq.SelectedValue = ds.Tables[0].Rows[0]["CopyRequired"].ToString();

                    if (rdoCopyReq.SelectedValue == "Yes")
                    {
                        txtReconDate.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        rowMobile.Visible = true;
                    }
                    else
                    {
                        txtReconDate.Text = "";
                        rowMobile.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                errorDisplay.AddItem("Exception : " + ex.Message.ToString(), DisplayIcons.Error, false);
            }
        }
    }
    protected void lnkBtnUpdate_Click(object sender, EventArgs e)
    {

        if(rdoCopyReq.SelectedValue == "Yes")
        {
            if (txtReconDate.Text == "")
            {
                errorDisplay.AddItem("Mobile No is Mandatory.", DisplayIcons.Error, false);
                txtReconDate.Focus();
                return;
            }
        }
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        try
        {
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, "Update tblSMSSettings Set CopyRequired='" + rdoCopyReq.SelectedValue + "', Mobile='"+ txtReconDate.Text +"'");
            manager.Dispose();

            Session["SMSRequired"] = rdoSMSReq.SelectedValue;
            Session["CopyRequired"] = rdoCopyReq.SelectedValue;
            Session["Mobile"] = txtReconDate.Text;

            errorDisplay.AddItem("Settings updated successfully.", DisplayIcons.GreenTick, false);
        }
        catch (Exception ex)
        {
            errorDisplay.AddItem("Exception : " + ex.Message.ToString(), DisplayIcons.Error, false);
        }
    }
    protected void rdoCopyReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoCopyReq.SelectedValue == "No")
        {
            rowMobile.Visible = false;
        }
        else
        {
            rowMobile.Visible = true;
        }

        txtReconDate.Focus();
    }

    protected void BtnSendSMS_Click(object sender, EventArgs e)
    {
        string provider = "http://sms.full2sms.com/pushsms.php";
        string userName = "muthukumar";
        string password = "muthukumar123";
        string phone = txtMobileNos.Text;
        string msgText = txtMessage.Text;

        if (Session["SMSRequired"] != null)
        {
            SMSLibrary.UtilitySMS utilSMS = new UtilitySMS();
            if (utilSMS.SendSMS(provider, "2", "test", userName, password, phone, msgText, false, Page.User.Identity.Name))
            {
                errorDisplay.AddItem("SMS Sent Successfully.", DisplayIcons.GreenTick, false);
                txtMessage.Text = "";
                txtMobileNos.Text = "";
                txtMobileNos.Focus();
            }
            else
                errorDisplay.AddItem("Failed to Send SMS.", DisplayIcons.Information, false);
        }
        else
        {
            errorDisplay.AddItem("You are not configured to Send SMS.", DisplayIcons.Information, false);
        }

    }
    protected void BtnGetNos_Click(object sender, EventArgs e)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {
            manager.Open();

            if (ddArea.SelectedIndex > 0)
            {
                dbQry.Append("SELECT phoneno FROM CustomerMaster where (category = 'NC' or category = 'RC') AND Area = '" + ddArea.SelectedValue.Replace("'", "''") + "'");
            }
            else
            {
                dbQry.Append("SELECT phoneno FROM CustomerMaster where (category = 'NC' or category = 'RC')");
            }

            if (txtBalance.Text != "")
                dbQry.Append(" And Balance " + ddOper.SelectedValue + txtBalance.Text);
                
            manager.Open();

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            manager.Dispose();

            string mobileNos = string.Empty;

            int i = 0;

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    i++;

                    if ((dr["phoneno"] != null) && (dr["phoneno"].ToString().Length == 10))
                    {
                        mobileNos = mobileNos + dr["phoneno"].ToString() + ",";
                        /*
                        if (i != ds.Tables[0].Rows.Count)
                        {
                            mobileNos = mobileNos + ",";
                        }*/

                    }
                }
            }
            else
            {
                txtMobileNos.Text = "";
            }

            if (rdoCopyReq.SelectedValue == "Yes")
                mobileNos = mobileNos + txtReconDate.Text;

            if (mobileNos != "")
            {
                if (mobileNos.Substring(mobileNos.Length - 1) == ",")
                    mobileNos = mobileNos.Remove(mobileNos.Length - 1, 1);
            }

            txtMobileNos.Text = mobileNos;

        }
        catch (Exception ex)
        {
            errorDisplay.AddItem("Exception : " + ex.Message.ToString(), DisplayIcons.Error, false);
        }
    }
}
