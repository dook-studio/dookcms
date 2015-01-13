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


public partial class crm_EditRole : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql1 = "SELECT * FROM [sys_Menu] order by [menuid]";

            chklMenu.DataSource = BLL.DataBaseHelper.instance.GetBySql(sql1);
            chklMenu.DataValueField = "ID";
            chklMenu.DataTextField = "MenuName";
            chklMenu.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int roleid = int.Parse(Request.QueryString["id"]);
                //string sql = string.Format("select * from Sys_Roles where id={0}", roleid);
                //SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.WebConnectionString, CommandType.Text, sql);
                DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("sys_roles", "*", "id=" + roleid);
                if (dr != null)
                {
                    txtRoleName.Text = dr["RoleName"].ToString();
                    string menuids = dr["menuids"].ToString();
                    foreach (ListItem item in chklMenu.Items)
                    {
                        if (("," + menuids + ",").IndexOf(("," + item.Value.Trim() + ",")) != -1)
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
        int roleid = 0;
        if (StringHelper.isNum(Request.QueryString["id"]))
        {
            roleid = int.Parse(Request.QueryString["id"]);
        }
        string rolename = StringHelper.ReplaceBadChar(txtRoleName.Text);
        string menuids = "";
        foreach (ListItem item in chklMenu.Items)
        {
            if (item.Selected)
            {
                menuids += item.Value + ",";
            }
        }
        if (menuids.Length > 0)
            menuids = menuids.Remove(menuids.Length - 1, 1);
        Hashtable hs = new Hashtable();
        hs.Add("rolename", rolename);
        hs.Add("menuids", menuids);
        if (StringHelper.isNum(Request.QueryString["id"]))
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "sys_roles", "id=" + id);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "roleslist.aspx");
        }
        else
        {
            object o = BLL.DataBaseHelper.instance.GetSingle("sys_roles", "1", "rolename='" + rolename + "'", "");
            if (o != null && o.ToString() == "1")
            {
                lblTip.Text = "名称已经存在!";
                return;
            }
            BLL.DataBaseHelper.instance.Insert(hs, "sys_roles");
            Common.MessageBox.ShowAndRedirect(this, "添加成功!", "roleslist.aspx");
        }
    }
}