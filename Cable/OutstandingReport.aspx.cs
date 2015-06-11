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


public partial class OutstandingReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (!IsPostBack)
        {
           
            hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["OutstandingFileName"].ToString();
            loadSundrys();
        }
        if (hdToDelete.Value == "BrowserClose")
        {
            deleteFile();
        }
    }
    private void loadSundrys()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor();
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();
        drpLedgerName.DataTextField = "GroupName";
        drpLedgerName.DataValueField = "GroupID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        
        int iGroupID = 0;

        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
        string sXmlPath = Server.MapPath(hdFilename.Value); 
        string sXmlNodeName = "Outstanding";
        string sGroupName = string.Empty;
        CustomerReportBL.ReportClass rptOutstandingReport;


        iGroupID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
        sGroupName = drpLedgerName.SelectedItem.Text; 

        rptOutstandingReport = new CustomerReportBL.ReportClass();
        rptOutstandingReport.generateOutStandingReport(iGroupID, sXmlNodeName, sDataSource, sXmlPath);

        Session["dataset"] = hdFilename.Value;
        Session["sundry"] = sGroupName;
        Session["Filename"] = hdFilename.Value;
        Response.Redirect("PrintOutstandingLedgerReport.aspx");
       
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
