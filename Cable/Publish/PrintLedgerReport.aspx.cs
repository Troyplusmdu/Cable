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

public partial class PrintLedgerReport : System.Web.UI.Page
{
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
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
            DataSet companyInfo = new DataSet();
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
        if (Session["Ledger"] != null)
        {
            lblLedger.Text = Session["Ledger"].ToString();
        }
        lblBillDate.Text = DateTime.Now.ToShortDateString();
        deleteFile(); 
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        deleteFile();
        if (Session["dataSet"] != null)
        {

            Response.Redirect("LedgerReport.aspx");

        }
       
    }
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Double cbDr = 0.00;
        Double cbCr = 0.00;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            damt = damt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
            camt = camt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
            
            Label lblOpDr = e.Row.FindControl("obDr") as Label;
            if(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceDR")!=null)
            lblOpDr.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceDR"));
            Label lblOpCr = e.Row.FindControl("obCr") as Label;
            if (DataBinder.Eval(e.Row.DataItem, "OpeningBalanceCR")!=null)
            lblOpCr.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceCR"));
            if (lblOpDr.Text != "")
                lblOBDR.Text = lblOpDr.Text;
            else
                lblOBDR.Text = "0.00";
            
            if (lblOpCr.Text != "")
                lblOBCR.Text = lblOpCr.Text;
            else
                lblOBCR.Text = "0.00";

          

            lblDebitSum.Text = Convert.ToString(damt);
            lblCreditSum.Text = Convert.ToString(camt);
            dDiffamt = damt - camt;
            cDiffamt = camt - damt;

            if (dDiffamt >= 0)
            {
                lblDebitDiff.Text = Convert.ToString(dDiffamt);
                lblCreditDiff.Text = "0.00";
            }
            if (cDiffamt > 0)
            {
                lblDebitDiff.Text = "0.00";
                lblCreditDiff.Text = Convert.ToString(cDiffamt);

            }
            cbDr = Convert.ToDouble(lblOBDR.Text) + Convert.ToDouble(lblDebitDiff.Text);
            cbCr = Convert.ToDouble(lblOBCR.Text) + Convert.ToDouble(lblCreditDiff.Text);

            if (cbDr > cbCr)
            {
                cbDr = cbDr - cbCr;
                lblClosDr.Text = Convert.ToString(cbDr);
                lblClosCr.Text = "0.00";
            }
            else
            {
                cbCr = cbCr - cbDr;
                lblClosCr.Text = Convert.ToString(cbCr);
                lblClosDr.Text = "0.00"; 
            }
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
