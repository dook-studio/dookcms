using System;
using Dukey.DBUtility;

public partial class UpdatePwd : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["MyCrmUserName"] != null)
            {
               txtUserName.Text= Session["MyCrmUserName"].ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string password =Common.DEncrypt.DESEncrypt.Encrypt(txtPassword1.Text);
        string passwordnew =Common.DEncrypt.DESEncrypt.Encrypt(txtPassword2.Text);

        if (string.IsNullOrEmpty(userName))
        {
            Response.Write("<script>alert('用户名不能为空!');</script>");
            return;

        }

        if (txtPassword2.Text != txtPassword3.Text)
        {
            Response.Write("<script>alert('两次输入密码不一致!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            Response.Write("<script>alert('密码不能为空!');</script>");
            return;
        }


        if (string.IsNullOrEmpty(passwordnew))
        {
            Response.Write("<script>alert('新密码不能为空!');</script>");
            return;
        }
       
        //OleDbParameter[] paras =
        //    {
        //        new OleDbParameter("@UserName",userName),
        //        new OleDbParameter("@Password",password),
        //        new OleDbParameter("@Passwordnew",passwordnew)
        //    };

        string sql = string.Format("update [Sys_Admin] set [Password]='{0}' where ([UserName]='admin' and [Password]='{1}')", passwordnew, password);
        if (BLL.DataBaseHelper.instance.ExecuteSql(sql, null) > 0)
        {
            Response.Write("<script>alert('修改成功!');</script>");
        }
        else
        {
            Response.Write("<script>alert('修改失败,请检查旧密码是否正确!');</script>");
        }
    }
}
