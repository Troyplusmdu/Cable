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
using SMSLibrary;

public partial class AdjustmentEntry : System.Web.UI.Page
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
            lnkBtnSave.Enabled = false;
        }

        srcArea.ConnectionString = connStr;
        srcReason.ConnectionString = connStr;

        if (!Page.IsPostBack)
        {
            txtDateEntered.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Session["Area"] != null)
                ddArea.SelectedValue = Session["Area"].ToString();
        }
    }
    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {

    }

    public string GetCompanyInfo()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        BusinessLogic bl = new BusinessLogic(connStr);
        DataSet ds = bl.getCompanyDetails(connStr);

        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        return Convert.ToString(dr["CompanyName"]);
                    }
                }
            }
        }

        return string.Empty;
    }


    private void ResetFormControlValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlValues(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Text = "";
                        break;
                    case "System.Web.UI.WebControls.RadioButtonList":
                        {
                            //((RadioButtonList)c).SelectedValue = "Y";
                            for (int j = 0; j < ((RadioButtonList)c).Items.Count; j++)
                            {
                                ((RadioButtonList)c).Items[j].Selected = false;

                            }
                            break;
                        }
                    case "System.Web.UI.WebControls.DropDownList":
                        ((DropDownList)c).SelectedValue = "0";
                        break;

                }
            }
        }
    }

    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }
	
	private DataSet GetCustDetails(string area, string code)
	{
		OleDbConnection conn = null;
		OleDbDataAdapter da = null;
		DataSet data = new DataSet();

		try
		{
			string connStr = string.Empty;
			if (Session["Company"] != null)
				connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
			else
				Response.Redirect("~/Login.aspx");

			conn = new OleDbConnection(connStr);
			conn.Open();

            string query = "Select name,balance,category,effectDate,phoneno FROM CustomerMaster WHERE Category <> 'DC' and area = '" + area.Replace("'", "''") + "' and code = " + code;

			OleDbCommand cmd = new OleDbCommand(query, conn);
			da = new OleDbDataAdapter(cmd);
			da.Fill(data);

			if (data != null)
				return data;
			else
				return null;

		}
		finally
		{

			if (conn != null)
				conn.Close();
		}
	}

	protected void btnDetails_Click(object sender, ImageClickEventArgs e)
	{
		bool isValid = true;
		string category = string.Empty;

		if (ddArea.SelectedValue == "0")
		{
			isValid = false;
		}
		if (txtCustCode.Text == "")
		{
			isValid = false;
		}

		if (isValid)
		{
			DataSet ds = new DataSet();
			ds = GetCustDetails(ddArea.SelectedValue, txtCustCode.Text);

			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				txtCustName.Text = ds.Tables[0].Rows[0]["name"].ToString();
				txtCustBalance.Text = ds.Tables[0].Rows[0]["balance"].ToString();
				category = ds.Tables[0].Rows[0]["category"].ToString();
				DateTime effDate = DateTime.Parse(ds.Tables[0].Rows[0]["effectdate"].ToString());
                txtCustMobile.Text = ds.Tables[0].Rows[0]["phoneno"].ToString();

				txtAmount.Focus();
			}
			else
			{
				txtCustName.Text = "";
				txtCustBalance.Text = "";
				txtCustCode.Focus();
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer details not found. Please try again! ')", true);
				return;
			}
		}
		else
		{
			txtCustName.Text = "";
			txtCustBalance.Text = "";
			txtCustCode.Focus();
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please search by entering Customer Code and Area! ')", true);
			return;
		}

	}

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {
        
        int amount = 0;
        string reason = "";
        string dateEntered = "";
        int slNo = 0;
        string code = "";
        string area = "";

        try
        {

            if (ddArea.SelectedValue != "0")
            {
                area = ddArea.SelectedValue;
                Session["Area"] = ddArea.SelectedValue;
            }

            if (txtCustCode.Text != "")
                code = txtCustCode.Text;

            if (ddReason.SelectedValue != "0")
                reason = ddReason.SelectedValue;

            if (txtAmount.Text != "")
                amount = int.Parse(txtAmount.Text);

            if (txtDateEntered.Text != "")
                dateEntered = txtDateEntered.Text;

            int balance = BalanceAmount(area, code);

            string dateToday = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime tempDateEntered = DateTime.Parse(dateEntered);
            DateTime tempDateToday = DateTime.Parse(dateToday);


            if (tempDateEntered > tempDateToday)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date Paid Cannot be Greater than Todays Date.');", true);
                return;
            }


			if(CheckForClosedCustomer(area,code))
			{
				errorDisplay.AddItem("You are not allowed to enter the Adjustments for Closed Customer.", DisplayIcons.Error, false);
				return;
			}

            if (balance != -1)
            {

                string connStr = string.Empty;

                if (Session["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                BusinessLogic bll = new BusinessLogic();
                string recondate = txtDateEntered.Text.Trim(); ;

                if (!bll.IsValidDate(connStr, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
                    return;
                }

                using (OleDbConnection connection = new OleDbConnection(connStr))
                {
                    OleDbCommand command = new OleDbCommand();
                    OleDbTransaction transaction = null;
                    OleDbDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = connection;

                    // Open the connection and execute the transaction.
                    try
                    {
                        connection.Open();

                        // Start a local transaction with ReadCommitted isolation level.
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        command.Transaction = transaction;

                        // Execute the commands.
                        command.CommandText = string.Format("INSERT INTO Adjustment VALUES('{0}',{1},{2},'{3}',Format('{4}', 'dd/mm/yyyy'),{5},{6})", area.Replace("'", "''"), code, amount, reason, dateEntered, "NULL", "NULL");
                        command.ExecuteNonQuery();
                        balance = balance = balance + amount;
                        command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''"), code);
                        command.ExecuteNonQuery();

                        // Commit the transaction.
                        transaction.Commit();
                        errorDisplay.AddItem("Adjustments added successfully", DisplayIcons.GreenTick, false);
                        

                        string sCustomerContact = string.Empty;
                        string smsTEXT = string.Empty;
                        string UserID = Page.User.Identity.Name;

                        sCustomerContact = txtCustMobile.Text;
                        string compName = GetCompanyInfo();

                        if (Session["CopyRequired"] != null)
                        {
                            if (Session["Mobile"] != null)
                            {
                                sCustomerContact = sCustomerContact + "," + Session["Mobile"].ToString();
                            }
                        }


                        if (compName != string.Empty)
                            smsTEXT = "Message from " + compName + ". Your new Balance is Rs." + balance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");
                        else
                            smsTEXT = "Your new Balance is Rs." + balance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");

                        UtilitySMS utilSMS = new UtilitySMS();

                        if (Session["SMSRequired"] != null)
                        {
                            if ((Session["SMSRequired"].ToString() == "Yes") && (txtCustMobile.Text.Length == 10))
                            {
                                if (Session["Provider"] != null)
                                {
                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, false, UserID);
                                }
                                else
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                            }
                        }

                        ResetFormControlValues(this);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            errorDisplay.AddItem("Exception while Adding Adjustment : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            errorDisplay.AddItem("Exception while Rollback Adjustment : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                        }
                    }
                    // The connection is automatically closed when the
                    // code exits the using block.
                }

                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();

            }
            else
            {
                errorDisplay.AddItem("Customer Code and Area dosent match. Please check the details again.", DisplayIcons.Error, true);
            }
        }
        catch (Exception ex)
        {
            errorDisplay.AddItem("Exception Occured: " + ex.Message + ex.StackTrace, DisplayIcons.Error, true);
        }
    }
	
	private bool CheckForClosedCustomer(string area, string code)
	{
		string connStr = string.Empty;

		if (Session["Company"] != null)
			connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
		else
			Response.Redirect("~/Login.aspx");
		
		try
		{
			BusinessLogic objBus = new BusinessLogic(connStr);
			DataSet ds = GetCustDetails(area,code);

			if(ds != null)
			{
				if(ds.Tables[0].Rows.Count == 1)
				{
					if(ds.Tables[0].Rows[0]["Category"].ToString() == "DC")
						return true;
					else
						return false;
				}
				else
				{
					throw new Exception("Too Many rows returned for this Area : " + area + " Code : " + code);
				}
			}
			else
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			errorDisplay.AddItem("Exception Occured: " + ex.Message + ex.StackTrace, DisplayIcons.Error, true);
            return true;
		}

	}

    private int BalanceAmount(string area, string code)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select balance FROM CustomerMaster WHERE area = '" + area.Replace("'", "''") + "' and code = " + code;

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return -1;
            //reader = cmd.ExecuteReader();

            //datagrid.DataSource = reader;
            //datagrid.DataBind();
        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }
}
