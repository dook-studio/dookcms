using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using Dukey.Model;

public partial class CreatePageMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDropList(dropchannel);
            GetDropList(dropchannel2);
        }
    }
    private void GetDropList(DropDownList droplist)
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid,channeltype,isshow", 0, "", "px");
        List<Channel> list = StringHelper.DataTableToList<Channel>(ds.Tables[0]).FindAll(i=>i.channeltype==1 && i.isshow==true);
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