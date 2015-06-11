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

public partial class NewDBCreation : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        //SetDbName();
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
    protected void btnAccount_Click(object sender, EventArgs e)
    {
       
            string fileName = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["OutstandingFileName"].ToString();
            string sXmlPath = Server.MapPath(fileName);
            string path = string.Empty;
            string curr = string.Empty;
            string monthname = string.Empty;
            string DBcompany = string.Empty;
            string DBname = string.Empty;
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            DBname = GetCurrentDBName(sDataSource)  ;//ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            if (Session["Company"] != null)
                DBcompany = Session["Company"].ToString();
            else
                Response.Redirect("~/Login.aspx");

            if (DateTime.Now.Month == 12)
                monthname = "December";
            else if (DateTime.Now.Month == 11)
                monthname = "November";
            else if (DateTime.Now.Month == 10)
                monthname = "October";
            else if (DateTime.Now.Month == 9)
                monthname = "September";
            else if (DateTime.Now.Month == 8)
                monthname = "August";
            else if (DateTime.Now.Month == 7)
                monthname = "July";
            else if (DateTime.Now.Month == 6)
                monthname = "June";
            else if (DateTime.Now.Month == 5)
                monthname = "May";
            else if (DateTime.Now.Month == 4)
                monthname = "April";
            else if (DateTime.Now.Month == 3)
                monthname = "March";
            else if (DateTime.Now.Month == 2)
                monthname = "February";
            else
                monthname = "January";

            curr = DateTime.Now.Year + "_" + monthname;
            //path = Server.MapPath("OldYear\\"+ DBname + "_curr.mdb");

            path = Server.MapPath("OldYear\\" + DBname + "_" + curr + ".mdb"); 
            if (File.Exists(path))
                File.Delete(path);
            File.Copy(Server.MapPath("App_Data\\" + DBname + ".mdb"), path);
            bool success =  bl.CreateNewAccount(sDataSource,fileName,sXmlPath);
            if (success)
                lblMsg.Text = "New Account is created successfully";
            else
            {
                if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
                {
                    File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb"));
                    System.IO.File.Move(path, Server.MapPath("App_Data\\" + DBname + ".mdb"));
                }
                if (File.Exists(path))
                    System.IO.File.Delete(path);
                lblMsg.Text = "Problem in creating the Account Please Contact the Administrator";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Problem in creating the Account Please Contact the Administrator : Exception";
            //if (File.Exists(path))
            //{
            //    File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //    System.IO.File.Move(path, Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //}
            //if (File.Exists(path))
            //    System.IO.File.Delete(path);
        }
    }
}
