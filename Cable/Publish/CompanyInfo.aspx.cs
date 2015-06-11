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

public partial class CompanyInfo : System.Web.UI.Page
{

    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        if (!IsPostBack)
        {
            //GetSettingsInfo();
            GetCompanyInfo();
            //Label1.Text = Helper.GenerateUniqueIDForThisPC();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Session["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            string strCompany = string.Empty;
            string strAddress = string.Empty;
            string strCity = string.Empty;
            string strState = string.Empty;
            string strPincode = string.Empty;
            string strPhone = string.Empty;
            string strFax = string.Empty;
            string strEmail = string.Empty;
            string strTin = string.Empty;
            string strCST = string.Empty;
            string smsRequired = string.Empty;

            strCompany = txtCompanyName.Text.Trim();
            strAddress = txtAddress.Text.Trim();
            strCity = txtCity.Text.Trim();
            strState = txtState.Text.Trim();
            strPincode = txtPincode.Text.Trim();
            strPhone = txtPhone.Text.Trim();
            strFax = txtFAX.Text.Trim();
            strEmail = txtEmail.Text.Trim();
            strTin = txtTin.Text.Trim();
            strCST = txtCST.Text.Trim();
            //smsRequired = rdoSMSRqrd.SelectedValue;

            clsCompany clscmp = new clsCompany();
            clscmp.Company = strCompany;
            clscmp.Address = strAddress;
            clscmp.City = strCity;
            clscmp.State = strState;
            clscmp.Pincode = strPincode;
            clscmp.Phone = strPhone;
            clscmp.Fax = strFax;
            clscmp.Email = strEmail;
            clscmp.TIN = strTin;
            clscmp.CST = strCST;

            BusinessLogic bl = new BusinessLogic(sDataSource);

            //bl.InsertSettings(smsRequired); 

            int affectedRows = bl.InsertCompanyInfo(clscmp);

            if (affectedRows == 1)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Company & Configuration Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);

        }
    }



    public void GetSettingsInfo()
    {
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetSettings();
        ListItem liProd;
        ListItem liBill;
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["Key"].ToString() == "SMSREQ")
                        {
                            //if (dr["KeyValue"] != null)
                                //rdoSMSRqrd.SelectedValue = dr["KeyValue"].ToString();
                        }

                    }
                }
            }
        }
    }

    public void GetCompanyInfo()
    {
        if (Session["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.getCompanyDetails(sDataSource);
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtCompanyName.Text = Convert.ToString(dr["CompanyName"]);
                        txtAddress.Text = Convert.ToString(dr["Address"]); ;
                        txtCity.Text = Convert.ToString(dr["City"]); ;
                        txtState.Text = Convert.ToString(dr["State"]); ;
                        txtPincode.Text = Convert.ToString(dr["Pincode"]); ;
                        txtPhone.Text = Convert.ToString(dr["Phone"]); ;
                        txtFAX.Text = Convert.ToString(dr["Fax"]); ;
                        txtEmail.Text = Convert.ToString(dr["Email"]); ;
                        txtTin.Text = Convert.ToString(dr["Tinno"]); ;
                        txtCST.Text = Convert.ToString(dr["gstno"]); ;
                    }
                }
            }
        }
    }

}
