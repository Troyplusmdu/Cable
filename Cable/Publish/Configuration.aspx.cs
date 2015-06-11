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
using System.Management;
using System.IO;
using ClientLog;

public partial class Configuration : System.Web.UI.Page
{
    private Log _logfile;

    protected void Page_Load(object sender, EventArgs e)
    {
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        _logfile = new Log();
        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");


        if (File.Exists(filename))
        {
            btnOffline.Enabled = false;
            btnOnline.Enabled = true;
            btnContOffline.Enabled = false;
            lblMsg.Text = "The application is currently configured to work Offline.";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            btnOffline.Enabled = true;
            btnOnline.Enabled = false;
            btnContOffline.Enabled = true;
            lblMsg.Text = "The application is currently configured to work Online.";
            lblMsg.ForeColor = System.Drawing.Color.Orange;
        }

        if (!Page.IsPostBack)
        {
            btnOffline.Attributes.Add("onclick", "return ConfirmOffline();");
            btnOnline.Attributes.Add("onclick", "return ConfirmOnline();");
            btnContOffline.Attributes.Add("onclick", "return ConfirmContinueOffline();");
        }

    }

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

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName);

        _logfile.WriteToLog("SERVER : " + server + " USER : " + username + " PSW : " + password);
        _logfile.WriteToLog("SRC : " + filename);
        _logfile.WriteToLog("DST : " + remotepath + dbfileName);
        _logfile.WriteToLog("Upload Started... ");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName);

        _logfile.WriteToLog("Upload Successfull... ");

    }

    private void RenameLocalFile()
    {
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        //File.Move(filename, Server.MapPath(localpath + dbfileName + ".online"));



        File.Copy(filename, Server.MapPath(localpath + dbfileName + ".online"), true);
        File.Delete(filename);

        _logfile.WriteToLog(" File Renamed From : " + filename + " To : " + Server.MapPath(localpath + dbfileName + ".online") + " Successfully.");

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

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName + ".offline");
        _logfile.WriteToLog(" Local file " + filename + " Sent Successfully to " + remotepath + dbfileName + ".offline");

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

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

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

        _logfile.WriteToLog("DB File " + info.FullName + " renamed Successfully... ");

        _logfile.WriteToLog("SERVER : " + server + " USER : " + username + " PSW : " + password);
        _logfile.WriteToLog("SRC : " + remotepath + remoteFile);
        _logfile.WriteToLog("DST : " + filename);
        _logfile.WriteToLog("Download Started... ");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Download(remotepath + remoteFile, filename, true);

        _logfile.WriteToLog("Download Completed... ");

    }

    private void CreateFile()
    {

        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        if (File.Exists(localpath + dbfileName + ".online"))
            File.Delete(localpath + dbfileName + ".online");

        if (File.Exists(localpath + dbfileName + ".offline"))
            File.Delete(localpath + dbfileName + ".offline");

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
        _logfile.WriteToLog("Local Offline File Created " + filename + " Successfully.");

    }

    protected void btnOnline_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload the file to server
            _logfile.WriteToLog("Online Configuration Started...");
            Upload();
            //Rename the local file from offline to online
            RenameLocalFile();
            //Delete the offline file on the server to make it as online
            DeleteFile();

            _logfile.WriteToLog("Online Configuration Completed...");

            btnOffline.Enabled = true;
            btnOnline.Enabled = false;
            btnContOffline.Enabled = true;
            lblMsg.Text = "The application is currently configured to work Online.";
            lblMsg.ForeColor = System.Drawing.Color.Orange;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Online. To work Online please switch it back to Offline.')", true);
        }
        catch (Exception ex)
        {
            _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
                _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configur to work Online. Please contact Administrator.');", true);

        }

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

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = ConfigurationManager.AppSettings["Server"].ToString();
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        remoteFile = dbfileName;
        username = ConfigurationManager.AppSettings["username"].ToString();
        password = ConfigurationManager.AppSettings["password"].ToString();

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);

        _logfile.WriteToLog("Server File to Delete : " + remotepath + remoteFile + ".offline");

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".offline"))
        {
            ftp2.FtpDelete(remotepath + remoteFile + ".offline");
            _logfile.WriteToLog("File Delete Successfully");
        }

    }
    protected void btnContOffline_Click(object sender, EventArgs e)
    {
        try
        {
            _logfile.WriteToLog("Conti Offline Configuration Started...");

            CreateFile();
            SendFile();

            _logfile.WriteToLog("Conti Offline Configuration Completed...");

            btnOffline.Enabled = false;
            btnOnline.Enabled = true;
            lblMsg.Text = "The application is currently configured to work Offline.";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);
        }
        catch (Exception ex)
        {
            _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
                _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configur to work Online. Please contact Administrator.');", true);

        }

    }

    protected void btnOffline_Click(object sender, EventArgs e)
    {
        try
        {
            _logfile.WriteToLog("Offline Configuration Started...");
            //Create a temp file on the local machine in Offline Folder
            CreateFile();
            //Download the file
            Download();
            // Send the temp file in Offline Folder to server Offline Folder
            SendFile();

            _logfile.WriteToLog("Offline Configuration Completed...");

            btnOffline.Enabled = false;
            btnOnline.Enabled = true;
            btnContOffline.Enabled = false;
            lblMsg.Text = "The application is currently configured to work Offline.";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);
        }
        catch (Exception ex)
        {
            _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
                _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configure to work Offline. Please contact Administrator.');", true);

        }

    }
}
