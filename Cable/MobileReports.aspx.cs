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

public partial class MobileReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            System.Text.StringBuilder cashScript = new System.Text.StringBuilder();
            cashScript.Append("window.open('CustomerReport.aspx");
            cashScript.Append(" ','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=700,width=800,left=10,top=10, scrollbars=yes');");
            lnkCustDetails.Attributes.Add("OnClick", cashScript.ToString());

            System.Text.StringBuilder ComparisonReport = new System.Text.StringBuilder();
            ComparisonReport.Append("window.open('MonthComparisonReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');");
            lnkMonthlyReport.Attributes.Add("OnClick", ComparisonReport.ToString());
        }
    }
    protected void lnkBtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MobileDefault.aspx");
    }
}
