using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Collections;



public partial class Crm_EditContent : Page
{
    //PageContentMgmt newsMgmt = new PageContentMgmt();
    protected void Page_Load(object sender, EventArgs e)
    {     
        if (!IsPostBack)
        {
            dropSort.DataSource = BLL.DataBaseHelper.instance.GetList("ads", "id,cname,ename", 0, "", "");
   dropSort.DataTextField = "cname";
            dropSort.DataValueField = "id";
            dropSort.DataBind();       

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                dropSort.SelectedValue = Request.QueryString["id"];
                object o=BLL.DataBaseHelper.instance.GetSingle("ads", "contents", "id=" + Request.QueryString["id"], "");
                if(o!=null)
                txtContent.Value = o.ToString();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (dropSort.SelectedValue == "")
        {
            lblTip.Text = "请选择分类!";
            return;
        }        
        Hashtable hs = new Hashtable();
        hs.Add("contents", txtContent.Value);
        BLL.DataBaseHelper.instance.Update(hs, "ads", "id=" + dropSort.SelectedValue + "");
        lblTip.Text = "操作成功!";
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {     
        txtContent.Value = BLL.DataBaseHelper.instance.GetSingle("ads", "contents", "id=" +dropSort.SelectedValue, "").ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)//还原默认值
    {
       
        string sql = "update ads set contents=defaulttxt where id=" + dropSort.SelectedValue;
        BLL.DataBaseHelper.instance.ExecuteSql(sql, null);
     
        Common.MessageBox.ShowAndRedirect(this,"已经还原默认值","EditContent.aspx?id="+dropSort.SelectedValue);
    }
}
