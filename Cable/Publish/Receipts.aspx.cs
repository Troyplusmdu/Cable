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
using System.Text;

public partial class Receipts : System.Web.UI.Page
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
            GrdViewReceipt.Columns[8].Visible = false;
            GrdViewReceipt.Columns[9].Visible = false;
        } 
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridSource.SelectParameters.Add(new SessionParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            GrdViewReceipt.Columns[8].Visible = false;
            lnkBtnAdd.Visible = false;

            if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                frmViewAdd.Visible = true;
        }
    }
    protected void GrdViewReceipt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewReceipt, e.Row, this);
        }
    }
    protected void GrdViewReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridView = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            int cellIndex = -1;
            foreach (DataControlField field in gridView.Columns)
            {
                if (field.SortExpression == gridView.SortExpression)
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                }
                else if(field.SortExpression != "")
                {
                    e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
                }
                
            }

            if (cellIndex > -1)
            {
                //  this is a header row,
                //  set the sort style
                e.Row.Cells[cellIndex].CssClass =
                    gridView.SortDirection == SortDirection.Ascending
                    ? "sortascheaderstyle" : "sortdescheaderstyle";
            }
        }
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        frmViewAdd.ChangeMode(FormViewMode.Insert);
        frmViewAdd.Visible = true;

        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            lnkBtnAdd.Visible = false;
            frmViewAdd.Visible = true;
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        GrdViewReceipt.Columns[8].Visible = true;

    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);
    }

    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewReceipt.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Date is invalid. Please contact Administrator.');");

                if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                e.ExceptionHandled = true;
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
            }
        }

    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewReceipt.Columns[8].Visible = true;
            GrdViewReceipt.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Date is invalid. Please contact Administrator.');");

                if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                e.ExceptionHandled = true;
            }
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = true;
            ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = true;
            HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
            table.Attributes.Add("class", "AdvancedSearch");

        }
        else
        {
            ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = false;
            ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = false;
            HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
            table.Attributes.Add("class", "hidden");

        }
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        frmViewAdd.Visible = false;
        lnkBtnAdd.Visible = true;
        GrdViewReceipt.Columns[8].Visible = true;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")) != null)
            e.InputParameters["CreditorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["DebitorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
            e.InputParameters["ChequeNo"] = "";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["DebitorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }

            if (((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text != "")
                e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;

        }

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        e.InputParameters["VoucherType"] = "Receipt";

        e.InputParameters["TransNo"] = GrdViewReceipt.SelectedDataKey.Value;
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")) != null)
            e.InputParameters["CreditorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["DebitorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["DebitorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text != "")
            e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;

        e.InputParameters["VoucherType"] = "Receipt";

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        AjaxControlToolkit.ComboBox ddl = (AjaxControlToolkit.ComboBox)sender;

        FormView frmV = (FormView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string debtorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

            ddl.ClearSelection();

            ListItem li = ddl.Items.FindByValue(debtorID);
            if (li != null) li.Selected = true;

        }

    }

    protected void ddBanks_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        FormView frmV = (FormView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string creditorID = ((DataRowView)frmV.DataItem)["DebtorID"].ToString();

            ddl.ClearSelection();

            ListItem li = ddl.Items.FindByValue(creditorID);
            if (li != null) li.Selected = true;

        }

    }

    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList chk = (RadioButtonList)sender;

        if (chk.SelectedItem.Text == "Cheque")
        {
            Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
            test.Visible = true;
        }
        else
        {
            Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
            test.Visible = false;
        }

    }

    protected void chkPayTo_DataBound(object sender, EventArgs e)
    {
       

        RadioButtonList chk = (RadioButtonList)sender;

        FormView frmV = (FormView)chk.NamingContainer;

        if (frmV.DataItem != null)
        {
            string paymode = ((DataRowView)frmV.DataItem)["paymode"].ToString();
            chk.ClearSelection();

            ListItem li = chk.Items.FindByValue(paymode);
            if (li != null) li.Selected = true;

        }

        if (chk.SelectedItem != null)
        {
            if (chk.SelectedItem.Text == "Cheque")
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
                table.Attributes.Add("class", "AdvancedSearch");
            }
            else
            {
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
                table.Attributes.Add("class", "hidden");
            }
        }
        else
        {
            //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
            //test.Visible = false;
            HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
            table.Attributes.Add("class", "hidden");
        }

    }

    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            if (this.frmViewAdd.FindControl("txtTransDate") != null)
                ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = true;
            ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = true;
            HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
            table.Attributes.Add("class", "AdvancedSearch");

        }
        else
        {
            ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = false;
            ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = false;
            HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
            table.Attributes.Add("class", "hidden");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        rvSearch.Enabled = true;
        Page.Validate();

        if (Page.IsValid)
        {
            GrdViewReceipt.DataBind();
        }
    }
    protected void GrdViewReceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GrdViewReceipt.SelectedDataKey != null)
            e.InputParameters["TransNo"] = GrdViewReceipt.SelectedDataKey.Value;
    }

    protected void GrdViewReceipt_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            GrdViewReceipt.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('You are not allowed to delete the record. Please contact Adminstrator.');");

                if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                e.ExceptionHandled = true;
            }
        }
    }
}
