using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using BLL;

public partial class Crm_EditDBTable : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DataBaseHelper dbop = new DataBaseHelper();
        try
        {
            dbop.CreateTable("U_"+StringHelper.ReplaceBadChar(txtTableName.Text));
            lblResult.Text = "操作成功!";
            Response.Redirect("DBColumnList.aspx?tablename=U_"+txtTableName.Text.Trim());
        }
        catch
        {
            lblResult.Text = "创建失败,可能表已经存在!";
        }
        ////上传文件
        //string imageName = UploadFile();
        //Dukey.Model.Articles model = new Dukey.Model.Articles();
        //model.title = StringHelper.ReplaceBadChar(txtTitle.Text);
        //model.brief = StringHelper.ReplaceBadChar(txtBrief.Text);
        //model.contents = txtContent.Value;
        //model.bid = int.Parse(dropSort.SelectedValue);
        //model.isshow = chkIsShow.Checked;
        //DateTime dt = DateTime.Now;
        //DateTime.TryParse(txtPublishTime.Text, out dt);
        //model.cur_time = dt;
        //model.picurl = imageName;
        //int px = 0;
        //int.TryParse(txtOrders.Text, out px);
        //model.px = px;
        //if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        //{
        //    BLL.Articles.instance.Add(model);
        //}
        //else//更新纪录
        //{
        //    model.id = int.Parse(Request.QueryString["id"]);
        //    BLL.Articles.instance.Update(model);
        //}





    }






}
