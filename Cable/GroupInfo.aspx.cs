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

public partial class GroupInfo : System.Web.UI.Page
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
            lnkBtnAddGroup.Visible = false;
            grdViewGroup.Columns[2].Visible = false;
        }

    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        frmViewDetails.Visible = false;
        lnkBtnAddGroup.Visible = true;
        grdViewGroup.Columns[2].Visible = true;
    }
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        frmViewDetails.Visible = false;
        lnkBtnAddGroup.Visible = true;
        grdViewGroup.Columns[2].Visible = true;
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewDetails_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAddGroup.Visible = true;
            frmViewDetails.Visible = false;
            grdViewGroup.Columns[2].Visible = true;
            System.Threading.Thread.Sleep(1000);
            grdViewGroup.DataBind();
        }
        else
        {
            if (e.Exception != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Group with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                }
            }
            lnkBtnAddGroup.Visible = true;
            frmViewDetails.Visible = false;
            grdViewGroup.Columns[2].Visible = true;
            e.ExceptionHandled = true;
        }
    }
    protected void frmViewDetails_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }
    protected void frmViewDetails_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAddGroup.Visible = true;
            frmViewDetails.Visible = false;
            grdViewGroup.Columns[2].Visible = true;
            System.Threading.Thread.Sleep(1000);
            grdViewGroup.DataBind();
        }
        else
        {
            if (e.Exception != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Group with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                }
            }

            lnkBtnAddGroup.Visible = true;
            frmViewDetails.Visible = false;
            grdViewGroup.Columns[2].Visible = true;
            e.ExceptionHandled = true;
        }
    }
    protected void grdViewGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewDetails.Visible = true;
            frmViewDetails.ChangeMode(FormViewMode.Edit);
            frmViewDetails.DataBind();
            grdViewGroup.Columns[2].Visible = false;
            lnkBtnAddGroup.Visible = false;

        }
    }
    protected void grdGroup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(grdViewGroup, e.Row, this);
        }
    }
    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);
    }
    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewDetails.FindControl("ddHeading")) != null)
            e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("ddHeading")).SelectedValue;

        if (((TextBox)this.frmViewDetails.FindControl("txtGroupName")).Text != "")
            e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("txtGroupName")).Text;

        e.InputParameters["GroupID"] = grdViewGroup.SelectedDataKey.Value;        

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


    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewDetails.FindControl("ddIHeading")) != null)
            e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("ddIHeading")).SelectedValue;

        if (((TextBox)this.frmViewDetails.FindControl("txtIGroup")).Text != "")
            e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("txtIGroup")).Text;

        BusinessLogic objBus = new BusinessLogic();
        int nextSeq = (int)objBus.GetNextSequence(GetConnectionString(), "Select Max(GroupID) from tblGroups");

        e.InputParameters["GroupID"] = nextSeq + 1;
        e.InputParameters["Order"] = "0";
 
    }

    protected void lnkBtnAddGroup_Click(object sender, EventArgs e)
    {
        frmViewDetails.ChangeMode(FormViewMode.Insert);
        frmViewDetails.Visible = true;
        if (frmViewDetails.CurrentMode == FormViewMode.Insert)
        {
            grdViewGroup.Columns[2].Visible = false;
            lnkBtnAddGroup.Visible = false;
        }
    }
    protected void grdViewGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[0].Text == "Bank Accounts") || (e.Row.Cells[0].Text == "Sales A/c") ||
                (e.Row.Cells[0].Text == "Cash in Hand") || (e.Row.Cells[0].Text == "Sundry Creditors") ||
                (e.Row.Cells[0].Text == "Purchase A/c") || (e.Row.Cells[0].Text == "Sundry Debtors") ||
                (e.Row.Cells[0].Text == "Service"))
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                 //   e.Row.Cells[6].Enabled = false;
            }
        }
    }
}
