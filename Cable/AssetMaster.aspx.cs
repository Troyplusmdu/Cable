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

public partial class AssetMaster : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadAssetCat();
            BindAsset();
            sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString().ToString() ].ToString();
        }
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
            GrdViewAsset.Columns[4].Visible = false;
            GrdViewAsset.Columns[3].Visible = false;
        } 
    }
    private void BindAsset()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sCode = string.Empty;
        string sCat = string.Empty;
       
       
        if (drpSAssetCat.SelectedIndex != 0)
            sCat= drpSAssetCat.SelectedItem.Value;
        
        if (txtSAssetCode.Text.Trim()!=string.Empty)
            sCode = txtSAssetCode.Text.Trim();
        //if (txtDescrition.Text.Trim() != string.Empty)
        //    iAssetNo = txtDescrition.Text.Trim();


        DataSet ds = bl.SearchAssetMaster(sCat,sCode);
        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();
    }
    private void loadAssetCat()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
       

        ds = bl.ListAssetCat();

        drpSAssetCat.DataSource = ds;
        drpSAssetCat.DataBind();
        drpSAssetCat.DataTextField = "CategoryDescription";
        drpSAssetCat.DataValueField = "CategoryID";

        drpAssetCat.DataSource = ds;
        drpAssetCat.DataBind();
        drpAssetCat.DataTextField = "CategoryDescription";
        drpAssetCat.DataValueField = "CategoryID";


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAsset(); 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sCode = string.Empty;
        int catID = 0;
        string sDesc = string.Empty;

        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (Page.IsValid)
        {
            sCode = txtAssetCode.Text;
            catID  = Convert.ToInt32(drpAssetCat.SelectedItem.Value);
            sDesc = txtDescrition.Text.Trim();

           

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string assetNo = bl.InsertAssetMaster(sCode, sDesc, catID);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Code Saved Successfully -  " + sCode + "')", true);
            Reset();
            ResetSearch();
            BindAsset();
            pnlAsset.Visible = false;
            lnkBtnAdd.Visible = true;

        }
    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        pnlAsset.Visible = true;
        lnkBtnAdd.Visible = false;
        btnCancel.Enabled = true;
        MyAccordion.Visible = false;
        GrdViewAsset.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        pnlAsset.Visible = false;
        lnkBtnAdd.Visible = true;
        btnCancel.Enabled = false;
        MyAccordion.Visible = true;
        GrdViewAsset.Visible = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string sCode = string.Empty;
        int catID = 0;
        string sDesc = string.Empty;
        string oldCode = string.Empty;

        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (Page.IsValid)
        {
            sCode = txtAssetCode.Text;
            catID = Convert.ToInt32(drpAssetCat.SelectedItem.Value);
            sDesc = txtDescrition.Text.Trim();
            oldCode = hdAsset.Value;


            BusinessLogic bl = new BusinessLogic(sDataSource);
            string assetNo = bl.UpdateAssetMaster(oldCode, sCode, sDesc, catID);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Update Notification -  " + assetNo + "')", true);
            Reset();
            ResetSearch();
            BindAsset();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            pnlAsset.Visible = false;
            lnkBtnAdd.Visible = true;

        }
    }

    public void Reset()
    {
        drpAssetCat.SelectedIndex = 0;
        txtAssetCode.Text = "";
        txtDescrition.Text = "";
    }

    public void ResetSearch()
    {
        
        drpSAssetCat.SelectedIndex = 0;
        txtSAssetCode   .Text = "";
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

        drpAssetCat.ClearSelection();
        ListItem li2 = drpAssetCat.Items.FindByText(row.Cells[2].Text.Trim());
        if (li2 != null) li2.Selected = true;

        txtAssetCode.Text = row.Cells[0].Text;


        string assetCode = Convert.ToString(GrdViewAsset.SelectedDataKey.Value);
        hdAsset.Value = assetCode;
        txtDescrition.Text = row.Cells[1].Text;
        BindAsset();
        btnUpdate.Enabled = true;
        btnCancel.Enabled = true;
        btnSave.Enabled = false;
        pnlAsset.Visible = true;
        lnkBtnAdd.Visible = false;
    }
    protected void GrdViewAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        string assetCode = Convert.ToString(GrdViewAsset.DataKeys[e.RowIndex].Value);
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string asset = bl.DeleteAssetMaster(assetCode);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Delete Notification -  " + asset + "')", true);
        BindAsset();
        btnUpdate.Enabled = false;
        btnSave.Enabled = true;

    }
    
}
