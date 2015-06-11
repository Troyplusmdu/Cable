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
using System.IO;
using System.Web.Configuration;

public partial class DataBackupAndRestore : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = "";
          //  SetDbName();
        }

    }
    public string GetCurrentDBName(string con)
    {
        string str = con; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\DemoDev\\Accsys\\App_Data\\sairama.mdb;Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
        string str2 = string.Empty;
        if (str.Contains(".mdb"))
        {
            str2 = str.Substring(str.IndexOf("Data Source"), str.IndexOf("Persist", 0));
            str2 = str2.Substring(str2.LastIndexOf("\\") + 1, str2.IndexOf(";") - str2.LastIndexOf("\\"));
            if (str2.EndsWith(";"))
            {
                str2 = str2.Remove(str2.Length - 5, 5);
            }
        }
        return str2;
    }
    protected void btnBackup_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg2.Text = "";
        string sFilename = string.Empty;
        string sPath = string.Empty;
        string destPath = string.Empty;
        string DBname = string.Empty;
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        DBname = GetCurrentDBName(sDataSource); // ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            sFilename = txtFilename.Text.Trim();
            sPath = txtFilePath.Text.Trim();
            sPath = sPath + "\\" + sFilename + ".mdb";
            destPath = sPath;

            System.IO.File.Copy(Server.MapPath("App_Data\\" + DBname + ".mdb"), destPath);
            lblMsg.Text = "Backup done at the folder " + destPath;
            txtFilename.Text = "";
            txtFilePath.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Problem in doing the backup please contact the administrator";
        }
    }

    protected void btnRestore_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg2.Text = "";
        string sFilename = string.Empty;
        string sPath = string.Empty;
        string destPath = string.Empty;
        string path= string.Empty;
        string curr = string.Empty;
        string DBname = string.Empty;
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        DBname = GetCurrentDBName(sDataSource); // ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            sFilename = txtFilenameR.Text.Trim();
            sPath = txtFilePathR.Text.Trim();
            path = sPath + "\\" + DBname + ".mdb";
            sPath = sPath + "\\" + sFilename + ".mdb";
            
            destPath = sPath;
            curr = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute +"_" + DateTime.Now.Second;
            System.IO.File.Copy(Server.MapPath("App_Data\\" + DBname + ".mdb"), Server.MapPath("Restore\\" + DBname + "_" + curr+  ".mdb"));
            if (File.Exists(path))
                File.Delete(path); 

            
            System.IO.File.Copy(destPath, path);

            if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
                File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb")); 

            System.IO.File.Copy(path, Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //System.IO.File.Move(path,destPath);
            lblMsg2.Text = "Restore Successfully done" + destPath;
            txtFilenameR.Text = "";
            txtFilePathR.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg2.Text = "Problem in doing the Restoring please contact the administrator";
            //if (File.Exists(Server.MapPath("Restore\\jandj" + curr + ".mdb"))) 
            //    File.Move(Server.MapPath("Restore\\jandj" + curr + ".mdb"), Server.MapPath("App_Data\\jandj.mdb"));
            //if(File.Exists(path))
            //    System.IO.File.Move(path,destPath);
        }
    }
}
