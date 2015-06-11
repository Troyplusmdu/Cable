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

public partial class LedgerInfo : System.Web.UI.Page
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
            GrdViewLedger.Columns[8].Visible = false;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridSource.SelectParameters.Add(new SessionParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

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
            GrdViewLedger.Visible = true;
            System.Threading.Thread.Sleep(2000);
            GrdViewLedger.DataBind();
        }
		else
		{
			if (e.Exception != null)
			{
				StringBuilder script = new StringBuilder();
				script.Append("alert('Ledger with this name already exists, Please try with a different name.');");

				if (e.Exception.InnerException != null)
				{
					if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
						ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
				}
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.InnerException.Message, true);
                }
			}

			lnkBtnAdd.Visible = true;
			frmViewAdd.Visible = false;
			GrdViewLedger.Visible = true;
			e.ExceptionHandled = true;
		}
    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewLedger.Visible = true;
            System.Threading.Thread.Sleep(2000);
            GrdViewLedger.DataBind();
        }
		else
		{
			if (e.Exception != null)
			{
				StringBuilder script = new StringBuilder();
				script.Append("alert('Product with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.InnerException.Message, true);
                }
			}

			lnkBtnAdd.Visible = true;
			frmViewAdd.Visible = false;
			GrdViewLedger.Visible = true;
			e.ExceptionHandled = true;
		}
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {

    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        frmViewAdd.ChangeMode(FormViewMode.Insert);
        frmViewAdd.Visible = true;
        
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            GrdViewLedger.Visible = false;
            lnkBtnAdd.Visible = false;
        }
    }
    protected void GrdViewLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            frmViewAdd.Visible = true;
            GrdViewLedger.Visible = false;
            lnkBtnAdd.Visible = false;
        }
    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
    }

    protected void GrdViewLedger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewLedger, e.Row, this);
        }

    }
    protected void GrdViewLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
		
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
            string DRORCR = ((HiddenField)e.Row.FindControl("DRORCR")).Value;
            double Debit = double.Parse(e.Row.Cells[5].Text);
            double Credit = double.Parse(e.Row.Cells[4].Text);
            double OpenBalance = double.Parse(((HiddenField)e.Row.FindControl("OpenBalance")).Value);

            if (DRORCR == "CR")
            {
                Credit = Credit + OpenBalance;
            }
            else if (DRORCR == "DR")
            {
                Debit = Debit + OpenBalance;
            }

            if (Debit > Credit)
            {
                ((Label)e.Row.FindControl("lblBalance")).Text = (Debit - Credit).ToString() + " DR";
            }
            else if (Credit > Debit)
            {
                ((Label)e.Row.FindControl("lblBalance")).Text = (Credit - Debit).ToString() + " CR";
            }
            else
            {
                ((Label)e.Row.FindControl("lblBalance")).Text = "0";
            }

            if ((e.Row.Cells[2].Text == "Service") || (e.Row.Cells[2].Text == "Cash in Hand") || (e.Row.Cells[2].Text == "Bank Accounts"))
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
            }

            if ((e.Row.Cells[0].Text == "Purchase A/c") || (e.Row.Cells[0].Text == "Sales A/c"))
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
            }
		}
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        frmViewAdd.Visible = false;
        lnkBtnAdd.Visible = true;
        GrdViewLedger.Visible = true;
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        GrdViewLedger.Visible = true;

    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
		
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")) != null)
            e.InputParameters["GroupID"] = ((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text != "")
            e.InputParameters["LedgerName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text != "")
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("ddCRDR")).SelectedValue == "CR")
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceCR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceDR"] = "0";
            }
        }
        else
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceDR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceCR"] = "0";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtContName")).Text != "")
            e.InputParameters["ContactName"] = ((TextBox)this.frmViewAdd.FindControl("txtContName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text != "")
            e.InputParameters["Phone"] = ((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text != "")
            e.InputParameters["Add1"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text != "")
            e.InputParameters["Add2"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text != "")
            e.InputParameters["Add3"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text;

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")) != null)
            e.InputParameters["GroupID"] = ((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text != "")
            e.InputParameters["LedgerName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text != "")
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("ddCRDR")).SelectedValue == "CR")
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceCR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceDR"] = "0";
            }
        }
        else
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceDR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceCR"] = "0";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtContName")).Text != "")
            e.InputParameters["ContactName"] = ((TextBox)this.frmViewAdd.FindControl("txtContName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text != "")
            e.InputParameters["Phone"] = ((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text != "")
            e.InputParameters["Add1"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text != "")
            e.InputParameters["Add2"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text != "")
            e.InputParameters["Add3"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text;

        e.InputParameters["LedgerID"] = GrdViewLedger.SelectedDataKey.Value;

        e.InputParameters["DRORCR"] = "NA";
        e.InputParameters["OpenBalance"] = "0";
        
        
    }
}
