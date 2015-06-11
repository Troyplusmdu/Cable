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

public partial class ModifyCash : System.Web.UI.Page
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
            grdCash.Columns[7].Visible = false;
        }

        srcArea.ConnectionString = connStr;
        srcBook.ConnectionString = connStr;
        srcReason.ConnectionString = connStr;


    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdCash.RowUpdated += new GridViewUpdatedEventHandler(ddBook_SelectedIndexChanged);
        ObjectDataSource1.SelectParameters.Add(new SessionParameter("connection", "Company"));
        ObjectDataSource1.SelectParameters.Add(new ControlParameter("billNo", TypeCode.String, txtBillNo.UniqueID, "Text"));
        ObjectDataSource1.SelectParameters.Add(new ControlParameter("bookID", TypeCode.Int32, ddBook.UniqueID, "SelectedValue"));
    }

    protected void grdCash_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void grdCash_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void grdCash_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //grdCash.EditIndex = e.NewEditIndex;
        //GridViewRow editingRow = grdCash.Rows[e.NewEditIndex];
        //DropDownList ddlPbx = (editingRow.FindControl("ddReason") as DropDownList);
        //string val = grdCash.Rows[grdCash.EditIndex].Cells[5].ToString();

        //if (ddlPbx != null)
        //{
        //    if (ddlPbx.Items.FindByValue(val) != null)
        //        ddlPbx.Items.FindByValue(val).Selected = true;
        //    else
        //        ddlPbx.ClearSelection();
        //} 
        
    }

   
    protected void grdCash_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string bookName = ((TextBox)grdCash.Rows[e.RowIndex].FindControl("txtBookName")).Text;
        string code = ((TextBox)grdCash.Rows[e.RowIndex].FindControl("txtCode")).Text;
        string amount = ((TextBox)grdCash.Rows[e.RowIndex].FindControl("txtAmount")).Text;
        string discount = ((TextBox)grdCash.Rows[e.RowIndex].FindControl("txtDiscount")).Text;
        string datepaid = ((TextBox)grdCash.Rows[e.RowIndex].FindControl("txtDatePaid")).Text;
        string area = ((DropDownList)grdCash.Rows[e.RowIndex].FindControl("ddArea")).SelectedValue;
        string reason = ((DropDownList)grdCash.Rows[e.RowIndex].FindControl("ddReason")).SelectedValue;
        string slno = grdCash.DataKeys[e.RowIndex].Value.ToString();

        DateTime tempDatePaid = DateTime.Parse(datepaid);
        DateTime tempDateEntered = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));


        if (tempDatePaid > tempDateEntered)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date Paid Cannot be Greater than Todays Date.');", true);
            return;
        }

      
        string connStr = string.Empty;

        string BookID = ddBook.SelectedValue;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        BusinessLogic bll = new BusinessLogic();
        string recondate = datepaid;

        if (!bll.IsValidDate(connStr, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
            grdCash.EditIndex = e.RowIndex;
            return;
        }

        DataSet ds = bll.IsDatePaidValid(connStr, int.Parse( BookID));

        if (ds != null)
        {
            string month = ds.Tables[0].Rows[0]["MonthPaid"].ToString();

            DateTime paidDate = DateTime.Parse(datepaid);

            if (month != ( paidDate.Year.ToString() + paidDate.Month.ToString()))
            {
                string mnth = month.Remove(0, 4);
                string year = month.Remove(4);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('DatePaid should be in the month of " + GetMonth(int.Parse(mnth))+" " +year.ToString() + ". Please check Book details.');", true);
                return;
            }
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

                command.CommandText = string.Format("Select Count(*) from CustomerMaster WHERE area = '{0}' and code = {1}", area.Replace("'", "''"), code);
                int exists = (int)command.ExecuteScalar();

                if (exists < 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer Details not found. Please check the Customer code and area.')", true);
                    return;
                }

                command.CommandText = string.Format("Select Amount from tblBook Where BookID={0}", BookID);
                int BookCash = (int)command.ExecuteScalar();

                command.CommandText = string.Format( "Select code,area,amount,discount FROM CashDetails Where slno={0}",slno);
                DataSet data = new DataSet();
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(data);

                if(data != null)
                {
                    if (data.Tables[0].Rows.Count == 1)
                    {
                        string old_code = data.Tables[0].Rows[0]["code"].ToString();
                        string old_area = data.Tables[0].Rows[0]["area"].ToString();
                        int old_amount = int.Parse(data.Tables[0].Rows[0]["amount"].ToString());
                        int old_discount = int.Parse(data.Tables[0].Rows[0]["discount"].ToString());


                        command.CommandText = string.Format("Select SUM(amount) as BookAmount from CashDetails Where BookID={0}", BookID);
                        string cashtotalamount  = command.ExecuteScalar().ToString();

                        command.CommandText = "Select SUM(amount) as InstAmount from InstallationCash Where BookID= " + BookID;
                        string insttotalamount = command.ExecuteScalar().ToString();

                        command.CommandText = string.Format("Select EndEntry from tblBook Where BookID={0}", BookID);
                        int BookEndEntry = (int)command.ExecuteScalar();

                        command.CommandText = string.Format("Select MAX(billno) as Bill from CashDetails Where BookID={0}", BookID);
                        string CashEndEntry = command.ExecuteScalar().ToString();

                        double cashTotalAmount = 0;
                        double InstTotalAmount = 0;

                        if(cashtotalamount != "")
                            cashTotalAmount = double.Parse(cashtotalamount);
                        
                        if (insttotalamount != "")
                            InstTotalAmount = double.Parse(insttotalamount);

                        if (((cashTotalAmount + InstTotalAmount) - old_amount + double.Parse( amount) ) > BookCash)
                        {
                            transaction.Rollback();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cash you entered is more than the Total Cash for this Book. Please try again! ');", true);
                            return;
                        }
                        else if ((((cashTotalAmount + InstTotalAmount) - old_amount + double.Parse( amount)) == BookCash) && (BookEndEntry == int.Parse( CashEndEntry)))
                        {
                            command.CommandText = string.Format("UPDATE tblBook SET BookStatus = 'Closed' WHERE BookID = {0}", BookID);
                            command.ExecuteNonQuery();
                            ddBook.Items.Remove(new ListItem(ddBook.SelectedItem.Text, ddBook.SelectedValue));
                            errorDisplay.AddItem("You have entered the Maximum Amount for this Book. Book is closed now.", DisplayIcons.GreenTick, false);
                        }

                        command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = Balance + {0} WHERE area = '{1}' and code = {2}", old_amount + old_discount, old_area.Replace("'", "''"), old_code);
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("UPDATE CashDetails SET code={0},area='{1}',amount={2},discount={3},reason='{4}',date_paid=Format('{5}', 'dd/mm/yyyy') Where slno={6}", code, area.Replace("'", "''"), amount, discount, reason, datepaid, slno);
                        command.ExecuteNonQuery();

                        int newBalance = int.Parse( amount ) + int.Parse( discount);

                        command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = Balance - {0} WHERE area = '{1}' and code = {2}", newBalance, area.Replace("'", "''"), code);
                        command.ExecuteNonQuery();

                        
                        string smsTEXT = string.Empty;
                        string UserID = Page.User.Identity.Name;
                        string compName = GetCompanyInfo();
                        string sCustomerContact = "";
                        UtilitySMS utilSMS = new UtilitySMS();

                        command.CommandText = string.Format("Select phoneno from CustomerMaster WHERE area = '{0}' and code = {1}", area.Replace("'", "''"), code);
                        sCustomerContact = (string)command.ExecuteScalar();
                        

                        if (Session["CopyRequired"] != null)
                        {
                            if (Session["Mobile"] != null)
                            {
                                sCustomerContact = sCustomerContact + "," + Session["Mobile"].ToString();
                            }
                        }


                        if (compName != string.Empty)
                            smsTEXT = "Message from " + compName + ". Your new Balance is Rs." + newBalance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");
                        else
                            smsTEXT = "Your new Balance is Rs." + newBalance.ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");

                        
                        if (Session["SMSRequired"] != null)
                        {
                            if ((Session["SMSRequired"].ToString() == "Yes"))
                            {
                                if (Session["Provider"] != null)
                                {
                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, false, UserID);
                                }
                                else
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                            }
                        }

                        command.CommandText = string.Format("Select phoneno from CustomerMaster WHERE area = '{0}' and code = {1}", old_area.Replace("'", "''"), old_code);
                        sCustomerContact = (string)command.ExecuteScalar();


                        if ((area.Replace("'", "''") != old_area.Replace("'", "''")) || (old_code != code))
                        {

                            if (Session["CopyRequired"] != null)
                            {
                                if (Session["Mobile"] != null)
                                {
                                    sCustomerContact = sCustomerContact + "," + Session["Mobile"].ToString();
                                }
                            }


                            if (compName != string.Empty)
                                smsTEXT = "Message from " + compName + ". Your new Balance is Rs." + (old_amount + old_discount).ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");
                            else
                                smsTEXT = "Your new Balance is Rs." + (old_amount + old_discount).ToString() + " as on " + DateTime.Now.ToString("dd/MM/yyyy");


                            if (Session["SMSRequired"] != null)
                            {
                                if ((Session["SMSRequired"].ToString() == "Yes"))
                                {
                                    if (Session["Provider"] != null)
                                    {
                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, false, UserID);
                                    }
                                    else
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                                }
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("Too Many rows returned from the query");
                    }
                }

                transaction.Commit();

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
                    errorDisplay.AddItem("Exception while Adding Cash : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                }
            }
        }
    }
    protected void grdCash_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(grdCash, e.Row, this);
        }

    }
    protected void grdCash_DataBound(object sender, EventArgs e)
    {

    }
    protected void grdCash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //((Label)e.Row.FindControl("lblDatePaid")).Attributes.Add("onclick", "return ValidatePaidDate();");
        }
    }

    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }
    protected void ddBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = Session["Company"].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        if (ddBook.SelectedValue != "0")
        {
            try
            {
                BusinessLogic objBus = new BusinessLogic();
            
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

                int cashRem = TotalKash - EntetedKash;
                lblCashRem.Text = cashRem.ToString();

                manager.Dispose();

            }
            catch (Exception ex)
            {
                errorDisplay.AddItem("Exception :" + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
            }
        }
    }
    protected void grdCash_RowUpdated(object sender, GridViewUpdatedEventArgs e)
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
}
