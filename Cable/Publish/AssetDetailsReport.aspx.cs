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

public partial class AssetDetailsReport : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadAssetCode();
            loadAssetArea();
            //BindAsset();

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


        DataSet ds = bl.SearchAsset(iAssetNo, sCode, sArea.Replace("'","''"), sStatus);
        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();
    }
    private void loadAssetCode()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        //ds = bl.ListAssetCode();

        //drpAssetCode.DataSource = ds;
        //drpAssetCode.DataBind();
        //drpAssetCode.DataTextField = "AssetCode";
        //drpAssetCode.DataValueField = "AssetCode";

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

       

    }
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
        DataSet ds = bl.SearchAsset(iAssetNo, sCode, sArea, sStatus);

        GrdViewAsset.DataSource = ds;
        GrdViewAsset.DataBind();




        if (drpSAssetCat.SelectedIndex != 0)
        {
            lblCat.Text = drpSAssetCat.SelectedItem.Text;
            lblCat.Visible = true; 
        }
        else
            lblCat.Visible = false;

        if (txtSAssetno.Text.Trim() != string.Empty)
        {
            lblAssetNo.Text = txtSAssetno.Text;
            lblAssetNo.Visible = true; 
        }
        else
            lblAssetNo.Visible = false;

        if (drpSAssetarea.SelectedIndex != 0)
        {
            lblArea.Text = drpSAssetarea.SelectedItem.Text;
            lblArea.Visible = true; 
        }
        else
            lblArea.Visible = false;
        
        if (drpSAStatus.SelectedIndex != 0)
        {
            lblStatus.Text = drpSAStatus.SelectedItem.Text;
            lblStatus.Visible = true; 
        }
        else
            lblStatus.Visible = false;

        ResetSearch(); 
    }
    public void ResetSearch()
    {
        drpSAssetarea.SelectedIndex = 0;
        drpSAssetCat.SelectedIndex = 0;
        drpSAStatus.SelectedIndex = 0;
        txtSAssetno.Text = "";
    }
}
