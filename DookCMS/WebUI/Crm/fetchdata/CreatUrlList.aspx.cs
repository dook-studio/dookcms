using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dukey.Model;
using System.Collections;
using System.Xml;
using Common;
using System.Text;
using System.Text.RegularExpressions;

public partial class Crm_FetchCreatUrlList : BaseCrm
{
    public int fid = 0;
    string strwhere = "1=1 ";
    public string cname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDropList(dropsort2);
            string id = Request.QueryString["id"];
            if (StringHelper.isNum(id))
            {
                cname = BLL.DataBaseHelper.instance.GetSingle("collectdata", "cname", "id=" + id, "").ObjToStr();
            }
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        int total = 0;
        int pagecount = 1;
        string id = Request.QueryString["id"];
        if (StringHelper.isNum(id))
        {
            strwhere += " and nodeid=" + id;
        }
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("collecttempdata", "[id]", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
    }


    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindData(strwhere);
    }
    protected void btnGetUrls_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (StringHelper.isNum(id))
        {
            //解析xml
            XmlDocument xmlDoc = new XmlDocument();
            string xmlstr = BLL.DataBaseHelper.instance.GetSingle("collectdata", "xmlstr", "id=" + id, "").ObjToStr();
            xmlDoc.LoadXml(xmlstr);

            //更新页面中文显示名称
            XmlNode listurls = xmlDoc.SelectSingleNode("//collect/listurls");
            if (listurls != null)
            {
                string htmlstart = xmlDoc.SelectSingleNode("//collect/urlrules").InnerText;
                string htmlend = xmlDoc.SelectSingleNode("//collect/urlrulee").InnerText;
                bool isagent = xmlDoc.SelectSingleNode("//collect/isagent").InnerText == "0" ? false : true;
                string cookies = xmlDoc.SelectSingleNode("//collect/cookies").InnerText;
                string inurl = xmlDoc.SelectSingleNode("//collect/inurl").InnerText;//url包含
                string outurl = xmlDoc.SelectSingleNode("//collect/outurl").InnerText;//url不包含

                string[] urls = listurls.InnerText.Split(',');
                foreach (string url in urls)
                {
                    GetUrl(id, url, htmlstart, htmlend, isagent, cookies, inurl, outurl);
                }
            }
            BindData(strwhere);
        }
    }
    protected void btnCollect_Click(object sender, EventArgs e)//开始采集
    {
        string id = Request.QueryString["id"];
        if (StringHelper.isNum(id))
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xmlstr = BLL.DataBaseHelper.instance.GetSingle("collectdata", "xmlstr", "id=" + id, "").ObjToStr();
            xmlDoc.LoadXml(xmlstr);

            //更新页面中文显示名称

            string htmlstart = xmlDoc.SelectSingleNode("//collect/bodys").InnerText;
            string htmlend = xmlDoc.SelectSingleNode("//collect/bodye").InnerText;
            bool isagent = xmlDoc.SelectSingleNode("//collect/isagent").InnerText == "0" ? false : true;
            string cookies = xmlDoc.SelectSingleNode("//collect/cookies").InnerText;


            DataSet ds = BLL.DataBaseHelper.instance.GetList("collecttempdata", "id,surl", 1000, "iscollect=False and nodeid=" + id, "");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                string theid = item["id"].ToString();
                string url = item["surl"].ToString();
                Encoding encoding = Common.DownLoadHelper.GetPageEncoding(url);
                string html = Common.DownLoadHelper.Html(url, encoding, isagent, cookies);
                int startindex = html.IndexOf(htmlstart);
                int endindex = html.IndexOf(htmlend);
                if (startindex >= 0 && endindex > startindex)
                {
                    string subhtml = html.Substring(startindex, endindex - startindex);

                    Hashtable hs = new Hashtable();
                    hs.Add("body", subhtml);
                    hs.Add("iscollect", true);
                    hs.Add("lasttime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    BLL.DataBaseHelper.instance.Update(hs, "CollectTempData", "id=" + theid);
                }
                else
                {
                    Common.MessageBox.Show(this, "没找到内容开始节点或结束节点错误!");
                }
            }
            Common.MessageBox.Show(this, "采集完成!");
        }
    }

    private void GetUrl(string nodeid, string url, string start, string end, bool isagent, string cookies, string inurl, string outurl)
    {
        Encoding encoding = Common.DownLoadHelper.GetPageEncoding(url);
        string html = Common.DownLoadHelper.Html(url, encoding, isagent, cookies);
        int startindex = html.IndexOf(start);
        int endindex = html.IndexOf(end);
        if (startindex >= 0 && endindex >= 0)
        {
            //获取根路径
            string basepath = url;
            int dd = basepath.LastIndexOf("//");
            int m = basepath.LastIndexOf("/");
            string dir = basepath.Remove(m, basepath.Length - m) + "/";
            dir = (m == (dd + 1) ? basepath + "/" : dir);

            Uri baseUri = new Uri(dir);
            string domain = baseUri.Host + "\\";



            string subhtml = html.Substring(startindex, endindex - startindex);
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            MatchCollection mc = reg.Matches(subhtml);
            foreach (Match ma in mc)
            {
                string href = ma.Groups["url"].Value;//得到href值                 

                if (!string.IsNullOrEmpty(inurl) && href.Contains(inurl) == false)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(outurl) && href.Contains(outurl) == true)
                {
                    continue;
                }

                if (!href.Contains("http://") && !href.Contains("https://"))
                {
                    Uri absoluteUri = new Uri(baseUri, href.Trim());
                    href = absoluteUri.ToString();
                }

                string title = ma.Groups["text"].Value;//得到<a><a/>中间的内容
                Hashtable hs = new Hashtable();
                hs.Add("title", title);
                hs.Add("surl", href);
                hs.Add("nodeid", nodeid);
                hs.Add("typeid", "0");
                BLL.DataBaseHelper.instance.Insert(hs, "collecttempdata");
            }
        }
    }



    protected void btnMove_Click(object sender, EventArgs e)
    {
        string catid = dropsort2.SelectedValue;

        string selArticleids = hidselids.Value;
        string[] ids = selArticleids.Split(',');
        if (selArticleids.Length == 0)
        {
            lbltips.Text = "请选择至少一个编号";
            return;
        }
        foreach (string item in ids)
        {
            Hashtable hs = new Hashtable();

            string sql = string.Format("insert into article(title,typeid,body) select title,{0},body from collecttempdata where id={1};", catid, item);

            BLL.DataBaseHelper.instance.ExecuteSql(sql, hs);
            sql = " delete from collecttempdata where id=" + item;
            BLL.DataBaseHelper.instance.ExecuteSql(sql, hs);
        }
        lbltips.Text = "操作成功! " + DateTime.Now.ToString("mm:ss");
        BindData(strwhere);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string selArticleids = hidselids.Value;
        string[] ids = selArticleids.Split(',');
        if (selArticleids.Length == 0)
        {
            lbltips.Text = "请选择至少一个编号";
            return;
        }
        foreach (string item in ids)
        {
            Hashtable hs = new Hashtable();
            BLL.DataBaseHelper.instance.Delete("collecttempdata", "id=" + item);
        }
        lbltips.Text = "操作成功! " + DateTime.Now.ToString("mm:ss");
        BindData(strwhere);
    }
    private void GetDropList(DropDownList droplist)
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid", 0, "patternid=0", "px");
        List<Channel> list = StringHelper.DataTableToList<Channel>(ds.Tables[0]);
        foreach (var item in list)
        {
            if (item.fid == 0)
            {
                droplist.Items.Add(new ListItem(item.cname, item.bid.ToString()));
                PopDroplist(list, item.bid, ref droplist);
            }
        }
    }
    string tip = "├";
    private void PopDroplist(List<Channel> list, int bid, ref DropDownList droplist)
    {

        foreach (var citem in list.FindAll(i => i.fid == bid))
        {
            tip += "─";
            droplist.Items.Add(new ListItem(tip + " " + citem.cname, citem.bid.ToString()));
            PopDroplist(list, citem.bid, ref droplist);
        }
        tip = "├";
    }
}
