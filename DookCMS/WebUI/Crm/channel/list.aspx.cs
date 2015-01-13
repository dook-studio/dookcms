using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using BLL;
using System.Configuration;
using Common;
using System.Data;

public partial class Crm_Channel_List : BaseCrm
{
    public int fid = 0;
    List<Dukey.Model.Channel> list = null;
    string strwhere = "1=1 ";
    protected void Page_Load(object sender, EventArgs e)
    {
        list = StringHelper.DataTableToList<Dukey.Model.Channel>(BLL.DataBaseHelper.instance.GetList("Channel").Tables[0]);

        if (!IsPostBack)
        {           
            ViewState["SortOrder"] = "px";
            ViewState["OrderDire"] = "ASC";
            BindData(strwhere);
        }


    }

    private void BindData(string strwhere)
    {
        if (StringHelper.isNum(Request.QueryString["fid"]))
        {
            fid = int.Parse(Request.QueryString["fid"]);
        }
        strwhere += " and fid=" + fid + "";
        int total = 0;
        int pagecount = 1;
        DataSet ds = BLL.DataBaseHelper.instance.GetList("Channel", "[bid]", "[bid],cname,ename,fid,xpath,channeltype,px,isshow,ismain,patternid,link", pager.PageSize, pager.CurrentPageIndex, strwhere, "px", out total, out pagecount);
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        gvList.DataSource = dv;
        gvList.DataBind();

        pager.RecordCount = total;
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindData(strwhere);
    }
    protected void btnClear_Click(object sender, EventArgs e)//清空所有栏目
    {      
            DataBaseHelper.instance.ResetTable("Channel");
            BindData(strwhere);
        
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
            px = px.ObjToInt();

            Hashtable hs1 = new Hashtable();
            hs1.Add("cname", "新版块");
            hs1.Add("ename", "page");
            hs1.Add("fid", fid);
            hs1.Add("px", Convert.ToInt32(px) + 1);
            hs1.Add("itemrule", "/html/{yyyy}/{m}/{d}/");
            hs1.Add("seotitle", "请设置栏目的seo标题");
            hs1.Add("addtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            object patternid = DataBaseHelper.instance.GetSingle("channel", "patternid", "bid=" + fid, "");
            if (patternid != null)
            {
                hs1.Add("patternid", patternid);
            }
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
                hs.Add("ename", "page" + id);
                DataBaseHelper.instance.Update(hs, "Channel", "bid=" + id);
            }
        }
        else
        {
            object px = DataBaseHelper.instance.GetSingle("Channel", "px", "fid=" + fid, "px desc");
            px = px.ObjToInt();
            Hashtable hs1 = new Hashtable();
            hs1.Add("cname", "新版块");
            hs1.Add("ename", "page");
            hs1.Add("fid", fid);
            hs1.Add("seotitle", "请设置栏目的seo标题");
            hs1.Add("addtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
        BindData(strwhere);
    }

    string str = "";
    public string GetNavLink(int id)
    {
        if (list != null)
        {
            Dukey.Model.Channel model = list.Find(item => (item.bid == id));
            if (model != null && model.fid == 0)
            {
                str = string.Format("<a style=\"color:#fff;\" href='list.aspx?fid={0}'>{1}</a>&gt;&gt;", model.bid, model.cname) + str;
                return str.ToString();
            }
            else if (model != null && model.fid != 0)
            {
                str = string.Format("<a style=\"color:#fff;\" href='list.aspx?fid={0}'>{1}</a>&gt;&gt;", model.bid, model.cname) + str;
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
    public string GetPatternidTypeCH(string id)
    {
        switch (id)
        {
            case "0": return "<span style=\"color:#666;\">文章</span>";
            case "1": return "<span style=\"color:#4B85C7;\">商品</span>";
            case "2": return "图集";
        }
        return "";
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
                        str = "<a  target=_blank href=\"/index.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";
                    }
                    else
                    {
                        if (link.EndsWith(".html"))
                        {
                            str = "<a style='color:#999;'   target=_blank href=\"" + link + "\" title=\"" + link + ",设置该值为空则表示动态访问\">已生成静态</a>";
                        }
                        else
                        {
                            str = "<a style='color:#999;'   target=_blank href=\"" + link + "\" title=\"" + link + ",设置该值为空则表示动态访问\">浏览</a>";
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
                                str = "<a  target=_blank href=\"/list.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";
                                break;
                            case "1":
                                str = "<a  target=_blank href=\"/prolist.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";
                                break;
                            case "2":
                                str = "<a  target=_blank href=\"/imglist.ashx?bid=" + bid + "\" title=\"" + link + ",设置该值为空则表示动态访问\">动态浏览</a>";
                                break;
                        }
                    }
                    else
                    {
                        if (link.EndsWith(".html"))
                        {
                            str = "<a style='color:#999;'   target=_blank href=\"" + link + "\" title=\"" + link + ",设置该值为空则表示动态访问\">已生成静态</a>";
                        }
                        else
                        {
                            str = "<a style='color:#999;'   target=_blank href=\"" + link + "\" title=\"" + link + ",设置该值为空则表示动态访问\">浏览</a>";
                        }
                    }
                } break;
            case "2": str = "<a style='color:red;' target=_blank href=\"" + link + "\">查看外链</a>"; break;
        }
        return str;
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        var id = gvList.DataKeys[e.RowIndex].Value.ToString();
        string cname = ((TextBox)(gvList.Rows[e.RowIndex].FindControl("txtcname"))).Text.ToString().Trim();
        string ename = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string fid = ((TextBox)(gvList.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        string partternid = ((DropDownList)(gvList.Rows[e.RowIndex].FindControl("droppatternid"))).SelectedValue.ToString().Trim();
        string channeltype = ((DropDownList)(gvList.Rows[e.RowIndex].FindControl("dropchanneltype"))).SelectedValue.ToString().Trim();
        string link = ((TextBox)(gvList.Rows[e.RowIndex].FindControl("txtlink"))).Text.ToString().Trim();
        string px = ((TextBox)(gvList.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim();
        bool isshow = ((CheckBox)(gvList.Rows[e.RowIndex].Cells[8].Controls[0])).Checked;
        bool ismain = ((CheckBox)(gvList.Rows[e.RowIndex].Cells[9].Controls[0])).Checked;

        Hashtable hs = new Hashtable();
        hs.Add("cname", cname);
        hs.Add("ename", ename);
        hs.Add("fid", fid);
        hs.Add("patternid", partternid);
        hs.Add("channeltype", channeltype);
        hs.Add("link", link);
        hs.Add("px", px);
        hs.Add("isshow", isshow);
        hs.Add("ismain", ismain);
        BLL.DataBaseHelper.instance.Update(hs, "Channel", "bid=" + id);
        this.gvList.EditIndex = -1;
        BindData(strwhere);
    }
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }
        BindData(strwhere);
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string id = e.CommandArgument.ToString();
            BLL.DataBaseHelper.instance.Delete("channel", "bid=" + id);
            BindData(strwhere);
        }
    }
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvList.EditIndex = e.NewEditIndex;
        BindData(strwhere);
    }
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvList.EditIndex = -1;
        BindData(strwhere);
    }
}
