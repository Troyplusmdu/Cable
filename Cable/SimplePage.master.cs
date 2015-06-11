using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;

public partial class SimplePage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationSettings.AppSettings["InstallationType"].ToString() == "ONLINE-OFFLINE-SERVER")
        {
            //lblTitle.Text = "Your IP : " + Request.UserHostAddress;
        }

        //String strHostName = string.Empty;
        //// Getting Ip address of local machine...
        //// First get the host name of local machine.
        //strHostName = Dns.GetHostName();
        //Response.Write("Local Machine's Host Name: " + strHostName);

        //// Then using host name, get the IP address list..
        //IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
        //IPAddress[] addr = ipEntry.AddressList;

        //for (int i = 0; i < addr.Length; i++)
        //{
        //    Response.Write("IP Address "+i+":" +  addr[i].ToString());
        //}


    }
}
