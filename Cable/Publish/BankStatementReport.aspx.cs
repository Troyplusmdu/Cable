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
using System.Xml;
using System.IO; 

public partial class BankStatementReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (!IsPostBack)
        {
            //AccessDataSource1.DataFile = sDataSource; 
            hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["BankStatementFileName"].ToString();
            loadBanks();
        }
        if (hdToDelete.Value == "BrowserClose")
        {
            deleteFile();
        }
    }
    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        drpBankName.DataSource = ds;
        drpBankName.DataBind();
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DateTime startDate, endDate;
        int iLedgerID = 0;
        
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
        string sXmlPath =  Server.MapPath(hdFilename.Value);
        string sXmlNodeName = "BankStatement";
       
               
           
        iLedgerID = Convert.ToInt32(drpBankName.SelectedItem.Value);
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        CustomerReportBL.ReportClass custReport = new CustomerReportBL.ReportClass();
        custReport.generateReportXML(iLedgerID, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);

        Session["dataset"] = hdFilename.Value;
        Session["startDate"] = txtStartDate.Text;
        Session["endDate"] = txtEndDate.Text;
        Session["Filename"] = hdFilename.Value;
        Response.Redirect("PrintBankStatement.aspx");
    
    }
    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath(delFilename)))
                File.Delete(Server.MapPath(delFilename));
        }
    }
    
    
   
}
