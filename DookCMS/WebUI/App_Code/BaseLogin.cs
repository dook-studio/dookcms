using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// BaseCrm 的摘要说明
/// </summary>
public class BaseLogin : Page
{  
    protected override void OnInit(EventArgs e)
    {
        
        //如果session超时或者不存在
        if (BaseWeb.instance.UserId==0)
        {
            Response.Write("<script>window.top.location.replace( '/login.html');</script>");
            Response.End();
            return;
        }
    }

}
