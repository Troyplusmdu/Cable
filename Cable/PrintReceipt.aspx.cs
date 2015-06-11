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

public partial class PrintReceipt : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            DataSet companyInfo = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            if (Session["Company"] != null)
            {
                companyInfo = bl.getCompanyInfo(Session["Company"].ToString());

                if (companyInfo != null)
                {
                    if (companyInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in companyInfo.Tables[0].Rows)
                        {
                            lblTNGST.Text = Convert.ToString(dr["TINno"]);
                            lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                            lblPhone.Text = Convert.ToString(dr["Phone"]);
                            lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                            lblAddress.Text = Convert.ToString(dr["Address"]);
                            lblCity.Text = Convert.ToString(dr["city"]);
                            lblPincode.Text = Convert.ToString(dr["Pincode"]);
                            lblState.Text = Convert.ToString(dr["state"]);
                            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            printPreview();
        }
    }
    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        int ID = 0;

        if (Request.QueryString["ID"] != null)
        {
            ID = int.Parse(Request.QueryString["ID"].ToString());
        }

        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetReceiptForId(Session["Company"].ToString(), ID);

        if (ds.Tables[0].Rows.Count == 1)
        {

            lblRefNo.Text = ": " + ds.Tables[0].Rows[0]["RefNo"].ToString();
            lblReceivedFrom.Text = ": " + ds.Tables[0].Rows[0]["LedgerName"].ToString();
            lblAmount.Text = ": " + ds.Tables[0].Rows[0]["Amount"].ToString();
            lblPayMode.Text = ": " + ds.Tables[0].Rows[0]["Paymode"].ToString();

            if (ds.Tables[0].Rows[0]["Paymode"].ToString().Trim() == "Cheque")
            {
                rowBank.Visible = true;
                rowCheque.Visible = true;
                lblBank.Text = ": " + ds.Tables[0].Rows[0]["Debi"].ToString();
                lblCheque.Text = ": " + ds.Tables[0].Rows[0]["ChequeNo"].ToString();
            }
            else
            {
                rowBank.Visible = false;
                rowCheque.Visible = false;
            }
            lblNarration.Text = ": " + ds.Tables[0].Rows[0]["Narration"].ToString();
            lblPayDate.Text = ": " + DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToString("dd/MM/yyyy");
        }

    }

}
