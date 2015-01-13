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


public partial class Crm_User_Edit : BaseCrm
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            if (StringHelper.isNum(Request.QueryString["id"]))//如果是更新数据,则加载该记录.
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataRowView model = BLL.DataBaseHelper.instance.GetModelView("U_Users", "*", "id=" + id);
                if (model != null)
                {
                    txtemail.Text = model["email"].ToString();
                    txtqq.Text = model["qq"].ToString();
                    txtpwd.Text = model["pwd"].ToString();
                    txtcoins.Text = model["coins"].ToString();
                    txtRemark.Text = model["remark"].ToString();
                    chkIslock.Checked = model["islock"].ToString() == "True" ? true : false;
                    txtPublishTime.Text = model["addtime"].ToString();

                }
            }
            else
            {
                txtPublishTime.Text = DateTime.Now.ToString("yyyy-M-d");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("email", txtemail.Text.Trim());
        hs.Add("qq", txtqq.Text.Trim());
     
        hs.Add("pwd", txtpwd.Text.Trim());
        hs.Add("coins", txtcoins.Text.Trim());
        hs.Add("remark", txtRemark.Text.Trim());
     
        hs.Add("islock", chkIslock.Checked ? "1" : "0");
   

        if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        {
            BLL.DataBaseHelper.instance.Insert(hs, "U_Users");
            txtemail.Text = "";
            txtqq.Text = "";
            txtpwd.Text = "";
            Common.MessageBox.Show(this, "注册成功!");
        }
        else//更新纪录
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "U_Users", "[id]=" + id);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "list.aspx");
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL.DataBaseHelper.instance.Delete("U_Users", "id=" + Request.QueryString["id"]);
        Response.Redirect("list.aspx");
    }
    
}
