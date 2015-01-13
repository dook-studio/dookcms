using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;

public partial class Crm_FormColumnList : BaseCrm
{
    public static string cname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt1 = new BLL.DataBaseHelper().GetShemaTable();
            listTables.DataSource = dt1;
            listTables.DataBind();

            //获取当前模板目录
            DataRowView dv = BLL.DataBaseHelper.instance.GetModelView("viewMyWeb", "folder", "");
            spCurTemplatePath.InnerText = "~/template/" + dv["folder"].ToString() + "/";
            BindData();
        }
    }
    protected void listTables_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tablename = listTables.SelectedValue;
        if (tablename != "")
        {
            DataTable dt = new BLL.DataBaseHelper().GetShemaColumnName(tablename);
            DataView dtv = dt.DefaultView;
            dtv.Sort = "ordinal_position";
            ListColumns.DataSource = dtv;
            ListColumns.DataValueField = "column_name";
            ListColumns.DataTextField = "column_name";
            ListColumns.DataBind();
        }
    }
    protected void btnAddCoulmns_Click(object sender, EventArgs e)//添加所选字段
    {
        int formId = 0;
        if (StringHelper.isNum(Request.QueryString["formid"]))
        {
            formId = int.Parse(Request.QueryString["formid"]);
        }
        foreach (ListItem item in ListColumns.Items)
        {
            if (item.Selected)
            {
                BLL.DataBaseHelper.instance.AddFormParas(formId, listTables.SelectedValue, item.Value);
            }
        }
        BindData();
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            BLL.DataBaseHelper.instance.Delete("formParas", "[ID]=" + e.CommandArgument.ToString());
            BindData();
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private void BindData()
    {
        int formid = 0;
        if (StringHelper.isNum(Request.QueryString["formid"]))
        {
            formid = int.Parse(Request.QueryString["formid"]);
        }
        string strWhere = "formId=" + formid;
        DataSet ds = BLL.DataBaseHelper.instance.GetList("formParas", "", 0, strWhere, "px asc,addtime asc");
        gvList.DataSource = ds;
        gvList.DataBind();


        List<Dukey.Model.FormParas> list = new List<Dukey.Model.FormParas>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Dukey.Model.FormParas model = new Dukey.Model.FormParas();
            model.tablename = ds.Tables[0].Rows[i]["tablename"].ToString();
            model.columnname = ds.Tables[0].Rows[i]["columnname"].ToString();
            model.para_name = ds.Tables[0].Rows[i]["para_name"].ToString();
            model.datatype = ds.Tables[0].Rows[i]["datatype"].ToString();
            list.Add(model);
        }



        //生成sql语句
        IEnumerable<IGrouping<string, Dukey.Model.FormParas>> list1 = list.GroupBy(i => i.tablename);

        foreach (IGrouping<string, Dukey.Model.FormParas> item in list1)
        {
            string sql = "insert into [{2}]({0}) values({1});";
            string sqlupdate = "update [{0}] set ";
            string name = "";
            string value = "";
            string tablename = "";
            string updatesql1 = "";
            foreach (Dukey.Model.FormParas o in item)
            {
                name += "[" + o.columnname + "],";
                value += "@" + o.columnname + ",";
                tablename = o.tablename;
                updatesql1 += " [" + o.columnname + "]=@" + o.columnname + ",";

            }
            sqlupdate = string.Format(sqlupdate, tablename);
            if (updatesql1 != "") updatesql1 = updatesql1.Remove(updatesql1.Length - 1, 1);

            sqlupdate += updatesql1+";";

            if (name != "") name = name.Remove(name.Length - 1, 1);
            if (value != "") value = value.Remove(value.Length - 1, 1);

            sql = string.Format(sql, name, value, tablename);

            Hashtable hs = new Hashtable();
            hs.Add("sqlstr", sql + "|" + sqlupdate);
            BLL.DataBaseHelper.instance.Update(hs, "Form", "FormID=" + formid);           
        }

    }



    protected void btnCreatePage_Click(object sender, EventArgs e)//生成页面
    {

    }
}
