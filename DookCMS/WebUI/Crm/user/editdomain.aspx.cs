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


public partial class Crm_User_Editdomain : BaseCrm
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            if (StringHelper.isNum(Request.QueryString["id"]))//如果是更新数据,则加载该记录.
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataRowView model = BLL.DataBaseHelper.instance.GetModelView("U_DomainBinds", "*", "id=" + id);
                if (model != null)
                {
                    txtdomain.Text = model["siteurl"].ToString();
                    txtUserid.Text = model["userid"].ToString();                 
                    chkIsuse.Checked = model["isuse"].ToString() == "True" ? true : false;
                }
            }
          
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("siteurl", txtdomain.Text.Trim());
        hs.Add("userid", txtUserid.Text.Trim());
     
    
     
        hs.Add("isuse", chkIsuse.Checked ? "1" : "0");
   

        if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        {
            BLL.DataBaseHelper.instance.Insert(hs, "U_DomainBinds");
            txtdomain.Text = "";
            txtUserid.Text = "";
            Common.MessageBox.Show(this, "操作成功!");
        }
        else//更新纪录
        {
            int id = int.Parse(Request.QueryString["id"]);
            BLL.DataBaseHelper.instance.Update(hs, "U_DomainBinds", "[id]=" + id);
            Common.MessageBox.ShowAndRedirect(this, "修改成功!", "domain.aspx");
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL.DataBaseHelper.instance.Delete("U_DomainBinds", "id=" + Request.QueryString["id"]);
        Response.Redirect("domain.aspx");
    }
    
}
