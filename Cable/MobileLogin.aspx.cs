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
using System.IO;

public partial class MobileLogin : System.Web.UI.Page
{

    private Hashtable listComp = new Hashtable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString() == "Demo")
                    DemoLogin();
            }

            foreach (ConnectionStringSettings company in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                if (company.Name != "LocalSqlServer")
                    listComp.Add(company.ProviderName, company.Name);
            }

            
            Session["CompanyList"] = listComp;
            txtLogin.Focus();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (Session["CompanyList"] != null)
        {
            listComp = (Hashtable)Session["CompanyList"];

            if (!listComp.Contains(txtCompany.Text.Trim().ToUpper()))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Company Code. Please try again.');", true);
                return;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

        if (txtCompany.Text != "0")
            Session["Company"] = txtCompany.Text;
        else
            return;

        bool isAuthenticated = IsAuthenticated(txtLogin.Text, txtPassword.Text);

        if (isAuthenticated == true)
        {
            string[] roles = GetRoles(txtLogin.Text);

            string roleData = string.Join("|", roles);

            Session["TEST"] = roleData.ToString();

            FormsAuthentication.SignOut();

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(txtLogin.Text, chkRemember.Checked);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, roleData);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            LoadAppSettings();

            //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);
            if (!chkRemember.Checked)
                Response.Redirect(FormsAuthentication.DefaultUrl, true);
            else
                Response.Redirect("MobileCashEntry.aspx");

        }
        else
        {
            lblErrorMsg.Text = "Invalid entry. Please check the username and password";
        }

    }

    private void DemoLogin()
    {
        
        Session["Company"] = "DEMO";
        
        bool isAuthenticated = IsAuthenticated("demo", "demo123");

        if (isAuthenticated == true)
        {
            string[] roles = GetRoles("demo");

            string roleData = string.Join("|", roles);

            FormsAuthentication.SignOut();

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie("demo", true);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, roleData);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            LoadAppSettings();

            //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);
            if (!chkRemember.Checked)
                Response.Redirect(FormsAuthentication.DefaultUrl, true);
            else
                Response.Redirect("MobileCashEntry.aspx");

        }
        else
        {
            lblErrorMsg.Text = "Invalid entry. Please check the username and password";
        }
    }

    private void LoadAppSettings()
    {
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = bl.GetSMSSettings(Session["Company"].ToString());

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["SMSRequired"] = ds.Tables[0].Rows[0]["SMSRequired"].ToString();
                Session["CopyRequired"] = ds.Tables[0].Rows[0]["CopyRequired"].ToString();
                Session["Mobile"] = ds.Tables[0].Rows[0]["Mobile"].ToString();
            }
        }
    }


    protected void lnkSignUp_Click(object sender, EventArgs e)
    {

    }

    private string[] GetRoles(string username)
    {
        // Lookup code omitted for clarity
        // This code would typically look up the role list from a database
        // table.
        // If the user was being authenticated against Active Directory,
        // the Security groups and/or distribution lists that the user
        // belongs to may be used instead

        // This GetRoles method returns a pipe delimited string containing
        // roles rather than returning an array, because the string format
        // is convenient for storing in the authentication ticket /
        // cookie, as user data

        BusinessLogic dbManager = new BusinessLogic();
        string[] roles = dbManager.GetRoles(GetConnectionString(), username);

        return roles;
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        string test = Session["Company"].ToString();

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    private bool IsAuthenticated(string username, string password)
    {
        // Lookup code omitted for clarity
        // This code would typically validate the user name and password
        // combination against a SQL database or Active Directory
        // Simulate an authenticated user
        BusinessLogic objBusLogic = new BusinessLogic();

        DataSet ds = objBusLogic.checkUserCredentials(GetConnectionString(), username, password);

        if (ds != null)
        {
            Session["UserId"] = ds.Tables[0].Rows[0]["UserID"].ToString();
            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
            Session["UserGroup"] = ds.Tables[0].Rows[0]["UserGroup"].ToString();
        }


        if (ds != null)
            return true;
        else
            return false;

    }

    /*
    protected void btnOffline_Click(object sender, EventArgs e)
    {

        if (ddCompany.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the company to work Offline.')", true);
            return;
        }

        //SetStatusInDB("OFFLINE");
        CreateFile();
        Download();
        SendFile();

        //btnOffline.Enabled = false;
        //btnOnline.Enabled = true;
        //lblMsg.Text = "The application is currently configured to work Offline.";
        //lblMsg.ForeColor = System.Drawing.Color.Green;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);

    }

    private void SetStatusInDB(string status)
    {
        BusinessLogic objBus = new BusinessLogic();
        objBus.SetStatusInDB(Session["Company"].ToString(), status);
    }

    */
    /*
    private void Upload()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = string.Empty;
        string connStr = string.Empty;

        localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();
        server = ConfigurationManager.AppSettings["Server"].ToString();
        remotepath = ConfigurationManager.AppSettings["RemotePath"].ToString();
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName);

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName);

    }

    private void RenameFile()
    {
        string server = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        server = ConfigurationManager.AppSettings["Server"].ToString();
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        File.Move(filename, Server.MapPath(localpath + dbfileName + ".online"));

    }

    private void SendFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        server = ConfigurationManager.AppSettings["Server"].ToString();
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();
        
        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName + ".offline");

    }

    private void Download()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string localpath = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = ConfigurationManager.AppSettings["Server"].ToString();
        remotepath = ConfigurationManager.AppSettings["RemotePath"].ToString();
        remoteFile = dbfileName;
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();
        localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();
        filename = Server.MapPath(localpath + dbfileName);

        FileInfo info = new FileInfo(filename);
        info.CopyTo(Path.ChangeExtension(info.FullName + info.Extension, DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()), false);
        File.Delete(info.FullName);

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Download(remotepath + remoteFile, filename, true);

    }

    private void CreateFile()
    {

        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        if (File.Exists(localpath + dbfileName + ".online"))
            File.Delete(localpath + dbfileName + ".online");

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        StreamWriter SW;
        SW = File.CreateText(filename);

        ManagementObjectSearcher query1 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectCollection queryCollection1 = query1.Get();

        foreach (ManagementObject mo in queryCollection1)
        {
            SW.WriteLine("Name : " + mo["name"].ToString());
            SW.WriteLine("Version : " + mo["version"].ToString());
            SW.WriteLine("Manufacturer : " + mo["Manufacturer"].ToString());
            SW.WriteLine("Computer Name : " + mo["csname"].ToString());
            SW.WriteLine("Windows Directory : " + mo["WindowsDirectory"].ToString());
            SW.WriteLine("Date :" + DateTime.Now.ToString());
        }
        
        SW.Close();

    }
    */
    protected void ddCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dbfileName = string.Empty;
        string filename = string.Empty;
        //string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        // DecryptAppSettings();

        //string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        //dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        //filename = Server.MapPath(localpath + dbfileName + ".offline");

        //if (File.Exists(filename))
        //{
        //    btnOffline.Enabled = false;
        //    btnOnline.Enabled = true;
        //    btnContOffline.Enabled = false;
        //    lblMsg.Text = "The application is currently configured to work Offline.";
        //    lblMsg.ForeColor = System.Drawing.Color.Green;
        //}
        //else
        //{
        //    btnOffline.Enabled = true;
        //    btnOnline.Enabled = false;
        //    btnContOffline.Enabled = true;
        //    lblMsg.Text = "The application is currently configured to work Online.";
        //    lblMsg.ForeColor = System.Drawing.Color.Orange;
        //}

    }
    /*
    protected void btnOnline_Click(object sender, EventArgs e)
    {
        if (ddCompany.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the company to work Offline.')", true);
            return;
        }

        //SetStatusInDB("ONLINE");
        Upload();
        RenameFile();
        DeleteFile();

        //btnOffline.Enabled = true;
        //btnOnline.Enabled = false;
        //lblMsg.Text = "The application is currently configured to work Online.";
        //lblMsg.ForeColor = System.Drawing.Color.Orange;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Online. To work Online please switch it back to Offline.')", true);

    }

    private void DeleteFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ddCompany.SelectedValue].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = ConfigurationManager.AppSettings["Server"].ToString();
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        remoteFile = dbfileName;
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        
        if (ftp2.FtpFileExists(remotepath + remoteFile + ".offline"))
        {
            ftp2.FtpDelete(remotepath + remoteFile + ".offline");
        }

    }

    protected void btnContOffline_Click(object sender, EventArgs e)
    {
        if (ddCompany.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the company to work Offline.')", true);
            return;
        }

        CreateFile();
        SendFile();

        //btnOffline.Enabled = false;
        //btnOnline.Enabled = true;
        //lblMsg.Text = "The application is currently configured to work Offline.";
        //lblMsg.ForeColor = System.Drawing.Color.Green;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);

    } */

}
