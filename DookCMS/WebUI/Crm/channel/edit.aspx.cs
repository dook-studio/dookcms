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



public partial class Crm_Channel_Edit : Page
{
    List<Dukey.Model.Channel> listboard = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request["id"];
            if (string.IsNullOrEmpty(id))
            {
                Response.Write("参数错误!");
                Response.End();
            }
            BindData(id);
        }
    }
    private void BindData(string id)
    {
        DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("channel", "*", "bid=" + id + "");
        if (dr != null)
        {
            diyfileurl.Text = dr["diyfileurl"].ToString();
            listfileurl.Text = dr["listfileurl"].ToString();
            itemfileurl.Text = dr["itemfileurl"].ToString();
            listrule.Text = dr["listrule"].ToString();
            itemrule.Text = dr["itemrule"].ToString();
            seotitle.Text = dr["seotitle"].ToString();
            seokeywords.Text = dr["seokeywords"].ToString();
            seodesc.Text = dr["seodesc"].ToString();
            txtBody.Value = dr["body"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("diyfileurl", diyfileurl.Text.Trim());
        hs.Add("listfileurl", listfileurl.Text.Trim());
        hs.Add("itemfileurl", itemfileurl.Text.Trim());
        hs.Add("listrule", listrule.Text.Trim());
        hs.Add("itemrule", itemrule.Text.Trim());
        hs.Add("seotitle", seotitle.Text.Trim());
        hs.Add("seokeywords", seokeywords.Text.Trim());
        hs.Add("seodesc", seodesc.Text.Trim());
        hs.Add("body", txtBody.Value.Trim());
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
        {
            BLL.DataBaseHelper.instance.Update(hs, "channel", "[bid]=" + id);
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                Common.MessageBox.ShowAndRedirect(this, "修改成功!", "list.aspx?fid=" + Request.QueryString["fid"]);

            }
            else
            {
                Common.MessageBox.ShowAndRedirect(this, "修改成功!", "list.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
