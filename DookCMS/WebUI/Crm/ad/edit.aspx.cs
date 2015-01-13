using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Collections;
using Common;


public partial class Crm_Ad_Edit : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropadtype.DataSource = BLL.DataBaseHelper.instance.GetList("adtype", "*", 0, "", "px");
            dropadtype.DataTextField = "cname";
            dropadtype.DataValueField = "id";
            dropadtype.DataBind();

            if (StringHelper.isNum(Request.QueryString["id"]))//如果是更新数据,则加载该记录.
            {
                if (Request.QueryString["fromtype"].IsEmpty())//为空直接读取广告信息
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("ad", "*", "id=" + id);
                    if (dr != null)
                    {
                        adno.Text = dr["adno"].ToString();
                        title.Text = dr["title"].ToString();
                        dropadtype.SelectedValue = dr["adtype"].ToString();
                        cname.Text = dr["cname"].ToString();
                        title.Text = dr["title"].ToString();
                        link.Text = dr["link"].ToString();
                        brief.Text = dr["brief"].ToString();
                        config.Value = dr["config"].ToString();
                        px.Text = dr["px"].ToString();
                        txtPublishTime.Text = dr["addtime"].ToString();
                        file1.Value = dr["litpic"].ToString();
                        txtContent.Value = dr["body"].ToString();
                        chkIsShow.Checked = dr["isshow"].ToString() == "0" ? false : true;
                    }
                }
                else//读取文章信息
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("ad", "*", "id=" + id);
                    if (dr != null)
                    {
                        adno.Text = dr["adno"].ToString();
                        dropadtype.SelectedValue = dr["adtype"].ToString();
                        cname.Text = dr["cname"].ToString();
                        px.Text = dr["px"].ToString();
                        txtPublishTime.Text = dr["addtime"].ToString();
                        file1.Value = dr["litpic"].ToString();
                        chkIsShow.Checked = dr["isshow"].ToString() == "0" ? false : true;
                    }
                    if (Request.QueryString["fromtype"] == "article")//读取文章信息
                    {
                        int fromid = Request.QueryString["fromid"].ObjToInt();
                        DataRowView model = BLL.DataBaseHelper.instance.GetModelView("article", "*", "id=" + fromid);
                        if (model != null)
                        {
                            title.Text = model["title"].ToString();
                            brief.Text = model["brief"].ToString();
                            txtContent.Value = model["body"].ToString();
                            file1.Value = model["picurl"].ToString();   
                            link.Text = model["link"].ToString().Trim() == "" ? "/item.ashx?aid=" + fromid : model["link"].ToString();
                        }
                    }
                }
            }
            else
            {
                txtPublishTime.Text = DateTime.Now.ToString("yyyy-M-d");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("adno", StringHelper.ReplaceBadChar(adno.Text));
        hs.Add("adtype", dropadtype.SelectedValue);
        hs.Add("cname", StringHelper.ReplaceBadChar(cname.Text));
        hs.Add("title", title.Text.Trim());
        hs.Add("link", link.Text.Trim());
        hs.Add("litpic", file1.Value.Trim());
        hs.Add("brief", brief.Text.Trim());
        hs.Add("remark", "");
        hs.Add("body", txtContent.Value.Trim());
        hs.Add("config", config.Value.Trim());
        hs.Add("px", px.Text.Trim());
        hs.Add("addtime", txtPublishTime.Text.Trim());
        hs.Add("isshow", chkIsShow.Checked == true ? 1 : 0);
        if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        {
            BLL.DataBaseHelper.instance.Insert(hs, "ad");
            cname.Text = "";
            title.Text = "";
            txtContent.Value = "";
            Common.MessageBox.Show(this, "操作成功");
        }
        else//更新纪录
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "ad", "[id]=" + id);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "list.aspx");
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL.DataBaseHelper.instance.Delete("ad", "id=" + Request.QueryString["id"]);
        Response.Redirect("list.aspx");
    }


}
