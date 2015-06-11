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
using System.Data.OleDb;

public partial class UpdateTransactions : System.Web.UI.Page
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
            btnRecTrans.Enabled = false;
        } 

    }
    protected void btnRecTrans_Click(object sender, EventArgs e)
    {
        UpdateReceiptTransactions();

        UpdateCashTransactions();

        UpdateAdjustmentTransactions();

    }

    private void UpdateReceiptTransactions()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");

        using (OleDbConnection connection = new OleDbConnection(connStr))
        {
            OleDbCommand command = new OleDbCommand();
            OleDbTransaction transaction = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DateTime TransDate;
            // Set the Connection to the new OleDbConnection.
            command.Connection = connection;
            DataSet dsDates = new DataSet();
            // Open the connection and execute the transaction.
            try
            {
                connection.Open();

                // Start a local transaction with ReadCommitted isolation level.
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                // Assign transaction object for a pending local transaction.
                command.Connection = connection;
                command.Transaction = transaction;
                adapter.SelectCommand = command;
                command.CommandText = string.Format("Select Distinct date_print from Receipt Where ProcessedDate IS NULL and TransNo IS NULL");
                adapter.Fill(dsDates);

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Cable Customers A/C'");
                string DebitorID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Subscription A/C'");
                string CreditorID = command.ExecuteScalar().ToString();

                if (dsDates.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow currRow in dsDates.Tables[0].Rows)
                    {
                        TransDate = DateTime.Parse( currRow["date_print"].ToString());
                        command.CommandText = string.Format("Select Sum(monthlycharge) as Amount from Receipt Where date_print=Format('{0}','dd/MM/yyyy') And ProcessedDate IS NULL and TransNo IS NULL",TransDate.ToShortDateString());
                        string Amount = command.ExecuteScalar().ToString();

                        if ((Amount == "0") || (Amount == ""))
                        {
                            //transaction.Rollback();
                            //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records to Update. Please try next time! ');", true);
                            errorDisplay.AddItem("No Receipt Transactions to Update.", DisplayIcons.Information, false);
                            return;
                        }

                        string Narration = "Receipt Transaction Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;

                        string VoucherType = "Service";

                        string RefNo = "0";

                        string dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                            TransDate.ToShortDateString(), DebitorID, CreditorID, Amount, Narration, VoucherType, RefNo);

                        command.CommandText = dbQry;
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                        int TransNo = (Int32)command.ExecuteScalar();

                        dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);
                        command.CommandText = dbQry;
                        double Debit = (double)command.ExecuteScalar();

                        dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + double.Parse(Amount), DebitorID);
                        command.CommandText = dbQry;
                        command.ExecuteNonQuery();

                        dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CreditorID);
                        command.CommandText = dbQry;
                        double Credit = (double)command.ExecuteScalar();

                        dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + double.Parse(Amount), CreditorID);
                        command.CommandText = dbQry;
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Update Receipt Set ProcessedDate = Format('{0}', 'dd/mm/yyyy'),TransNo = {1} Where date_print=Format('{2}','dd/MM/yyyy') And ProcessedDate IS NULL and TransNo IS NULL", DateTime.Now.ToShortDateString(), TransNo, TransDate.ToShortDateString());
                        command.ExecuteNonQuery();

                        errorDisplay.AddItem("Receipt Transaction Update successfully for " + TransDate.ToShortDateString(), DisplayIcons.GreenTick, false);
                    }

                    transaction.Commit();
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
        }

    }
    private void UpdateCashTransactions()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");

        using (OleDbConnection connection = new OleDbConnection(connStr))
        {
            OleDbCommand command = new OleDbCommand();
            OleDbTransaction transaction = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string VoucherType = "Service";
            string RefNo = "0";
            string Narration = string.Empty;
            string dbQry = string.Empty;
            DateTime TransDate;
            // Set the Connection to the new OleDbConnection.
            command.Connection = connection;
            DataSet dsDates = new DataSet();

            // Open the connection and execute the transaction.
            try
            {
                connection.Open();

                // Start a local transaction with ReadCommitted isolation level.
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                // Assign transaction object for a pending local transaction.
                command.Connection = connection;
                command.Transaction = transaction;
                adapter.SelectCommand = command;
                command.CommandText = string.Format("Select Distinct DatePart('yyyy', date_paid) & DatePart('m', date_paid) AS monthyear from CashDetails Where ProcessedDate IS NULL and TransNo IS NULL");
                adapter.Fill(dsDates);

                string processingMonth ;

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Cash A/C'");
                string DebitorID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Subscription A/C'");
                string SubAccID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Cable Customers A/C'");
                string CableCustID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Connection A/C'");
                string ConnAccID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Re-Connection A/C'");
                string ReConnAccID = command.ExecuteScalar().ToString();
				string SubAmount = string.Empty;
				string InstAmount = string.Empty;
				string ReInstAmount = string.Empty;

                if (dsDates.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow currRow in dsDates.Tables[0].Rows)
                    {
                        processingMonth = currRow["monthyear"].ToString();
                        TransDate = DateTime.Parse("15/" + processingMonth.Remove(0,4) +"/" + processingMonth.Remove(4));

                        if (Session["CashMode"].ToString() != "Book")
                        {
                            command.CommandText = string.Format("Select Sum(amount) as Amount from CashDetails Where ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_paid) & DatePart('m', date_paid)='{0}'", processingMonth);
                            SubAmount = command.ExecuteScalar().ToString();

                            command.CommandText = string.Format("Select Sum(amount) as Amount from InstallationCash Where CashType='INST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', EnteredDate) & DatePart('m', EnteredDate)='{0}'", processingMonth);
                            InstAmount = command.ExecuteScalar().ToString();

                            command.CommandText = string.Format("Select Sum(amount) as Amount from InstallationCash Where CashType='REINST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', EnteredDate) & DatePart('m', EnteredDate)='{0}'", processingMonth);
                            ReInstAmount = command.ExecuteScalar().ToString();

                        }
                        else
                        {
                            command.CommandText = string.Format("Select Sum(CashDetails.amount) as Amount from CashDetails inner join tblBook on CashDetails.BookId = tblBook.BookId Where tblBook.BookStatus = 'Closed' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', CashDetails.date_paid) & DatePart('m', CashDetails.date_paid)='{0}'", processingMonth);
                            SubAmount = command.ExecuteScalar().ToString();

                            command.CommandText = string.Format("Select Sum(InstallationCash.amount) as Amount from InstallationCash inner join tblBook on InstallationCash.BookId = tblBook.BookId Where tblBook.BookStatus = 'Closed' and CashType='INST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', InstallationCash.EnteredDate) & DatePart('m', InstallationCash.EnteredDate)='{0}'", processingMonth);
                            InstAmount = command.ExecuteScalar().ToString();

                            command.CommandText = string.Format("Select Sum(InstallationCash.amount) as Amount from InstallationCash inner join tblBook on InstallationCash.BookId = tblBook.BookId Where tblBook.BookStatus = 'Closed' and CashType='REINST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', InstallationCash.EnteredDate) & DatePart('m', InstallationCash.EnteredDate)='{0}'", processingMonth);
                            ReInstAmount = command.ExecuteScalar().ToString();

                        }

                        if (((SubAmount == "0") || (SubAmount == "")) && ((InstAmount == "0") || (InstAmount == "")) && ((ReInstAmount == "0") || (ReInstAmount == "")))
                        {
                            //transaction.Rollback();
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records to Update. Please try next time! ');", true);
                            errorDisplay.AddItem("No Cash Transactions to Update.", DisplayIcons.Information, false);
                            return;
                        }

                        if ((SubAmount != "0") && (SubAmount != ""))
                        {
                            Narration = "Subscription Cash Transaction Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;

                            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                            TransDate, DebitorID, CableCustID, SubAmount, Narration, VoucherType, RefNo);

                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                            int TransNo = (Int32)command.ExecuteScalar();

                            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);
                            command.CommandText = dbQry;
                            double Debit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + double.Parse(SubAmount), DebitorID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CableCustID);
                            command.CommandText = dbQry;
                            double Credit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + double.Parse(SubAmount), CableCustID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Update CashDetails Set ProcessedDate = Format('{0}', 'dd/mm/yyyy'),TransNo = {1} Where ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_paid) & DatePart('m', date_paid)='{2}'", DateTime.Now.ToShortDateString(), TransNo, processingMonth);
                            command.ExecuteNonQuery();

                            errorDisplay.AddItem("Cash Transaction Updated for " + processingMonth + " month successfully", DisplayIcons.GreenTick, false);

                        }

                        if ((InstAmount != "0") && (InstAmount != ""))
                        {
                            Narration = "Installation Cash Transaction Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;

                            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                            TransDate.ToShortDateString(), DebitorID, ConnAccID, InstAmount, Narration, VoucherType, RefNo);

                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                            int TransNo = (Int32)command.ExecuteScalar();

                            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);
                            command.CommandText = dbQry;
                            double Debit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + double.Parse(InstAmount), DebitorID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", ConnAccID);
                            command.CommandText = dbQry;
                            double Credit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + double.Parse(InstAmount), ConnAccID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Update InstallationCash Set ProcessedDate = '{0}',TransNo = {1} Where CashType='INST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', EnteredDate) & DatePart('m', EnteredDate)='{2}'", DateTime.Now.ToString(), TransNo, processingMonth);
                            command.ExecuteNonQuery();

                            errorDisplay.AddItem("Installation-Cash Transaction Updated for " + processingMonth + " month successfully", DisplayIcons.GreenTick, false);

                        }

                        if ((ReInstAmount != "0") && (ReInstAmount != ""))
                        {
                            Narration = "Re-Installation Cash Transaction Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;

                            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                            TransDate.ToShortDateString(), DebitorID, ReConnAccID, ReInstAmount, Narration, VoucherType, RefNo);

                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                            int TransNo = (Int32)command.ExecuteScalar();

                            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);
                            command.CommandText = dbQry;
                            double Debit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + double.Parse(ReInstAmount), DebitorID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", ReConnAccID);
                            command.CommandText = dbQry;
                            double Credit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + double.Parse(ReInstAmount), ReConnAccID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Update InstallationCash Set ProcessedDate = Format('{0}', 'dd/mm/yyyy'),TransNo = {1} Where CashType='REINST' and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', EnteredDate) & DatePart('m', EnteredDate)='{2}'", DateTime.Now.ToString(), TransNo, processingMonth);
                            command.ExecuteNonQuery();

                            errorDisplay.AddItem("Re-Installation-Cash Transaction Updated for " + processingMonth + " month successfully", DisplayIcons.GreenTick, false);

                        }

                    }
                }
                else
                {
                    errorDisplay.AddItem("No Cash Transactions to Update.", DisplayIcons.Information, false);
                }

                transaction.Commit();

            }
            catch (Exception ex)
            {
                try
                {
                    // Attempt to roll back the transaction.
                    transaction.Rollback();
                    errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                }
                catch (Exception ep)
                {
                    // Do nothing here; transaction is not active.
                    errorDisplay.AddItem("Exception while Rollback the Cash Transaction : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                }
            }
        }
    }
    private void UpdateAdjustmentTransactions()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");

        using (OleDbConnection connection = new OleDbConnection(connStr))
        {
            OleDbCommand command = new OleDbCommand();
            OleDbTransaction transaction = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string VoucherType = "Service";
            string RefNo = "0";
            DataSet dsDates = new DataSet();
            string processingMonth;
            DateTime TransDate;

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
                adapter.SelectCommand = command;
                command.CommandText = string.Format("Select Distinct DatePart('yyyy', date_entered) & DatePart('m', date_entered) AS monthyear from Adjustment Where ProcessedDate IS NULL and TransNo IS NULL");
                adapter.Fill(dsDates);

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Cable Customers A/C'");
                string CabCustID = command.ExecuteScalar().ToString();

                command.CommandText = string.Format("Select LedgerID from tblLedger Where LedgerName='Subscription A/C'");
                
                string SubAccID = command.ExecuteScalar().ToString();

                if (dsDates.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow currRow in dsDates.Tables[0].Rows)
                    {
                        processingMonth = currRow["monthyear"].ToString();
                        TransDate = DateTime.Parse("15/" + processingMonth.Remove(0, 4) + "/" + processingMonth.Remove(4));

                        command.CommandText = string.Format("Select Sum(amount) as Amount from Adjustment Where amount >= 0 and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_entered) & DatePart('m', date_entered)='{0}'", processingMonth);
                        string IncAmount = command.ExecuteScalar().ToString();

                        command.CommandText = string.Format("Select Sum(amount) as Amount from Adjustment Where amount < 0 and ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_entered) & DatePart('m', date_entered)='{0}'", processingMonth);
                        string decAmount = command.ExecuteScalar().ToString();

                        if (((IncAmount == "0") || (IncAmount == "")) && ((decAmount == "0") || (decAmount == "")))
                        {
                            //transaction.Rollback();
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records to Update. Please try next time! ');", true);
                            errorDisplay.AddItem("No Adjusments to Update.", DisplayIcons.Information, false);
                            return;
                        }


                        if ((IncAmount != "0") && (IncAmount != ""))
                        {
                            string Narration = "Incresed Adjusment Trans Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;

                            string dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                                TransDate.ToShortDateString(), SubAccID, CabCustID, IncAmount, Narration, VoucherType, RefNo);

                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                            int TransNo = (Int32)command.ExecuteScalar();

                            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", SubAccID);
                            command.CommandText = dbQry;
                            double Debit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + double.Parse(IncAmount), SubAccID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CabCustID);
                            command.CommandText = dbQry;
                            double Credit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + double.Parse(IncAmount), CabCustID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Update Adjustment Set ProcessedDate = Format('{0}', 'dd/mm/yyyy'),TransNo = {1} Where ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_entered) & DatePart('m', date_entered)='{2}' ", DateTime.Now.ToShortDateString(), TransNo, processingMonth);
                            command.ExecuteNonQuery();

                            errorDisplay.AddItem("Increase Adjusment Updated for " + processingMonth + " successfully", DisplayIcons.GreenTick, false);

                        }

                        if ((decAmount != "0") && (decAmount != ""))
                        {
                            double decrAmount = double.Parse(decAmount);

                            string Narration = "Decreased Adjusment Trans Updated Date : " + DateTime.Now.ToString() + ", Created By : " + Page.User.Identity.Name;
                            decrAmount = 0 - decrAmount;
                            string dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
                                TransDate.ToShortDateString(), CabCustID, SubAccID, decrAmount, Narration, VoucherType, RefNo);

                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(TransNo) FROM tblDayBook";
                            int TransNo = (Int32)command.ExecuteScalar();

                            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", CabCustID);
                            command.CommandText = dbQry;
                            double Debit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + decrAmount, CabCustID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", SubAccID);
                            command.CommandText = dbQry;
                            double Credit = (double)command.ExecuteScalar();

                            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + decrAmount, SubAccID);
                            command.CommandText = dbQry;
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Update Adjustment Set ProcessedDate = Format('{0}', 'dd/mm/yyyy'),TransNo = {1} Where ProcessedDate IS NULL and TransNo IS NULL AND DatePart('yyyy', date_entered) & DatePart('m', date_entered)='{2}'", DateTime.Now.ToShortDateString(), TransNo, processingMonth);
                            command.ExecuteNonQuery();

                            errorDisplay.AddItem("Decreased Adjusment Updated for " + processingMonth + " month successfully", DisplayIcons.GreenTick, false);

                        }
                    }
                }
                else
                {
                    errorDisplay.AddItem("No Adjusments to Update.", DisplayIcons.Information, false);
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
                    errorDisplay.AddItem("Exception while Rollback the Cash Transaction : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                }
            }
        }
    }
}
