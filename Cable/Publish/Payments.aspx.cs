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

public partial class Payments : System.Web.UI.Page
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
            GrdViewPayment.Columns[9].Visible = false;
            GrdViewPayment.Columns[10].Visible = false;
        }  
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search =    (TextBox)txtSearch;
        GridSource.SelectParameters.Add(new SessionParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Page.Form.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            GrdViewPayment.Columns[9].Visible = false;
            lnkBtnAdd.Visible = false;

            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
               // Accordion1.SelectedIndex = 1;
        }
    }
    protected void GrdViewPayment_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewPayment, e.Row, this);
        }
    }
    protected void GrdViewPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        frmViewAdd.ChangeMode(FormViewMode.Insert);
        frmViewAdd.Visible = true;
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            lnkBtnAdd.Visible = false;
        }
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        GrdViewPayment.Columns[9].Visible = true;

    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        
    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);

        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bll = new BusinessLogic();
        string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
        if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.');", true);
            return;
        }
    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);

        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

        BusinessLogic bll = new BusinessLogic();
        string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
        if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.');", true);
            return;
        }

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
            System.Threading.Thread.Sleep(2000);
            GrdViewPayment.DataBind();
        }
        else
        {
            e.ExceptionHandled = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
        }

    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewPayment.Columns[9].Visible = true;
            System.Threading.Thread.Sleep(2000);
            GrdViewPayment.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {
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
        GrdViewPayment.Columns[9].Visible = true;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")) != null)
            e.InputParameters["DebitorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
            e.InputParameters["ChequeNo"] = "";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }

            if (((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text != "")
                e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;

        }
        if (((RadioButtonList)this.frmViewAdd.FindControl("rdExpense")).SelectedValue == "Capex")
        {
            e.InputParameters["ExpenseType"] = "Capex";
        }
        if (((RadioButtonList)this.frmViewAdd.FindControl("rdExpense")).SelectedValue == "Opex")
        {
            e.InputParameters["ExpenseType"] = "Opex";
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        e.InputParameters["VoucherType"] = "Payment";

        e.InputParameters["TransNo"] = GrdViewPayment.SelectedDataKey.Value;

    }
    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")) != null)
            e.InputParameters["DebitorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["PaymentMode"] = "Cash";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBank");
            
            if(bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["PaymentMode"] = "Cheque";
            }
        }
        if (((RadioButtonList)this.frmViewAdd.FindControl("rdExpense")).SelectedValue == "Capex")
        {
            e.InputParameters["ExpenseType"] = "Capex";
        }
        if (((RadioButtonList)this.frmViewAdd.FindControl("rdExpense")).SelectedValue == "Opex")
        {
            e.InputParameters["ExpenseType"] = "Opex";
        }
        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text != "")
            e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;

        e.InputParameters["VoucherType"] = "Payment";

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        AjaxControlToolkit.ComboBox ddl = (AjaxControlToolkit.ComboBox)sender;

        FormView frmV = (FormView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string debtorID = ((DataRowView)frmV.DataItem)["DebtorID"].ToString();

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
            string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

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
            table.Attributes.Add("class","hidden");
        }

    }


    protected void rdExpense_DataBound(object sender, EventArgs e)
    {
        RadioButtonList eType = (RadioButtonList)sender;

        FormView frmV = (FormView)eType.NamingContainer;

        if (frmV.DataItem != null)
        {
            string expType = ((DataRowView)frmV.DataItem)["ExpenseType"].ToString();
            eType.ClearSelection();

            ListItem li = eType.Items.FindByValue(expType);
            if (li != null) li.Selected = true;

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
            GrdViewPayment.DataBind();
        }
    }
    protected void GrdViewPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GrdViewPayment.SelectedDataKey != null)
            e.InputParameters["TransNo"] = GrdViewPayment.SelectedDataKey.Value;
    }
    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        
    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {
        
    }

    private bool CheckDate(DateTime dateTime)
    {
        BusinessLogic objBus = new BusinessLogic();
        return objBus.IsValidDate(Session["Connection"].ToString(), dateTime);
    }
    protected void GrdViewPayment_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            GrdViewPayment.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('You are not allowed to delete the record. Please contact Adminstrator');");

                if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                e.ExceptionHandled = true;
            }
        }   
    }
}
