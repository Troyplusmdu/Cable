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
using System.Data.OleDb;
using System.Text;

public partial class MobileCustomerSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        }

        srcArea.ConnectionString = connStr;

        if (!Page.IsPostBack)
        {
            txtUserId.Focus();
            txtUserId.Attributes.Add("onKeyPress", " return clickButton(event,'" + lnkBtnSearchId.ClientID + "')");
            txtCode.Attributes.Add("onKeyPress", " return clickButton(event,'" + lnkBtnSearchId.ClientID + "')");
            CheckBox1.Checked = true;
            //ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + txtUserId.ClientID + "').focus();</script>");
            ddArea.DataBind();
            if (Page.Request.Cookies["User"] != null)
            {
                txtUserId.Text = Page.Request.Cookies["User"].Value;
                ddArea.SelectedValue = Page.Request.Cookies["Area"].Value;
                txtCode.Text = Page.Request.Cookies["Code"].Value;
                txtDoorNo.Text = Page.Request.Cookies["DoorNo"].Value;
                CheckBox1.Checked = Convert.ToBoolean(Page.Request.Cookies["ActiveCustomer"].Value);
                lnkBtnSearch_Click(this, null);
            }
        }
    }
    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        Page.Response.Cookies.Add(new HttpCookie("User", txtUserId.Text));
        Page.Response.Cookies.Add(new HttpCookie("Area", ddArea.Text));
        Page.Response.Cookies.Add(new HttpCookie("Code", txtCode.Text));
        Page.Response.Cookies.Add(new HttpCookie("DoorNo", txtDoorNo.Text));
        Page.Response.Cookies.Add(new HttpCookie("ActiveCustomer", CheckBox1.Checked.ToString()));
        BindGrid();
    }

    private void BindGrid()
    {
        FillUserGrid(GetCustomerForName(txtUserId.Text, txtCode.Text, ddArea.Text, CheckBox1.Checked, txtDoorNo.Text));

    }

    private void FillUserGrid(DataView dvuserData)
    {

        GrdViewCust.DataSource = dvuserData;
        GrdViewCust.DataBind();

        if (dvuserData.Count == 0)
        {
            errordisplay1.AddItem("No customer found for the Search Criteria.", DisplayIcons.Information, false);
        }

        lblTotal.Text = "Total Customers found : <b>" + dvuserData.Count.ToString() + "</b>";

    }

    private DataView GetCustomerForName(string Name, string Code, string Area, bool ActiveCustomer, string DoorNo)
    {
        Name = "%" + Name + "%";
        DoorNo = "%" + DoorNo + "%";
        //UserService userService = new UserService();
        CustomerData custData = new CustomerData();
        //custData = userService.GetListForLogonId(LogonId);
        custData = ReadRecords(Name, Code, Area, ActiveCustomer, DoorNo);

        DataView dvCustData = custData._CustomerData.DefaultView;
        return dvCustData;

    }

    protected void GrdViewCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerStates(GrdViewCust, e.Row, this);
        }
    }
    protected void GrdViewCust_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int intCurIndex = GrdViewCust.PageIndex;

        switch (e.CommandArgument.ToString())
        {
            case "First":
                GrdViewCust.PageIndex = 0;
                break;
            case "Prev":
                GrdViewCust.PageIndex = intCurIndex - 1;
                break;
            case "Next":
                GrdViewCust.PageIndex = intCurIndex + 1;
                break;
            case "Last":
                GrdViewCust.PageIndex = GrdViewCust.PageCount;
                break;
        }

        // popultate the gridview control
        BindGrid();

    }

    protected void GrdViewCust_DataBound(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
            GrdViewCust.Columns[9].Visible = false;
        else
            GrdViewCust.Columns[9].Visible = true;
    }

    private CustomerData ReadRecords(string Name, string Code, string Area, bool ActiveCustomer, string DoorNo)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            StringBuilder query = new StringBuilder();
            query.Append("Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed,area + name + Cstr(code) as areanamecode FROM CustomerMaster Where area <> '' ");

            if (Name != "")
            {
                query.AppendFormat(" And (name like '{0}')", Name);
            }

            if (DoorNo != "")
            {
                query.AppendFormat(" And (doorno like '{0}')", DoorNo);
            }

            if (Code != "")
            {
                query.AppendFormat(" And code={0} ", Code);
            }

            if (Area != "0")
            {
                query.AppendFormat(" And Area='{0}' ", Area.Replace("'", "''"));
            }

            if (ActiveCustomer == true)
            {
                query.Append(" And category <> 'DC' ");
            }
            else
            {
                query.Append(" And category = 'DC' ");
            }


            query.Append(" Order By name, code ");

            OleDbCommand cmd = new OleDbCommand(query.ToString(), conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data._CustomerData);
            return data;
            //reader = cmd.ExecuteReader();

            //datagrid.DataSource = reader;
            //datagrid.DataBind();
        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdViewCust.PageIndex = ((DropDownList)sender).SelectedIndex;
        BindGrid();
    }

    protected void DeleteCustomer(object sender, GridViewDeleteEventArgs e)
    {
        string autoid = GrdViewCust.DataKeys[e.RowIndex].Value.ToString();

        string code = GrdViewCust.Rows[e.RowIndex].Cells[1].Text;
        string area = GrdViewCust.Rows[e.RowIndex].Cells[2].Text.Replace("'", "''");
        StringBuilder sqlDel = new StringBuilder();

        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString());
        OleDbCommand dCmd = new OleDbCommand();
        OleDbTransaction transaction = null;

        try
        {
            using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString()))
            {
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    dCmd.CommandType = CommandType.Text;
                    dCmd.Connection = conn;
                    dCmd.Transaction = transaction;

                    sqlDel.Append("Insert Into DeletedCustomers(area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed) ");
                    sqlDel.AppendFormat(" Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed From CustomerMaster Where code={0} and area= '{1}' ", code, area);

                    dCmd.CommandText = sqlDel.ToString();

                    dCmd.ExecuteNonQuery();

                    dCmd.CommandText = "Delete from CustomerMaster Where Category = 'DC' and Code = " + code + " and area= '" + area + "'";

                    dCmd.ExecuteNonQuery();

                    dCmd.CommandText = "Update DeletedCustomers Set deletedDate = Format('" + DateTime.Now.ToString() + "', 'dd/mm/yyyy'), UserName='" + Page.User.Identity.Name.ToString() + "' Where Code = " + code + " and area= '" + area + "'";

                    dCmd.ExecuteNonQuery();

                    errordisplay1.AddItem("Record Deleted successfully.", DisplayIcons.GreenTick, false); ;

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    errordisplay1.AddItem("Exception while Deleting Customer : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                }

            }

        }
        catch (Exception ee)
        {
            errordisplay1.AddItem("Exception : " + ee.Message + ee.StackTrace, DisplayIcons.Error, false);
        }
        finally
        {
            dCmd.Dispose();
            conn.Close();
            conn.Dispose();
            BindGrid();
        }
    }


    protected void GrdViewCust_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GrdViewCust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string lblCat = ((HiddenField)e.Row.FindControl("lblCategory")).Value;

        //    if (lblCat == "DC")
        //    {
        //        ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
        //    }
        //}
    }

    protected void BtnGoBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("MobileDefault.aspx");
    }
}
