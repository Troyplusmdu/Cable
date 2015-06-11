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

public partial class OutstandingBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (!Page.IsPostBack)
        {
            hdFilename.Value = "Reports\\" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["OutstandingBalanceFileName"].ToString();
            if (hdToDelete.Value == "BrowserClose")
            {
                deleteFile();
            }
            if (Session["Company"] != null)
            {
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            }
            else
            {
                deleteFile();
                Response.Redirect("~/frm_Login.aspx");
            }
            srcArea.ConnectionString = connStr;

            drpMonth.ClearSelection();
            ListItem liMonth = drpMonth.Items.FindByText(Convert.ToString(DateTime.Now.Month));
            if (liMonth != null) liMonth.Selected = true;
            ListItem liYear = drpYear.Items.FindByText(Convert.ToString(DateTime.Now.Year));
            if (liYear != null) liYear.Selected = true;
        }
    }

    public DataSet GenerateDs()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        }
        else
        {
            deleteFile();
            Response.Redirect("~/frm_Login.aspx");
        }
        string sArea = string.Empty;
        string sDataSource = connStr.Remove(0, 45);
        string sXmlPath = Server.MapPath(hdFilename.Value);  //Server.MapPath(ConfigurationSettings.AppSettings["OutstandingBalanceFileName"].ToString());
        string sXmlNodeName = "Outstanding";
        CustomerReportBL.ReportClass outsReport;
        int month = 0;
        int year = 0;
        month = Convert.ToInt32(drpMonth.SelectedItem.Text);
        year = Convert.ToInt32(drpYear.SelectedItem.Text);
        Session["month"] = month;
        Session["year"] = year;
        sArea = drpArea.SelectedItem.Text;
        outsReport = new CustomerReportBL.ReportClass();
        //outsReport.generateOutstandingReportmodified(sArea.Replace("'", "''"), month, year, sXmlNodeName, sDataSource, sXmlPath);

        DataSet ds = new DataSet();

        ds = outsReport.generateOutstandingReportmodified(sArea.Replace("'", "''"), month, year, sDataSource);
        //Session["dataSet"] = sXmlPath;
        //Session["OutStandingFilename"] = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OutstandingBalanceFileName"].ToString());
        //ds.ReadXml(sXmlPath, XmlReadMode.InferSchema);
        return ds;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        int year = Convert.ToInt32(drpYear.SelectedItem.Text);
        int month = Convert.ToInt32(drpMonth.SelectedItem.Text);

        //if (year <= DateTime.Now.Year)
        if(true)
        {
            //if (month <= DateTime.Now.Month)
            if(true)
            {
                DataSet ds = GenerateDs();
                //Session["dataset"] = System.Configuration.ConfigurationManager.AppSettings["OutstandingBalanceFileName"].ToString();
                Session["OutStandingRptData"] = ds;
                Response.Redirect("PrintOutstandingReport.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the Year less than or month to current month')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the Year less than or equal to current year')", true);
        }
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
