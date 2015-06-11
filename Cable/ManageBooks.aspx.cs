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

public partial class ManageBooks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");

        string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            GrdViewBook.Columns[6].Visible = false;
            lnkBtnAdd.Visible = false;
        }

        if(!Page.IsPostBack)
        {
            chkStatus.Checked = true;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        //GridSource.SelectParameters.Add(new SessionParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
        GridSource.SelectParameters.Add(new ControlParameter("ActiveBooks", TypeCode.String, chkStatus.UniqueID, "Checked"));

        //GridSource.SelectParameters["txtSearch"].DefaultValue = search.Text;
        //GridSource.SelectParameters["dropdown"].DefaultValue = ddCriteria.SelectedValue;
        //GrdViewBook.DataBind();
    }

    protected void GrdViewBook_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewBook, e.Row, this);
        }

    }



    protected void GrdViewBook_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            GrdViewBook.Columns[6].Visible = false;
            lnkBtnAdd.Visible = false;
            MyAccordion.Visible = false;
            GrdViewBook.Visible = false;
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;
        }
    }
    protected void GrdViewBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "Closed")
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                e.Row.Cells[7].Enabled = false;
            }
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
            GrdViewBook.Columns[6].Visible = true;
            System.Threading.Thread.Sleep(2000);
            GrdViewBook.DataBind();
            MyAccordion.Visible = true;
            GrdViewBook.Visible = true;
        }
        else
        {
            ScriptManager manager = (ScriptManager)this.Master.FindControl("ScriptManager1");
            Response.Write(manager.AsyncPostBackErrorMessage);
            errorDisplay.AddItem("Exception : " + e.Exception.Message + e.Exception.Source, DisplayIcons.Error, false);
            return;
        }
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewBook.Columns[6].Visible = true;
            GrdViewBook.DataSourceID = GridSource.UniqueID;
            System.Threading.Thread.Sleep(1000);  
            GrdViewBook.DataBind();
            MyAccordion.Visible = true;
            GrdViewBook.Visible = true;
        }
        else
        {
            errorDisplay.AddItem("Exception : " + e.Exception.Message + e.Exception.Source, DisplayIcons.Error, false);
        }
    }
    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {

    }
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        frmViewAdd.Visible = false;
        lnkBtnAdd.Visible = true;
        GrdViewBook.Columns[6].Visible = true;
        MyAccordion.Visible = true;
        GrdViewBook.Visible = true;
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        GrdViewBook.Columns[6].Visible = true;
        MyAccordion.Visible = true;
        GrdViewBook.Visible = true;
    }
    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        frmViewAdd.ChangeMode(FormViewMode.Insert);
        frmViewAdd.Visible = true;
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            GrdViewBook.Columns[6].Visible = false;
            lnkBtnAdd.Visible = false;
            MyAccordion.Visible = false;
            GrdViewBook.Visible = false;
        }
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["BookRef"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtBookName")).Text != "")
            e.InputParameters["BookName"] = ((TextBox)this.frmViewAdd.FindControl("txtBookName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtStartEntry")).Text != "")
            e.InputParameters["StartEntry"] = ((TextBox)this.frmViewAdd.FindControl("txtStartEntry")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtEndEntry")).Text != "")
            e.InputParameters["EndEntry"] = ((TextBox)this.frmViewAdd.FindControl("txtEndEntry")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtDateCreated")).Text != "")
            e.InputParameters["dateCreated"] = DateTime.Parse( ((TextBox)this.frmViewAdd.FindControl("txtDateCreated")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("ddCashCollected")) != null)
            e.InputParameters["FlagCollected"] = ((DropDownList)this.frmViewAdd.FindControl("ddCashCollected")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("ddStatus")) != null)
            e.InputParameters["BookStatus"] = ((DropDownList)this.frmViewAdd.FindControl("ddStatus")).SelectedValue;

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["BookRef"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtBookName")).Text != "")
            e.InputParameters["BookName"] = ((TextBox)this.frmViewAdd.FindControl("txtBookName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtStartEntry")).Text != "")
            e.InputParameters["StartEntry"] = ((TextBox)this.frmViewAdd.FindControl("txtStartEntry")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtEndEntry")).Text != "")
            e.InputParameters["EndEntry"] = ((TextBox)this.frmViewAdd.FindControl("txtEndEntry")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        e.InputParameters["BookID"] = GrdViewBook.SelectedDataKey.Value;

        if (((TextBox)this.frmViewAdd.FindControl("txtDateCreated")).Text != "")
            e.InputParameters["dateCreated"] = DateTime.Parse( ((TextBox)this.frmViewAdd.FindControl("txtDateCreated")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("ddCashCollected")) != null)
            e.InputParameters["FlagCollected"] = ((DropDownList)this.frmViewAdd.FindControl("ddCashCollected")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("ddStatus")) != null)
            e.InputParameters["BookStatus"] = ((DropDownList)this.frmViewAdd.FindControl("ddStatus")).SelectedValue;

    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
    }

    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        {
            BusinessLogic objBus = new BusinessLogic();
            int IsRecordExists = objBus.GetRecord(Session["Company"].ToString(), "Select Count(*) From CashDetails Where BookID =" + GrdViewBook.SelectedDataKey.Value);

            if (IsRecordExists > 0)
            {
                ((TextBox)this.frmViewAdd.FindControl("txtStartEntry")).Enabled = false;
                ((TextBox)this.frmViewAdd.FindControl("txtEndEntry")).Enabled = false;
            }
        }
    }
    protected void frmViewAdd_DataBinding(object sender, EventArgs e)
    {


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void ddCashCollected_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        FormView frmV = (FormView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string Flag = ((DataRowView)frmV.DataItem)["FlagCollected"].ToString();

            if (Flag == "Y")
            {
                ((DropDownList)(frmV.FindControl("ddStatus"))).Enabled = false;
            }

        }
    }
}
