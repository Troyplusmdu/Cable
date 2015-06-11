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
using System.Xml;
using System.Text;
using System.Globalization;

public partial class CashDetailsReport : System.Web.UI.Page
{
    public string sDataSource ;
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            
              
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            if ((Page.Request.Cookies["StartDate"] != null) && (Page.Request.Cookies["EndDate"] != null) && (Page.Request.Cookies["Area"] != null) && (Page.Request.Cookies["CashType"] != null))
            {
                txtStartDate.Text = Page.Request.Cookies["StartDate"].Value;
                txtEndDate.Text = Page.Request.Cookies["EndDate"].Value;

                if(drpArea.Items.FindByText(Page.Request.Cookies["Area"].Value) != null)
                {
                    drpArea.Items.FindByText(Page.Request.Cookies["Area"].Value).Selected = true;
                }

                if (drpCashType.Items.FindByText(Page.Request.Cookies["CashType"].Value) != null)
                {
                    drpCashType.Items.FindByText(Page.Request.Cookies["CashType"].Value).Selected = true;
                }

            }


            hdFilename.Value = "Reports\\" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["CashDetailsFileName"].ToString();
            loadArea();
        }
        if (hdToDelete.Value == "BrowserClose")
        {
            deleteFile();
        }
    }

   public void loadArea()
   {
       string connStr = string.Empty;
       if (Session["Company"] != null)
       {
           connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
           sDataSource = connStr.Remove(0, 45);
       }
       else
       {
           deleteFile();
           Response.Redirect("~/frm_Login.aspx");
       }
       CustomerReportBL.ReportClass custReport = new CustomerReportBL.ReportClass();
       DataSet ds = custReport.ListArea(sDataSource);
       drpArea.DataSource = ds;
       drpArea.DataBind();
       drpArea.DataTextField = "Area";
       drpArea.DataValueField = "Area";

 

   }

    public DataSet GenerateDs()
    {
        string connStr = string.Empty;
        
        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            sDataSource = connStr.Remove(0, 45);
        }
        else
        {
            deleteFile();
            Response.Redirect("~/frm_Login.aspx");
        }
        deleteFile();
        string sArea = string.Empty;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //string sXmlPath = Server.MapPath(ConfigurationSettings.AppSettings["CashDetailsFileName"].ToString());
        string sXmlPath = Server.MapPath(hdFilename.Value);
        string sXmlNodeName = "CashDetails";
        string sCashType = drpCashType.SelectedItem.Value; 
        DateTime startDate, endDate;
        CustomerReportBL.ReportClass custReport;

        sArea = drpArea.SelectedItem.Text;
     
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        custReport = new CustomerReportBL.ReportClass();
        
        custReport.generateCashDetailsReport(sArea.Replace("'","''"), startDate, endDate, sXmlNodeName, sDataSource, sXmlPath,sCashType);
        DataSet ds = new DataSet();

        ds.ReadXml(sXmlPath, XmlReadMode.InferSchema);
        return ds;
    
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Page.Response.Cookies.Add(new HttpCookie("Area", drpArea.SelectedItem.Text));
            Page.Response.Cookies.Add(new HttpCookie("CashType", drpCashType.SelectedItem.Text));
            Page.Response.Cookies.Add(new HttpCookie("StartDate", txtStartDate.Text));
            Page.Response.Cookies.Add(new HttpCookie("EndDate", txtEndDate.Text));

            DataSet ds = GenerateDs();
            Session["dataset"] = hdFilename.Value;
            Session["startDate"] = txtStartDate.Text;
            Session["endDate"] = txtEndDate.Text;
            Session["Filename"] = hdFilename.Value;
            Session["CashType"] = drpCashType.SelectedItem.Text;  
            Response.Redirect("PrintCashDetails.aspx");
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
