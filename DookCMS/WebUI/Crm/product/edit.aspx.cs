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
using Dukey.Model;
using Common;


public partial class Crm_Product_Edit : BaseCrm
{
    public string tags = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listcate1.DataSource = BLL.DataBaseHelper.instance.GetList("productcat", "id,cname", 0, "pid=0", "");
            listcate1.DataBind();
            GetDropList(dropSort);
            rptlist.DataSource = BLL.DataBaseHelper.instance.GetList("dictionary", "keytext,keyvalue", 0, "pid=(select id from dictionary where keyvalue='product_plus_property')", "px");
            rptlist.DataBind();

            if (StringHelper.isNum(Request.QueryString["id"]))//如果是更新数据,则加载该记录.
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataRowView model = BLL.DataBaseHelper.instance.GetModelView("product", "*", "id=" + id);
                if (model != null)
                {
                    txtTitle.Text = model["title"].ToString();
                    txtStitle.Text = model["stitle"].ToString();
                    txtcolor.Value = model["color"].ToString();
                    txtTag.Text = model["tags"].ToString();
                    txtBody.Value = model["body"].ToString();
                    file1.Value = model["picurl"].ToString();
                    txtDots.Text = model["click"].ToString();
                    txtOrders.Text = model["px"].ToString();
                    txtLink.Text = model["link"].ToString();
                    dropSort.SelectedValue = model["typeid"].ToString();
                    chkIsshow.Checked = model["isshow"].ToString() == "True" ? true : false;
                    txtPublishTime.Text = model["addtime"].ToString();
                    txtBrand.Text = model["brand"].ToString();
                    txtPrice.Text = model["price"].ToString();
                    txtMprice.Text = model["mprice"].ToString();
                    txtUnit.Text = model["unit"].ToString();
                    hidCateID.Value = model["catid"].ToString();
                    txttotal.Text = model["total"].ToString();
                    foreach (ListItem item in chklFlag.Items)
                    {
                        if (("," + model["flag"].ToString() + ",").Contains("," + item.Value + ","))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            else
            {
                txtPublishTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("title", StringHelper.ReplaceBadChar(txtTitle.Text.Trim()));
        hs.Add("stitle", StringHelper.ReplaceBadChar(txtStitle.Text.Trim()));
        hs.Add("color", StringHelper.ReplaceBadChar(txtcolor.Value.Trim()));
        hs.Add("tags", txtTag.Text.Trim());
        hs.Add("body", txtBody.Value.Trim());
        hs.Add("picurl", file1.Value.Trim());
        hs.Add("typeid", dropSort.SelectedValue);
        hs.Add("isshow", chkIsshow.Checked ? "1" : "0");
        hs.Add("click", StringHelper.ReplaceBadChar(txtDots.Text.Trim()));
        hs.Add("px", StringHelper.ReplaceBadChar(txtOrders.Text.Trim()));
        hs.Add("link", txtLink.Text.Trim());
        hs.Add("brand", txtBrand.Text.Trim());
        hs.Add("mprice", StringHelper.ReplaceBadChar(txtMprice.Text.Trim()));
        hs.Add("price", StringHelper.ReplaceBadChar(txtPrice.Text.Trim()));   
        hs.Add("unit",  StringHelper.ReplaceBadChar(txtUnit.Text.Trim()));
        hs.Add("catid", hidCateID.Value);
        hs.Add("total", StringHelper.ReplaceBadChar(txttotal.Text.Trim().ObjToInt().ObjToStr()));
        hs.Add("uptime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        string flags = string.Empty;
        foreach (ListItem item in chklFlag.Items)
        {
            if (item.Selected)
            {
                flags += item.Value + ",";
            }
        }
        if (flags.Length > 0)
        {
            flags = flags.Remove(flags.Length - 1, 1);
        }
        hs.Add("flag", flags);

        if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        {
            if (txtPublishTime.Text.IsDate())
            {
                hs.Add("addtime", txtPublishTime.Text.Trim());
            }
            else
            {
                hs.Add("addtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            BLL.DataBaseHelper.instance.Insert(hs, "product");
            txtTitle.Text = "";
            txtBody.Value = "";
            Common.MessageBox.Show(this, "发布成功,你可以继续发布下一篇!");
        }
        else//更新纪录
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "product", "[id]=" + id);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "list.aspx");
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL.DataBaseHelper.instance.Delete("product", "id=" + Request.QueryString["id"]);
        Response.Redirect("list.aspx");
    }
    private void GetDropList(DropDownList droplist)
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid", 0, "patternid=1", "px");
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


    protected void listcate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listcate1.SelectedValue != "")
        {
            hidCateID.Value = listcate1.SelectedValue;
            DataSet ds = BLL.DataBaseHelper.instance.GetList("productcat", "id,cname", 0, "pid=" + listcate1.SelectedValue, "");
            listcate2.DataSource = ds;
            listcate2.DataBind();
            listcate2.Items.Add(new ListItem("--请选择--", ""));
            listcate2.SelectedIndex = listcate2.Items.Count - 1;
            listcate3.Items.Clear();

        }
        else
        {
            hidCateID.Value = "";
            listcate2.Items.Clear();
            listcate3.Items.Clear();
        }
    }
    protected void listcate2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listcate2.SelectedValue != "")
        {
            hidCateID.Value = listcate2.SelectedValue;
            hidSelIds.Value = "";
            listcate3.DataSource = BLL.DataBaseHelper.instance.GetList("productcat", "id,cname", 0, "pid=" + listcate2.SelectedValue, "");
            listcate3.DataBind();
            listcate3.Items.Add(new ListItem("--请选择--", ""));
            listcate3.SelectedIndex = listcate3.Items.Count - 1;
        }
        else
        {
            hidCateID.Value = "";
            listcate3.Items.Clear();

        }
    }
    protected void listcate3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listcate3.SelectedValue != "")
        {

            hidCateID.Value = listcate3.SelectedValue;
            hidSelIds.Value = "";
        }
        else
        {
            hidCateID.Value = "";

        }

    }
}
