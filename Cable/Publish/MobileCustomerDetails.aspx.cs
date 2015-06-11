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
using System.Data.OleDb;
using System.Text;

public partial class MobileCustomerDetails : System.Web.UI.Page
{
    private enum mode
    {
        mode_Edit,
        mode_Add
    };
    mode mMode;


    protected void Page_Load(object sender, EventArgs e)
    {

        string connStr = string.Empty;
        if (Session["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();
        else
            Response.Redirect("~/MobileLogin.aspx");

        string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic();
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            lnkBtnSave.Enabled = false;
        }

        srcArea.ConnectionString = connStr;
        srcAsset.ConnectionString = connStr;
        ddArea.DataBind();

        mMode = ParseQueryString();

        if (!Page.IsPostBack)
        {
            if (Request.UrlReferrer != null)
                lnkBtncancel.PostBackUrl = Request.UrlReferrer.ToString();

            switch (mMode)
            {
                case mode.mode_Add:
                    txtCustName.Focus();
                    populateAssetDD(ddArea.SelectedValue);
                    txtEffDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case mode.mode_Edit:
                    LoadCustomerData();
                    ddArea.Enabled = false;
                    break;
            }

            System.Text.StringBuilder cashScript = new System.Text.StringBuilder();
            cashScript.Append("window.open('CashHistory.aspx?Code=");
            cashScript.Append(txtCustCode.Text);
            cashScript.Append("&Area=");
            cashScript.Append(ddArea.SelectedValue.Replace("'", "^"));
            cashScript.Append("','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=500,width=600,scrollbars=yes,modal=yes');");
            //cashScript.Append("',self,'dialogWidth:600px;dialogHeight:500px;status:no;scrollbars=yes;dialogHide:yes;unadorned:no;');");
            btnCashHistory.Attributes.Add("OnClick", Server.UrlPathEncode(cashScript.ToString()));

        }

        //StringBuilder script = new StringBuilder();
        ////script.Append("<script type=\"text/javascript\" language=\"javascript\">\r\n");
        //script.Append("function AddressSearch(){\r\n");

        //script.Append("var querystring =''; \r\n ");
        //if (ddArea.SelectedValue != "0" && txtCustName.Text != "")
        //{
        //    // We only need to pass this in the app admin form...
        //    script.AppendFormat("querystring = 'area={0}", ddArea.SelectedValue + '&');
        //    script.AppendFormat("name={0}';\r\n\r\n", txtCustName.Text);
        //}
        //else
        //{
        //    script.Append("alert('Minimum search criteria not met. Postcode or Street and Town required.')\r\n");
        //    //script.Append("return;");
        //}


        //script.AppendFormat("window.showModalDialog('DisconCustomers.aspx?' + querystring ");
        //script.Append(",self,'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;' );\r\n\r\n");

        //script.Append("}\r\n");
        ////script.Append("</script>");
        imgBtnHist.Attributes.Add("OnClick", "CustomersSearch();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        ////RegisterStartupScript("startup", script.ToString());
        ////return;

    }

    private void populateAssetDD(string Area)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = string.Empty;

            if (Area == "0")
                query = "SELECT M.AssetCode,M.AssetDesc,D.AssetStatus,D.AssetLocation,D.AssetArea,D.SerialNo,D.AssetNo FROM AssetMaster M Inner Join AssetDetails D on M.AssetCode = D.AssetCode Where D.AssetStatus <> 'Scrapped' ";
            else
                query = "SELECT M.AssetCode,M.AssetDesc,D.AssetStatus,D.AssetLocation,D.AssetArea,D.SerialNo,D.AssetNo FROM AssetMaster M Inner Join AssetDetails D on M.AssetCode = D.AssetCode Where D.AssetStatus <> 'Scrapped' And D.AssetArea = '" + Area.Replace("'", "''") + "'";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data._CustomerData);
            ddAsset.Items.Clear();
            ddAsset.Items.Add(new ListItem(" -- Select Area -- ", "0"));
            ddAsset.DataSource = data;
            ddAsset.DataTextField = "SerialNo";
            ddAsset.DataValueField = "AssetNo";
            ddAsset.DataBind();

        }
        finally
        {

            if (conn != null)
                conn.Close();
        }

    }

    private CustomerData ReadRecords(string Name)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed,AssetNo FROM CustomerMaster Where (area + name + Cstr(code) = '" + Name + "')";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data._CustomerData);
            return data;

        }
        finally
        {

            if (conn != null)
                conn.Close();
        }
    }

    private void LoadCustomerData()
    {
        string Customer = "";
        CustomerData data = new CustomerData();

        if (Request.QueryString["ID"] != null || Request.QueryString["ID"] != "")
            Customer = Request.QueryString["ID"];

        if (Customer != "")
        {
            data = ReadRecords(Customer);

            if (data._CustomerData.Count > 0)
            {
                CustomerData.CustomerDataRow dataRow = data._CustomerData[0];

                if (dataRow.name != null)
                    txtCustName.Text = dataRow.name;

                if (dataRow.phoneno != null)
                    txtPhoneNo.Text = dataRow.phoneno;

                if (dataRow.doorno != null)
                    txtDoorNo.Text = dataRow.doorno;
                if (dataRow.address1 != null)
                    txtAdd1.Text = dataRow.address1;

                if (dataRow.address2 != null)
                    txtAdd2.Text = dataRow.address2;

                if (dataRow.place != null)
                    txtPlace.Text = dataRow.place;

                if (dataRow.balance >= 0)
                    txtBalance.Text = dataRow.balance.ToString();

                if (dataRow.effectdate != null)
                {
                    txtEffDate.Text = dataRow.effectdate.ToString("dd/MM/yyyy");
                }

                if (dataRow.monthlycharge != 0)
                    txtMnthCrge.Text = dataRow.monthlycharge.ToString();

                chkPrev.Checked = dataRow.prevailed;

                if (dataRow.code != 0)
                    txtCustCode.Text = dataRow.code.ToString();

                if (!dataRow.IsareaNull())
                {
                    ddArea.DataBind();
                    string area = dataRow.area.Trim();
                    if (ddArea.Items.FindByValue(area) != null)
                        ddArea.Items.FindByValue(area).Selected = true;
                }

                if (!dataRow.IsAssetNoNull())
                {
                    populateAssetDD(ddArea.SelectedValue);
                    string asset = dataRow.AssetNo.ToString();
                    if (ddAsset.Items.FindByValue(asset) != null)
                        ddAsset.Items.FindByValue(asset).Selected = true;
                }
                else
                {
                    populateAssetDD(ddArea.SelectedValue);
                }

                if (!dataRow.IscategoryNull())
                {

                    string category = dataRow.category.Trim();
                    if (ddCategory.Items.FindByValue(category) != null)
                        ddCategory.Items.FindByValue(category).Selected = true;

                    Session["Category"] = dataRow.category.Trim();
                }

                txtInstCrge.Text = GetCustInstCash(ddArea.SelectedValue.Replace("'", "''") + txtCustCode.Text);
            }

        }

    }

    private string GetCustInstCash(string Customer)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;

            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select amount,MAX(enteredDate) as entDate FROM InstallationCash Where (area + Cstr(code)) = '" + Customer + "' Group By (area + Cstr(code)),amount ";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return data.Tables[0].Rows[0]["amount"].ToString();
            }

        }
        finally
        {

            if (conn != null)
                conn.Close();
        }
    }

    private mode ParseQueryString()
    {
        string modestring = null;

        try
        {
            modestring = Request["ID"];

            if (modestring == "")
                modestring = "AddNew";
            else
                modestring = "Edit";
        }
        finally
        {
            if (modestring == null || modestring == "")
            {
                //throw new ParameterValidationError("Id missing");
            }
        }

        switch (modestring)
        {
            case "AddNew":
                return mode.mode_Add;
            default:
                return mode.mode_Edit;
        }

    }

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {
        string custName = String.Empty;
        string area = String.Empty;
        string category = String.Empty;
        string doorNo = String.Empty;
        string address1 = String.Empty;
        string address2 = String.Empty;
        string place = String.Empty;
        string phoneNo = String.Empty;
        string effDate = String.Empty;
        string Installation = String.Empty;
        string monthlyCharge = String.Empty;
        string prevelage = String.Empty;
        string enteredDate = string.Empty;
        string asset = String.Empty;
        int custCode = 0;

        _UserControl_errordisplay errDisp = (_UserControl_errordisplay)this.FindControl("errorDisplay");

        if (Session["Category"] != null)
        {
            if (Session["Category"].ToString() != ddCategory.SelectedValue)
            {
                txtEffDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                StringBuilder script = new StringBuilder();
                //script.Append("<script language='Javascript'>alert('You have changed the Category, effective date has been set to todays Date.')</script>");

                script.Append("alert('You have changed the Category, Effective date has been set to todays Date.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
            }

        }

        //if(ddCategory.SelectedValue == "DC")
        //{
        //    if(txtBalance.Text != "0")
        //    {
        //        StringBuilder script = new StringBuilder();
        //        script.Append("alert('You are not allowed to Disconnect the customer when the Balance is more than Zero. Please Clear the Balance and try again.')");
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        //        return;
        //    }
        //}

        if (!Page.IsValid)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    errorDisplay.AddItem(validator.ErrorMessage, DisplayIcons.Error, false);
                }
            }
        }
        else
        {


            try
            {

                if (txtCustName.Text != "")
                    custName = txtCustName.Text;

                if (ddArea.SelectedValue != "0")
                    area = ddArea.SelectedValue;

                if (ddAsset.SelectedValue != "0")
                    asset = ddAsset.SelectedValue;

                if (ddCategory.Text != "0")
                    category = ddCategory.SelectedValue;

                if (txtDoorNo.Text != "")
                    doorNo = txtDoorNo.Text;

                if (txtAdd1.Text != "")
                    address1 = txtAdd1.Text;

                if (txtAdd2.Text != "")
                    address2 = txtAdd2.Text;

                if (txtPlace.Text != "")
                    place = txtPlace.Text;

                if (txtPhoneNo.Text != "")
                    phoneNo = txtPhoneNo.Text;

                if (txtEffDate.Text != "")
                    effDate = txtEffDate.Text;

                if (txtInstCrge.Text != "")
                    Installation = txtInstCrge.Text;
                else
                    Installation = "0";

                if (txtMnthCrge.Text != "")
                    monthlyCharge = txtMnthCrge.Text;

                if (txtCustName.Text != "")
                    custName = txtCustName.Text;

                if (txtCustCode.Text != "")
                    custCode = int.Parse(txtCustCode.Text);


                prevelage = chkPrev.Checked.ToString();
                enteredDate = DateTime.Now.ToString("dd/MM/yyyy");

                string sql = string.Empty;

                if (mMode == mode.mode_Edit)
                {

                    sql = "UPDATE CustomerMaster SET area='" + area.Replace("'", "''") + "',name='" + custName + "', category='" +
                    category + "',doorno='" + doorNo + "',address1='" + address1 + "', address2='" + address2 +
                    "',place='" + place + "', phoneno='" + phoneNo + "',effectdate=Format('" + effDate + "', 'dd/mm/yyyy'),installation=" +
                    Installation + ",monthlycharge=" + monthlyCharge + ",prevailed=" + prevelage + ",AssetNo= '" + asset + "'" +
                    " WHERE area = '" + area.Replace("'", "''") + "'" + " and code = " + custCode.ToString();

                    ExecuteNonQuery(sql);
                    Session["Category"] = ddCategory.SelectedValue;
                    errorDisplay.AddItem("Customer Details Saved successfully", DisplayIcons.GreenTick, false);

                }
                else if (mMode == mode.mode_Add)
                {
                    int maxCode = GetMaxCustoCode(area);
                    custCode = maxCode + 1;
                    sql = string.Format("INSERT INTO CustomerMaster(area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed, AssetNo) VALUES('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}',Format('{9}', 'dd/mm/yyyy'),{10},{11},Format('{12}', 'dd/mm/yyyy'),{13},{14},{15})", area.Replace("'", "''"), custCode, custName, category, doorNo, address1, address2, place, phoneNo, effDate, Installation, monthlyCharge, enteredDate, 0, prevelage, asset);

                    ExecuteNonQuery(sql);
                    errorDisplay.AddItem("Customer Details Saved successfully. Customer Code is : " + custCode.ToString(), DisplayIcons.GreenTick, false);
                    Session["Category"] = null;
                    ResetFormControlValues(this);
                }
            }
            catch (Exception ex)
            {
                errorDisplay.AddItem("Exception: " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
            }

        }

    }

    private void ResetFormControlValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlValues(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Text = "";
                        break;
                    case "System.Web.UI.WebControls.RadioButtonList":
                        {
                            //((RadioButtonList)c).SelectedValue = "Y";
                            for (int j = 0; j < ((RadioButtonList)c).Items.Count; j++)
                            {
                                ((RadioButtonList)c).Items[j].Selected = false;

                            }
                            break;
                        }
                    case "System.Web.UI.WebControls.DropDownList":
                        ((DropDownList)c).SelectedValue = "0";
                        break;

                }
            }
        }
    }

    private int GetMaxCustoCode(string area)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = string.Format("Select MAX(Code) FROM CustomerMaster Where area= '{0}' Group by area", area.Replace("'", "''"));

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return 0;
            //reader = cmd.ExecuteReader();

            //datagrid.DataSource = reader;
            //datagrid.DataBind();
        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }

    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;
            if (Session["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Session["Company"].ToString()].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

    protected void btnAddCash_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {

    }

    protected void ddArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        populateAssetDD(ddArea.SelectedValue);
    }

}
