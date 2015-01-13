using System;
using System.Data.OleDb;
using Dukey.DBUtility;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        if (StringHelper.IsBadChar(userName))
        {          
            Common.MessageBox.Show(this,"请填写正确的账号!");            
            return;
        }
        string password =Common.DEncrypt.DESEncrypt.Encrypt(txtPassword.Text);
        //string code = validecode.Value.Trim();
        //if (StringHelper.IsBadChar(code))
        //{   
        //    Common.MessageBox.Show(this, "请输入验证码!");            
        //    return;
        //}
        //if (Session["validcode"] != null)
        //{
        //    if (code != Session["validcode"].ToString())
        //    {               
        //        Common.MessageBox.Show(this, "验证码不正确!");  
        //        return;
        //    }
        //}
        object o = new BLL.Sys_Admin().IsLoginAdmin(userName, password);
        if (o != null)
        {
            BaseWeb.instance.AdminId = int.Parse(o.ToString());
            Session["MyCrmUserName"] = userName;
            Response.Redirect("default.aspx", true);
        }
        else
        {            
            Common.MessageBox.Show(this, "对不起,登录失败!");
            //validecode.Value = "";
            txtPassword.Text = "";
        }
    }
}
