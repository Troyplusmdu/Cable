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

public partial class PrintJournal : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet companyInfo = new DataSet();
        int ID = 0;
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
                        lblBillDate.Text = DateTime.Now.ToShortDateString();   
                    }
                }
            }
        }
        if (Request.QueryString["ID"] != null)
        {
            ID = int.Parse(Request.QueryString["ID"].ToString());
       
        //if (Session["JournalID"] != null)
        //{
            lblBillno.Text = ID.ToString();
            lblBillDate.Text  = DateTime.Now.ToShortDateString();
            DataSet ds = bl.GetJournalForId(ID, Session["Company"].ToString());
            GrdViewJournal.DataSource = ds;
            GrdViewJournal.DataBind();
 }
        //}
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Journals.aspx");

    }
}
