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
using System.Net;
using System.IO;
using System.Data.OleDb;
using DataAccessLayer;
using System.Text;
using SMSLibrary;

public partial class MobileCashEntry : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/MobileLogin.aspx");


        string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            BtnSave.Enabled = false;

        srcArea.ConnectionString = connStr;
        srcReason.ConnectionString = connStr;

        hdDateEnt.Value = DateTime.Now.ToString("dd/MM/yyyy");

        if (!Page.IsPostBack)
        {
            txtDatePaid.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BusinessLogic objBus = new BusinessLogic(connStr);

            BtnSave.Attributes.Add("onclick", "Javascript:return ConfirmCancel();");
            BtnSave.Attributes.Add("onclick", "Javascript:return ValidatePaidDate();");

            if (Session["CashMode"] == null)
            {
                Session["CashMode"] = objBus.getCashEntryMode(connStr);
            }

            if (Session["CashMode"].ToString() != "Book")
            {
                ddBook.Enabled = false;
                txtBillNo.Enabled = true;
                cvBook.Enabled = false;
                txtBillNo.Focus();
                Label1.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
            }
            else
            {
                srcBook.ConnectionString = connStr;
                ddBook.DataSourceID = "srcBook";
                ddBook.AppendDataBoundItems = true;
                ddBook.Enabled = true;
                txtBillNo.Enabled = false;
                cvBook.Enabled = true;
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }

            if (Session["Area"] != null)
            {
                ddArea.ClearSelection();
                ddArea.SelectedValue = Session["Area"].ToString();
            }
            if (Session["CashMode"].ToString() == "Book")
            {
                if (Request.Cookies["BookID"] != null)
                {
                    if (ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()) != null)
                    {
                        ddBook.ClearSelection();
                        ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()).Selected = true;
                    }
                }
            }
        }

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
                        if (((DropDownList)c).Items.FindByValue("0") != null)
                        {
                            ((DropDownList)c).ClearSelection();
                            ((DropDownList)c).Items.FindByValue("0").Selected = true;
                        }
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
            DataSet ds = GetCustDetails(area, code);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    if (ds.Tables[0].Rows[0]["Category"].ToString() == "DC")
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

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {

        string billNo = "";
        int amount = 0;
        int discount = 0;
        string datepaid = string.Empty;
        string dateEntered = "";
        Int64 slNo = 0;
        string code = "";
        string area = "";
        string phone = "";
        int BookID = 0;
        int InstCash = 0;
        string CashType = string.Empty;
        int BookCash = 0;
        int CurrTotalAmount = 0;

        if (ddEntryType.SelectedValue != "CASH")
        {
            rvInstCash.Enabled = true;
            Page.Validate();
        }

        if (!Page.IsValid)
        {
            /*
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    errorDisplay.AddItem(validator.ErrorMessage, DisplayIcons.Error, false);
                }
            }*/
            return;
        }
        else
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/Login.aspx");

            if (ddBook.SelectedValue != "0")
            {
                BookID = int.Parse(ddBook.SelectedValue);
                Page.Response.Cookies.Add(new HttpCookie("BookID", ddBook.SelectedValue));
            }
            else
            {
                BookID = 0;
            }


            BusinessLogic bll = new BusinessLogic();
            string recondate = txtDatePaid.Text.Trim();

            if (!bll.IsValidDate(connStr, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.');", true);
                return;
            }

            if (Session["CashMode"].ToString() == "Book")
            {
                DataSet ds = bll.IsDatePaidValid(connStr, BookID);

                if (ds != null)
                {
                    string month = ds.Tables[0].Rows[0]["MonthPaid"].ToString();

                    DateTime paidDate = DateTime.Parse(txtDatePaid.Text);

                    if (month != (paidDate.Year.ToString() + paidDate.Month.ToString()))
                    {
                        string mnth = month.Remove(0, 4);
                        string year = month.Remove(4);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('DatePaid should be in the month of " + GetMonth(int.Parse(mnth)) + " " + year.ToString() + ". Please check Book details.');", true);
                        txtDatePaid.Focus();
                        return;
                    }
                }
            }

            if (txtCustCode.Text != "")
                code = txtCustCode.Text;

            if (ddArea.SelectedValue != "0")
            {
                area = ddArea.SelectedValue;
                Session["Area"] = ddArea.SelectedValue;
            }

            if (txtBillNo.Text != "")
                billNo = txtBillNo.Text;
            
            if(txtMobileNo.Text != "")
                phone = txtMobileNo.Text;

            if (txtDatePaid.Text != "")
            {
                datepaid = DateTime.Parse(txtDatePaid.Text).ToString("dd/MM/yyyy");
                Page.Response.Cookies.Add(new HttpCookie("DatePaid", txtDatePaid.Text));
            }

            if (txtAmount.Text != "")
                amount = int.Parse(txtAmount.Text);

            if (txtInstCash.Text != "")
                InstCash = int.Parse(txtInstCash.Text);

            CashType = ddEntryType.SelectedValue;

            dateEntered = DateTime.Now.ToString("dd/MM/yyyy");


            DateTime tempDatePaid = DateTime.Parse(datepaid);
            DateTime tempDateEntered = DateTime.Parse(dateEntered);


            if (tempDatePaid > tempDateEntered)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date Paid Cannot be Greater than Todays Date.');", true);
                return;
            }

            TimeSpan diff = tempDateEntered.Subtract(tempDatePaid);

            if (diff.Days > 30)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "confirm(' will go Negative. Are you sure you want to continue with this Cash Entry?')", true);
                //return;
            }

            if (CheckForClosedCustomer(area.Replace("'", "''"), code))
            {
                errorDisplay.AddItem("You are not allowed to enter the Cash for Closed Customer.", DisplayIcons.Error, false);
                return;
            }

            try
            {

                int balance = BalanceAmount(area.Replace("'", "''"), code);

                if (hidRetVal.Value == "false")
                {
                    return;
                }

                if (balance != -1)
                {

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

                            command.CommandText = "Select MAX(slNo) FROM CashDetails";
                            DataSet data = new DataSet();
                            adapter = new OleDbDataAdapter(command);
                            adapter.Fill(data);

                            if (data != null)
                            {
                                if (data.Tables[0].Rows.Count > 0)
                                {
                                    if (data.Tables[0].Rows[0][0].ToString() != "")
                                        slNo = Int64.Parse(data.Tables[0].Rows[0][0].ToString());
                                    else
                                        slNo = 0;
                                }
                            }
                            else
                            {
                                throw new Exception("Unable to get Max SlNo");
                            }
                            /*
                            if (Session["CashMode"].ToString() == "Book")
                            {
                                //Check the cash entered for this book is not crossing the maximum cash entered for this book
                                command.CommandText = string.Format("Select SUM(amount) as BookAmount from CashDetails Where BookID={0}", BookID);
                                string cashTotalAmount = command.ExecuteScalar().ToString();

                                command.CommandText = "Select SUM(amount) as InstAmount from InstallationCash Where BookID= " + BookID;
                                string InstTotalAmount = command.ExecuteScalar().ToString();


                                if (cashTotalAmount != "")
                                    CurrTotalAmount = int.Parse(cashTotalAmount);

                                if (InstTotalAmount != "")
                                    CurrTotalAmount = CurrTotalAmount + int.Parse(InstTotalAmount);

                                command.CommandText = string.Format("Select Amount from tblBook Where BookID={0}", BookID);
                                BookCash = (int)command.ExecuteScalar();

                                if (BookCash < (CurrTotalAmount + amount + InstCash))
                                {
                                    transaction.Rollback();
                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cash you entered is more than the Total Cash for this Book. Please try again! ');", true);
                                    return;
                                }

                            }*/
                            // Execute the commands.
                            command.CommandText = string.Format("INSERT INTO CashDetails(slno,code,area,amount,date_paid,date_entered,billno,BookID,ProcessedDate,TransNo) VALUES({0},{1},'{2}',{3},Format('{4}', 'dd/mm/yyyy'),Format('{5}', 'dd/mm/yyyy'),'{6}',{7},{8},{9})", slNo + 1, code, area.Replace("'", "''"), amount, datepaid, dateEntered, billNo, BookID, "NULL", "NULL");
                            command.ExecuteNonQuery();

                            balance = balance - amount - discount;
                            command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = {0},phoneno='{3}' WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''"), code, txtMobileNo.Text);
                            command.ExecuteNonQuery();

                            if ((CashType == "CASH"))
                            {
                                InstRow.Attributes.Add("class", "hidden");
                            }
                            else if ((CashType == "INST"))
                            {
                                command.CommandText = string.Format("INSERT Into InstallationCash(area,code,BookId,billno,CashType,Amount,EnteredDate,ProcessedDate,TransNo) VALUES('{0}',{1},{2},{3},'{4}',{5},Format('{6}', 'dd/mm/yyyy'),{7},{8})", area.Replace("'", "''"), code, BookID, billNo, CashType, InstCash, datepaid, "NULL", "NULL");
                                command.ExecuteNonQuery();
                                InstRow.Attributes.Add("class", "AdvancedSearch");
                            }
                            else if ((CashType == "REINST"))
                            {
                                command.CommandText = string.Format("INSERT Into InstallationCash(area,code,BookId,billno,CashType,Amount,EnteredDate,ProcessedDate,TransNo) VALUES('{0}',{1},{2},{3},'{4}',{5},Format('{6}', 'dd/mm/yyyy'),{7},{8})", area.Replace("'", "''"), code, BookID, billNo, CashType, InstCash, datepaid, "NULL", "NULL");
                                command.ExecuteNonQuery();
                                InstRow.Attributes.Add("class", "AdvancedSearch");
                            }

                            if (Session["CashMode"].ToString() == "Book")
                            {

                                command.CommandText = string.Format("Select EndEntry from tblBook Where BookID={0}", BookID);
                                int EndEntry = (int)command.ExecuteScalar();

                                if (EndEntry.ToString() == billNo)
                                {
                                    //command.CommandText = string.Format("UPDATE tblBook SET BookStatus = 'Closed' WHERE BookID = {0}", BookID);
                                    //command.ExecuteNonQuery();
                                    //ddBook.Items.Remove(new ListItem(ddBook.SelectedItem.Text, ddBook.SelectedValue));

                                    //if (EndEntry.ToString() == billNo)
                                    //errorDisplay.AddItem("You have entered the Maximum Bill No for this Book.", DisplayIcons.GreenTick, false);
                                    command.CommandText = string.Format("UPDATE tblBook SET NextEntry = {0} WHERE BookID = {1}", int.Parse(billNo) + 1, BookID);
                                    command.ExecuteNonQuery();
                                }
                                else
                                {
                                    command.CommandText = string.Format("UPDATE tblBook SET NextEntry = {0} WHERE BookID = {1}", int.Parse(billNo) + 1, BookID);
                                    command.ExecuteNonQuery();
                                }
                            }
                            // Commit the transaction.
                            transaction.Commit();
                            errorDisplay.AddItem("Cash details added successfully. Customer new balance is Rs." + balance.ToString(), DisplayIcons.GreenTick, false);
                            ResetFormControlValues(this);
                            string compName = GetCompanyInfo();

                            //string sURL;
                            //StreamReader objReader;
                            //give the USERNAME,PASSWORD,moblienumbers.... on URL 
                            //string provider = "http://sms.full2sms.com/pushsms.php";
                            //string userName = "muthukumar";
                            //string password = "muthukumar123";
                            ///string no1 = phone;
                            string msgText = string.Empty;
                            string UserID = Page.User.Identity.Name;

                            if (compName != string.Empty)
                                msgText = "Message from " + compName + ". Thanks for your Payment of Rs." + (amount + InstCash).ToString() + ". Your new Balance is Rs." + balance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");
                            else
                                msgText = "Thanks for your Payment of Rs." + (amount + InstCash).ToString() + ". Your new Balance is Rs." + balance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");

                            if(Session["CopyRequired"] != null)
                            {
                                if (Session["Mobile"] != null)
                                {
                                    phone = phone + "," + Session["Mobile"].ToString();
                                }
                            }

                            //SMSLibrary.UtilitySMS utilSMS = new UtilitySMS();
                            //utilSMS.SendSMS(provider, "2", "test", userName, password, phone, msgText, false, Page.User.Identity.Name);

                            UtilitySMS utilSMS = new UtilitySMS();

                            if (Session["SMSRequired"] != null)
                            {
                                if ((Session["SMSRequired"].ToString() == "Yes") && (txtMobileNo.Text.Length == 10))
                                {
                                    if (Session["Provider"] != null)
                                    {
                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), phone, msgText, false, UserID);
                                    }
                                    else
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                // Attempt to roll back the transaction.
                                transaction.Rollback();
                                errorDisplay.AddItem("Exception while Adding Cash : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                            }
                            catch (Exception ep)
                            {
                                // Do nothing here; transaction is not active.
                                errorDisplay.AddItem("Exception while Rollback the Cash Transaction : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                            }
                        }
                        // The connection is automatically closed when the
                        // code exits the using block.
                    }

                    //slNo = GetNextSeqNo();

                    //string sqlInsert = string.Format("INSERT INTO CashDetails VALUES({0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}')", slNo + 1, code, area.Replace("'","''"), amount, discount, reason, datepaid, dateEntered, billNo);

                    //ExecuteNonQuery(sqlInsert);

                    //balance = balance - amount - discount;

                    //string updateSql = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''") , code);

                    //ExecuteNonQuery(updateSql);

                    //errDisp.AddItem("Cash details added successfully", DisplayIcons.GreenTick, true);

                }
                else
                {
                    errorDisplay.AddItem("Customer Code and Area dosent match, please check the details again", DisplayIcons.Error, true);
                }


                if (Session["Area"] != null)
                {
                    ddArea.ClearSelection();
                    ddArea.SelectedValue = Session["Area"].ToString();
                }
                if (Page.Request.Cookies["DatePaid"] != null)
                    txtDatePaid.Text = Page.Request.Cookies["DatePaid"].Value;

                if (Page.Request.Cookies["BookID"] != null)
                {
                    if (ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()) != null)
                    {
                        ddBook.ClearSelection();
                        ddBook.SelectedValue = Request.Cookies["BookID"].Value;
                    }
                }
                ddBook_SelectedIndexChanged(this, null);

                //SendSMS();

                txtCustCode.Focus();

            }
            catch (Exception ex)
            {
                errorDisplay.AddItem("Exception Occured: " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
            }
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

    private int GetNextSeqNo()
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
                Response.Redirect("~/MobileLogin.aspx");


            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select MAX(slNo) FROM CashDetails";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return 0;

        }
        finally
        {
            if (conn != null)
                conn.Close();
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
                Response.Redirect("~/MobileLogin.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select balance FROM CustomerMaster WHERE area = '" + area + "' and code = " + code;

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return -1;

        }
        finally
        {

            if (conn != null)
                conn.Close();
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
                Response.Redirect("~/MobileLogin.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select name,balance,category,effectDate,phoneno FROM CustomerMaster WHERE Category <> 'DC' and area = '" + area + "' and code = " + code;

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

    }

    private string GetMonth(int month)
    {
        string Retmonth = string.Empty;

        switch (month)
        {
            case 1:
                Retmonth = "January";
                break;
            case 2:
                Retmonth = "February";
                break;
            case 3:
                Retmonth = "March";
                break;
            case 4:
                Retmonth = "April";
                break;
            case 5:
                Retmonth = "May";
                break;
            case 6:
                Retmonth = "June";
                break;
            case 7:
                Retmonth = "July";
                break;
            case 8:
                Retmonth = "August";
                break;
            case 9:
                Retmonth = "September";
                break;
            case 10:
                Retmonth = "October";
                break;
            case 11:
                Retmonth = "November";
                break;
            default:
                Retmonth = "December";
                break;
        }

        return Retmonth;
    }

    protected void ddEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddEntryType.SelectedValue == "CASH")
        {
            InstRow.Attributes.Add("class", "hidden");
            rvInstCash.Enabled = false;
        }
        else if (ddEntryType.SelectedValue == "INST")
        {
            InstRow.Attributes.Add("class", "AdvancedSearch");
            rvInstCash.Enabled = true;
        }
        else if (ddEntryType.SelectedValue == "REINST")
        {
            InstRow.Attributes.Add("class", "AdvancedSearch");
            rvInstCash.Enabled = true;
        }
    }

    protected void ddBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = Session["Company"].ToString();
        else
            Response.Redirect("~/MobileLogin.aspx");

        if (ddBook.SelectedValue != "0")
        {
            try
            {
                BusinessLogic objBus = new BusinessLogic();
                DataSet ds = objBus.GetNextBillNo(connStr, int.Parse(ddBook.SelectedValue));

                txtBillNo.Text = string.Empty;

                if (ds != null)
                    txtBillNo.Text = ds.Tables[0].Rows[0]["NextEntry"].ToString();
                else
                    errorDisplay.AddItem("You have entered the Max Bill no for this book. Please check <b>Manage Books</b> for further Information.");


                DBManager manager = new DBManager(DataProvider.OleDb);
                manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
                manager.Open();

                object TotalCash = manager.ExecuteScalar(CommandType.Text, "Select Amount From tblBook Where BookID=" + ddBook.SelectedValue);

                object CashEntered = manager.ExecuteScalar(CommandType.Text, "Select SUM(amount) as amount from CashDetails Where BookID= " + ddBook.SelectedValue);

                object InstCashEntered = manager.ExecuteScalar(CommandType.Text, "Select SUM(amount) as amount from InstallationCash Where BookID= " + ddBook.SelectedValue);

                int TotalKash = 0;
                int EntetedKash = 0;
                int InstKashEntered = 0;
                
                if (TotalCash != null)
                {
                    if (TotalCash.ToString() != "")
                    {
                        TotalKash = int.Parse(TotalCash.ToString());
                    }
                    lblTotalAmount.Text = TotalKash.ToString();
                }

                if (InstCashEntered != null)
                {
                    if (InstCashEntered.ToString() != "")
                    {
                        InstKashEntered = int.Parse(InstCashEntered.ToString());
                    }
                }

                if (CashEntered != null)
                {
                    if (CashEntered.ToString() != "")
                    {
                        EntetedKash = int.Parse(CashEntered.ToString());
                    }
                }

                EntetedKash = EntetedKash + InstKashEntered;
                lblCashEnteted.Text = EntetedKash.ToString();

                //int cashRem = TotalKash - EntetedKash;
                //lblCashRem.Text = cashRem.ToString();

                manager.Dispose();

                ddArea.Focus();

            }
            catch (Exception ex)
            {
                errorDisplay.AddItem("Exception :" + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
            }
        }
    }

    
    private void SendSMS()
    {

    }

    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        bool isValid = true;
        string category = string.Empty;

        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = Session["Company"].ToString();
        else
            Response.Redirect("~/MobileLogin.aspx");

        if (ddBook.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the book.')", true);
            return;
        }

        BusinessLogic objBus = new BusinessLogic();
        DataSet dsr = objBus.GetNextBillNo(connStr, int.Parse(ddBook.SelectedValue));

        if (dsr == null)
        {
            errorDisplay.AddItem("You have entered the Max Bill no for this book. Please check <b>Manage Books</b> for further Information.");
            return;
        }
        if (dsr.Tables[0].Rows.Count == 0)
        {
            errorDisplay.AddItem("You have entered the Max Bill no for this book. Please check <b>Manage Books</b> for further Information.");
            return;
        }
        ddBook_SelectedIndexChanged(this, null);

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
            ds = GetCustDetails(ddArea.SelectedValue.Replace("'", "''"), txtCustCode.Text);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtCustName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtCustBalance.Text = ds.Tables[0].Rows[0]["balance"].ToString();
                category = ds.Tables[0].Rows[0]["category"].ToString();
                txtMobileNo.Text = ds.Tables[0].Rows[0]["phoneno"].ToString();
                DateTime effDate = DateTime.Parse(ds.Tables[0].Rows[0]["effectdate"].ToString());

                txtBillNo.Focus();
                if (txtCustBalance.Text == "0")
                {
                    if (category == "NC")
                    {
                        ddEntryType.SelectedValue = "INST";
                    }
                    else if (category == "RC")
                    {
                        ddEntryType.SelectedValue = "REINST";
                    }
                    else
                    {
                        ddEntryType.SelectedValue = "CASH";
                    }

                    ddEntryType_SelectedIndexChanged(this, null);

                    TimeSpan timespan = DateTime.Now.Subtract(effDate);

                    txtMobileNo.Focus();

                    if (timespan.Days <= 30)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Balance Amount for this Customer is 0. Cash Entry Type should be selected as Installation/Re-Installation Cash. ')", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Balance Amount for this Customer is 0. Are you sure you wish to Add the cash for this customer? ')", true);
                        return;
                    }

                    
                }
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

        txtMobileNo.Focus();

    }
    protected void BtnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/MobileDefault.aspx");
    }
}
