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
using System.Xml;
using System.IO;
using System.Globalization;
using AjaxControlToolkit;


public partial class Purchase : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    private Double amtTotal = 0;
    public Decimal rateTotal = 0;
    public Decimal vatTotal = 0;
    public Decimal disTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["Company"] = "SMS";
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        Button1.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", Button1.UniqueID, "");
        if (!IsPostBack)
        {
            BindGrid("0");

            hdFilename.Value = System.Guid.NewGuid().ToString();
            loadSupplier();
            loadProducts();
            //rvCheque.Enabled = true;
            //rvbank.Enabled = true;
            loadBanks();
            loadAssetArea();
        }
        Session["Filename"] = "Reports//" + hdFilename.Value + "_Product.xml";
        //if (hdDel.Value == "BrowserClose" && delFlag.Value=="0")
        //{
        //    deleteFile();
        //}
        err.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
    }
    protected void btnSerial_Click(object sender, EventArgs e)
    {
        lstSerial.Items.Add(txtSerialNo.Text);
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= lstSerial.Items.Count - 1; i++)
        {
            if (lstSerial.Items[i].Selected)
                lstSerial.Items.Remove(lstSerial.Items[i].Text);
        }
    }
    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        cmbBankName.DataSource = ds;
        cmbBankName.DataBind();
        cmbBankName.DataTextField = "LedgerName";
        cmbBankName.DataValueField = "LedgerID";

    }
    private void loadAssetArea()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAssetArea();


        drpAssetArea.DataSource = ds;
        drpAssetArea.DataBind();
        drpAssetArea.DataTextField = "area";
        drpAssetArea.DataValueField = "area";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        String strBillno = string.Empty;

        strBillno = txtBillnoSrc.Text.Trim();
        Accordion1.SelectedIndex = 0;
        BindGrid(strBillno);
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
        //lblTotalSum.Text = "0";
        purchasePanel.Visible = false;
        PanelBill.Visible = true;
        PanelCmd.Visible = false;
        delFlag.Value = "0";
        lnkBtnAdd.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        ResetProduct();
        if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
            File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
        lnkBtnAdd.Visible = true;
        cmdDelete.Enabled = false;
        cmdUpdate.Enabled = false;
        purchasePanel.Visible = false;
        PanelCmd.Visible = false;
        Accordion1.Visible = true;
        PanelBill.Visible = true;
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iPurchaseId = 0;
            string connection = Session["Company"].ToString();
            BusinessLogic bll = new BusinessLogic();
            string recondate = txtBillDate.Text.Trim(); ;
            string salesReturn = string.Empty;
            string srReason = string.Empty;

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }
            string sBillno = string.Empty;

            int iSupplier = 0;
            int iPaymode = 0;
            string[] sDate;
            string sChequeno = string.Empty;
            int iBank = 0;
            int iPurchase = 0;
            string filename = string.Empty;
            double dTotalAmt = 0;
            iPurchase = Convert.ToInt32(hdPurchase.Value);
            sBillno = txtBillno.Text.Trim();
            DateTime sBilldate;
            string delim = "/";
            char[] delimA = delim.ToCharArray();
            CultureInfo culture = new CultureInfo("pt-BR");
            salesReturn = drpSalesReturn.SelectedItem.Text;
            srReason = txtSRReason.Text.Trim();
            try
            {
                sDate = txtBillDate.Text.Trim().Split(delimA);


                sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

            }
            catch (Exception ex)
            {
                Response.Write("<b><font face=verdana size=2 color=red>Invalid Bill Date Format</font></b>");
                return;
            }
            iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);
            iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);
            //if (lblTotalSum.Text != string.Empty || lblTotalSum.Text != "0")
            //    dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
            if (iPaymode == 2)
            {
                sChequeno = Convert.ToString(txtChequeNo.Text);
                iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
                rvCheque.Enabled = true;
                rvbank.Enabled = true;
            }
            else
            {
                rvbank.Enabled = false;
                rvCheque.Enabled = false;
            }
            filename = hdFilename.Value;
            dTotalAmt = Convert.ToDouble(txtAmount.Text);
            BindProduct();
            DataSet ds = (DataSet)GrdViewItems.DataSource;

            if (ds != null)
            {

                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);

                int cntB = bl.isDuplicateBill(sBillno, iSupplier);
                if (cntB > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Bill Number')", true);
                    return;
                }



                iPurchaseId = bl.InsertPurchase(sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, ds);
                Reset();
                ResetProduct();
                if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));


                //PanelBill.Visible = true;
                //purchasePanel.Visible = false;
                //lnkBtnAdd.Visible = true;

                //PanelCmd.Visible = false;
                //hdMode.Value = "Edit";
                //cmdPrint.Enabled = false;
                //Session["purchaseID"] = iPurchaseId.ToString();
                ////deleteFile();
                purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;
                PanelBill.Visible = false;
                PanelCmd.Visible = false;
                hdMode.Value = "Edit";
                cmdPrint.Enabled = false;
                Session["purchaseID"] = iPurchaseId.ToString();
                deleteFile();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Saved Successfully. Updated Bill No is " + iPurchaseId.ToString() + "')", true);
                Session["SalesReturn"] = salesReturn;
                BindGrid("0");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Saved Successfully. Updated Bill No is " + iPurchaseId.ToString() + "')", true);
                //Response.Redirect("PrintBill.aspx");   

                lstSerial.Items.Clear();
            }
            delFlag.Value = "0";
            //lnkBtnAdd.Visible = true;
            Accordion1.SelectedIndex = 0;
            btnCancel.Enabled = false;

        }
    }
    //cmdSave_Click
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iPurchaseId = 0;
            string connection = Session["Company"].ToString();
            BusinessLogic bll = new BusinessLogic();
            string recondate = txtBillDate.Text.Trim();
            string salesReturn = string.Empty;
            string srReason = string.Empty;
            salesReturn = drpSalesReturn.SelectedItem.Text;
            srReason = txtSRReason.Text.Trim();
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }
            string sBillno = string.Empty;

            int iSupplier = 0;
            int iPaymode = 0;
            string sChequeno = string.Empty;
            int iBank = 0;
            int iPurchase = 0;
            string filename = string.Empty;
            double dTotalAmt = 0;
            iPurchase = Convert.ToInt32(hdPurchase.Value);
            sBillno = txtBillno.Text.Trim();

            DateTime sBilldate;
            string[] sDate;
            string delim = "/";
            char[] delimA = delim.ToCharArray();
            CultureInfo culture = new CultureInfo("pt-BR");
            dTotalAmt = Convert.ToDouble(txtAmount.Text);
            try
            {
                sDate = txtBillDate.Text.Trim().Split(delimA);


                sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

            }
            catch (Exception ex)
            {
                Response.Write("<b><font face=verdana size=2 color=red>Invalid Bill Date Format</font></b>");
                return;
            }
            iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);
            iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);
            //dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
            if (iPaymode == 2)
            {
                sChequeno = Convert.ToString(txtChequeNo.Text);
                iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
            }
            filename = hdFilename.Value;
            BindProduct();
            DataSet ds = (DataSet)GrdViewItems.DataSource;

            if (ds != null)
            {
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

                BusinessLogic bl = new BusinessLogic(sDataSource);
                //int cntB = bl.isDuplicateBill(sBillno, iSupplier);
                //if (cntB > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Bill Number')", true);
                //    return;
                //}
                //iPurchaseId =bl.UpdatePurchase(iPurchase, sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, ds);
                iPurchaseId = bl.UpdatePurchase(iPurchase, sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, ds);
                Reset();
                ResetProduct();
                if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;
                //PanelBill.Visible = false;
                PanelCmd.Visible = false;
                hdMode.Value = "Edit";
                cmdPrint.Enabled = false;
                BindGrid("0");
            }
            delFlag.Value = "0";
            deleteFile();
            Accordion1.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Updated Successfully. Updated Bill No is " + iPurchaseId.ToString() + "')", true);
            Session["purchaseID"] = iPurchaseId.ToString();
            deleteFile();
            btnCancel.Enabled = false;
            Session["SalesReturn"] = salesReturn;
            //Response.Redirect("PrintBill.aspx");   
        }
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string connection = Session["Company"].ToString();
            BusinessLogic bll = new BusinessLogic();
            string recondate = txtBillDate.Text.Trim(); ;
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }
            int iPurchase = 0;
            string sBillNo = txtBillno.Text.Trim();
            iPurchase = Convert.ToInt32(hdPurchase.Value);
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeletePurchase(iPurchase, sBillNo);
            Reset();
            ResetProduct();
            if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
            purchasePanel.Visible = false;
            lnkBtnAdd.Visible = true;
            PanelBill.Visible = false;
            PanelCmd.Visible = false;
            hdMode.Value = "Delete";
            cmdPrint.Enabled = false;
            delFlag.Value = "0";
            deleteFile();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Deleted Successfully.  Bill No is " + sBillNo.ToString() + "')", true);
            BindGrid("0");
            btnCancel.Enabled = false;
        }
    }



    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        
        PanelCmd.Visible = true;
        purchasePanel.Visible = true;
        cmdSave.Enabled = true;
        cmdUpdate.Enabled = false;
        cmdDelete.Enabled = false;
        //AccordionPane2.Visible = true;
        lnkBtnAdd.Visible = false;
        hdMode.Value = "New";
        Reset();
        //lblTotalSum.Text = "0";
        ResetProduct();
        txtBillDate.Text = DateTime.Now.ToShortDateString();
        XmlDocument xDoc = new XmlDocument();

        if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
        {
            File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        }
        ViewState["xmlDs"] = null;
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
        btnCancel.Enabled = true;
        Accordion1.Visible = false;
        PanelBill.Visible = false;
    }
    private void Reset()
    {
        txtBillno.Text = "";
        txtBillDate.Text = "";
        txtAmount.Text = "";

        foreach (Control control in cmbSupplier.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }

        //cmbSupplier.SelectedItem.Text = "";
        cmbSupplier.ClearSelection();

        foreach (Control control in cmdPaymode.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmdPaymode.ClearSelection();

        foreach (Control control in cmbBankName.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbBankName.ClearSelection();

        foreach (Control control in drpAssetCode.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        drpAssetCode.ClearSelection();
        txtChequeNo.Text = ""; // cmbBankName.SelectedItem.Text;
        //BindGrid(txtBillnoSrc.Text);
        //Accordion1.SelectedIndex = 1;
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
    }
    private void loadSupplier()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor(sDataSource);
        cmbSupplier.DataSource = ds;
        cmbSupplier.DataBind();
        cmbSupplier.DataTextField = "LedgerName";
        cmbSupplier.DataValueField = "LedgerID";

    }
    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAssetCode();
        drpAssetCode.DataSource = ds;
        drpAssetCode.DataBind();

        drpAssetCode.DataTextField = "AssetCode";
        drpAssetCode.DataValueField = "AssetCode";


    }
    private bool paymodeVisible(string paymode)
    {
        if (paymode.ToUpper() != "BANK")
        {
            //lblCheque.Visible = false;
            //lblBankname.Visible = false;
            //cmbBankName.Visible = false;
            //txtChequeNo.Visible = false;
            //rvbank.Enabled = false;
            //rvCheque.Enabled = false;
            pnlBank.Visible = false;
            return false;
        }
        else
        {
            //lblCheque.Visible = true;
            //lblBankname.Visible = true;
            //cmbBankName.Visible = true;
            //txtChequeNo.Visible = true;
            //rvbank.Enabled = true;
            //rvCheque.Enabled = true;
            pnlBank.Visible = true;
            return true;
        }
    }

    protected void GrdViewPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdViewPurchase.PageIndex = e.NewPageIndex;
        String strBillno = string.Empty;
        if (txtBillnoSrc.Text.Trim() != "")
            strBillno = txtBillnoSrc.Text.Trim();
        else
            strBillno = "0";
        BindGrid(strBillno);
    }
    private void BindGrid(string strBillno)
    {


        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        if (strBillno == "0")
            ds = bl.GetPurchase();
        else
            ds = bl.GetPurchaseForId(strBillno);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewPurchase.DataSource = ds.Tables[0].DefaultView;
                GrdViewPurchase.DataBind();
            }
        }
    }
    protected void GrdViewPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string paymode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymode"));
            Label payMode = (Label)e.Row.FindControl("lblPaymode");
            if (paymode == "1")
                payMode.Text = "Cash";
            else if (paymode == "2")
                payMode.Text = "Bank";
            else
                payMode.Text = "Credit";
        }
    }




    protected void GrdViewPurchase_SelectedIndexChanged(object sender, EventArgs e)
    {

        string strPaymode = string.Empty;
        int SupplierID = 0;
        int purchaseID = 0;
        GridViewRow row = GrdViewPurchase.SelectedRow;
        string connection = Session["Company"].ToString();
        BusinessLogic bl = new BusinessLogic();
        string recondate = row.Cells[2].Text;
        if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
            return;
        }
        if (row.Cells[1].Text != "&nbsp;")
            txtBillno.Text = row.Cells[1].Text;
        purchaseID = Convert.ToInt32(GrdViewPurchase.SelectedDataKey.Value);

        if (row.Cells[4].Text != "&nbsp;")
        {
            SupplierID = Convert.ToInt32(row.Cells[4].Text);
            cmbSupplier.ClearSelection();
            ListItem li = cmbSupplier.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(SupplierID.ToString()));
            if (li != null) li.Selected = true;
        }

        Label pM = (Label)row.FindControl("lblPaymode"); //row.Cells[3].Text;
        strPaymode = pM.Text;
        if (paymodeVisible(strPaymode))
        {
            if (row.Cells[5].Text != "&nbsp;")
                txtChequeNo.Text = row.Cells[5].Text;
            if (row.Cells[6].Text != "&nbsp;")
            {
                cmbBankName.ClearSelection();
                ListItem cli = cmbBankName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(row.Cells[6].Text));
                if (cli != null) cli.Selected = true;
            }

        }
        cmdPaymode.ClearSelection();
        ListItem pLi = cmdPaymode.Items.FindByText(strPaymode.Trim());
        if (pLi != null) pLi.Selected = true;
        if (row.Cells[2].Text != "&nbsp;")
            txtBillDate.Text = Convert.ToDateTime(row.Cells[2].Text).ToString("dd/MM/yyyy");
        if (row.Cells[9].Text != "&nbsp;")
            txtSRReason.Text = row.Cells[9].Text;
        if (row.Cells[7].Text != "&nbsp;")
            txtAmount.Text = row.Cells[7].Text;
        if (row.Cells[8].Text != "&nbsp;")
        {
            drpSalesReturn.ClearSelection();
            drpSalesReturn.SelectedItem.Text = row.Cells[8].Text;
        }
        else
        {
            drpSalesReturn.SelectedIndex = 0;
        }
        if (txtBillnoSrc.Text == "")
            BindGrid("0");
        else
            BindGrid(txtBillnoSrc.Text);
        Accordion1.SelectedIndex = 1;

        hdPurchase.Value = purchaseID.ToString();
        formXml();
        BindProduct();
        calcSum();
        hdMode.Value = "Edit";
        lnkBtnAdd.Visible = false;
        purchasePanel.Visible = true;
        PanelCmd.Visible = true;
        cmdSave.Enabled = false;
        cmdUpdate.Enabled = true;
        cmdDelete.Enabled = true;
        cmdPrint.Enabled = true;
        ResetProduct();
        Accordion1.Visible = false;
        PanelBill.Visible = false;

    }

    private void formXml()
    {
        int purchaseID = 0;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        purchaseID = Convert.ToInt32(hdPurchase.Value);
        DataSet ds = new DataSet();
        string sSerial = string.Empty;
        string[] arInfo;
        char[] splitter = { ',' };


        ds = bl.GetPurchaseItemsForId(purchaseID);
        if (ds != null)
        {
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;

            string filename = string.Empty;
            DataSet childDs = new DataSet();
            string dupItem = string.Empty;
            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.Formatting = Formatting.Indented;
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement("Purchase");

            if (ds.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("Product");
                //reportXMLWriter.WriteElementString("AssetNo", String.Empty);
                reportXMLWriter.WriteElementString("PurchaseID", String.Empty);
                reportXMLWriter.WriteElementString("AssetCode", String.Empty);
                reportXMLWriter.WriteElementString("AssetDesc", String.Empty);
                reportXMLWriter.WriteElementString("CategoryDescription", String.Empty);
                reportXMLWriter.WriteElementString("AssetLocation", String.Empty);
                reportXMLWriter.WriteElementString("AssetArea", String.Empty);
                reportXMLWriter.WriteElementString("AssetStatus", String.Empty);
                reportXMLWriter.WriteElementString("Qty", "0");

                reportXMLWriter.WriteElementString("SerialNo", String.Empty);
                reportXMLWriter.WriteEndElement();

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (dupItem != Convert.ToString(dr["itemCode"]))
                    {
                        reportXMLWriter.WriteStartElement("Product");
                        //reportXMLWriter.WriteElementString("AssetNo", Convert.ToString(dr["AssetNo"]));
                        reportXMLWriter.WriteElementString("PurchaseID", Convert.ToString(dr["PurchaseID"]));
                        reportXMLWriter.WriteElementString("AssetCode", Convert.ToString(dr["itemCode"]));
                        reportXMLWriter.WriteElementString("AssetDesc", Convert.ToString(dr["AssetDesc"]));
                        reportXMLWriter.WriteElementString("CategoryDescription", Convert.ToString(dr["CategoryDescription"]));
                        reportXMLWriter.WriteElementString("AssetLocation", Convert.ToString(dr["AssetLocation"]));
                        reportXMLWriter.WriteElementString("AssetArea", Convert.ToString(dr["AssetArea"]));
                        reportXMLWriter.WriteElementString("AssetStatus", "New");

                        dupItem = Convert.ToString(dr["itemCode"]);

                        reportXMLWriter.WriteElementString("Qty", Convert.ToString(dr["Qty"]));

                        foreach (DataRow sr in ds.Tables[0].Rows)
                        {
                            sSerial = sSerial + sr["SerialNo"].ToString() + ",";


                        }
                        sSerial = sSerial.Remove(sSerial.Length - 1, 1);
                        reportXMLWriter.WriteElementString("SerialNo", sSerial);
                        reportXMLWriter.WriteEndElement();
                    }
                }
            }
            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            filename = hdFilename.Value;
            xmlDoc.Save(Server.MapPath("Reports\\" + filename + "_Product.xml"));
            Session["PurchaseFileName"] = Server.MapPath("Reports\\" + filename + "_Product.xml");
        }

    }
    private void BindProduct()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath("Reports\\" + filename + "_Product.xml")))
        {
            xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Product.xml"));
            if (xmlDs != null)
            {
                if (xmlDs.Tables.Count > 0)
                {
                    //GrdViewItems.DataSource = xmlDs;
                    //GrdViewItems.DataBind();
                    ViewState["xmlDs"] = xmlDs;
                    if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                    {

                        deleteFile();
                        if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                            File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                    }

                }
                else
                {
                    GrdViewItems.DataSource = null;
                    GrdViewItems.DataBind();
                }
            }

        }

        if (ViewState["xmlDs"] != null)
        {
            GrdViewItems.DataSource = ViewState["xmlDs"];
            GrdViewItems.DataBind();
            calcSum();
        }


    }
    private void BindProductP()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath("Reports\\" + filename + "_Product.xml")))
        {
            xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Product.xml"));
            if (xmlDs != null)
            {
                if (xmlDs.Tables.Count > 0)
                {
                    GrdViewItems.DataSource = xmlDs;
                    GrdViewItems.DataBind();
                    calcSum();

                }
                else
                {
                    GrdViewItems.DataSource = null;
                    GrdViewItems.DataBind();
                }
            }
            //File.Delete(Server.MapPath(filename + "_Product.xml")); 
        }

    }
    private void calcSum()
    {
        //Double sumAmt = 0;
        //Double sumVat = 0;
        //Double sumDis = 0;
        //DataSet ds = new DataSet();
        ////ds.ReadXml(Server.MapPath("Reports\\" + hdFilename.Value + "_product.xml"));

        //ds = (DataSet)GrdViewItems.DataSource;
        //if (ds != null)
        //{
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            if (dr["Total"] != null)
        //                sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDecimal(dr["Qty"]), Convert.ToDecimal(dr["PurchaseRate"]), Convert.ToDecimal(dr["Discount"]), Convert.ToDecimal(dr["VAT"])));
        //            sumDis = sumDis + Convert.ToDouble(dr["Discount"]);
        //            sumVat = sumVat + Convert.ToDouble(dr["VAT"]);
        //        }
        //    }
        //}
        //lblTotalSum.Text = sumAmt.ToString("#0.00");
        //lblTotalDis.Text = sumDis.ToString("#0.00");
        //lblTotalVAT.Text = sumVat.ToString("#0.00"); 
    }


    protected void cmdPrint_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Session["purchaseID"] = hdPurchase.Value;
            delFlag.Value = "0";
            deleteFile();
            Response.Redirect("PrintBill.aspx");
        }
    }
    protected void GrdViewPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {



    }
    protected void GrdViewPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void BtnViewDetails_Click(object sender, EventArgs e)
    {

    }
    public string GetTotal(Decimal qty, Decimal rate, Decimal discount, Decimal VAT)
    {

        Decimal tot = 0;
        //tot = (qty * rate) - ((qty * rate) * (discount / 100)) + ((qty * rate) * (VAT / 100));
        //amtTotal = amtTotal + Convert.ToDouble(tot);
        //disTotal = disTotal + discount; 
        //rateTotal = rateTotal + rate;
        //vatTotal = vatTotal + VAT;
        //hdTotalAmt.Value = amtTotal.ToString("#0.00");
        ////lblGrantTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }
    public Double GetSum()
    {
        return amtTotal;// Convert.ToDouble(hdTotalAmt.Value);
    }
    public Decimal GetDis()
    {
        return disTotal;
    }
    public Decimal GetRate()
    {
        return rateTotal;
    }
    public Decimal GetVat()
    {
        return vatTotal;
    }





    protected void GrdViewItems_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GrdViewItems.EditIndex = e.NewEditIndex;
        //for(int i;i<=GrdViewItems.Columns.Count-1;i++)
        //{
        //    if (i < 4)
        //        GrdViewItems.Columns[i].ReadOnly = true;
        //}
        BindProduct();
    }
    protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdViewItems.EditIndex = -1;
        BindProduct();
    }
    protected void drpSalesReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpSalesReturn.SelectedItem.Text == "No")
        {
            rqSalesReturn.Enabled = false;
        }
        else
        {
            rqSalesReturn.Enabled = true;
        }
    }

    protected void cmdPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmdPaymode.SelectedIndex == 2)
        {
            //lblCheque.Visible = true;
            //txtChequeNo.Visible = true;
            //lblBankname.Visible = true;
            //cmbBankName.Visible = true;
            //rvCheque.Enabled = true;
            //rvbank.Enabled = true;
            pnlBank.Visible = true;
        }
        else
        {
            //lblCheque.Visible = false;
            //txtChequeNo.Visible = false;
            //lblBankname.Visible = false;
            //cmbBankName.Visible = false;
            //rvCheque.Enabled = false;
            //rvbank.Enabled = false;
            pnlBank.Visible = false;
        }


    }
    protected void drpAssetCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        if (drpAssetCode.SelectedIndex != 0)
        {
            ds = bl.ListAssetDetailsPurchase(drpAssetCode.SelectedItem.Value);
            if (ds != null)
            {
                lblAssetDesc.Text = Convert.ToString(ds.Tables[0].Rows[0]["AssetDesc"]);
                lblAssetCat.Text = Convert.ToString(ds.Tables[0].Rows[0]["CategoryDescription"]);


            }
            else
            {
                lblAssetDesc.Text = "";
                lblAssetCat.Text = "";
                err.Text = "No Description for the Asset";
            }
        }
        else
        {
            err.Text = "Select the Asset";
        }
        delFlag.Value = "0";
    }
    protected void cmdSaveProduct_Click(object sender, EventArgs e)
    {
        string connection = Session["Company"].ToString();
        string sSerial = string.Empty;


        int isQty = Convert.ToInt32(txtQtyAdd.Text);
        if (isQty != lstSerial.Items.Count)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Serial Number is not matching with Asset Qty')", true);
            return;
        }

        for (int i = 0; i <= lstSerial.Items.Count - 1; i++)
        {
            sSerial = sSerial + lstSerial.Items[i].Text + ",";
        }

        sSerial = sSerial.Remove(sSerial.Length - 1, 1);


        BusinessLogic bll = new BusinessLogic();
        string recondate = txtBillDate.Text.Trim(); ;
        if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
            return;
        }





        bool dupFlag = false;
        int iQty = 0;
        string itemCode = string.Empty;
        BindProduct();
        if (drpAssetCode.SelectedItem.Value == "0")
        {
            return;
        }
        DataSet ds = (DataSet)GrdViewItems.DataSource;
        DataRow dr;
        DataColumn dc;

        if (txtQtyAdd.Text.Trim() != "")
            iQty = Convert.ToInt32(txtQtyAdd.Text.Trim());
        if (ds == null)
        {
            //if (stock >= iQty)
            //{
            ds = new DataSet();
            DataTable dt = new DataTable();
            //DataTable st = new DataTable();    
            DataRow drNew = dt.NewRow();
            dc = new DataColumn("AssetNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("AssetCode");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseID");
            dt.Columns.Add(dc);

            dc = new DataColumn("AssetDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("CategoryDescription");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("AssetLocation");
            dt.Columns.Add(dc);

            dc = new DataColumn("AssetArea");
            dt.Columns.Add(dc);
            dc = new DataColumn("AssetStatus");
            dt.Columns.Add(dc);


            //string sSerial = string.Empty;
            //string[] arInfo;
            //char[] splitter = { ',' };

            //sSerial = dr["SerialNo"].ToString();
            //arInfo = sSerial.Split(splitter);


            //DataRow drnew2 = st.NewRow(); 
            dc = new DataColumn("SerialNo");
            dt.Columns.Add(dc);





            drNew["AssetCode"] = drpAssetCode.SelectedItem.Text;
            drNew["PurchaseID"] = hdPurchase.Value;
            drNew["AssetDesc"] = lblAssetDesc.Text;
            drNew["CategoryDescription"] = lblAssetCat.Text;
            drNew["Qty"] = txtQtyAdd.Text.Trim();
            drNew["AssetLocation"] = txtLocation.Text.Trim();
            drNew["AssetArea"] = drpAssetArea.SelectedItem.Text;
            drNew["AssetStatus"] = "New";
            drNew["SerialNo"] = sSerial;
            //drnew2["SerialNo"]=

            ds.Tables.Add(dt);
            ds.Tables[0].Rows.Add(drNew);
            ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
            BindProduct();
            ResetProduct();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Only " + stock + "')", true);
            //    //BindProduct();
            //    //ResetProduct();
            //}
        }
        else
        {
            itemCode = drpAssetCode.SelectedItem.Value;
            foreach (DataRow dR in ds.Tables[0].Rows)
            {
                if (dR["AssetCode"].ToString() == itemCode)
                {
                    dupFlag = true;
                    break;
                }
            }
            if (!dupFlag)
            {
                //if (stock >= iQty)
                //{
                dr = ds.Tables[0].NewRow();
                dr["AssetCode"] = drpAssetCode.SelectedItem.Text;
                dr["PurchaseID"] = hdPurchase.Value;
                dr["AssetDesc"] = lblAssetDesc.Text;
                dr["CategoryDescription"] = lblAssetCat.Text;
                dr["Qty"] = txtQtyAdd.Text.Trim();
                dr["AssetLocation"] = txtLocation.Text.Trim();
                dr["AssetArea"] = drpAssetArea.SelectedItem.Text;
                dr["AssetStatus"] = "New";
                dr["SerialNo"] = sSerial;

                ds.Tables[0].Rows.Add(dr);
                ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));

                BindProduct();
                ResetProduct();
                delFlag.Value = "0";
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Only " + stock + "')", true);
                //    //BindProduct();
                //    //ResetProduct();
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
            }
            lstSerial.Items.Clear();
        }

    }
    private void ResetProduct()
    {
        txtSerialNo.Text = "";
        drpAssetArea.SelectedIndex = 0;
        drpAssetCode.SelectedIndex = 0;
        txtLocation.Text = "";
        lblAssetCat.Text = "";
        lblAssetDesc.Text = "";
        txtQtyAdd.Text = "";



    }
    protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int i;
        i = GrdViewItems.Rows[e.RowIndex].DataItemIndex;
        TextBox txtQtyEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtQty");
        TextBox txtLocEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtLocation");
        TextBox txtAreaEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtArea");
        TextBox txtSerialNoEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtSerialNo");






        GrdViewItems.EditIndex = -1;
        BindProduct();
        DataSet ds = (DataSet)GrdViewItems.DataSource;
        ds.Tables[0].Rows[i]["Qty"] = txtQtyEd.Text;
        ds.Tables[0].Rows[i]["AssetLocation"] = txtLocEd.Text;
        ds.Tables[0].Rows[i]["AssetArea"] = txtAreaEd.Text;
        ds.Tables[0].Rows[i]["SerialNo"] = txtSerialNoEd.Text;


        ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        BindProduct();
    }
    protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdViewItems.PageIndex = e.NewPageIndex;
        BindProduct();
    }
    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindProduct();
        DataSet ds = (DataSet)GrdViewItems.DataSource;
        ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
        ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        BindProduct();
        calcSum();
    }
    protected void GrdViewItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates2(GrdViewItems, e.Row, this);
        }
    }
    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (GrdViewItems.EditIndex == e.Row.RowIndex)
            {
                //CompareValidator cv = new CompareValidator();
                //cv.ID = "cDis";
                //cv.ControlToValidate = "txtDiscount";
                //cv.ValueToCompare = "100";
                //cv.Type = ValidationDataType.Double;
                //cv.Operator = ValidationCompareOperator.LessThanEqual;
                //cv.ErrorMessage = "Invalid Discount";
                //cv.SetFocusOnError = true;
                //e.Row.Cells[5].Controls.Add(cv);

                //CompareValidator cv2 = new CompareValidator();
                //cv2.ID = "cVat";
                //cv2.ControlToValidate = "txtVAT";
                //cv2.ValueToCompare = "100";
                //cv2.Type = ValidationDataType.Double;
                //cv2.Operator = ValidationCompareOperator.LessThanEqual;
                //cv2.ErrorMessage = "Invalid VAT";
                //cv2.SetFocusOnError = true;

                //e.Row.Cells[6].Controls.Add(cv2);
            }
        }
    }

    protected void GrdViewPurchase_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewPurchase, e.Row, this);
        }
    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

        GrdViewPurchase.PageIndex = ((DropDownList)sender).SelectedIndex;
        String strBillno = string.Empty;
        if (txtBillnoSrc.Text.Trim() != "")
            strBillno = txtBillnoSrc.Text.Trim();
        else
            strBillno = "0";
        BindGrid(strBillno);
    }
    protected void ddlPageSelector2_SelectedIndexChanged(object sender, EventArgs e)
    {

        GrdViewItems.PageIndex = ((DropDownList)sender).SelectedIndex;
        BindProduct();

    }
    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath("Reports\\" + delFilename)))
                File.Delete(Server.MapPath("Reports\\" + delFilename));
        }
    }
}
