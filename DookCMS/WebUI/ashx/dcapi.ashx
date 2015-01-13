<%@ WebHandler Language="C#" Class="dcapi"   %>
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.SessionState;


public class dcapi : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        ////添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
        //if (context.Request.ServerVariables["HTTP_Referer"] == null || context.Request.ServerVariables["REMOTE_HOST"] == null || context.Request.ServerVariables["SERVER_NAME"] != context.Request.Url.Host)
        //{
        //    context.Response.Write("非法访问!");
        //    context.Response.End();
        //    return;
        //}
        string action = context.Request.QueryString["ac"];
        switch (action)
        {
            case "getnewslist": { GetNewsList(context); break; }//键值和表的字段相对应.         
            default: { break; }
        }
    }

    private void GetNewsList(HttpContext context)
    {
        try
        {
            int pagesize = 50;
            int.TryParse(context.Request["pagesize"],out pagesize);
            int total = 0;
            int pageCount = 1;  
            int pageindex = 1;
            int.TryParse(context.Request["pn"], out pageindex);
            StringBuilder str = new StringBuilder();
            str.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            str.Append("<root>");
           DataSet ds= BLL.DataBaseHelper.instance.GetList("article", "id", "id,title,brief,picurl,addtime", pagesize, pageindex, "", "", out total, out pageCount);
           if (ds != null && ds.Tables.Count > 0)
           {
               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   DataRow dr = ds.Tables[0].Rows[i];
                   str.Append(string.Format("<item><id>{0}</id>", dr["id"]));
                   str.Append(string.Format("<title><![CDATA[{0}]]></title>",StringHelper.StringFormat(dr["title"].ToString(),40)));
                   str.Append(string.Format("<brief><![CDATA[{0}]]></brief>",StringHelper.StringFormat(dr["brief"].ToString(),80)));
                   str.Append(string.Format("<picurl><![CDATA[{0}]]></picurl>", dr["picurl"]));
                   str.Append(string.Format("<addtime><![CDATA[{0}]]></addtime></item>", dr["addtime"]));                   
               }
           }
            str.Append("</root>");
            context.Response.ContentType = "text/xml";
            context.Response.Write(str.ToString());
        }
        catch (Exception ex) { context.Response.Write(ex.Message); }
    }
   

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}