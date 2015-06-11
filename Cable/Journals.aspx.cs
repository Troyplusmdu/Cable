﻿using System;
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

public partial class Journals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            //loadCreditorDebtor();

            if (Session["Company"] != null)
                hdDataSource.Value = Session["Company"].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");


        }
    }

    public void BindGrid()
    {

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/Login.aspx");


        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.ListJournal(txtRefno.Text.Trim(), txtNaration.Text.Trim(), txtDate.Text, sDataSource);

        if (ds != null)
        {
            GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
            GrdViewJournal.DataBind();
        }
        else
        {
            GrdViewJournal.EmptyDataText = "No journals found";
            GrdViewJournal.DataSource = null;
            GrdViewJournal.DataBind();

        }

    }
    protected void cmdPrint_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Session["JournalID"] = hdJournal.Value;
            Response.Redirect("PrintJournal.aspx");
        }
    }
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");


        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.ListJournal(txtRefno.Text.Trim(), txtNaration.Text.Trim(), txtDate.Text, sDataSource);

        if (ds != null)
        {
            GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
            GrdViewJournal.DataBind();
        }
        else
        {
            GrdViewJournal.EmptyDataText = "No journals found";
            GrdViewJournal.DataSource = null;
            GrdViewJournal.DataBind();

        }
    }

    protected void cmdListAll_Click(object sender, EventArgs e)
    {
        txtNaration.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtRefno.Text = string.Empty;

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");


        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListJournal(txtRefno.Text, txtNaration.Text, txtDate.Text, sDataSource);
        if (ds != null)
        {
            GrdViewJournal.PageIndex = 0;
            GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
            GrdViewJournal.DataBind();
        }
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
    protected void cmbCreditor_DataBound(object sender, EventArgs e)
    {
        AjaxControlToolkit.ComboBox ddl = (AjaxControlToolkit.ComboBox)sender;

        FormView frmV = (FormView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

            ddl.ClearSelection();

            ListItem li = ddl.Items.FindByValue(creditorID);
            if (li != null) li.Selected = true;

        }

    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbDebtor")) != null)
            e.InputParameters["DebitorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbDebtor")).SelectedValue;

        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbCreditor")) != null)
            e.InputParameters["CreditorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbCreditor")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text;

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = Session["Company"].ToString();

        e.InputParameters["VoucherType"] = "Journal";
        e.InputParameters["sPath"] = sDataSource;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbDebtor")) != null)
            e.InputParameters["DebitorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbDebtor")).SelectedValue;

        if (((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbCreditor")) != null)
            e.InputParameters["CreditorID"] = ((AjaxControlToolkit.ComboBox)this.frmViewAdd.FindControl("cmbCreditor")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);



        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text;

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = Session["Company"].ToString();

        e.InputParameters["sPath"] = sDataSource;
        e.InputParameters["VoucherType"] = "Journal";

        e.InputParameters["TransNo"] = GrdViewJournal.SelectedDataKey.Value;

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        frmViewAdd.Visible = false;
        lnkBtnAdd.Visible = true;
        //GrdViewJournal.Columns[8].Visible = true;
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
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
        if (this.frmViewAdd.FindControl("txtTransDate") != null)
            ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text = DateTime.Now.ToShortDateString();
    }
    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            if (this.frmViewAdd.FindControl("txtTransDate") != null)
                ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        //GrdViewJournal.Columns[8].Visible = true;

    }

    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }


    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
        txtNaration.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtRefno.Text = string.Empty;

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        BusinessLogic bll = new BusinessLogic();
        string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;

        if (!bll.IsValidDate(sDataSource, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
            return;
        }
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        //ds = bl.ListJournal("", "", "", sDataSource);
        //if (ds != null)
        //{
        //    GrdViewJournal.PageIndex = 0;
        //    GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
        //    GrdViewJournal.DataBind();
        //}
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        cmdPrint.Enabled = true;
        BindGrid();
        //BindGrid();

    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);

        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic();
        string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
        if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
            return;
        }

        lnkBtnAdd.Visible = true;
        BindGrid();


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
            BindGrid();
            //GrdViewJournal.DataBind();
            //Session["JournalID"] = hdJournal.Value;
            //Response.Redirect("PrintJournal.aspx");
            cmdPrint.Enabled = false;
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
            BindGrid();
            //Session["JournalID"] = hdJournal.Value;
            //Response.Redirect("PrintJournal.aspx");
            cmdPrint.Enabled = false;


        }
        else
        {
            e.ExceptionHandled = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
        }
    }

    protected void GrdViewJournal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewJournal, e.Row, this);
        }
        //hdJournal.Value = Convert.ToString(GrdViewJournal.SelectedDataKey.Value);   
    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

        GrdViewJournal.PageIndex = ((DropDownList)sender).SelectedIndex;
        BindGrid();

    }
    protected void GrdViewJournal_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow Row = GrdViewJournal.SelectedRow;
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic();
        string recondate = Row.Cells[1].Text;
        hdJournal.Value = Convert.ToString(GrdViewJournal.SelectedDataKey.Value);

        if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
            return;

        }
        cmdPrint.Enabled = true;
    }

    protected void GrdViewJournal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void GrdViewJournal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {


            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewPayment.Columns[8].Visible = false;
            lnkBtnAdd.Visible = false;
            cmdPrint.Enabled = true;
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;


        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GrdViewJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdViewJournal.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void GrdViewJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int TransNo = (int)GrdViewJournal.DataKeys[e.RowIndex].Value;

        string sDataSource = string.Empty;

        if (Session["Company"] != null)
            sDataSource = Session["Company"].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[sDataSource].ToString();
        BusinessLogic bl1 = new BusinessLogic();

        string recondate = GrdViewJournal.Rows[e.RowIndex].Cells[1].Text; ;

        if (!bl1.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to delete this record. Please contact Administrator.')", true);
            //frmViewAdd.Visible = true;
            //frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
            return;

        }
        BusinessLogic bl = new BusinessLogic(sDataSource);
        bl.DeleteJournal(TransNo, sDataSource);
        BindGrid();

    }
    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GrdViewJournal.SelectedDataKey != null)
            e.InputParameters["TransNo"] = GrdViewJournal.SelectedDataKey.Value;
        e.InputParameters["sPath"] = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BindGrid();
    }

}
