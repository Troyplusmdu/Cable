﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;
using System.Text;
using System.Collections;
using System.IO;
 /// <summary>
/// Summary description for BusinessLogic
/// </summary>
public class BusinessLogic
{
    public BusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public BusinessLogic(string con)
    {
        //
        // TODO: Add constructor logic here
        this.ConnectionString = con;
        //
    }

    private string _connectionstring = string.Empty;

    protected string ConnectionString
    {
        get { return _connectionstring; }
        set { _connectionstring = value; }
    }

    public bool CheckForOffline(string filePath)
    {
        /*
        if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
        {
            return false;
        }
        else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
        {
            return true;
        }
        else if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
        {
            return true;
        }
        else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
        {
            return false;
        }
        else
        {
            return false;
        }*/
        return false;
        
        //if (ConfigurationManager.AppSettings["InstallationType"] == "SERVER")
        //    return false;
        //else if (ConfigurationManager.AppSettings["InstallationType"] == "CLIENT")
        //    return false;
        //else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] != "ONLINE-OFFLINE-CLIENT"))
        //    return false;
        //else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
        //    return false;
        //else if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
        //    return false;
        //else if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] != "ONLINE-OFFLINE-CLIENT"))
        //    return true;
        //else
        //    return false;
    }

    public DataSet GetUserInfo(string userId, string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString; ;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;


        try
        {
            dbQry = "select username,userpwd,locked,count,Email from tblUserInfo where StrConv(username,3) like '" + userId.ToLower() + "'";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count == 1)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet ListUsers(string txtSearch, string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString; ;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        if (txtSearch == null || txtSearch == "")
            txtSearch = "%%";
        else
            txtSearch = "%" + txtSearch + "%";

        try
        {
            dbQry = "select username,userpwd,locked,count,Email from tblUserInfo where StrConv(username,3) like '" + txtSearch.ToLower() + "'";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool IsValidDate(string connection, DateTime datetime)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);

        if (connection.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            manager.ConnectionString = connection;
        else
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        string dbQry = string.Empty;

        try
        {
            dbQry = "Select recon_date from last_recon";

            manager.Open();
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry);

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                if (DateTime.Parse(retVal.ToString()) < datetime)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public DataSet GetNextBillNo(string connection, int BookID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select BookID, BookName,NextEntry from tblBook Where BookStatus='Open' And NextEntry <= EndEntry And BookID=" + BookID.ToString();
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetSMSSettings(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select SMSRequired,CopyRequired,Mobile from tblSMSSettings";

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListBooks(string txtSearch, string connection, string dropDown, bool ActiveBooks)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString; ;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        if (txtSearch == null || txtSearch == "")
            txtSearch = "%%";
        else
            txtSearch = "%" + txtSearch + "%";

        try
        {
            
            if (dropDown == "BookRef")
                dbQry = "select BookID,BookRef,BookName,StartEntry,EndEntry,BookStatus,Amount,DateCreated from tblBook where StrConv(BookRef,3) like '" + txtSearch.ToLower() + "' And";
            else if (dropDown == "BookName")
                dbQry = "select BookID,BookRef,BookName,StartEntry,EndEntry,BookStatus,Amount,DateCreated from tblBook where StrConv(BookName,3) like '" + txtSearch.ToLower() + "' And";
            else
                dbQry = "select BookID,BookRef,BookName,StartEntry,EndEntry,BookStatus,Amount,DateCreated from tblBook Where";

            if (ActiveBooks)
                dbQry = dbQry + " BookStatus='Open'";
            else
                dbQry = dbQry + " BookStatus='Closed'";

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet checkUserCredentials(string connection, string username, string password)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select UserID, UserName, UserGroup from tblUserInfo where UserName = '" + username.ToLower()
                + "' and Userpwd = '" + password + "' ";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Close();
            manager.Dispose();
        }
    }

    public void InsertSettings(string SMSrequired)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            if (SMSrequired.Trim() != "")
            {
                dbQry = string.Format("UPDATE tblSettings SET KEYVALUE='{0}' WHERE KEY='SMSREQ' ", SMSrequired.ToUpper());
                manager.ExecuteNonQuery(CommandType.Text, dbQry);
            }

            manager.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public bool checkUserRoleExists(string role, string connection, string user)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            bool retval = false;

            manager.BeginTransaction();

            object exists = manager.ExecuteScalar(CommandType.Text, "SELECT Count(*) FROM tblUserRole Where Role='" + role + "' And UserName <> '" + user + "'");

            if (exists.ToString() != string.Empty)
            {
                if (int.Parse(exists.ToString()) > 0)
                {
                    retval = true;
                }
            }
            else
            {
                retval = false;
            }

            return retval;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public DataSet GetAppSettings(string connection)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "Select KEY,KEYVALUE From tblSettings";

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }



    public DataSet GetMasterRoles(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select * from tblRoleMaster";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            return ds;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetSettings()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select Key,KeyValue FROM tblSettings";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int InsertCompanyInfo(clsCompany clscmp)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString =this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;

        //DeleteClosingStock(cDate);
        dbQry = "Delete From tblCompanyInfo";
        manager.ExecuteNonQuery(CommandType.Text, dbQry);
        dbQry = string.Format("INSERT INTO tblCompanyInfo(CompanyName,Address,City,state,Pincode,phone,Fax,eMail,TINno,GstNo) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
        clscmp.Company, clscmp.Address, clscmp.City, clscmp.State, clscmp.Pincode, clscmp.Phone, clscmp.Fax, clscmp.Email, clscmp.TIN, clscmp.CST);
        int rows = manager.ExecuteNonQuery(CommandType.Text, dbQry);
        return rows;
        manager.Dispose();
    }

    public DataSet getCompanyDetails(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection; // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        try
        {
            manager.Open();
            dbQry = "SELECT CompanyName,Address,City,State,PinCode,Phone,Tinno,Gstno,FAX,email FROM tblCompanyInfo";
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetCategoryData(string connection)
    {
        if (connection == null)
        {
            throw new Exception("Connection Expired");
        }

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select * from tblCategories";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListHeading(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select HeadingID, Heading from tblAccHeading";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int GetNextSequence(string connection, string sql)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        string dbQry = string.Empty;

        try
        {
            dbQry = sql;
            manager.Open();
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry);

            if ((retVal != null) && (retVal != DBNull.Value))
                return int.Parse(retVal.ToString());
            else if (retVal == DBNull.Value)
                return 1;
            else
                return -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void InsertRecord(string connection, string sQl)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        int retValue = 0;
        string dbQry = string.Empty;

        try
        {
            dbQry = sQl;
            manager.Open();
            retValue = manager.ExecuteNonQuery(CommandType.Text, dbQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int GetRecord(string connection, string sQl)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        int retValue = 0;
        string dbQry = string.Empty;

        try
        {
            dbQry = sQl;
            manager.Open();
            retValue = (int)manager.ExecuteScalar(CommandType.Text, dbQry);
            return retValue;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateRecord(string connection, string sQl)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        int retValue = 0;
        string dbQry = string.Empty;

        try
        {
            dbQry = sQl;
            manager.Open();
            retValue = manager.ExecuteNonQuery(CommandType.Text, dbQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateCategory(string connection, string CategoryName, int CategoryID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        int retValue = 0;
        string dbQry = string.Format("Update tblCategories Set CategoryName = '{0}' Where CategoryID = {1}", CategoryName, CategoryID);

        try
        {
            manager.Open();
            retValue = manager.ExecuteNonQuery(CommandType.Text, dbQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string GetPassword(string userName, string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {
            dbQry.AppendFormat("select Userpwd from tblUserInfo Where UserName = '{0}'", userName);
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["Userpwd"].ToString();
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void ChangePassword(string userName, string password, string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        int retValue = 0;
        string dbQry = string.Format("Update tblUserInfo Set Userpwd = '{0}' Where UserName = '{1}'", password, userName);

        try
        {
            manager.Open();
            retValue = manager.ExecuteNonQuery(CommandType.Text, dbQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListGroupInfo(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select GroupID, GroupName, Heading, tblGroups.Order from tblGroups inner join tblAccHeading on tblGroups.HeadingID = tblAccHeading.HeadingID ";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public DataSet ListBanks()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        //manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where tblGroups.GroupName = '{0}'", "Bank Accounts");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListBanks(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where tblGroups.GroupName = '{0}'", "Bank Accounts");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListCreditorDebitor()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);

        manager.ConnectionString = this.ConnectionString;// System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("select GroupID, GroupName from  tblGroups Where tblGroups.GroupName IN ('{0}','{1}')", "Sundry Debtors", "Sundry Creditors");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListCreditorDebitor(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        if (connection.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            manager.ConnectionString = connection;
        else
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Order By LedgerName Asc");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListExpenses(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        if (connection.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            manager.ConnectionString = connection;
        else
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where tblGroups.GroupName IN ('{0}')", "Expenses");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListLedger()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select LedgerID,LedgerName, AliasName, tblGroups.GroupID,GroupName, IIF(OpenBalanceDR <> 0,'DR','CR') AS DRORCR ,IIF(OpenBalanceDR <> 0,OpenBalanceDR,OpenBalanceCR) AS OpenBalance,ContactName,Add1, Add2, Add3,Debit,Credit, Phone from tblLedger inner join tblGroups on tblLedger.GroupID = tblGroups.GroupID Where LedgerName Order By LedgerName";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void UpdateGroupInfo(string connection, int HeadingID, string GroupName, int GroupID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("Update tblGroups Set GroupName = '{0}', HeadingID = {1} Where GroupID = {2}", GroupName, HeadingID, GroupID);
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void InsertGroupInfo(string connection, int HeadingID, string GroupName, int GroupID, int Order)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("Insert Into tblGroups Values ({0},'{1}',{2},{3})", GroupID, GroupName, HeadingID, Order);
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public bool CheckOpenBooks(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString; ;

        string dbQry = string.Empty;

        try
        {
            dbQry = "select count(*) as BookCount from tblBook where BookStatus = 'Open'";
            manager.Open();
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry);

            if (int.Parse(retVal.ToString()) > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SetStatusInDB(string connection, string status)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;        
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("UPDATE Settings SET Value = '{0}' WHERE Key = 'APPTYPE' ", status);
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertBook(string connection, string BookRef, string BookName, int StartEntry, int EndEntry, decimal Amount, DateTime dateCreated, string FlagCollected, string BookStatus)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("Insert Into tblBook(BookRef,BookName,StartEntry,EndEntry,NextEntry,BookStatus,Amount,DateCreated,FlagCollected) Values ('{0}','{1}',{2},{3},{4},'{5}',{6},Format('{7}', 'dd/mm/yyyy'),'{8}')", BookRef, BookName, StartEntry, EndEntry, StartEntry, BookStatus, Amount, dateCreated, FlagCollected);
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateBook(string connection, string BookRef, string BookName, int StartEntry, int EndEntry, decimal Amount, int BookID, DateTime dateCreated, string FlagCollected, string BookStatus)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("Update tblBook Set BookRef='{0}',BookName='{1}',StartEntry={2},EndEntry={3},Amount={4},DateCreated= Format('{5}', 'dd/mm/yyyy'),FlagCollected = '{7}',BookStatus = '{8}' Where BookID={6}", BookRef, BookName, StartEntry, EndEntry, Amount, dateCreated, BookID, FlagCollected,BookStatus);
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void DeletePayment(string connection, int TransNo, bool requireValidation)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {

            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            ds = manager.ExecuteDataSet(CommandType.Text, "Select Amount,DebtorID,CreditorID,TransDate from tblDayBook Where TransNo=" + TransNo);

            int DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"].ToString());
            int CreditorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CreditorID"].ToString());
            double Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"].ToString());
            DateTime TransDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["TransDate"].ToString());

            if (requireValidation)
            {
                if (!IsValidDate(manager.ConnectionString, TransDate))
                {
                    throw new Exception("Invalid Date");
                }
            }

            dbQry = string.Format("Update tblLedger SET Debit = Debit - {0} Where LedgerID={1}", Amount, DebitorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = Credit - {0} Where LedgerID={1}", Amount, CreditorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPayment Where JournalID = {0}", TransNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public void DeleteReceipt(string connection, int TransNo, bool requireValidation)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            ds = manager.ExecuteDataSet(CommandType.Text, "Select Amount,DebtorID,CreditorID,TransDate from tblDayBook Where TransNo=" + TransNo);

            int DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"].ToString());
            int CreditorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CreditorID"].ToString());
            double Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"].ToString());
            DateTime TransDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["TransDate"].ToString());

            if (requireValidation)
            {
                if (!IsValidDate(manager.ConnectionString, TransDate))
                {
                    throw new Exception("Invalid Date");
                }
            }

            dbQry = string.Format("Update tblLedger SET Debit = Debit - {0} Where LedgerID={1}", Amount, DebitorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = Credit - {0} Where LedgerID={1}", Amount, CreditorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblReceipt Where JournalID = {0}", TransNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }


    public DataSet ListProducts(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        txtSearch = "%" + txtSearch + "%";

        if (dropDown == "ItemCode")
        {
            dbQry = "select ItemCode,ProductName, Model, tblCategories.CategoryID, tblCategories.CategoryName, ProductDesc,Stock,ROL, Rate, Unit, VAT, Discount,BuyUnit, BuyRate, BuyVAT, BuyDiscount from tblProductMaster inner join tblCategories on tblProductMaster.CategoryID = tblCategories.CategoryID Where ItemCode like '" + txtSearch + "' Order By ItemCode";
        }
        else if (dropDown == "ProductName")
        {
            dbQry = "select ItemCode,ProductName, Model, tblCategories.CategoryID, tblCategories.CategoryName, ProductDesc,Stock,ROL, Rate, Unit, VAT, Discount,BuyUnit, BuyRate, BuyVAT, BuyDiscount from tblProductMaster inner join tblCategories on tblProductMaster.CategoryID = tblCategories.CategoryID Where ProductName like '" + txtSearch + "' Order By ItemCode";
        }
        else if (dropDown == "Model")
        {
            dbQry = "select ItemCode,ProductName, Model, tblCategories.CategoryID, tblCategories.CategoryName, ProductDesc,Stock,ROL, Rate, Unit, VAT, Discount,BuyUnit, BuyRate, BuyVAT, BuyDiscount from tblProductMaster inner join tblCategories on tblProductMaster.CategoryID = tblCategories.CategoryID Where Model like '" + txtSearch + "' Order By ItemCode";
        }
        else
        {
            dbQry = string.Format("select ItemCode,ProductName, Model, tblCategories.CategoryID, tblCategories.CategoryName, ProductDesc,Stock,ROL, Rate, Unit, VAT, Discount,BuyUnit, BuyRate, BuyVAT, BuyDiscount from tblProductMaster inner join tblCategories on tblProductMaster.CategoryID = tblCategories.CategoryID Order By ItemCode");
        }

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetPaymentForId(string connection, int TransNo)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {
            dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName,tblDayBook.CreditorID,tblDayBook.DebtorID, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo, Payment.Paymode,tblDayBook.ExpenseType FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID) LEFT JOIN ");
            dbQry.Append(" tblPayMent Payment ON tblDayBook.TransNo = Payment.JournalID)");
            dbQry.AppendFormat("Where tblDayBook.TransNo = {0}", TransNo);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetReceiptForId(string connection, int TransNo)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {
            dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName,tblDayBook.CreditorID,tblDayBook.DebtorID, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo, Receipt.Paymode FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID) INNER JOIN ");
            dbQry.Append(" tblReceipt Receipt ON tblDayBook.TransNo = Receipt.JournalID)");
            dbQry.AppendFormat("Where tblDayBook.TransNo = {0}", TransNo);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListCashForBookId(string connection, string billNo, int bookID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        if (billNo == null)
            billNo = string.Empty;

        try
        {
            if (billNo == string.Empty)
            {
                dbQry.Append("SELECT  slno,code,area,CashDetails.amount,discount,reason,date_paid,billno,bookname,CashDetails.BookID ");
                dbQry.Append("From CashDetails INNER JOIN tblBook ON tblBook.BookID = CashDetails.BookID ");
                dbQry.AppendFormat("Where tblBook.BookID = {0} ", bookID);
            }
            else
            {
                dbQry.Append("SELECT  slno,code,area,CashDetails.amount,discount,reason,date_paid,billno,bookname,CashDetails.BookID ");
                dbQry.Append("From CashDetails INNER JOIN tblBook ON tblBook.BookID = CashDetails.BookID ");
                dbQry.AppendFormat("Where tblBook.BookID = {0} AND billno='{1}' ", bookID, billNo);
            }
            dbQry.Append("Order By slno,billno");

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListInstallationCash(string connection, string billNo, int bookID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        if (billNo == null)
            billNo = string.Empty;

        try
        {
            if (billNo == string.Empty)
            {
                dbQry.Append("SELECT ID,code,area,InstallationCash.amount,InstallationCash.EnteredDate,InstallationCash.billno,InstallationCash.BookID,InstallationCash.CashType ");
                dbQry.Append("From InstallationCash INNER JOIN tblBook ON tblBook.BookID = InstallationCash.BookID ");
                dbQry.AppendFormat("Where tblBook.BookID = {0} ", bookID);
            }
            else
            {
                dbQry.Append("SELECT ID,code,area,InstallationCash.amount,InstallationCash.EnteredDate,InstallationCash.billno,InstallationCash.BookID,InstallationCash.CashType ");
                dbQry.Append("From InstallationCash INNER JOIN tblBook ON tblBook.BookID = InstallationCash.BookID ");
                dbQry.AppendFormat("Where tblBook.BookID = {0} AND billno='{1}' ", bookID, billNo);
            }
            dbQry.Append("Order By InstallationCash.billno");

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public DataSet ListPayments(string connection, string txtSearch, string dropDown)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();


        if (dropDown == "TransDate")
            txtSearch = txtSearch;
        else if (dropDown == "RefNo")
            txtSearch = txtSearch;
        else
            txtSearch = "%" + txtSearch + "%";

        dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
        dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo, Payment.Paymode,tblDayBook.ExpenseType FROM  (((tblDayBook INNER JOIN ");
        dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID) INNER JOIN ");
        dbQry.Append("tblPayMent Payment ON tblDayBook.TransNo = Payment.JournalID) ");

        if (dropDown == "RefNo" && txtSearch != null)
        {
            dbQry.AppendFormat("Where tblDayBook.VoucherType = 'Payment' and tblDayBook.RefNo = {0} ", txtSearch);
        }
        else if (dropDown == "TransDate" && txtSearch != null)
        {
            dbQry.AppendFormat("WHERE tblDayBook.VoucherType = 'Payment' and Format([tblDayBook.TransDate], 'dd/mm/yyyy') = '{0}' ", Convert.ToDateTime(txtSearch).ToShortDateString());
        }
        else if (dropDown == "LedgerName" && txtSearch != null)
        {
            dbQry.AppendFormat("Where tblDayBook.VoucherType = 'Payment' and Debitor.LedgerName like '{0}' ", txtSearch);
        }
        else if (dropDown == "Narration" && txtSearch != null)
        {
            dbQry.AppendFormat("Where tblDayBook.VoucherType = 'Payment' and tblDayBook.Narration like '{0}' ", txtSearch);
        }
        else
        {
            dbQry.Append("Where tblDayBook.VoucherType = 'Payment' ");
        }

        dbQry.Append(" Order By tblDayBook.RefNo ");

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListReceipts(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();


        if (dropDown == "TransDate")
            txtSearch = txtSearch;
        else if (dropDown == "RefNo")
            txtSearch = txtSearch;
        else
            txtSearch = "%" + txtSearch + "%";

        try
        {
            manager.Open();
            object reconDate = manager.ExecuteScalar(CommandType.Text, "Select recon_date from last_recon");

            dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo, Receipt.Paymode FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID) LEFT JOIN ");
            dbQry.Append("tblReceipt Receipt ON tblDayBook.TransNo = Receipt.JournalID) ");

            if (dropDown == "RefNo" && txtSearch != null)
            {
                dbQry.AppendFormat("Where tblDayBook.VoucherType= 'Receipt' and tblDayBook.RefNo = {0} ", txtSearch);
            }
            else if (dropDown == "TransDate" && txtSearch != null)
            {
                dbQry.AppendFormat("WHERE tblDayBook.VoucherType= 'Receipt' and Format([tblDayBook.TransDate], 'dd/mm/yyyy') = '{0}' ", Convert.ToDateTime(txtSearch).ToShortDateString());
            }
            else if (dropDown == "LedgerName" && txtSearch != null)
            {
                dbQry.AppendFormat("Where tblDayBook.VoucherType= 'Receipt' and Creditor.LedgerName like '{0}' ", txtSearch);
            }
            else if (dropDown == "Narration" && txtSearch != null)
            {
                dbQry.AppendFormat("Where tblDayBook.VoucherType= 'Receipt' and tblDayBook.Narration like '{0}' ", txtSearch);
            }
            else
            {
                dbQry.AppendFormat("Where tblDayBook.VoucherType= 'Receipt' ");
            }

            dbQry.Append(" AND tblDayBook.TransDate > #" + DateTime.Parse(reconDate.ToString()).ToString("MM/dd/yyyy") + "#");
            dbQry.Append("Order By tblDayBook.RefNo Asc");


            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet ListLedgerInfo(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        txtSearch = "%" + txtSearch + "%";

        if (dropDown == "LedgerName")
        {
            dbQry = "select LedgerID,LedgerName, AliasName, tblGroups.GroupID,GroupName, IIF(OpenBalanceDR <> 0,'DR','CR') AS DRORCR ,IIF(OpenBalanceDR <> 0,OpenBalanceDR,OpenBalanceCR) AS OpenBalance,ContactName,Add1, Add2, Add3,Debit,Credit, Phone from tblLedger inner join tblGroups on tblLedger.GroupID = tblGroups.GroupID Where LedgerName like '" + txtSearch + "'" + " Order By LedgerName";
        }
        else if (dropDown == "AliasName")
        {
            dbQry = "select LedgerID,LedgerName, AliasName, tblGroups.GroupID,GroupName,IIF(OpenBalanceDR <> 0,'DR','CR') AS DRORCR ,IIF(OpenBalanceDR <> 0,OpenBalanceDR,OpenBalanceCR) AS OpenBalance,ContactName,Add1, Add2, Add3,Debit,Credit, Phone from tblLedger inner join tblGroups on tblLedger.GroupID = tblGroups.GroupID Where AliasName like '" + txtSearch + "'" + " Order By LedgerName";
        }
        else
        {
            dbQry = string.Format("select LedgerID,LedgerName, AliasName, tblGroups.GroupID,GroupName, IIF(OpenBalanceDR <> 0,'DR','CR') AS DRORCR ,IIF(OpenBalanceDR <> 0,OpenBalanceDR,OpenBalanceCR) AS OpenBalance,ContactName,Add1, Add2, Add3,Debit,Credit,Phone from tblLedger inner join tblGroups on tblLedger.GroupID = tblGroups.GroupID Where (LedgerName like '{0}' or AliasName like '{0}') Order By LedgerName", txtSearch);
        }

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void InsertLedgerInfo(string connection, string LedgerName, string AliasName, int GroupID, int OpenBalanceDR, int OpenBalanceCR, int OpenBalance, string DRORCR, string ContactName, string Add1, string Add2, string Add3, string Phone)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();

            int LedgerID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(LedgerID) FROM tblLedger");

            dbQry = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13})",
                LedgerID + 1, LedgerName, AliasName, GroupID, OpenBalanceDR, OpenBalanceCR, 0, 0, ContactName, Add1, Add2, Add3, Phone, 0);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public void InsertPayment(string connection, string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, string ChequeNo, string PaymentMode, string ExpenseType)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {

            if (!IsValidDate(manager.ConnectionString, TransDate))
            {
                throw new Exception("Invalid Date");
            }

            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,ChequeNo,Commission,RefNo,ExpenseType) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7},{8},'{9}')",
                TransDate.ToShortDateString(), DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, 0, RefNo, ExpenseType);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            int TransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            dbQry = string.Format("Insert Into tblPayment(JournalID,Paymode) Values({0},'{1}')", TransNo, PaymentMode);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);

            double Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebitorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CreditorID);

            double Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, CreditorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

            manager.Dispose();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void InsertReceipt(string connection, string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, string ChequeNo, string Paymode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {

            if (!IsValidDate(manager.ConnectionString, TransDate))
            {
                throw new Exception("Invalid Date");
            }

            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,ChequeNo,Commission,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7},{8})",
                TransDate.ToShortDateString(), DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, 0, RefNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            int TransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            dbQry = string.Format("Insert Into tblReceipt(CreditorID,JournalID,Paymode) Values({0},{1},'{2}')", CreditorID, TransNo, Paymode);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);

            double Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebitorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CreditorID);

            double Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, CreditorID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public void UpdatePayment(string connection, int TransNo, string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, string ChequeNo, string Paymode, string ExpenseType)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {

            if (IsValidDate(manager.ConnectionString, TransDate))
            {
                DeletePayment(connection, TransNo, false);

                InsertPayment(connection, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, ExpenseType);
            }
            else
            {
                throw new Exception("Invalid Date");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void DeleteUserInfo(string connection, string username)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            dbQry = string.Format("Delete from tblUserInfo Where UserName = '{0}' ", username);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblUserRole Where UserName = '{0}' ", username);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public void UpdateUserInfo(string connection, string UserName, string Email, bool Locked, ArrayList roles)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            dbQry = string.Format("Update tblUserInfo SET Email='{0}',Locked={1} Where UserName = '{2}' ", Email, Locked, UserName);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblUserRole Where UserName = '{0}' and Role not in ('SMS','ACCSYS') ", UserName);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            for (int i = 0; i < roles.Count; i++)
            {
                dbQry = string.Format("Insert Into tblUserRole Values('{0}','{1}')", UserName, roles[i]);
                manager.ExecuteNonQuery(CommandType.Text, dbQry);
            }

            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public bool InsertUserInfo(string connection, string UserName, string Email, bool Locked, ArrayList roles)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            dbQry = string.Format("Select Count(*) From tblUserInfo Where UserName='{0}'", UserName);

            int exists = (int)manager.ExecuteScalar(CommandType.Text, dbQry);

            if (exists > 0)
                return false;

            dbQry = string.Format("Insert Into tblUserInfo(UserID,UserName,Userpwd,UserGroup,Email,Locked) VALUES ('{0}','{1}','abc123','Users','{2}',{3} )", UserName, UserName, Email, Locked);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            for (int i = 0; i < roles.Count; i++)
            {
                dbQry = string.Format("Insert Into tblUserRole Values('{0}','{1}')", UserName, roles[i]);
                manager.ExecuteNonQuery(CommandType.Text, dbQry);
            }

            manager.CommitTransaction();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void InsertUserInfo1232(string connection, string UserName, string Email, bool Locked, ArrayList roles)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();

            dbQry = string.Format("Insert Into tblUserInfo Values ('{0}','{0}','abc123','Users',{1},0,'{2}') ", UserName, Locked, Email);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblUserRole Where UserName = '{0}' and Role not in ('SMS','ACCSYS') ", UserName);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            for (int i = 0; i < roles.Count; i++)
            {
                dbQry = string.Format("Insert Into tblUserRole Values('{0}','{1}')", UserName, roles[i]);
                manager.ExecuteNonQuery(CommandType.Text, dbQry);
            }

            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public void UpdateReceipt(string connection, int TransNo, string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, string ChequeNo, string Paymode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            if (IsValidDate(manager.ConnectionString, TransDate))
            {
                DeleteReceipt(connection, TransNo, false);

                InsertReceipt(connection, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode);
            }
            else
            {
                throw new Exception("Invalid Date");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateLedgerInfo(string connection, int LedgerID, string LedgerName, string AliasName, int OpenBalance, string DRORCR, int GroupID, int OpenBalanceDR, int OpenBalanceCR, string ContactName, string Add1, string Add2, string Add3, string Phone)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = string.Format("Update tblLedger SET LedgerName='{0}', AliasName='{1}', GroupID={2},OpenBalanceDR={3},ContactName='{4}',Add1='{5}', Add2='{6}', Add3='{7}', Phone='{8}', OpenBalanceCR= {9} WHERE LedgerID={10}", LedgerName, AliasName, GroupID, OpenBalanceDR, ContactName, Add1, Add2, Add3, Phone, OpenBalanceCR, LedgerID);

        try
        {
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListCategory(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select CategoryID, CategoryName from tblCategories Order By CategoryName ";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetGroupInfoForId(string connection, int groupID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select GroupID, GroupName, Heading, tblGroups.HeadingID from tblGroups inner join tblAccHeading on tblGroups.HeadingID = tblAccHeading.HeadingID where GroupID = " + groupID.ToString();
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public DataSet GetLedgerInfoForId(string connection, int ledgerID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select LedgerID,LedgerName, AliasName, tblGroups.GroupID,GroupName,IIF(OpenBalanceDR <> 0,'DR','CR') AS DRORCR ,IIF(OpenBalanceDR <> 0,OpenBalanceDR,OpenBalanceCR) AS OpenBalance,ContactName,Add1, Add2, Add3, Phone from tblLedger inner join tblGroups on tblLedger.GroupID = tblGroups.GroupID where LedgerID = " + ledgerID.ToString();
            manager.Open();

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetProductForId(string connection, string ItemCode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select ItemCode,ProductName, Model, tblCategories.CategoryID, tblCategories.CategoryName, ProductDesc,Stock,ROL, Rate, Unit, VAT, Discount, BuyRate,BuyVAT, BuyDiscount,BuyUnit from tblProductMaster inner join tblCategories on tblProductMaster.CategoryID = tblCategories.CategoryID where ItemCode = '" + ItemCode + "'";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet IsDatePaidValid(string connection, int BookId )
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select DatePart('yyyy', DateCreated) & DatePart('m', DateCreated) as MonthPaid from tblBook where BookID = " + BookId;
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetBookForId(string connection, int BookID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select BookID,BookName, BookRef, StartEntry, EndEntry, BookStatus,Amount, Format(DateCreated, 'dd/mm/yyyy') as DateCreated,FlagCollected,BookStatus from tblBook where BookID = " + BookID + "";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateProduct(string connection, string ItemCode, string ProductName, string Model, int CategoryID, string ProductDesc, int ROL, int Stock, double Rate, int Unit, int VAT, int Discount, double BuyRate, int BuyVAT, int BuyDiscount, int BuyUnit)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        try
        {
            dbQry = string.Format("UPDATE tblProductMaster SET ProductName='{0}', Model='{1}', CategoryID={2}, ProductDesc='{3}',Stock ={4},ROL={5}, Rate={6}, Unit={7}, VAT={8}, Discount={9},BuyRate={10},BuyVAT={11},BuyDiscount={12},BuyUnit={13} where ItemCode = '{14}'",
                ProductName, Model, CategoryID, ProductDesc, Stock, ROL, Rate, Unit, VAT, Discount, BuyRate, BuyVAT, BuyDiscount, BuyUnit, ItemCode);

            manager.Open();
            manager.ExecuteDataSet(CommandType.Text, dbQry);

            //sAuditStr = "Product Update: " + TransNo + " got edited and deleted Record Details : DebtorID=" + oldDebitID + ",CreditorID=" + oldCreditID + ",Amount=" + oldAmt + " New Trans No :" + NewTransNo;
            //dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string[] GetRoles(string connection, string UserName)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = connection;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select UserName,Role from tblUserRole where UserName= '" + UserName + "'";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            string[] retRoles = new string[ds.Tables[0].Rows.Count];

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    retRoles[i] = ds.Tables[0].Rows[i]["Role"].ToString();
                }
                return retRoles;
            }
            else
                return retRoles;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertProduct(string connection, string ItemCode, string ProductName, string Model, int CategoryID, string ProductDesc, int ROL, int Stock, double Rate, int Unit, int BuyUnit, int VAT, int Discount, double BuyRate, int BuyVAT, int BuyDiscount)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = string.Format("INSERT INTO tblProductMaster VALUES('{0}','{1}', '{2}',{3},'{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})",
                ItemCode, ProductName, Model, CategoryID, ProductDesc, Stock, ROL, Rate, Unit, VAT, Discount, BuyUnit, BuyRate, BuyVAT, BuyDiscount);
            manager.Open();
            manager.ExecuteDataSet(CommandType.Text, dbQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #region Journal Section
    public DataSet ListCreditorDebitorJ(string connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        if (connection.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            manager.ConnectionString = connection;
        else
            manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            //dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where tblGroups.GroupName IN ('{0}','{1}','{2}','{3}','{4}') OR tblGroups.HeadingID IN (11) Order By LedgerName Asc ", "Sundry Debtors", "Sundry Creditors", "Bank Accounts", "Cash in Hand", "InCome");
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID ORDER By LedgerName");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListJournal(string sRefno, string sNaration, string sDate, string sPath)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = sPath;
        string dbQry2 = string.Empty;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        String searchStr = string.Empty;

        if (sRefno != string.Empty && sRefno != null)
            searchStr = "tblDayBook.Refno=" + sRefno.Trim();
        if (sNaration != string.Empty && sNaration != null)
        {
            if (searchStr == string.Empty)
                searchStr = searchStr + "tblDayBook.Narration LIKE '%" + sNaration.Trim() + "%'";
            else
                searchStr = searchStr + " AND tblDayBook.Narration LIKE '%" + sNaration.Trim() + "%'";
        }
        if (sDate != string.Empty && sDate != null)
        {
            if (searchStr == string.Empty)
                searchStr = searchStr + "tblDayBook.TransDate=#" + Convert.ToDateTime(sDate).ToString("MM/dd/yyyy").Trim() + "#";
            else
                searchStr = searchStr + " AND tblDayBook.TransDate=#" + Convert.ToDateTime(sDate).ToString("MM/dd/yyyy").Trim() + "#";
        }
        dbQry2 = "Select recon_date from last_recon";
        manager.Open();
        object retVal = manager.ExecuteScalar(CommandType.Text, dbQry2);

        if (searchStr != string.Empty)
        {
            dbQry.Append("SELECT  tblDayBook.TransNo,  Format(tblDayBook.TransDate, 'dd/mm/yyyy') As TransDate, Creditor.LedgerName As Cred, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID))");

            dbQry.AppendFormat("Where {0} AND VoucherType='Journal' AND tblDayBook.TransDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "#  Order By tblDayBook.TransDate Desc", searchStr);

        }
        else
        {
            dbQry.Append("SELECT  tblDayBook.TransNo, Format(tblDayBook.TransDate, 'dd/mm/yyyy') As TransDate, Creditor.LedgerName As Cred, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID))");

            dbQry.AppendFormat(" Where VoucherType='Journal' AND tblDayBook.TransDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "#   Order By tblDayBook.TransDate Desc");
        }


        try
        {

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet GetJournalForId(int TransNo, String ConStr)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConStr].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        string dbQry2 = string.Empty;
        try
        {
            manager.Open();
            dbQry2 = "Select recon_date from last_recon";
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry2);

            dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName,tblDayBook.CreditorID,tblDayBook.DebtorID, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
            dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo FROM  (((tblDayBook INNER JOIN ");
            dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID)) ");
            dbQry.AppendFormat("Where tblDayBook.TransNo = {0} AND tblDayBook.TransDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "#   Order By tblDayBook.TransDate Desc", TransNo);


            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void UpdateJournal(int TransNo, string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, String sPath)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[sPath].ConnectionString; ;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        DataSet dsOld = new DataSet();
        int oldDebtorID = 0;
        int oldCreditorID = 0;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID
            dsOld = GetJournalForId(TransNo, sPath);
            if (dsOld.Tables[0].Rows[0]["DebtorID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["DebtorID"].ToString() != string.Empty)
                {
                    oldDebtorID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"].ToString());
                }
            }
            if (dsOld.Tables[0].Rows[0]["CreditorID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["CreditorID"].ToString() != string.Empty)
                {
                    oldCreditorID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"].ToString());
                }
            }
            //End Retriving the old Debtor and CreditorID

            //Start Updating the Debit and credit 
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebtorID);
            double Debit = 0;

            object retDebit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebit != null) && (retDebit != DBNull.Value))
            {
                Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }


            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit - Amount, oldDebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditorID);
            double Credit = 0;

            object retCredit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCredit != null) && (retCredit != DBNull.Value))
            {
                Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }



            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit - Amount, oldCreditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit
            //Start Delete the old record
            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Deleting the old record
            //Insert New Record
            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
               TransDate.ToShortDateString(), DebitorID, CreditorID, Amount, Narration, VoucherType, RefNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            int NewTransNo = 0;

            object retVal = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                NewTransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            }
            else
            {
                NewTransNo = NewTransNo + 1;
            }



            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);

            double DebitNew = 0;

            object retDebitNew = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebitNew != null) && (retDebitNew != DBNull.Value))
            {
                DebitNew = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", DebitNew + Amount, DebitorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CreditorID);


            double CreditNew = 0;

            object retCreditNew = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCreditNew != null) && (retCreditNew != DBNull.Value))
            {
                CreditNew = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", CreditNew + Amount, CreditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Store the details in the Audit Table.
            sAuditStr = "Transaction: " + TransNo + " got edited and deleted Record Details : DebtorID=" + DebitorID + ",CreditorID=" + CreditorID + ",Amount=" + Amount + " New Trans No :" + NewTransNo;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    public void InsertJournal(string RefNo, DateTime TransDate, int DebitorID, int CreditorID, double Amount, string Narration, string VoucherType, string sPath)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[sPath].ConnectionString; ;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}',{6})",
            TransDate.ToShortDateString(), DebitorID, CreditorID, Amount, Narration, VoucherType, RefNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            int TransNo = 0;
            object retVal = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                TransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            }
            else
            {
                TransNo = TransNo + 1;
            }

            //dbQry = string.Format("Insert Into tblPayment(JournalID,Paymode) Values({0},'{1}')", TransNo, PaymentMode);
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebitorID);
            double Debit = 0;

            object retDebit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebit != null) && (retDebit != DBNull.Value))
            {
                Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }



            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebitorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", CreditorID);
            double Credit = 0;

            object retCredit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCredit != null) && (retCredit != DBNull.Value))
            {
                Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }


            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, CreditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    public void DeleteJournal(int TransNo, string sPath)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[sPath].ConnectionString; ;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        DataSet dsOld = new DataSet();
        int oldDebtorID = 0;
        int oldCreditorID = 0;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;
            manager.BeginTransaction();
            dsOld = GetJournalForId(TransNo, sPath);
            if (dsOld.Tables[0].Rows[0]["DebtorID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["DebtorID"].ToString() != string.Empty)
                {
                    oldDebtorID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"].ToString());
                }
            }
            if (dsOld.Tables[0].Rows[0]["CreditorID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["CreditorID"].ToString() != string.Empty)
                {
                    oldCreditorID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"].ToString());
                }
            }

            //Start Updating the Debit and credit 
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebtorID);
            double Debit = 0;

            object retDebit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebit != null) && (retDebit != DBNull.Value))
            {
                Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }


            double Amount = (double)manager.ExecuteScalar(CommandType.Text, "Select Amount from tblDayBook Where TransNo=" + TransNo);
            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit - Amount, oldDebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditorID);
            double Credit = 0;

            object retCredit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCredit != null) && (retCredit != DBNull.Value))
            {
                Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }


            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit - Amount, oldCreditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit

            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //dbQry = string.Format("Delete From tblPayment Where JournalID = {0}", TransNo);

            //manager.ExecuteNonQuery(CommandType.Text, dbQry);
            sAuditStr = "Transaction: " + TransNo + " got deleted old Record Details : DebtorID=" + oldDebtorID + ",CreditorID=" + oldCreditorID + ",Amount=" + Amount;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    #endregion


    #region Purchase Section
    public DataSet GetPurchase()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        string dbQry2 = string.Empty;
        try
        {
            manager.Open();
            dbQry2 = "Select recon_date from last_recon";

            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry2);

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,Format(tblPurchase.Billdate, 'dd/mm/yyyy') As BillDate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID)) Where tblPurchase.BillDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "# ORDER BY tblPurchase.BillDate Desc");

            }
            else
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,Format(tblPurchase.Billdate, 'dd/mm/yyyy') As BillDate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID)) ORDER BY tblPurchase.BillDate Desc");
            }
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet GetPurchaseForId(String Billno)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        string dbQry2 = string.Empty;
        try
        {

            manager.Open();
            dbQry2 = "Select recon_date from last_recon";
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry2);

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,Format(tblPurchase.Billdate, 'dd/mm/yyyy') As BillDate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID))");
                dbQry.Append(" Where tblPurchase.Billno='" + Billno.Trim() + "' AND tblPurchase.BillDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "# ORDER BY tblPurchase.BillDate Desc");
            }
            else
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,Format(tblPurchase.Billdate, 'dd/mm/yyyy') As BillDate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID))");
                dbQry.Append(" ORDER BY tblPurchase.BillDate Desc");

            }
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet GetPurchaseForId(int purchaseID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        string dbQry2 = string.Empty;
        try
        {

            manager.Open();
            dbQry2 = "Select recon_date from last_recon";
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry2);

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,tblPurchase.Billdate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID))");
                dbQry.Append(" Where tblPurchase.purchaseID=" + purchaseID + " AND tblPurchase.BillDate > #" + DateTime.Parse(retVal.ToString()).ToString("MM/dd/yyyy") + "# ORDER BY tblPurchase.BillDate Desc");
            }
            else
            {
                dbQry.Append("SELECT tblPurchase.PurchaseId,tblPurchase.Billno,tblPurchase.Billdate,tblPurchase.Paymode,tblPurchase.SupplierID,tblDaybook.Chequeno,Creditor.LedgerName As Creditor,tblDayBook.Amount,tblDayBook.narration,tblPurchase.JournalID,tblPurchase.SalesReturn,tblPurchase.SalesReturnReason");
                dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblPurchase ON tblPurchase.JournalID = tblDayBook.Transno) INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID))");
                dbQry.Append("  ORDER BY tblPurchase.BillDate Desc");
            }
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet GetSerialNo(int purchaseID, string AssetCode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        try
        {
            dbQry.Append("SELECT SerialNo FROM AssetDetails WHERE AssetCode='" + AssetCode + "' AND purchaseID=" + purchaseID);
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());
            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetPurchaseItemsForId(int purchaseId)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {
            //    dbQry.Append("SELECT AssetDetails.AssetNo,AssetDetails.AssetCode,tblPurchaseitems.PurchaseID,AssetMaster.AssetDesc,AssetCatagory.CategoryDescription,AssetDetails.AssetLocation,AssetDetails.AssetArea,AssetDetails.AssetStatus,tblPurchaseItems.Qty");
            //    dbQry.Append(" FROM AssetDetails,AssetMaster,AssetCatagory,tblPurchaseitems WHERE AssetDetails.AssetCode = AssetMaster.AssetCode AND AssetMaster.CategoryID = AssetCatagory.CategoryID AND tblPurchaseItems.purchaseID =AssetDetails.purchaseID ");
            //    dbQry.Append(" AND tblPurchaseItems.purchaseID = " + purchaseId);
            dbQry.Append("SELECT tblPurchaseItems.PurchaseID,tblPurchaseItems.ItemCode,tblPurchaseItems.Qty,AssetMaster.AssetDesc,AssetCatagory.CategoryDescription,AssetDetails.AssetLocation,AssetDetails.AssetArea,");
            dbQry.Append(" AssetDetails.AssetStatus,AssetDetails.SerialNo FROM AssetMaster,tblPurchaseItems,AssetCatagory,AssetDetails");
            dbQry.Append(" WHERE tblPurchaseItems.ItemCode = AssetDetails.AssetCode AND AssetMaster.CategoryID = AssetCatagory.CategoryID AND tblPurchaseItems.purchaseID = AssetDetails.purchaseID AND AssetDetails.AssetCode = AssetMaster.AssetCode AND tblPurchaseITems.purchaseID=" + purchaseId);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListProducts()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;



        //dbQry = "select ItemCode,ProductName from tblProductMaster  Order By ProductName";
        dbQry = "SELECT ItemCode + ' - ' + ProductDesc As ProductName,ItemCode FROM tblProductMaster";

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet ListProductDetails(string itemCode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select ProductName,ProductDesc,Discount,Model,Vat,Rate,Stock from tblProductMaster Where itemCode='" + itemCode + "'";

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int isDuplicateBill(string Billno, int SupplierID)
    {
        string dbQry = string.Empty;
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        dbQry = "SELECT COUNT(*) FROM tblPurchase WHERE Billno='" + Billno + "' and SupplierID=" + SupplierID;
        int cnt = (int)manager.ExecuteScalar(CommandType.Text, dbQry);
        manager.Close();
        return cnt;
    }

    public int InsertPurchase(string Billno, DateTime BillDate, int SupplierID, int paymode, string Chequeno, int BankName, double Amount, string salesreturn, string srReason, DataSet purchaseDS)
    //    public int InsertPurchase(string Billno, DateTime BillDate, int SupplierID, int paymode, string Chequeno, int BankName, double Amount, DataSet purchaseDS)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        int oldSupplierID = 0;
        double oldAmt = 0;
        string sNarration = string.Empty;
        int creditorID = 0;
        int DebtorID = 3;//Purchase A/C HardCoded
        int purchaseID = 0;
        string sVoucherType = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID

            if (paymode == 1)
                creditorID = 1;
            else if (paymode == 2)
                creditorID = BankName;
            else
                creditorID = SupplierID;
            if (salesreturn == "No")
                sVoucherType = "Purchase";
            else
                sVoucherType = "Sales Return";
            sNarration = sVoucherType + " - Bill No:" + Billno;
            //End Retriving the old Debtor and CreditorID

            //Purchase A/c - 3 will always be the debtor
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebtorID);
            double Debit = 0;

            object retDebit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebit != null) && (retDebit != DBNull.Value))
            {
                Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }
            //double Payment = (double)manager.ExecuteScalar(CommandType.Text, "Select Amount from tblDayBook Where TransNo=" + TransNo);
            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", creditorID);
            double Credit = 0;
            object retCredit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCredit != null) && (retCredit != DBNull.Value))
            {
                Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }
            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, creditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit
            //Start Delete the old record


            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,Chequeno,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7})",
             BillDate.ToShortDateString(), DebtorID, creditorID, Amount, sNarration, sVoucherType, Chequeno, 0);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            int NewTransNo = 0;

            object retVal = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                NewTransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            }
            else
            {
                NewTransNo = NewTransNo + 1;
            }


            dbQry = string.Format("INSERT INTO tblPurchase(Billno,BillDate,SupplierID,JournalID,Paymode,TotalAmt,salesreturn,salesreturnreason) VALUES('{0}',Format('{1}', 'dd/mm/yyyy'),{2},{3},{4},{5},'{6}','{7}')",
            Billno, BillDate.ToShortDateString(), SupplierID, NewTransNo, paymode, Amount, salesreturn, srReason);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            object retPurchase = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(PurchaseID) FROM tblPurchase");

            if ((retPurchase != null) && (retPurchase != DBNull.Value))
            {
                purchaseID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(PurchaseID) FROM tblPurchase");
            }
            else
            {
                purchaseID = purchaseID + 1;
            }

            //Store the details in the Audit Table.
            //sAuditStr = "Transaction: " + TransNo + " got edited and deleted Record Details : SupplierID=" + SupplierID + ",CreditorID=3,Amount=" + oldAmt + " New Trans No :" + NewTransNo;
            //dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Adding the Purchase Items Table

            string sSerial = string.Empty;
            string[] arInfo;
            char[] splitter = { ',' };
            int iInsert = 0;
            if (purchaseDS != null)
            {
                if (purchaseDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in purchaseDS.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblPurchaseItems VALUES({0},'{1}',{2},'{3}')", purchaseID, Convert.ToString(dr["AssetCode"]), Convert.ToInt32(dr["Qty"]), "N/A");
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                        //dbQry = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  tblProductMaster.Stock + {0} WHERE ItemCode='{1}'", Convert.ToInt32(dr["Qty"]), Convert.ToString(dr["ItemCode"]));
                        //manager.ExecuteNonQuery(CommandType.Text, dbQry);
                        //foreach(DataRow sR in purchaseDS.Tables[1].Rows)
                        //{
                        sSerial = dr["SerialNo"].ToString();
                        arInfo = sSerial.Split(splitter);
                        for (int k = 0; k <= arInfo.Length - 1; k++)
                        {
                            iInsert = InsertAssetDetails(Convert.ToString(dr["AssetCode"]), Convert.ToString(dr["AssetStatus"]), Convert.ToString(dr["AssetLocation"]), Convert.ToString(dr["AssetArea"]), arInfo[k].ToString(), purchaseID);

                        }
                        //}

                    }
                }
            }


            manager.CommitTransaction();
            return purchaseID;
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    public int UpdatePurchase(int purchaseID, string Billno, DateTime BillDate, int SupplierID, int paymode, string Chequeno, int BankName, double Amount, string salesreturn, string srReason, DataSet purchaseDS)
    //public int UpdatePurchase(int purchaseID, string Billno, DateTime BillDate, int SupplierID, int paymode, string Chequeno, int BankName, double Amount, DataSet purchaseDS)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();

        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        int oldDebitID = 0;
        int oldCreditID = 0;
        double oldAmt = 0;
        int oldPurchaseID = 0;
        string sVoucherType = string.Empty;

        if (salesreturn == "No")
            sVoucherType = "Purchase";
        else
            sVoucherType = "Sales Return";
        string sNarration = string.Empty;
        int creditorID = 0;
        int DebtorID = 3;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID
            dsOld = GetPurchaseForId(Billno);


            if (dsOld.Tables[0].Rows[0]["JournalID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["JournalID"].ToString() != string.Empty)
                {
                    TransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["JournalID"].ToString());
                }
            }
            dbQry = string.Format("Select DebtorID,CreditorID,Amount from tblDaybook Where TransNo={0}", TransNo);
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (dsOld != null)
            {
                if (dsOld.Tables.Count > 0)
                {
                    oldDebitID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"]);
                    oldCreditID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"]);
                    oldAmt = Convert.ToDouble(dsOld.Tables[0].Rows[0]["Amount"]);
                }

            }
            //End Retriving

            if (paymode == 1)
                creditorID = 1;
            else if (paymode == 2)
                creditorID = BankName;
            else
                creditorID = SupplierID;
            sNarration = sVoucherType + " - Bill No:" + Billno;

            //Delete Purchase

            /*
             Step 1 : Decrese the Debit and Credit for the Old Debtor and Old Creditor.
             Step 2 : Delete the Entry From tblDayBook,tblPurchase,tblPurchaseItem.
             Step 3 : Increase the Debit and Credit for the new Debtor and new Creditor.
             Step 4 : Add Entry From tblDayBook,tblPurchase,tblPurchaseItem.
             Step 5 : Make an entry in the audit table.
             */
            //Step 1 - Start

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebitID);
            double DebitDel = 0;

            object retDebitDel = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebitDel != null) && (retDebitDel != DBNull.Value))
            {
                DebitDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", DebitDel - oldAmt, oldDebitID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditID);
            double CreditDel = 0;

            object retCreditDel = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCreditDel != null) && (retCreditDel != DBNull.Value))
            {
                CreditDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", CreditDel - oldAmt, oldCreditID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //Step 1 - End


            //Step 2 - Start
            //Start Delete the old record
            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("SELECT Qty,ItemCode From tblPurchaseItems WHERE PurchaseID={0}", purchaseID);
            DataSet purchasedS = (DataSet)manager.ExecuteDataSet(CommandType.Text, dbQry);
            oldPurchaseID = purchaseID;
            if (purchasedS != null)
            {
                if (purchasedS.Tables.Count > 0)
                {
                    foreach (DataRow dr in purchasedS.Tables[0].Rows)
                    {
                        //dbQry = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  tblProductMaster.Stock - {0} WHERE ItemCode='{1}'", Convert.ToInt32(dr["Qty"]), Convert.ToString(dr["ItemCode"]));
                        //manager.ExecuteNonQuery(CommandType.Text, dbQry);
                        dbQry = string.Format("DELETE FROM AssetDetails WHERE PurchaseID={0} AND AssetCode='{1}'", purchaseID, Convert.ToString(dr["ItemCode"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }
            dbQry = string.Format("Delete From tblPurchase Where PurchaseID={0}", purchaseID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPurchaseItems Where PurchaseID={0}", purchaseID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Deleting the old record


            //Step 2 - End




            //End Delete Purchase
            //Purchase A/c - 3 will always be the debtor
            //Step 3 - Start
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebtorID);
            double Debit = 0;

            object retDebit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebit != null) && (retDebit != DBNull.Value))
            {
                Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }
            //double Payment = (double)manager.ExecuteScalar(CommandType.Text, "Select Amount from tblDayBook Where TransNo=" + TransNo);
            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", creditorID);
            double Credit = 0;

            object retCredit = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCredit != null) && (retCredit != DBNull.Value))
            {
                Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }
            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, creditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit
            //Step 3 - End

            //Step 4 - Start
            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,Chequeno,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7})",
             BillDate.ToShortDateString(), DebtorID, creditorID, Amount, sNarration, sVoucherType, Chequeno, 0);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            int NewTransNo = 0;
            object retVal = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                NewTransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            }
            else
            {
                NewTransNo = NewTransNo + 1;
            }

            dbQry = string.Format("INSERT INTO tblPurchase(Billno,BillDate,SupplierID,JournalID,Paymode,TotalAmt) VALUES({0},Format('{1}', 'dd/mm/yyyy'),{2},{3},{4},{5})",
           Billno, BillDate.ToShortDateString(), SupplierID, NewTransNo, paymode, Amount);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            object retPurchase = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(PurchaseID) FROM tblPurchase");

            if ((retPurchase != null) && (retPurchase != DBNull.Value))
            {
                purchaseID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(PurchaseID) FROM tblPurchase");
            }
            else
            {
                purchaseID = purchaseID + 1;
            }
            //Store the details in the Audit Table.
            //sAuditStr = "Transaction: " + TransNo + " got edited and deleted Record Details : SupplierID=" + SupplierID + ",CreditorID=3,Amount=" + oldAmt + " New Trans No :" + NewTransNo;
            //dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Adding the Purchase Items Table
            string sSerial = string.Empty;
            string[] arInfo;
            char[] splitter = { ',' };
            int iInsert = 0;
            if (purchaseDS != null)
            {
                if (purchaseDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in purchaseDS.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblPurchaseItems VALUES({0},'{1}',{2},'{3}')", purchaseID, Convert.ToString(dr["AssetCode"]), Convert.ToInt32(dr["Qty"]), "N/A");
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                        //dbQry = string.Format("INSERT INTO tblPurchaseItems VALUES({0},'{1}',{2},{3},{4},{5})", purchaseID, Convert.ToString(dr["ItemCode"]), Convert.ToInt32(dr["Qty"]), Convert.ToDouble(dr["PurchaseRate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]));
                        //manager.ExecuteNonQuery(CommandType.Text, dbQry);


                        //foreach (DataRow sR in purchaseDS.Tables[1].Rows)
                        //{
                        sSerial = dr["SerialNo"].ToString();
                        arInfo = sSerial.Split(splitter);
                        for (int k = 0; k <= arInfo.Length - 1; k++)
                        {
                            //iInsert = InsertAssetDetails(Convert.ToString(dr["AssetCode"]), Convert.ToString(dr["AssetStatus"]), Convert.ToString(dr["AssetLocation"]), Convert.ToString(dr["AssetArea"]), arInfo[k].ToString(), purchaseID);
                            dbQry = string.Format("INSERT INTO AssetDetails(AssetCode,AssetStatus,AssetLocation,AssetArea,DateEntered,SerialNo,purchaseID) VALUES('{0}','{1}','{2}','{3}',Format('{4}', 'dd/mm/yyyy'),'{5}',{6})",
                            Convert.ToString(dr["AssetCode"]), Convert.ToString(dr["AssetStatus"]), Convert.ToString(dr["AssetLocation"]), Convert.ToString(dr["AssetArea"]), DateTime.Now.ToShortDateString(), arInfo[k].ToString(), purchaseID);
                            manager.ExecuteNonQuery(CommandType.Text, dbQry);

                        }
                        //}
                    }
                }
            }
            //Step 4 - End

            //Step 5 - Start
            //Store the details in the Audit Table.
            sAuditStr = "Purchase Transaction: " + TransNo + " got edited and deleted Record Details : DebtorID=" + oldDebitID + ",CreditorID=" + oldCreditID + ",Amount=" + oldAmt + " New Trans No :" + NewTransNo;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();
            return purchaseID;
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    public void DeletePurchase(int purchaseID, string Billno)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        int oldDebitID = 0;
        int oldCreditID = 0;
        double oldAmt = 0;


        string sNarration = string.Empty;

        int creditorID = 0;
        int DebtorID = 3;



        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID
            dsOld = GetPurchaseForId(Billno);


            if (dsOld.Tables[0].Rows[0]["JournalID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["JournalID"].ToString() != string.Empty)
                {
                    TransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["JournalID"].ToString());
                }
            }
            dbQry = string.Format("Select DebtorID,CreditorID,Amount from tblDaybook Where TransNo={0}", TransNo);
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (dsOld != null)
            {
                if (dsOld.Tables.Count > 0)
                {
                    oldDebitID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"]);
                    oldCreditID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"]);
                    oldAmt = Convert.ToDouble(dsOld.Tables[0].Rows[0]["Amount"]);
                }

            }

            //Step 1 - Start

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebitID);
            double DebitDel = 0;

            object retDebitDel = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retDebitDel != null) && (retDebitDel != DBNull.Value))
            {
                DebitDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", DebitDel - oldAmt, oldDebitID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditID);
            double CreditDel = 0;

            object retCreditDel = manager.ExecuteScalar(CommandType.Text, dbQry);
            if ((retCreditDel != null) && (retCreditDel != DBNull.Value))
            {
                CreditDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            }

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", CreditDel - oldAmt, oldCreditID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //Step 1 - End


            //Step 2 - Start
            //Start Delete the old record
            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            dbQry = string.Format("SELECT Qty,ItemCode From tblPurchaseItems WHERE PurchaseID={0}", purchaseID);
            DataSet purchaseDS = (DataSet)manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (purchaseDS != null)
            {
                if (purchaseDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in purchaseDS.Tables[0].Rows)
                    {
                        //dbQry = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  tblProductMaster.Stock - {0} WHERE ItemCode='{1}'", Convert.ToInt32(dr["Qty"]), Convert.ToString(dr["ItemCode"]));
                        dbQry = string.Format("DELETE FROM AssetDetails WHERE PurchaseID={0} AND AssetCode='{1}'", purchaseID, Convert.ToString(dr["ItemCode"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            dbQry = string.Format("Delete From tblPurchase Where PurchaseID={0}", purchaseID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPurchaseItems Where PurchaseID={0}", purchaseID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Deleting the old record


            sAuditStr = "Purchase Transaction: " + TransNo + " got deleted old Record Details : DebtorID=" + oldDebitID + ",CreditorID=" + oldCreditID + ",Amount=" + oldAmt;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);




            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    #endregion


    #region "Sales"

    public DataSet GetSalesForId(int Billno)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {



            //dbQry.Append("SELECT Billno,BillDate,CustomerName FROM tblSales WHERE Billno =" + Billno);
            dbQry.Append("SELECT tblSales.Billno,Format(tblSales.Billdate, 'dd/mm/yyyy') As BillDate,tblSales.CustomerName,tblSales.CustomerAddress,tblSales.CustomerContacts,tblSales.Paymode,tblDayBook.Amount,tblDayBook.narration,tblDayBook.CreditCardNo,tblSales.JournalID,Creditor.LedgerName As Creditor");
            dbQry.Append(" FROM (((tblDayBook  INNER JOIN  tblSales ON tblSales.JournalID = tblDayBook.Transno)INNER JOIN tblLedger Creditor ON tblDaybook.CreditorID = Creditor.LedgerID))");
            dbQry.Append(" Where tblSales.Billno=" + Billno);
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int InsertSales(string BillDate, int sCustomerID, string sCustomerName, string sCustomerAddress, string sCustomerContact, int paymode, string sCreditCardno, int BankName, double Amount, DataSet salesDS)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        DateTime sBilldate;
        string[] sDate;
        string delim = "/";
        char[] delimA = delim.ToCharArray();

        try
        {
            sDate = BillDate.Trim().Split(delimA);


            sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[0].ToString()), Convert.ToInt32(sDate[1].ToString()));

        }
        catch (Exception ex)
        {
            //Response.Write("<b><font face=verdana size=2 color=red>Invalid Bill Date Format</font></b>");
            throw new Exception("Invalid Bill Date Format", ex);
        }
        double oldAmt = 0;
        string sNarration = string.Empty;
        int creditorID = 2;
        int DebtorID = 0;
        int salesID = 0;
        int BillNo = 0;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID

            if (paymode == 1)
                DebtorID = 1;
            else if (paymode == 2)
                DebtorID = BankName;
            else
                DebtorID = sCustomerID;

            //End Retriving the old Debtor and CreditorID

            //Sales A/c -2 will always be the Creditor
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebtorID);
            double Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            //double Payment = (double)manager.ExecuteScalar(CommandType.Text, "Select Amount from tblDayBook Where TransNo=" + TransNo);
            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", creditorID);
            double Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, creditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit
            //Start Delete the old record


            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,CreditCardNo,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7})",
             DateTime.Now.ToShortDateString(), DebtorID, creditorID, Amount, sNarration, "Sales", sCreditCardno, 0);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            int NewTransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            BillNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BillNo)+1 FROM tblSales");
            dbQry = string.Format("INSERT INTO tblSales(Billno,BillDate,JournalID,CustomerID,CustomerName,CustomerAddress,CustomerContacts,Paymode) VALUES({0},Format('{1}', 'dd/mm/yyyy'),{2},{3},'{4}','{5}','{6}',{7})",
           BillNo, sBilldate.ToShortDateString(), NewTransNo, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, paymode);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            salesID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BillNo) FROM tblSales");
            sNarration = "Sales-Bill No:" + BillNo;
            dbQry = string.Format("Update tblDayBook SET Narration = '{0}' Where TransNo={1}", sNarration, NewTransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            //Adding the Purchase Items Table
            if (salesDS != null)
            {
                if (salesDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in salesDS.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblSalesItems(BillNo,ItemCode,Qty,Rate,Discount,Vat,SlNo) VALUES({0},'{1}',{2},{3},{4},{5},{6})", salesID, Convert.ToString(dr["ItemCode"]), Convert.ToInt32(dr["Qty"]), Convert.ToDouble(dr["Rate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToInt32(dr["SlNo"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }
            return salesID;
            manager.CommitTransaction();

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public void UpdateSales(int Billno, string BillDate, int sCustomerID, string sCustomerName, string sCustomerAddress, string sCustomerContact, int paymode, string sCreditCardno, int BankName, double Amount, DataSet salesDS)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();




        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        int oldDebitID = 0;
        int oldCreditID = 0;
        double oldAmt = 0;
        DateTime sBilldate;
        string[] sDate;
        string delim = "/";
        char[] delimA = delim.ToCharArray();

        try
        {
            sDate = BillDate.Trim().Split(delimA);


            sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[0].ToString()), Convert.ToInt32(sDate[1].ToString()));

        }
        catch (Exception ex)
        {
            //Response.Write("<b><font face=verdana size=2 color=red>Invalid Bill Date Format</font></b>");
            return;
        }

        string sNarration = string.Empty;

        int creditorID = 2;
        int DebtorID = 0;
        int salesID = 0;
        int BillNo = 0;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID
            dsOld = GetSalesForId(Billno);


            if (dsOld.Tables[0].Rows[0]["JournalID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["JournalID"].ToString() != string.Empty)
                {
                    TransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["JournalID"].ToString());
                }
            }
            dbQry = string.Format("Select DebtorID,CreditorID,Amount from tblDaybook Where TransNo={0}", TransNo);
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (dsOld != null)
            {
                if (dsOld.Tables.Count > 0)
                {
                    oldDebitID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"]);
                    oldCreditID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"]);
                    oldAmt = Convert.ToDouble(dsOld.Tables[0].Rows[0]["Amount"]);
                }

            }
            //End Retriving

            if (paymode == 1)
                DebtorID = 1;
            else if (paymode == 2)
                DebtorID = BankName;
            else
                DebtorID = sCustomerID;
            sNarration = "Sales - Bill No:" + Billno;

            //Delete Purchase

            /*
             Step 1 : Decrese the Debit and Credit for the Old Debtor and Old Creditor.
             Step 2 : Delete the Entry From tblDayBook,tblPurchase,tblPurchaseItem.
             Step 3 : Increase the Debit and Credit for the new Debtor and new Creditor.
             Step 4 : Add Entry From tblDayBook,tblPurchase,tblPurchaseItem.
             Step 5 : Make an entry in the audit table.
             */
            //Step 1 - Start

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebitID);
            double DebitDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", DebitDel - oldAmt, oldDebitID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditID);
            double CreditDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", CreditDel - oldAmt, oldCreditID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //Step 1 - End


            //Step 2 - Start
            //Start Delete the old record
            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblSales Where Billno={0}", Billno);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblSalesItems Where Billno={0}", Billno);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Deleting the old record


            //Step 2 - End




            //End Delete Purchase
            //Purchase A/c - 3 will always be the debtor
            //Step 3 - Start
            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", DebtorID);
            double Debit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            //double Payment = (double)manager.ExecuteScalar(CommandType.Text, "Select Amount from tblDayBook Where TransNo=" + TransNo);
            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", Debit + Amount, DebtorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", creditorID);
            double Credit = (double)manager.ExecuteScalar(CommandType.Text, dbQry);
            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", Credit + Amount, creditorID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Updating the Debit and credit
            //Step 3 - End

            //Step 4 - Start
            dbQry = string.Format("INSERT INTO tblDayBook(TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType,CreditCardNo,RefNo) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2},{3},'{4}','{5}','{6}',{7})",
           DateTime.Now.ToShortDateString(), DebtorID, creditorID, Amount, sNarration, "Sales", sCreditCardno, 0);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            int NewTransNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(TransNo) FROM tblDayBook");
            BillNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BillNo)+1 FROM tblSales");
            dbQry = string.Format("INSERT INTO tblSales(Billno,BillDate,JournalID,CustomerID,CustomerName,CustomerAddress,CustomerContacts,Paymode) VALUES({0},Format('{1}', 'dd/mm/yyyy'),{2},{3},'{4}','{5}','{6}',{7})",
           BillNo, sBilldate.ToShortDateString(), NewTransNo, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, paymode);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            salesID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BillNo) FROM tblSales");
            sNarration = "Sales - Bill No:" + BillNo;
            dbQry = string.Format("Update tblDayBook SET Narration = '{0}' Where TransNo={1}", sNarration, NewTransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            //Adding the Purchase Items Table
            if (salesDS != null)
            {
                if (salesDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in salesDS.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblSalesItems(BillNo,ItemCode,Qty,Rate,Discount,Vat,SlNo) VALUES({0},'{1}',{2},{3},{4},{5},{6})", salesID, Convert.ToString(dr["ItemCode"]), Convert.ToInt32(dr["Qty"]), Convert.ToDouble(dr["Rate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToInt32(dr["SlNo"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }
            //Step 4 - End

            //Step 5 - Start
            //Store the details in the Audit Table.
            sAuditStr = "Sales Transaction: " + TransNo + " got edited and deleted Record Details : DebtorID=" + oldDebitID + ",CreditorID=" + oldCreditID + ",Amount=" + oldAmt + " New Trans No :" + NewTransNo;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    public DataSet GetSalesItemsForId(int Billno)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        try
        {

            dbQry.Append("Select tblSalesitems.ItemCode,tblProductMaster.ProductName,tblProductMaster.ProductDesc,tblSalesitems.Rate,tblSalesitems.Qty,");
            dbQry.Append("tblSalesitems.discount,tblSalesitems.Vat,tblSalesitems.billno,tblSalesItems.SlNo,tblProductMaster.Model FROM tblSalesitems INNER JOIN tblProductmaster ON tblSalesitems.itemCode = tblProductMaster.itemCode");
            dbQry.Append(" Where tblSalesitems.Billno = " + Billno);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void DeleteSales(int Billno)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();

        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        int TransNo = 0;
        DataSet dsOld = new DataSet();
        int oldDebitID = 0;
        int oldCreditID = 0;
        double oldAmt = 0;


        string sNarration = string.Empty;

        int creditorID = 0;
        int DebtorID = 3;



        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID
            dsOld = GetSalesForId(Billno);


            if (dsOld.Tables[0].Rows[0]["JournalID"] != null)
            {
                if (dsOld.Tables[0].Rows[0]["JournalID"].ToString() != string.Empty)
                {
                    TransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["JournalID"].ToString());
                }
            }
            dbQry = string.Format("Select DebtorID,CreditorID,Amount from tblDaybook Where TransNo={0}", TransNo);
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (dsOld != null)
            {
                if (dsOld.Tables.Count > 0)
                {
                    oldDebitID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["DebtorID"]);
                    oldCreditID = Convert.ToInt32(dsOld.Tables[0].Rows[0]["CreditorID"]);
                    oldAmt = Convert.ToDouble(dsOld.Tables[0].Rows[0]["Amount"]);
                }

            }

            //Step 1 - Start

            dbQry = string.Format("Select Debit from tblLedger Where LedgerID={0}", oldDebitID);
            double DebitDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Debit = {0} Where LedgerID={1}", DebitDel - oldAmt, oldDebitID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Select Credit from tblLedger Where LedgerID={0}", oldCreditID);
            double CreditDel = (double)manager.ExecuteScalar(CommandType.Text, dbQry);

            dbQry = string.Format("Update tblLedger SET Credit = {0} Where LedgerID={1}", CreditDel - oldAmt, oldCreditID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //Step 1 - End


            //Step 2 - Start
            //Start Delete the old record
            dbQry = string.Format("Delete From tblDayBook Where TransNo={0}", TransNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblSales Where BillNo={0}", Billno);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblSalesItems Where BillNo={0}", Billno);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //End Deleting the old record


            sAuditStr = "Sales Transaction: " + TransNo + " got deleted old Record Details : DebtorID=" + oldDebitID + ",CreditorID=" + oldCreditID + ",Amount=" + oldAmt;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);




            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }
    #endregion

    #region Asset Details
    public DataSet ListAssetCode()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select AssetCode FROM AssetMaster";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListAssetArea()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select area FROM AreaMaster";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListAssetCat()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select CategoryID,CategoryDescription FROM AssetCatagory ORDER BY CategoryDescription";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListAssetDetails()
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select AssetNo,AssetCode,AssetStatus,Assetlocation,AssetArea,DateEntered,SerialNo FROM AssetDetails ORDER BY DateEntered DESC";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet ListAssetDetailsPurchase(string AssetCode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "SELECT AssetMaster.AssetDesc,AssetCatagory.CategoryDescription FROM AssetMaster,AssetCatagory WHERE AssetMaster.CategoryID = AssetCatagory.CategoryID AND AssetMaster.AssetCode ='" + AssetCode + "'";
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public int InsertAssetDetails(string AssetCode, string AssetStatus, string AssetLocation, string AssetArea, string SerialNo, int purchaseID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;

        dbQry = string.Format("INSERT INTO AssetDetails(AssetCode,AssetStatus,AssetLocation,AssetArea,DateEntered,SerialNo,purchaseID) VALUES('{0}','{1}','{2}','{3}',Format('{4}', 'dd/mm/yyyy'),'{5}',{6})",
        AssetCode, AssetStatus, AssetLocation, AssetArea, DateTime.Now.ToShortDateString(), SerialNo, purchaseID);
        manager.ExecuteNonQuery(CommandType.Text, dbQry);
        int NewAssetNo = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(AssetNo) FROM AssetDetails");

        manager.Dispose();
        return NewAssetNo;
    }

    public int UpdateAssetDetails(string AssetCode, string AssetStatus, string AssetLocation, string AssetArea, int AssetNo, string SerialNo)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();

        dbQry = string.Format("UPDATE AssetDetails SET AssetCode='{0}',AssetStatus='{1}',AssetLocation='{2}',AssetArea='{3}',DateEntered=Format('{4}', 'dd/mm/yyyy'),SerialNo='{6}' WHERE AssetNo={5}",
        AssetCode, AssetStatus, AssetLocation, AssetArea, DateTime.Now.ToShortDateString(), AssetNo, SerialNo);
        manager.ExecuteNonQuery(CommandType.Text, dbQry);

        sAuditStr = "Asset Details Modification: " + AssetNo + " Got Modified ";
        dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
        manager.ExecuteNonQuery(CommandType.Text, dbQry);
        //Step 5 -  End
        manager.CommitTransaction();
        manager.Dispose();
        return AssetNo;
    }

    public int DeleteAssetDetails(int AssetNo)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();

        /*
         * dbQry = string.Format("Select purchaseID,itemcode from AssetDetails Where AssetNo={0}", AssetNo);
        //int purchaseID = (int)manager.ExecuteScalar(CommandType.Text, dbQry);
        
        ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dbQry = string.Format("DELETE FROM tblPurchaseitems WHERE itemcode='{0}' AND purchaseID ={1} ", Convert.ToString(dr["itemcode"]),Convert.ToInt32(dr["purchaseID"])) ;
                    manager.ExecuteNonQuery(CommandType.Text, dbQry);

                }
            }
        }
         */
        dbQry = string.Format("DELETE FROM AssetDetails WHERE AssetNo={0}", AssetNo);
        manager.ExecuteNonQuery(CommandType.Text, dbQry);

        sAuditStr = "Asset Details Deleted: " + AssetNo + " Got Deleted ";
        dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
        manager.ExecuteNonQuery(CommandType.Text, dbQry);
        //Step 5 -  End
        manager.CommitTransaction();

        manager.Dispose();
        return AssetNo;
    }
    public DataSet SearchAsset(int assetNo, string sAssetCat, string sArea, string sStatus)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;

        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        String searchStr = string.Empty;

        if (assetNo > 0)
            searchStr = " AssetNo=" + assetNo;
        if (sAssetCat != string.Empty && sAssetCat != null)
        {
            if (searchStr == string.Empty)
                //AssetDetails.AssetCode IN (SELECT AssetCode FROM AssetMaster Where CategoryID IN (SELECT CategoryID From AssetCatagory) ) 
                searchStr = searchStr + " AssetDetails.AssetCode IN  (SELECT AssetCode FROM AssetMaster Where CategoryID IN (SELECT CategoryID From AssetCatagory WHERE CategoryID=" + Convert.ToInt32(sAssetCat.Trim()) + "))";
            else
                searchStr = searchStr + " AND AssetDetails.AssetCode IN  (SELECT AssetCode FROM AssetMaster Where CategoryID IN (SELECT CategoryID From AssetCatagory WHERE CategoryID=" + Convert.ToInt32(sAssetCat.Trim()) + "))";
        }
        if (sArea != string.Empty && sArea != null)
        {
            if (searchStr == string.Empty)
                searchStr = searchStr + " AssetArea='" + sArea.Trim() + "' ";
            else
                searchStr = searchStr + " AND AssetArea='" + sArea.Trim() + "' ";
        }
        if (sStatus != string.Empty && sStatus != null)
        {
            if (searchStr == string.Empty)
                searchStr = searchStr + " AssetStatus='" + sStatus.Trim() + "' ";
            else
                searchStr = searchStr + " AND AssetStatus='" + sStatus.Trim() + "' ";
        }

        if (searchStr != string.Empty)
        {
            dbQry.Append("select AssetNo,AssetCode,AssetStatus,Assetlocation,AssetArea,DateEntered,SerialNo FROM AssetDetails");
            dbQry.AppendFormat(" Where {0} ORDER BY DateEntered DESC", searchStr);

        }
        else
        {
            dbQry.Append("select AssetNo,AssetCode,AssetStatus,Assetlocation,AssetArea,DateEntered,SerialNo FROM AssetDetails ORDER BY DateEntered DESC");
        }


        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Asset Master
    public DataSet SearchAssetMaster(string sAssetCat, string sCode)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;

        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        String searchStr = string.Empty;


        if (sAssetCat != string.Empty && sAssetCat != null)
        {
            if (searchStr == string.Empty)
                //AssetDetails.AssetCode IN (SELECT AssetCode FROM AssetMaster Where CategoryID IN (SELECT CategoryID From AssetCatagory) ) 
                searchStr = searchStr + " AssetMaster.CategoryID=" + Convert.ToInt32(sAssetCat.Trim());
            else
                searchStr = searchStr + " AND AssetMaster.CategoryID=" + Convert.ToInt32(sAssetCat.Trim());
        }
        if (sCode != string.Empty && sCode != null)
        {
            if (searchStr == string.Empty)
                searchStr = searchStr + " AssetMaster.AssetCode LIKE '" + sCode.Trim() + "%' ";
            else
                searchStr = searchStr + " AND AssetMaster.AssetCode LIKE '" + sCode.Trim() + "%' ";
        }

        if (searchStr != string.Empty)
        {
            //dbQry.Append("select AssetNo,AssetCode,AssetStatus,Assetlocation,AssetArea,DateEntered FROM AssetDetails");
            //dbQry.AppendFormat(" Where {0} ORDER BY DateEntered DESC", searchStr);
            dbQry.Append("SELECT AssetMaster.AssetCode,AssetCatagory.CategoryDescription,AssetMaster.AssetDesc  FROM AssetMaster,AssetCatagory Where AssetMaster.CategoryID = AssetCatagory.CategoryID AND ");
            dbQry.AppendFormat("  {0} ORDER BY AssetMaster.AssetCode", searchStr);
        }
        else
        {
            dbQry.Append("SELECT AssetMaster.AssetCode,AssetCatagory.CategoryDescription,AssetMaster.AssetDesc  FROM AssetMaster,AssetCatagory Where AssetMaster.CategoryID = AssetCatagory.CategoryID ORDER BY AssetMaster.AssetCode");
        }


        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string InsertAssetMaster(string AssetCode, string AssetDesc, int catID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;

        dbQry = string.Format("INSERT INTO AssetMaster(AssetCode,AssetDesc,CategoryID) VALUES('{0}','{1}',{2})",
        AssetCode, AssetDesc, catID);
        manager.ExecuteNonQuery(CommandType.Text, dbQry);


        manager.Dispose();
        return AssetCode;

    }

    public string UpdateAssetMaster(string oldcode, string AssetCode, string AssetDesc, int catID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();
        dbQry = "SELECT COUNT(*) As  Cnt  FROM AssetDetails WHERE AssetDetails.AssetCode = '" + oldcode + "'";


        int cnt = (Int32)manager.ExecuteScalar(CommandType.Text, dbQry);
        if (oldcode == AssetCode && cnt == 0)
        {
            dbQry = string.Format("UPDATE AssetMaster SET AssetCode='{0}',AssetDesc='{1}',categoryID={2} WHERE AssetCode='{0}'",
            AssetCode, AssetDesc, catID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);



            sAuditStr = "Asset Master Modification: " + AssetCode + " Got Modified ";
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();


            manager.Dispose();
            return AssetCode;
        }
        else
        {
            return "Updation Denied  - (Assets are there in the Category)";
        }
    }

    public string DeleteAssetMaster(string AssetCode)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();

        //dbQry = "SELECT COUNT(*) As  Cnt FROM AssetMaster WHERE AssetMaster.AssetCode IN (SELECT AssetCode FROM AssetDetails) AND AssetMaster.AssetCode = '" + AssetCode  + "'";
        dbQry = "SELECT COUNT(*) As  Cnt  FROM AssetDetails WHERE AssetDetails.AssetCode = '" + AssetCode + "'";


        int cnt = (Int32)manager.ExecuteScalar(CommandType.Text, dbQry);
        if (cnt == 0)
        {
            dbQry = string.Format("DELETE FROM AssetMaster WHERE AssetCode='{0}'", AssetCode);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            sAuditStr = "Asset Master Deleted: " + AssetCode + " Got Deleted ";
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();

            manager.Dispose();
            return AssetCode;

        }
        else
        {
            return "Delete Denied  - (Assets are there in the Category)";
        }
    }
    #endregion

    #region Asset Cateory
    public DataSet SearchAssetCat(string sAssetCat)
    {

        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString;

        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        String searchStr = string.Empty;


        if (sAssetCat != string.Empty && sAssetCat != null)
        {
            if (searchStr == string.Empty)
                //AssetDetails.AssetCode IN (SELECT AssetCode FROM AssetMaster Where CategoryID IN (SELECT CategoryID From AssetCatagory) ) 
                searchStr = searchStr + " AssetCatagory.CategoryDescription='" + sAssetCat.Trim() + "'";
            else
                searchStr = searchStr + " AND AssetCatagory.CategoryDescription='" + sAssetCat.Trim() + "'";
        }


        if (searchStr != string.Empty)
        {
            //dbQry.Append("select AssetNo,AssetCode,AssetStatus,Assetlocation,AssetArea,DateEntered FROM AssetDetails");
            //dbQry.AppendFormat(" Where {0} ORDER BY DateEntered DESC", searchStr);
            dbQry.Append("SELECT AssetCatagory.CategoryDescription,AssetCatagory.CategoryID  FROM AssetCatagory Where  ");
            dbQry.AppendFormat("  {0} ORDER BY AssetCatagory.CategoryDescription", searchStr);
        }
        else
        {
            dbQry.Append("SELECT AssetCatagory.CategoryDescription,AssetCatagory.CategoryID  FROM AssetCatagory ORDER BY AssetCatagory.CategoryDescription  ");

        }


        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string InsertAssetCat(string AssetCat)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;

        dbQry = string.Format("INSERT INTO AssetCatagory(CategoryDescription) VALUES('{0}')",
       AssetCat);
        manager.ExecuteNonQuery(CommandType.Text, dbQry);


        manager.Dispose();
        return AssetCat;

    }

    public string UpdateAssetCat(string oldcode, string AssetCat, int catID)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();
        dbQry = "SELECT COUNT(*) As  Cnt  FROM AssetMaster WHERE AssetMaster.CategoryID = " + catID;


        int cnt = (Int32)manager.ExecuteScalar(CommandType.Text, dbQry);
        if (cnt == 0)
        {
            dbQry = string.Format("UPDATE AssetCatagory SET CategoryDescription='{0}' WHERE CategoryID={1}",
            AssetCat, catID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);



            sAuditStr = "Asset Category Modification: " + AssetCat + " Got Modified ";
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();


            manager.Dispose();
            return AssetCat;
        }
        else
        {
            return "Updation Denied - (Assets are there in the Category)";
        }
    }

    public string DeleteAssetCat(int AssetCat)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        manager.Open();
        manager.ProviderType = DataProvider.OleDb;
        manager.BeginTransaction();

        //dbQry = "SELECT COUNT(*) As  Cnt FROM AssetMaster WHERE AssetMaster.AssetCode IN (SELECT AssetCode FROM AssetDetails) AND AssetMaster.AssetCode = '" + AssetCode  + "'";
        dbQry = "SELECT COUNT(*) As  Cnt  FROM AssetMaster WHERE AssetMaster.CategoryID = " + AssetCat;


        int cnt = (Int32)manager.ExecuteScalar(CommandType.Text, dbQry);
        if (cnt == 0)
        {
            dbQry = string.Format("DELETE FROM AssetCatagory WHERE CategoryID={0}", AssetCat);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            sAuditStr = "Asset category Deleted: " + AssetCat + " Got Deleted ";
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //Step 5 -  End
            manager.CommitTransaction();

            manager.Dispose();
            return AssetCat.ToString();

        }
        else
        {
            return "Delete Denied - (Assets are there in the Category)";
        }
    }
    #endregion

    public DataSet getCompanyInfo(string company)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        try
        {
            manager.Open();
            dbQry = "SELECT CompanyName,Address,City,State,PinCode,Phone,Tinno,Gstno FROM tblCompanyInfo WHERE CompanyName='" + company + "'";
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public string getCashEntryMode(string company)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = this.ConnectionString; // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        try
        {
            manager.Open();
            dbQry = "SELECT Mode FROM CashEntryMode";
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            if (ds.Tables[0].Rows.Count == 1)
                return ds.Tables[0].Rows[0]["Mode"].ToString();
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetDisconCustomers(string Area, string Name, string Connection)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Connection].ConnectionString;
        DataSet ds = new DataSet();
        StringBuilder dbQry = new StringBuilder();

        Area = Area.Replace("^", "''");
        Name = "%" + Name + "%";

        try
        {
            dbQry.Append("Select code,area,name,category,effectDate,doorno From CustomerMaster ");
            dbQry.AppendFormat("Where category = 'DC' and area = '{0}' and name like '{1}'", Area, Name);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetCashHistory(string connection, string Code, string Area)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        Area = Area.Replace("^", "''");

        try
        {
            dbQry = string.Format("SELECT slno, [code], [area], [amount], [discount], [reason], [date_paid], [date_entered], [billno] FROM [CashDetails] WHERE (([code] = {0}) AND ([area] = '{1}')) ORDER BY slno DESC ", Code, Area);
            manager.Open();

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataSet GetAdjustHistory(string connection, string Code, string Area)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        Area = Area.Replace("^", "''");

        try
        {
            dbQry = string.Format("SELECT [amount],[reason],[date_entered] FROM [Adjustment] WHERE (([code] = {0}) AND ([area] = '{1}')) ORDER BY date_entered DESC ", Code, Area);
            manager.Open();

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    #region New DB
    public bool CreateNewAccount(string sDataSource, string fileName, string sXmlPath)
    {
        DBManager manager = new DBManager(DataProvider.OleDb);
        manager.ConnectionString = sDataSource;//this.ConnectionString; // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        DataSet ds = new DataSet();

        string dbQry = string.Empty;




        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.OleDb;

            manager.BeginTransaction();
            //Start Retriving the old Debtor and CreditorID

            //Step 1 - Start

            CustomerReportBL.ReportClass rpt = new CustomerReportBL.ReportClass();
            int iGroupID = 0;

            string sXmlNodeName = "Outstanding";
            string sLedger = string.Empty;
            string sFilename = string.Empty;
            double obD = 0;
            double obC = 0;
            rpt.generateOutStandingReport(iGroupID, sXmlNodeName, sDataSource, sXmlPath);
            sFilename = sXmlPath;
            if (File.Exists(sFilename))
            {
                ds.ReadXml(sFilename, XmlReadMode.InferSchema);
                if (ds != null)
                {
                    if (ds.Tables["Transaction"] != null)
                    {

                        if (ds.Tables["Transaction"].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                obD = Convert.ToDouble(dr["Debit"]);
                                obC = Convert.ToDouble(dr["Credit"]);
                                sLedger = dr["LedgerName"].ToString();
                                dbQry = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3} Where LedgerName = '{4}'", 0, 0, obD, obC, sLedger);
                                manager.ExecuteNonQuery(CommandType.Text, dbQry);
                            }
                        }
                    }
                }
            }
            else
            {
                ds = null;
                if (File.Exists(sFilename))
                    File.Delete(sFilename);
                return false;
            }


            //dbQry = string.Format("Delete From ClosingStock");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblAudit");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPayment");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //dbQry = string.Format("Delete From tblReceipt");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPurchaseItems");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblPurchase");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //dbQry = string.Format("Delete From tblSalesItems");
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //dbQry = string.Format("Delete From tblSales");l
            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

            string dateRecon = DateTime.Now.ToString("MM/dd/yyyy");
            manager.ExecuteNonQuery(CommandType.Text, "Update last_recon Set recon_date=Format('" + dateRecon + "', 'MM/dd/yyyy')");



            dbQry = string.Format("Delete From tblDaybook");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();


            if (File.Exists(sFilename))
                File.Delete(sFilename);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }
        finally
        {
            manager.Dispose();
        }

    }
    #endregion


}