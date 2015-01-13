using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dukey.Model;
using System.Collections;

public partial class Crm_ImageList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    string strwhere = "1=1 ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDropList(dropSort);

            GetDropList(dropsort2);

            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        int total = 0;
        int pagecount = 1;
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("image", "[id]", "[id],[flag], [title], [picurl], [typeid], [isshow], [click], [addtime]", pager.PageSize, pager.CurrentPageIndex, strwhere, "id desc", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx");
    }

    protected void dropSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and title like '%{0}%' ", txtSearch.Text.Trim());
        }
        if (dropSort.SelectedValue != "")
        {
            strwhere += string.Format(" and typeid=" + dropSort.SelectedValue);
        }
        BindData(strwhere);
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and title like '%{0}%' ", txtSearch.Text.Trim());
        }
        if (dropSort.SelectedValue != "")
        {
            strwhere += string.Format(" and typeid=" + dropSort.SelectedValue);
        }
        BindData(strwhere);
    }
    protected void btnClear_Click(object sender, EventArgs e)//清空所有栏目
    {
        BLL.DataBaseHelper.instance.ResetTable("image");
        BindData(strwhere);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pager.CurrentPageIndex = 1;
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and title like '%{0}%' ", txtSearch.Text.Trim());
        }
        if (dropSort.SelectedValue != "")
        {
            strwhere += string.Format(" and typeid=" + dropSort.SelectedValue);
        }
        BindData(strwhere);
    }
    public string GetFlashStr(string flag)
    {
        string str = string.Empty;
        str = flag.Replace("h", "头条");
        str = str.Replace("c", "推荐");
        str = str.Replace("f", "幻灯");
        str = str.Replace("a", "特荐");
        str = str.Replace("s", "滚动");
        str = str.Replace("b", "加粗");
        str = str.Replace("p", "图片");
        str = str.Replace("j", "跳转");
        if (!string.IsNullOrEmpty(str))
        {
            str = "[" + str + "]";
        }
        return str;
    }
    protected void btnMove_Click(object sender, EventArgs e)
    {
        string catid = dropsort2.SelectedValue;
      
        string selArticleids = hidselids.Value;
        string[] ids = selArticleids.Split(',');
        foreach (string item in ids)
        {
            Hashtable hs = new Hashtable();
            hs.Add("typeid", catid);
            BLL.DataBaseHelper.instance.Update(hs, "image", "id=" + item);
        }
        lbltips.Text = "操作成功! " + DateTime.Now.ToString("mm:ss");
        BindData(strwhere);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string selArticleids = hidselids.Value;
        string[] ids = selArticleids.Split(',');
        foreach (string item in ids)
        {
            Hashtable hs = new Hashtable();
            BLL.DataBaseHelper.instance.Delete("image", "id=" + item);
        }
        lbltips.Text = "操作成功! " + DateTime.Now.ToString("mm:ss");
        BindData(strwhere);
    }
    private void GetDropList(DropDownList droplist)
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid", 0, "patternid=2", "px");
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
            droplist.Items.Add(new ListItem(tip+" "+citem.cname, citem.bid.ToString())); 
            PopDroplist(list, citem.bid, ref droplist);            
        }
        tip = "├";
    }
    protected void btnSetFlag_Click(object sender, EventArgs e)//设置属性
    {
        string selArticleids = hidselids.Value;
        string[] ids = selArticleids.Split(',');

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
        else
        {
            Common.MessageBox.Show(this, "至少选中一项进行操作!");
            return;
        }
        foreach (string item in ids)
        {
            Hashtable hs = new Hashtable();
            hs.Add("flag", flags);
            BLL.DataBaseHelper.instance.Update(hs, "image", "id=" + item);
        }
        lbltips.Text = "操作成功! " + DateTime.Now.ToString("mm:ss");
        BindData(strwhere);
    }
}
