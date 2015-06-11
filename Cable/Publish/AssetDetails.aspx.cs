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

public partial class AssetDetails : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadAssetCode();
            loadAssetArea();
            BindAsset();

        }
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
    }
    private void BindAsset()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sCode = string.Empty;
        string sStatus = string.Empty;
        int iAssetNo = 0;
        string sArea = string.Empty;
        if (drpSAssetCat.SelectedIndex != 0)
            sCode = drpSAssetCat.SelectedItem.Value;
        if (drpSAStatus.SelectedIndex != 0)
            sStatus = drpSAStatus.SelectedItem.Text;
        if (drpSAssetarea.SelectedIndex != 0)
            sArea = drpSAssetarea.SelectedItem.Text;
        if (txtSAssetno.Text != "")
            iAssetNo = Convert.ToInt32(txtSAssetno.Text.Trim());


        DataSet ds = bl.SearchAsset(iAssetNo, sCode, sArea.Replace("'", "''"), sStatus);
        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();
    }
    private void loadAssetCode()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAssetCode();

        drpAssetCode.DataSource = ds;
        drpAssetCode.DataBind();
        drpAssetCode.DataTextField = "AssetCode";
        drpAssetCode.DataValueField = "AssetCode";

        ds = bl.ListAssetCat();

        drpSAssetCat.DataSource = ds;
        drpSAssetCat.DataBind();
        drpSAssetCat.DataTextField = "CategoryDescription";
        drpSAssetCat.DataValueField = "CategoryID";


    }

    private void loadAssetArea()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAssetArea();
        drpSAssetarea.DataSource = ds;
        drpSAssetarea.DataBind();
        drpSAssetarea.DataTextField = "area";
        drpSAssetarea.DataValueField = "area";

        drpAssetArea.DataSource = ds;
        drpAssetArea.DataBind();
        drpAssetArea.DataTextField = "area";
        drpAssetArea.DataValueField = "area";

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sCode = string.Empty;
        string sStatus = string.Empty;
        string sLocation = string.Empty;
        string sArea = string.Empty;
        string sSerial = string.Empty;
        if (Page.IsValid)
        {
            sCode = drpAssetCode.SelectedItem.Text;
            sStatus = drpAssetStatus.SelectedItem.Text;
            sLocation = txtLocation.Text.Trim();
            sSerial = txtSerialNo.Text.Trim();

            if (drpAssetArea.SelectedIndex != 0)
                sArea = drpAssetArea.SelectedItem.Text.Replace("'", "''");

            BusinessLogic bl = new BusinessLogic(sDataSource);
            int assetNo = bl.InsertAssetDetails(sCode, sStatus, sLocation, sArea.Replace("'", "''"), sSerial, 0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Details Saved Successfully. New Asset No " + assetNo + "')", true);
            Reset();
            ResetSearch();
            BindAsset();
            pnlAsset.Visible = false;
            lnkBtnAdd.Visible = true;
            MyAccordion.Visible = true;
        }

    }
    //btnSearch_Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sCode = string.Empty;
        string sStatus = string.Empty;
        int iAssetNo = 0;
        string sArea = string.Empty;
        if (drpSAssetCat.SelectedIndex != 0)
            sCode = drpSAssetCat.SelectedItem.Value;
        if (drpSAStatus.SelectedIndex != 0)
            sStatus = drpSAStatus.SelectedItem.Text;
        if (drpSAssetarea.SelectedIndex != 0)
            sArea = drpSAssetarea.SelectedItem.Text;
        if (txtSAssetno.Text != "")
            iAssetNo = Convert.ToInt32(txtSAssetno.Text.Trim());

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.SearchAsset(iAssetNo, sCode, sArea.Replace("'", "''"), sStatus);

        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();


    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        MyAccordion.Visible = false;
        pnlAsset.Visible = true;
        lnkBtnAdd.Visible = false;
        btnCancel.Enabled = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        MyAccordion.Visible = true;
        pnlAsset.Visible = false;
        lnkBtnAdd.Visible = true;
        btnCancel.Enabled = false;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string sCode = string.Empty;
        string sStatus = string.Empty;
        string sLocation = string.Empty;
        string sArea = string.Empty;
        string sSerial = string.Empty;

        if (Page.IsValid)
        {
            sCode = drpAssetCode.SelectedItem.Text;
            sStatus = drpAssetStatus.SelectedItem.Text;
            sLocation = txtLocation.Text.Trim();
            sSerial = txtSerialNo.Text.Trim();

            if (drpAssetArea.SelectedIndex != 0)
                sArea = drpAssetArea.SelectedItem.Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int assetNo = Convert.ToInt32(hdAsset.Value);
            int output = bl.UpdateAssetDetails(sCode, sStatus, sLocation, sArea.Replace("'", "''"), assetNo, sSerial);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Details Updated Successfully. New Asset No " + assetNo + "')", true);
            Reset();
            BindAsset();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            ResetSearch();
            pnlAsset.Visible = false;
            lnkBtnAdd.Visible = true;
            MyAccordion.Visible = true;
        }
    }

    public void Reset()
    {
        drpAssetArea.SelectedIndex = 0;
        drpAssetCode.SelectedIndex = 0;
        drpAssetStatus.SelectedIndex = 0;
        txtLocation.Text = "";
        txtSAssetno.Text = "";
        txtSerialNo.Text = "";
    }

    public void ResetSearch()
    {
        drpSAssetCat.SelectedIndex = 0;
        drpSAssetarea.SelectedIndex = 0;
        drpSAStatus.SelectedIndex = 0;
        txtSAssetno.Text = "";
    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdViewAsset.PageIndex = ((DropDownList)sender).SelectedIndex;
        BindAsset();
    }

    protected void GrdViewAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdViewAsset.PageIndex = e.NewPageIndex;
        BindAsset();
    }
    protected void GrdViewAsset_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewAsset, e.Row, this);
        }
    }
    protected void GrdViewAsset_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GrdViewAsset.SelectedRow;
        drpAssetArea.ClearSelection();
        ListItem li = drpAssetArea.Items.FindByText(row.Cells[3].Text.Trim());
        if (li != null) li.Selected = true;

        drpAssetCode.ClearSelection();
        ListItem li2 = drpAssetCode.Items.FindByText(row.Cells[0].Text.Trim());
        if (li2 != null) li2.Selected = true;

        drpAssetStatus.ClearSelection();
        ListItem li3 = drpAssetStatus.Items.FindByText(row.Cells[1].Text.Trim());
        if (li3 != null) li3.Selected = true;


        int assetNo = Convert.ToInt32(GrdViewAsset.SelectedDataKey.Value);
        hdAsset.Value = Convert.ToString(assetNo);
        txtLocation.Text = row.Cells[2].Text;
        txtSerialNo.Text = row.Cells[5].Text;
        BindAsset();
        btnUpdate.Enabled = true;
        btnSave.Enabled = false;
        btnCancel.Enabled = true;
        pnlAsset.Visible = true;
        lnkBtnAdd.Visible = false;
        MyAccordion.Visible = false;

    }
    protected void GrdViewAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int assetno = (int)GrdViewAsset.DataKeys[e.RowIndex].Value;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        bl.DeleteAssetDetails(assetno);
        BindAsset();
        btnUpdate.Enabled = false;
        btnSave.Enabled = true;

    }

}
