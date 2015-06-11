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

public partial class AssetCategory : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
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
            GrdViewAsset.Columns[2].Visible = false;
            GrdViewAsset.Columns[3].Visible = false;
        } 

        if(!IsPostBack)
            BindAsset();
    }
    private void BindAsset()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        
        string sCat = string.Empty;



        sCat = txtSCat.Text.Trim(); ;

     


        DataSet ds = bl.SearchAssetCat(sCat);
        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAsset();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string sCat = string.Empty;

        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (Page.IsValid)
        {
            sCat = txtCat.Text.Trim() ;



            BusinessLogic bl = new BusinessLogic(sDataSource);
            string assetNo = bl.InsertAssetCat(sCat);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Code Saved Successfully -  " + sCat + "')", true);
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
        string sCat = string.Empty;

        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        if (Page.IsValid)
        {
            sCat = txtCat.Text.Trim();
            int catID = Convert.ToInt32(hdAsset.Value); 
            string sOld = hdCat.Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string assetNo = bl.UpdateAssetCat(sOld,sCat,catID);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Update Notification -  " + assetNo + "')", true);
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
        txtCat.Text = "";
    }

    public void ResetSearch()
    {

        txtSCat.Text = "";
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



        txtCat.Text = row.Cells[1].Text;

        hdCat.Value = row.Cells[1].Text; 
        string catCode = Convert.ToString(GrdViewAsset.SelectedDataKey.Value);
        hdAsset.Value = catCode;
       
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
        int catCode = Convert.ToInt32(GrdViewAsset.DataKeys[e.RowIndex].Value);
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string asset = bl.DeleteAssetCat(catCode);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Asset Category Delete Notification -  " + asset + "')", true);
        BindAsset();
        btnUpdate.Enabled = false;
        btnSave.Enabled = true;

    }
    
}
