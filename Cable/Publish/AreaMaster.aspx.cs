using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;
using System.Text;

public partial class AreaMaster : System.Web.UI.Page
{
    string reportPath = @"c:\inetpub\vhosts\lathaconsultancy.com\httpdocs\Reports\AreaMaster_Report.txt";
    System.IO.StreamWriter logfile = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //    ReadRecords();
        if (Session["Company"] != null)
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");


        string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            lnkBtnAdd.Visible = false;
            grdAreaMaster.Columns[5].Visible = false;
        }
        
    }

    private void ReadRecords()
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        try
        {
            conn = new OleDbConnection( System.Configuration.ConfigurationManager.ConnectionStrings["MASTConnectionString"].ToString());
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT area, personIncharge, contactno, mobileno, within_network, areacount FROM AreaMaster", conn);
            reader = cmd.ExecuteReader();

            grdAreaMaster.DataSource = reader;
            grdAreaMaster.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (reader != null) reader.Close();
            if (conn != null) conn.Close();
        }
    }

    public void WriteToLog(string message)
    {
        if (!File.Exists(reportPath))
            logfile = File.CreateText(reportPath);
        else
            logfile = File.AppendText(reportPath);

        logfile.WriteLine("{0}", message);

        logfile.Flush();
        logfile.Close();

    }
    private string FormatString(string inputString)
    {
        StringBuilder str = new StringBuilder();

        if (inputString.Length < 16)
        {
            str.Append(inputString);

            for (int i = 0; i < 16 - inputString.Length; i++)
            {
                str.Append(" ");
            }
        }

        return str.ToString();

    }

    private void GenerateReport()
    {

        string connStr = string.Empty;
        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbDataAdapter da = new OleDbDataAdapter("Select * from AreaMaster", conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds != null)
        {

            if (File.Exists(reportPath))
                File.Delete(reportPath);

            StringBuilder title = new StringBuilder();
            WriteToLog(title.ToString());
            WriteToLog(title.ToString());

            title.Append("                              ");
            title.Append("Area Report");
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.Append("                              ");
            title.Append("--------------------");
            title.Append("                              ");
            title.Append("Date  :");
            title.Append(DateTime.Now.ToString("dd/MM/yyyy"));
            WriteToLog(title.ToString());

            StringBuilder header = new StringBuilder();
            WriteToLog(header.ToString());

            header.AppendFormat("{0}", "Sl");
            header.Append("   ");
            header.AppendFormat("{0}", "Area Name");
            header.Append("            ");
            header.AppendFormat("{0}", "Contact Person");
            header.Append("       ");
            header.AppendFormat("{0}", "Contact No");
            header.Append("       ");
            header.AppendFormat("{0}", "Mobile No");

            WriteToLog(header.ToString());

            header = new StringBuilder();

            header.AppendFormat("{0}", "---");
            header.Append("  ");
            header.AppendFormat("{0}", "------------------");
            header.Append("   ");
            header.AppendFormat("{0}", "------------------");
            header.Append("   ");
            header.AppendFormat("{0}", "--------------");
            header.Append("	");
            header.AppendFormat("{0}", "--------------");

            WriteToLog(header.ToString());
            int i = 0;
            WriteToLog("");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                i = i + 1;
                StringBuilder data = new StringBuilder();
                data.AppendFormat("{0}", FormatStringLarge(i.ToString(),5));
                data.AppendFormat("{0}", FormatStringLarge(dr["area"].ToString(),21));
                data.AppendFormat("{0}", FormatStringLarge(dr["personIncharge"].ToString(),21));
                data.AppendFormat("{0}", FormatString(dr["contactno"].ToString()));
                data.AppendFormat("{0}", FormatString(dr["mobileno"].ToString()));
                WriteToLog(data.ToString());
                WriteToLog("");
            }

            WriteToLog("");
            WriteToLog("");
            StringBuilder footer = new StringBuilder();
            footer.Append("                                ");
            footer.Append("END OF THE REPORT");
            WriteToLog(footer.ToString());

            try
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=AreaMaster_Report.txt");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.ContentType = "application/vnd.text";
                Response.TransmitFile(reportPath);
                Response.Flush();
                Response.Buffer = false;
                Response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }

    private string FormatStringLarge(string inputString, int len)
    {
        StringBuilder str = new StringBuilder();

        if (inputString.Length < len)
        {
            str.Append(inputString);

            for (int i = 0; i < len - inputString.Length; i++)
            {
                str.Append(" ");
            }
        }

        return str.ToString();

    }


    protected void grdAreaMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            //ReadRecords();
            PresentationUtils.SetPagerButtonStates(grdAreaMaster, e.Row, this);
        }

    }
    protected void grdAreaMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
            grdAreaMaster.ShowFooter = false;
        
    }
    protected void grdAreaMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        IDictionary data = e.NewValues;

        string area = ((TextBox)grdAreaMaster.Rows[e.RowIndex].FindControl("txtArea")).Text;
        string personIncharge = ((TextBox)grdAreaMaster.Rows[e.RowIndex].FindControl("txtPIncharge")).Text;
        string contactNo = ((TextBox)grdAreaMaster.Rows[e.RowIndex].FindControl("txtCntactNo")).Text;
        string mobileNo = ((TextBox)grdAreaMaster.Rows[e.RowIndex].FindControl("txtMobileNo")).Text;
        string withInNtr = ((CheckBox)grdAreaMaster.Rows[e.RowIndex].FindControl("chkWithinntwrk")).Checked.ToString();
        //string areaCount = ((TextBox)grdAreaMaster.Rows[e.RowIndex].FindControl("txtAreaCount")).Text;

        //int areaExists = ExecuteScalar("Select Count(*) from AreaMaster WHERE area = '" + area.Replace("'", "''") + "'");

        /*if (areaExists > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Area with this name already exists. Please try with different name.');", true);
            return;
        }*/

        string sql = "UPDATE AreaMaster SET Personincharge = '" + personIncharge
            + "', contactno = '" + contactNo + "', mobileno= '" + mobileNo + "' ,within_network=" +
            withInNtr + " WHERE area = '" + area.Replace("'","''") + "'";

        try
        {
            ExecuteNonQuery(sql);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            //Response.Write(sql);
        }
        //grdAreaMaster.EditIndex = -1;
        //ReadRecords();


    }

    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

    private int ExecuteScalar(string sql)
    {
        int countVal = 0;
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            countVal = (int)cmd.ExecuteScalar();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (conn != null) conn.Close();
        }

        return countVal;
    }

    protected void grdAreaMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        
    }
    protected void grdAreaMaster_PreRender(object sender, EventArgs e)
    {

    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        grdAreaMaster.FooterRow.Visible = true;
        lnkBtnAdd.Visible = false;
    }
    protected void grdAreaMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {

            string area = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooArea"))).Text;
            string personincharge = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooPIncharge"))).Text;
            string contactno = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooContactNo"))).Text;
            string mobileno = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFoomobileNo"))).Text;
            string withinnetwork = ((CheckBox)(this.grdAreaMaster.FooterRow.FindControl("chkFooWithinntwrk"))).Checked.ToString();
            //string areacount = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooAreaCount"))).Text;

            int areaExists = ExecuteScalar("Select Count(*) from AreaMaster WHERE area = '" + area.Replace("'", "''") + "'");

            if (areaExists > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Area with this name already exists. Please try with different name.');", true);
                return;
            }


            string sql = "INSERT INTO Areamaster(area, personIncharge, contactno, mobileno, within_network) VALUES( '" +
                area.Replace("'","''") + "','" + personincharge + "','" + contactno + "','" + mobileno + "'," + withinnetwork + " )";

            try
            {
                ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Response.Write(sql);
                grdAreaMaster.DataBind();
                lnkBtnAdd.Visible = true;
            }

        }
        else if (e.CommandName == "Cancel")
        {
            grdAreaMaster.FooterRow.Visible = false;
            lnkBtnAdd.Visible = true;

        }
        
    }
    protected void grdAreaMaster_DataBound(object sender, EventArgs e)
    {
        grdAreaMaster.ShowFooter = false;
    }

    protected void grdAreaMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //grdAreaMaster.EditIndex = e.NewEditIndex;
        //grdAreaMaster.DataBind();
        //ReadRecords();
    }
    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Cancel = true;
    }
    protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        //Console.WriteLine(((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooArea"))).Text);
        //e.Command.Parameters["area"].Value = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooArea"))).Text;
        //e.Command.Parameters["personIncharge"].Value = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooPIncharge"))).Text;
        //e.Command.Parameters["contactno"].Value = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooContactNo"))).Text;
        //e.Command.Parameters["mobileno"].Value = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFoomobileNo"))).Text;
        //e.Command.Parameters["within_network"].Value = ((CheckBox)(this.grdAreaMaster.FooterRow.FindControl("chkFooWithinntwrk"))).Checked.ToString();
        //e.Command.Parameters["areacount"].Value = ((TextBox)(this.grdAreaMaster.FooterRow.FindControl("txtFooAreaCount"))).Text;
        e.Cancel = true;
        
    }
    protected void lnkBtnDownload_Click(object sender, EventArgs e)
    {
        _UserControl_errordisplay errDisp = (_UserControl_errordisplay)this.Master.FindControl("errorDisplay");

        try
        {
            GenerateReport();
            errDisp.AddItem("Report Generated Successfully.", DisplayIcons.GreenTick, true);
        }
        catch (Exception ex)
        {
            errDisp.AddItem("Exception Occured : " + ex.Message + ex.StackTrace,DisplayIcons.Error,false);
        }
    }
}
