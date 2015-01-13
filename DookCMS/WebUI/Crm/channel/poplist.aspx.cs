using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using BLL;
using System.Configuration;

public partial class Crm_Channel_PopList : BaseCrm
{
    public int fid = 0;
    List<Dukey.Model.Channel> list =null;
    public string typestr = "accessdata";
    protected void Page_Load(object sender, EventArgs e)
    {
        list = StringHelper.DataTableToList<Dukey.Model.Channel>(BLL.DataBaseHelper.instance.GetList("channel").Tables[0]);
        if (StringHelper.isNum(Request.QueryString["fid"]))
        {
            fid = int.Parse(Request.QueryString["fid"]);
        }
        if (Common.MyWeb.DbType == 1)
        {
            typestr = "sqlserverdata";
        }
        gvList.DataSourceID = typestr;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        string xpath = "";
        int fid = 0;
        if (StringHelper.isNum(Request.QueryString["fid"]))
        {
            fid = int.Parse(Request.QueryString["fid"]);
            var fboard = list.Find(item => item.bid == fid);
            if (fboard != null)
            {
                xpath = fboard.xpath;
            }
            object px = DataBaseHelper.instance.GetSingle("Channel", "px", "fid=" + fid, "px desc");
            px = (px == null ? 0 : px);

            Hashtable hs1 = new Hashtable();
            hs1.Add("cname", "新版块");
            hs1.Add("ename", "page");
            hs1.Add("fid", fid);
            hs1.Add("px", Convert.ToInt32(px) + 1);
            hs1.Add("itemrule", "/html/{yyyy}/{m}/{d}/");
            BLL.DataBaseHelper.instance.Insert(hs1, "Channel");
            int id = BLL.DataBaseHelper.instance.GetMaxID("bid", "Channel");
            if (id > 0)
            {
                Hashtable hs = new Hashtable();
                //Response.Write(xpath);
                //Common.MessageBox.Show(this,xpath);
                hs.Add("xpath", xpath + "," + id);
                hs.Add("link", "");
                hs.Add("channeltype", "1");
                hs.Add("ename", "page"+id);
                DataBaseHelper.instance.Update(hs, "Channel", "bid=" + id);
            }
        }
        else
        {
            object px = DataBaseHelper.instance.GetSingle("Channel", "px", "fid=" + fid, "px desc");
            px = (px == null ? 0 : px);
            Hashtable hs1 = new Hashtable();
            hs1.Add("cname", "新版块");
            hs1.Add("ename", "page");
            hs1.Add("fid", fid);
            hs1.Add("px", Convert.ToInt32(px) + 1);
            hs1.Add("itemrule", "/html/{yyyy}/{m}/{d}/");
            BLL.DataBaseHelper.instance.Insert(hs1, "Channel");
            int id = BLL.DataBaseHelper.instance.GetMaxID("bid", "Channel");
            if (id > 0)
            {
                Hashtable hs = new Hashtable();
                hs.Add("xpath", id);
                hs.Add("link", "");
                hs.Add("channeltype", "1"); 
                hs.Add("ename", "page" + id);
                DataBaseHelper.instance.Update(hs, "Channel", "bid=" + id);
            }
        }

        gvList.DataSourceID = typestr;
    }

    string str = "";
    public string GetNavLink(int id)
    {
        if (list != null)
        {
            Dukey.Model.Channel model = list.Find(item => (item.bid == id));
            if (model != null && model.fid == 0)
            {
                str = string.Format("<a style=\"color:#fff;\" href='poplist.aspx?fid={0}'>{1}</a>&gt;&gt;", model.bid, model.cname) + str;
                return str.ToString();
            }
            else if (model != null && model.fid != 0)
            {
                str = string.Format("<a style=\"color:#fff;\" href='poplist.aspx?fid={0}'>{1}</a>&gt;&gt;", model.bid, model.cname) + str;
                return GetNavLink(model.fid);
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }


    public string GetChannelTypeCH(string type)
    {
        switch (type)
        {
            case "0": return "<span style=\"color:Red;\">封面</span>";
            case "1": return "<span style=\"color:#4B85C7;\">列表</span>";
            case "2": return "外链";
        }
        return "";
    }
    public string GetLinkStr(string type,string bid,string link)
    {
        string str = string.Empty;
        switch (type)
        {
            case "0":
                {
                    if (link.Trim() == string.Empty)
                    {
                        str = "<a  target=_blank href=\"/index.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";
                    }
                    else
                    {
                        str = "<a style='color:#999;'   target=_blank href=\"" + link + "\" title=\"" + link + ",设置该值为空则表示动态访问\">已生成静态</a>";
                    }
                    break;
                }
            case "1":
                {
                    if (link.Trim() == string.Empty)
                    {
                        str = "<a  target=_blank href=\"/list.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";                        
                    }
                    else
                    {
                        str = "<a style='color:#999;'   target=_blank href=\""+link+"\" title=\"" + link + ",设置该值为空则表示动态访问\">已生成静态</a>";
                    }
                } break;
            case "2": str = "<a style='color:red;' target=_blank href=\"" + link + "\">查看外链</a>"; break;
        }
        return str;
    }
}
