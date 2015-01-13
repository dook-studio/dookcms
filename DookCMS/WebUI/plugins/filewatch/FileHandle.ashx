<%@ WebHandler Language="C#" Class="FileHandle" %>

using System;
using System.Web;

public class FileHandle : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        #region 判断访问权限
        if (context.Request.ServerVariables["HTTP_Referer"] == null || context.Request.ServerVariables["REMOTE_HOST"] == null || context.Request.ServerVariables["SERVER_NAME"] != context.Request.Url.Host)
        {
            context.Response.Write("非法访问!");
            context.Response.End();
            return;
        }
        #endregion

        string action = context.Request.QueryString["ac"];
        switch (action)
        {
            case "filerename": FileRename(context); break;//重命名文件
        }
    }
    private void FileRename(HttpContext context)
    {
        string type = "folder";
        type = context.Request.Form["type"];
        string localpath = context.Server.UrlDecode(context.Request.Form["localpath"]);
        string oldname = context.Request.Form["oldname"];
        string newname = context.Request.Form["newname"];
        if ("folder".Equals(type))
        {
            System.IO.Directory.Move(context.Server.MapPath(localpath + "/" + oldname), context.Server.MapPath(localpath + "/" + newname));
        }
        else
        {
            System.IO.File.Move(context.Server.MapPath(localpath + "/" + oldname), context.Server.MapPath(localpath + "/" + newname));
        }
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