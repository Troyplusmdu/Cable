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
using System.Text;
using System.IO;

public partial class FraudCheckReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        if (Session["Company"] != null)
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        }
        else
        {
            Response.Redirect("~/frm_Login.aspx");
        }
        srcArea.ConnectionString = connStr;

    }
    protected void lnkBtnSearchId_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = GenerateReport();
        gvReport.DataSource = ds;
        gvReport.DataBind();
    }
    private DataSet GenerateReport()
    {

        string area = string.Empty;
        string orderBy = string.Empty;
        string customer = string.Empty;
        string qrystring = string.Empty;
        string doorNo = string.Empty;

        if (ddArea.SelectedValue != "0")
        {
            area = ddArea.SelectedValue;
            area = area.Replace("'", "''");
        }
            customer = txtCustomer.Text.Trim();
            doorNo = txtDoorNo.Text.Trim();
        if (area != string.Empty)
            qrystring = qrystring + " AND area='" + area + "'";
        if(customer !=string.Empty)
            qrystring = qrystring + " AND name LIKE '" + customer + "%'";
        if (doorNo != string.Empty)
            qrystring = qrystring + " AND doorno LIKE '" + doorNo + "%'";
      
        string query = string.Empty;
        
        query = "Select area,code,name,doorno,address1,balance,category,effectdate from CustomerMaster Where category = 'DC' " + qrystring + " Order by area";
        string connStr = string.Empty;
        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }
}
