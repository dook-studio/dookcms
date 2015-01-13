<%@ WebHandler Language="C#" Class="FileHandle" %>

using System;
using System.Web;
using System.Collections;
public class FileHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.ServerVariables["HTTP_Referer"] == null || context.Request.ServerVariables["REMOTE_HOST"] == null || context.Request.ServerVariables["SERVER_NAME"] != context.Request.Url.Host)
        {
            context.Response.Write("非法访问!");
            context.Response.End();
            return;
        }
        //如果session超时或者不存在
        if (context.Session["MyCrmUserName"] == null)
        {
            context.Response.Write("<script>window.top.location.replace( '/crm/login.aspx');</script>");
            context.Response.End();
            return;
        }
        string action = context.Request.QueryString["ac"];
        switch (action)
        {
            case "renametb": Renametb(context); break;//修改数据表名称.
            case "changedesc": ChangeDbDesc(context); break;//修改说明
        }
    }


    private void Renametb(HttpContext context)
    {
        string old = context.Request.Form["oldname"];
        string newname = context.Request.Form["newname"];
        try
        {
            Hashtable hs = new Hashtable();
            hs.Add("tbname", newname);           
            BLL.DataBaseHelper.instance.Update(hs, "tbnote", "tbname='" + old + "'");

            BLL.DataBaseHelper.instance.RenameTableName(old, newname);
            context.Response.Write("ok");
        }
        catch
        {
            context.Response.Write("error");
        }
    }
    private void ChangeDbDesc(HttpContext context)
    {
        string tablename = context.Request.Form["tbname"];
        string description = context.Request.Form["description"];
        Hashtable hs = new Hashtable();
        hs.Add("tbdesc", description);
        BLL.DataBaseHelper.instance.Update(hs, "tbnote", "tbname='" + tablename + "'");
        context.Response.Write("ok");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}