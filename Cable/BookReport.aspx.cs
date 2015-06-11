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

public partial class BookReport : System.Web.UI.Page
{

    public double totalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        DataSet ds = new DataSet();
        ds = GenerateReport(startDate,endDate);
        gvReport.DataSource = ds;
        gvReport.DataBind();
    }
    private DataSet GenerateReport(DateTime startDate, DateTime endDate)
    {

        string query = string.Empty;
        
        query = "SELECT BookRef,BookName,StartEntry,NextEntry,EndEntry,BookStatus,Amount,DateCreated FROM  tblBook WHERE DateCreated >=#" + startDate.ToString("MM/dd/yyyy") + "# AND DateCreated <=#" + endDate.ToString("MM/dd/yyyy") + "# Order By DateCreated, BookRef Asc ";

        string connStr = string.Empty;
        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalAmount = totalAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }

        lblAmount.Text = totalAmount.ToString();

    }
}
