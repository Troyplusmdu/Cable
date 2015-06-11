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
public partial class PrintDayReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        printPreview();
        if (hdToDelete.Value == "BrowserClose")
        {
            deleteFile();
        }
        if (!IsPostBack)
        {
            if (Session["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            //DataSet companyInfo = new DataSet();
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //if (Session["Company"] != null)
            //{
            //    companyInfo = bl.getCompanyInfo(Session["Company"].ToString());

            //    if (companyInfo != null)
            //    {
            //        if (companyInfo.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in companyInfo.Tables[0].Rows)
            //            {
            //                lblTNGST.Text = Convert.ToString(dr["TINno"]);
            //                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
            //                lblPhone.Text = Convert.ToString(dr["Phone"]);
            //                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

            //                lblAddress.Text = Convert.ToString(dr["Address"]);
            //                lblCity.Text = Convert.ToString(dr["city"]);
            //                lblPincode.Text = Convert.ToString(dr["Pincode"]);
            //                lblState.Text = Convert.ToString(dr["state"]);

            //            }
            //        }
            //    }
            //}
        }
    }
    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        if (Session["dataSet"] != null)
        {
            sFilename = Server.MapPath(Session["dataSet"].ToString());
            if (File.Exists(sFilename))
            {
                ds.ReadXml(sFilename, XmlReadMode.InferSchema);
                ViewState["filename"] = ds;
            }
            else
            {
                ds = (DataSet)ViewState["filename"];
            }
           
                gvLedger.DataSource = ds;
                gvLedger.DataBind();
            
        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }
        
        lblBillDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        deleteFile();
        if (Session["dataSet"] != null)
        {

            Response.Redirect("DayBookReport.aspx");

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
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
