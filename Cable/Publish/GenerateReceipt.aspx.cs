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

public partial class GenerateReceipt : System.Web.UI.Page
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

        if (Session["SMSRequired"] != null)
        {
            if (Session["SMSRequired"].ToString() == "Yes")
            {
                hdSMSRequired.Value = "YES";
            }
        }

        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            lnkBtncancel.Enabled = false;
        }

        if (!Page.IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DateInString");
            dt.Columns.Add("DateVal");

            DataRow dr3 = dt.NewRow();
            dr3["DateInString"] = GetDates(DateTime.Now.AddMonths(-3));
            dr3["DateVal"] = DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy");
            dt.Rows.Add(dr3);


            DataRow dr = dt.NewRow();
            dr["DateInString"] = GetDates(DateTime.Now.AddMonths(-2));
            dr["DateVal"] = DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy");
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["DateInString"] = GetDates(DateTime.Now.AddMonths(-1));
            dr1["DateVal"] = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["DateInString"] = GetDates(DateTime.Now);
            dr2["DateVal"] = DateTime.Now.ToString("dd/MM/yyyy");
            dt.Rows.Add(dr2);

            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "DateInString";
            DropDownList1.DataValueField = "DateVal";
            DropDownList1.DataBind();

        }
    }

    private string GetDates(DateTime dt)
    {
        string monthYear = string.Empty;

        switch (dt.Month)
        {
            case 1:
                monthYear = "January";
                break;
            case 2:
                monthYear = "February";
                break;
            case 3:
                monthYear = "March";
                break;
            case 4:
                monthYear = "April";
                break;
            case 5:
                monthYear = "May";
                break;
            case 6:
                monthYear = "June";
                break;
            case 7:
                monthYear = "July";
                break;
            case 8:
                monthYear = "August";
                break;
            case 9:
                monthYear = "September";
                break;
            case 10:
                monthYear = "October";
                break;
            case 11:
                monthYear = "November";
                break;
            default:
                monthYear = "December";
                break;
        }

        return monthYear + " " + dt.Year.ToString();

    }


    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
			errorDisplay.AddItem("Exception while Exec Query : " + e.Message + e.StackTrace, DisplayIcons.Error, false);
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

    private DataSet GetAllReceiptDates()
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

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = string.Format("Select distinct date_print FROM Receipt");

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return data;
            else
                return null;

        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }

    private bool CheckIfDateExists(DateTime enteredDate)
    {
        DataSet ds = GetAllReceiptDates();

        string entDate = GetDates(enteredDate);
        string dbDate = string.Empty;

        if (ds != null)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dbDate = GetDates(DateTime.Parse(dr[0].ToString()));

                if (entDate == dbDate)
                {
                    return true;
                }
            }
        }
        else
        {
            return false;
        }

        return false;
    }

    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {

        //_UserControl_errordisplay errDisp = (_UserControl_errordisplay)this.Master.FindControl("errorDisplay");

        try
        {
            System.IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            if (DropDownList1.SelectedValue != "0" && !CheckIfDateExists(DateTime.Parse(DropDownList1.SelectedValue, format, System.Globalization.DateTimeStyles.NoCurrentDateDefault)))
            {
				
				BusinessLogic objBus = new BusinessLogic();
				/*
				if (Session["CashMode"] != null)
				{
					if(Session["CashMode"].ToString() == "Book")
					{
						if(objBus.CheckOpenBooks(Session["Company"].ToString()))
						{
							errorDisplay.AddItem("Please Close all the Books before you Generate the Receipt.", DisplayIcons.Error, false);
							return;
						}
					}
				}*/

                if (CheckforFutureMonths())
                {
                    errorDisplay.AddItem("You are not allowed to Generate Receipt for this month.", DisplayIcons.Error, false);
                    return;
                }

                CustomerData data = null;
                string datePrint = DropDownList1.SelectedValue;
                string connStr = string.Empty;

                if (Session["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
                else
                    Response.Redirect("login.aspx");

                using (OleDbConnection connection = new OleDbConnection(connStr))
                {
                    OleDbCommand command = new OleDbCommand();
                    OleDbTransaction transaction = null;

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
                        command.CommandText = "Insert into receipt(area,code,monthlycharge,date_print) select area,code,monthlycharge, Format('" + datePrint + "', 'dd/mm/yyyy') from customermaster where (category = 'NC' or category = 'RC')";
                        command.ExecuteNonQuery();
                        command.CommandText = "update customermaster set balance = (balance + monthlycharge) where (category='NC' or category='RC')";
                        command.ExecuteNonQuery();
                        
                        // Commit the transaction.
                        transaction.Commit();
                        errorDisplay.AddItem("All receipts generated successfully...", DisplayIcons.GreenTick, false);

                        string phone = string.Empty;
                        string UserID = Page.User.Identity.Name;
                        CustomerData dsCusts = ReadRecords();
                        string compName = GetCompanyInfo();
                        string msgText = string.Empty;

                        if (Session["SMSRequired"] != null)
                        {
                            if (Session["SMSRequired"].ToString() == "Yes" && hdSMS.Value == "YES")
                            {
                                foreach(DataRow dr in dsCusts.Tables[0].Rows)
                                {
                                    msgText = "Message from "+ compName +". This month's cable charge is Rs." + dr["monthlycharge"].ToString() + " . Total Balance due is Rs." + dr["balance"].ToString();
                                    phone = dr["phoneno"].ToString();

                                    if (Session["Provider"] != null)
                                    {
                                        if (phone.Length == 10)
                                        {
                                            SMSLibrary.UtilitySMS utilSMS = new UtilitySMS();
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), phone, msgText, false, Page.User.Identity.Name);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            errorDisplay.AddItem("Exception while Generating receipts : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            errorDisplay.AddItem("Exception while Generating receipts : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                        }
                    }
                    // The connection is automatically closed when the
                    // code exits the using block.
                }

            }
            else
            {
                errorDisplay.AddItem("You have already generated Bill for the selected month", DisplayIcons.Error, false);
            }
        }
        catch (Exception ex)
        {
            errorDisplay.AddItem(ex.Message + ex.StackTrace, DisplayIcons.Error, true);
        }
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


    public void ExecuteTransaction(string connectionString)
    {

    }

    private string GetCustomerPhoneNumbers()
    {
        CustomerData data = ReadRecords();
        string phoneNos = string.Empty;

        foreach(DataRow dr in data.Tables[0].Rows)
        {
            phoneNos = phoneNos + dr["phoneno"].ToString();
            phoneNos = phoneNos + ",";
        }

        return phoneNos;
    }

    private bool CheckforFutureMonths()
    {
        CustomerData data = ReadRecords();
        string datePrint = DropDownList1.SelectedValue;

        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select count(*) FROM Receipt where date_print >= #" + DateTime.Parse(datePrint).ToString("MM/dd/yyyy") + "#";

            OleDbCommand cmd = new OleDbCommand(query, conn);

            object count = cmd.ExecuteScalar();

            if (count.ToString() != "")
            {
                if (int.Parse(count.ToString()) > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }


        }
        finally
        {
            if (conn != null)
                conn.Close();
        }

    }


    private CustomerData ReadRecords()
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select area, code, name, category, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed FROM CustomerMaster where (category = 'NC' or category = 'RC')";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.CommandTimeout = 0;
            da = new OleDbDataAdapter(cmd);
            da.Fill(data._CustomerData);
            return data;

        }
        finally
        {
            if (conn != null)
                conn.Close();
        }
    }

}
