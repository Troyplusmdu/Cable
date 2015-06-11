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


public partial class AdjustmentDetailsReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        if (!Page.IsPostBack)
        {
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            srcArea.ConnectionString = connStr;
        }
    }
   

    public DataSet GenerateDs()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string sArea = string.Empty;
        string sDataSource = connStr.Remove(0, 45);
        string sXmlPath = Server.MapPath("Reports\\" + ConfigurationSettings.AppSettings["AdjustmentDetailsFileName"].ToString());
        string sXmlNodeName = "AdjustmentDetails";

        DateTime startDate, endDate;
        CustomerReportBL.ReportClass custReport;

        sArea = drpArea.SelectedItem.Text;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        custReport = new CustomerReportBL.ReportClass();
        custReport.generateAdjustmentDetailsReport(sArea, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("Reports\\" + System.Configuration.ConfigurationManager.AppSettings["AdjustmentDetailsFileName"].ToString()), XmlReadMode.InferSchema);
        return ds;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DataSet ds = GenerateDs();
        Session["dataset"] = System.Configuration.ConfigurationManager.AppSettings["AdjustmentDetailsFileName"].ToString();
        Session["startDate"] = txtStartDate.Text;
        Session["endDate"] = txtEndDate.Text;
        Response.Redirect("PrintAdjustmentReport.aspx");
    }
}
