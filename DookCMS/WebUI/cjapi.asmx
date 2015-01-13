<%@ WebService Language="C#"  Class="SoftCaiJIAPI" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;

/// <summary>
///SoftCaiJIAPI 的摘要说明
/// </summary>
[WebService(Namespace = "http://www.yunaishuo.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class SoftCaiJIAPI : System.Web.Services.WebService {

    public SoftCaiJIAPI () {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    
    //private string RsaKey = Common.MyWeb.config["RSAKey"];//请设置RSAKey,这里由用户自行设置.
    private string RsaKey = BLL.DataBaseHelper.instance.GetSingle("webconfig", "keyvalue", "keytext='rsakey'", "").ToString();
    
    [WebMethod]
    public List<Dukey.Model.Channel> GetChannel(string RSAKey)
    {
        if (RSAKey == RsaKey)
        {            
            System.Data.DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid", 0, "patternid=1", "px");
            return StringHelper.DataTableToList<Dukey.Model.Channel>(ds.Tables[0]);
        }
        else
        {
            throw new Exception("密文不正确!");
        }
    }
    [WebMethod]
    public string PublishCaiji(List<tbproductasmx> list, string channelid,string RSAKey)
    {
        if (RSAKey != RsaKey)
        {
            throw new Exception("密文不正确!");
        }
        if (list!=null && list.Count>0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                tbproductasmx item = list[i];
                Hashtable hs = new Hashtable();
                hs.Add("title", item.title);
                hs.Add("picurl", item.picurl.Replace("_sum.jpg", ""));
                hs.Add("link", item.link);
                hs.Add("price", item.price.Replace("元", "").Trim());//原价
                hs.Add("mprice", item.mprice);
                hs.Add("brand", item.id);
                hs.Add("typeid", channelid);
                hs.Add("isshow", true);
                hs.Add("flag", "j");
                BLL.DataBaseHelper.instance.Insert(hs, "product");
            }
            return "ok";
        }
        else
        {
            return "empty";
        }
    }   
   
    private void GetDropList(System.Web.UI.WebControls.DropDownList droplist)
    {
        System.Data.DataSet ds = BLL.DataBaseHelper.instance.GetList("channel", "bid,cname,fid", 0, "patternid=1", "px");
        System.Collections.Generic.List<Dukey.Model.Channel> list = StringHelper.DataTableToList<Dukey.Model.Channel>(ds.Tables[0]);
        foreach (var item in list)
        {
            if (item.fid == 0)
            {
                droplist.Items.Add(new System.Web.UI.WebControls.ListItem(item.cname, item.bid.ToString()));
                PopDroplist(list, item.bid, ref droplist);
            }
        }
    }
    string tip = "├";
    private void PopDroplist(System.Collections.Generic.List<Dukey.Model.Channel> list, int bid, ref System.Web.UI.WebControls.DropDownList droplist)
    {
        foreach (var citem in list.FindAll(i => i.fid == bid))
        {
            tip += "─";
            droplist.Items.Add(new System.Web.UI.WebControls.ListItem(tip + " " + citem.cname, citem.bid.ToString()));
            PopDroplist(list, citem.bid, ref droplist);
        }
        tip = "├";
    }
}
public class tbproductasmx
{
    public string id { get; set; }
    public string picurl { get; set; }
    public string title { get; set; }
    public string price { get; set; }
    public string mprice { get; set; }//打折价
    public string link { get; set; }
    public string shopname { get; set; }
    public string shopurl { get; set; }
    public string zhekou { get; set; }
    public string yjbl { get; set; }
    public string yj { get; set; }
    public string outnum { get; set; }
    public string outprice { get; set; }
}