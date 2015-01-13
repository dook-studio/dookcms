using System;
using System.Web.UI;
using MdbWatch;
using System.Collections;

public partial class Mdbw_EditDBTable : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string tablename = "U_" + StringHelper.ReplaceBadChar(txtTableName.Text);
            string sql = "create table " + tablename + "(";
            if (chklColumn.Items[0].Selected)
            {
                sql += "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,";
            }
            if (chklColumn.Items[1].Selected)
            {
                sql += "userid integer,";
            }
            if (chklColumn.Items[2].Selected)
            {
                sql += "adminid integer,";
            }
            if (chklColumn.Items[3].Selected)
            {
                sql += "addtime datetime,";
            }
            if (chklColumn.Items[4].Selected)
            {
                sql += "uptime datetime,";
            }
            if (sql.EndsWith(","))
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }
            sql += ")";

            if (false == BLL.DataBaseHelper.instance.IsExistTable(tablename))
            {
                BLL.DataBaseHelper.instance.ExecuteSql(sql, null);
                
               
                    //添加说明
                    ChangeDesc(tablename, txtBrief.Text.Trim());
               
                lblResult.Text = "操作成功!";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "Finish()", true);
            }
            else
            {
                lblResult.Text = "表已经存在!";
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "Finish()", true);
            }
            
        }
        catch(Exception ex)
        {
            throw ex;
            lblResult.Text = "创建失败,可能表已经存在!";
        }
    }
    private void ChangeDesc(string tablename, string description)
    {
        Hashtable hs = new Hashtable();
        hs.Add("tbname", tablename);
        hs.Add("tbdesc", description);
        BLL.DataBaseHelper.instance.Insert(hs, "tbnote");
    }

    protected override void OnInit(EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
        //    if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
        //    {
        //        Response.Write("非法访问!");
        //        Response.End();
        //        return;
        //    }
        //}
    }

}
