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

public partial class crm_EditSysUser : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql1 = "SELECT * FROM [Sys_Roles] order by [ID]";
            chklRoles.DataSource = BLL.DataBaseHelper.instance.GetBySql(sql1);
            chklRoles.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int adminid = int.Parse(Request.QueryString["id"]);
                string sql = string.Format("select * from Sys_Admin where [ID]={0}", adminid);

                DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("sys_admin", "*", string.Format("id={0}", adminid));
                if (dr != null)
                {
                    txtUserName.Text = dr["UserName"].ToString();
                    txtPassword.Text =Common.DEncrypt.DESEncrypt.Decrypt(dr["Password"].ToString());
                    string roleids = dr["roleids"].ToString();
                    foreach (ListItem item in chklRoles.Items)
                    {
                        if (("," + roleids + ",").IndexOf(("," + item.Value.Trim() + ",")) != -1)
                        {
                            item.Selected = true;
                        }
                    }
                }
                lbltitle.Text = "修改";
            }
          
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int userid = 0;    
        string username = StringHelper.ReplaceBadChar(txtUserName.Text);
        string password =Common.DEncrypt.DESEncrypt.Encrypt(txtPassword.Text);
        string roleids = "";
        foreach (ListItem item in chklRoles.Items)
        {
            if (item.Selected)
            {
                roleids += item.Value + ",";
            }
        }
        if (roleids.Length > 0)
            roleids = roleids.Remove(roleids.Length - 1, 1);
        else
        {
            lblTip.Text = "请选择角色!";
            return;
        }    
        Hashtable hs = new Hashtable();
        hs.Add("[username]", username);
        hs.Add("[password]", password);
        hs.Add("[roleids]", roleids);
        if (StringHelper.isNum(Request.QueryString["id"]))
        {
            userid = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "sys_admin","id="+userid);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "sysUserList.aspx");
        }
        else
        {
            object o=BLL.DataBaseHelper.instance.GetSingle("sys_admin", "1", "username='" + username + "'", "") ;
            if (o!= null && o.ToString()=="1")
            {
                lblTip.Text = "名称已经存在!";
                return;
            }
            BLL.DataBaseHelper.instance.Insert(hs, "sys_admin");
            Common.MessageBox.ShowAndRedirect(this, "添加成功!", "sysUserList.aspx");           
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysUserList.aspx");
    }
}