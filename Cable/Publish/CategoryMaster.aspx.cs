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

public partial class CategoryMaster : System.Web.UI.Page
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
			lnkBtnAdd.Visible = false; 
			GrdCategory.Columns[1].Visible = false; 
		}

    }
    protected void GrdCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

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

    protected void GrdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            GrdCategory.FooterRow.Visible = false;
            lnkBtnAdd.Visible = true;

        }
        else if (e.CommandName == "Insert")
        {
            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                    }
                }
            }
            else
            {
                BusinessLogic objBus = new BusinessLogic();
                int nextSeq = objBus.GetNextSequence(GetConnectionString(), "Select Max(CategoryID) from tblCategories");
                string cateDes = ((TextBox)GrdCategory.FooterRow.FindControl("txtAddDescr")).Text;

                if (nextSeq == -1)
                    return;

                string sQl = string.Format("Insert Into tblCategories Values({0},'{1}')", nextSeq + 1, cateDes);

                srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
                srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());
                try
                {
                    srcGridView.Insert();
                    System.Threading.Thread.Sleep(2000);
                    GrdCategory.DataBind();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException!= null)
                    {
                        StringBuilder script = new StringBuilder();
                        script.Append("alert('Category with this name already exists, Please try with a different name.');");

                        if(ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(),true);

                        return;
                    }
                }
                lnkBtnAdd.Visible = true;
            }


        }
        else if (e.CommandName == "Update")
        {
            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                    }
                }
                return;
            }
        }
        else if (e.CommandName == "Edit")
        {
            lnkBtnAdd.Visible = false;
        }
        else if (e.CommandName == "Page")
        {
            lnkBtnAdd.Visible = true;
        }

    }
    protected void GrdCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdCategory, e.Row, this);
        }
    }
    protected void GrdCategory_DataBound(object sender, EventArgs e)
    {

    }
    protected void GrdCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        GrdCategory.FooterRow.Visible = true;
        lnkBtnAdd.Visible = false;
    }
    protected void GrdCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (!Page.IsValid)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                }
            }

        }
        else
        {

            string catDescr = ((TextBox)GrdCategory.Rows[e.RowIndex].FindControl("txtCatDescr")).Text;
            string catId = GrdCategory.DataKeys[e.RowIndex].Value.ToString();

            srcGridView.UpdateMethod = "UpdateCategory";
            srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
            srcGridView.UpdateParameters.Add("CategoryName", TypeCode.String, catDescr);
            srcGridView.UpdateParameters.Add("CategoryID", TypeCode.Int32, catId);
            lnkBtnAdd.Visible = true;

        }
    }
    protected void srcGridView_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }
    protected void GrdCategory_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        GrdCategory.DataBind();
    }
    protected void GrdCategory_PreRender(object sender, EventArgs e)
    {

    }
}
