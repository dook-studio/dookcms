using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataRowView model = BLL.DataBaseHelper.instance.GetModelViewByCache("channel", "bid,channeltype,patternid,link", "ename='index'","channel_ename_index");
        if (model != null)
        {
            string indexurl = GetLinkStrByCache(model["channeltype"].ToString(), model["bid"].ToString(), model["link"].ToString(), model["patternid"].ToString());
            Response.Redirect(indexurl);
        }
        else
        {
            Response.Redirect("index.ashx" + Request.Url.Query);
        }
    }

    public string GetLinkStrByCache(string type, string bid, string link, string patternid)
    {

        object objModel = Common.DataCache.GetCache("indexurl");
        if (objModel == null)
        {
            try
            {
                objModel = GetLinkStr(type, bid, link, patternid);
                if (objModel != null)
                {
                    Common.DataCache.SetCache("indexurl", objModel);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        return objModel as string;
    }

    public string GetLinkStr(string type, string bid, string link, string patternid)
    {
        string str = string.Empty;
        switch (type)
        {
            case "0":
                {
                    if (link.Trim() == string.Empty)
                    {
                        str = "/index.ashx?bid=" + bid + "";
                    }
                    else
                    {
                        if (link.EndsWith(".html"))
                        {
                            str = link;
                        }
                        else
                        {
                            str = link;
                        }
                    }
                    break;
                }
            case "1":
                {
                    if (link.Trim() == string.Empty)
                    {
                        switch (patternid)
                        {
                            case "0":
                                str = "/list.ashx?bid=" + bid + "";
                                break;
                            case "1":
                                str = "/prolist.ashx?bid=" + bid + "";
                                break;
                            case "2":
                                str = "/imglist.ashx?bid=" + bid + "";
                                break;
                        }
                    }
                    else
                    {
                        if (link.EndsWith(".html"))
                        {
                            str = link;
                        }
                        else
                        {
                            str = link;
                        }
                    }
                } break;
            case "2": str = link; break;
        }
        return str;
    }
}
