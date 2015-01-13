using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Crm_plugins_EditPlugin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (StringHelper.isNum(Request.QueryString["id"]))
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataRowView dv = BLL.DataBaseHelper.instance.GetModelView("plugin", "*", "id=" + id);
                txtCname.Text = dv["cname"].ToString();
                txtEname.Text = dv["ename"].ToString();
                txtAuthor.Text = dv["author"].ToString();
                txtRemark.Text = dv["remark"].ToString();
                txtJsonStr.Value = Server.HtmlDecode(dv["jsonstr"].ToString());
                btnDelete.Enabled = true;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (StringHelper.isNum(Request.QueryString["id"]))
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Delete("plugin", "id=" + id);
            Response.Redirect("pluginlist.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();
        ht.Add("cname", StringHelper.ReplaceBadChar(txtCname.Text));
        ht.Add("ename", StringHelper.ReplaceBadChar(txtEname.Text));
        ht.Add("author", StringHelper.ReplaceBadChar(txtAuthor.Text));
        ht.Add("remark", StringHelper.ReplaceBadChar(txtRemark.Text));
        ht.Add("jsonstr", Server.HtmlEncode(txtJsonStr.Value.Trim()));
        if (StringHelper.isNum(Request.QueryString["id"]))
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(ht, "plugin", "id=" + id);
            lblResult.Text = "修改成功!";
            Cache.Remove("plugin");
        }
        else//增加
        {
            BLL.DataBaseHelper.instance.Insert(ht, "plugin");
            Common.MessageBox.ShowAndRedirect(this, "添加成功!", "pluginlist.aspx");
        }
    }
}
