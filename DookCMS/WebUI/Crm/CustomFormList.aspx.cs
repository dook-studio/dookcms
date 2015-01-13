using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class CustomFormList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    public static string pagesize = "0";
    private static string tablename="",tablekey="";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        string formename = Request.QueryString["frmname"];
        DataSet ds= BLL.DataBaseHelper.instance.GetList("Form", "formid,cname,ename", 0, "", "addtime desc");
        dropTable.DataSource = ds;
        dropTable.DataTextField = "cname";
        dropTable.DataValueField = "formid";
        dropTable.DataBind();
        if(!string.IsNullOrEmpty( Request.QueryString["frmname"]))
        {
            string ename = Request.QueryString["frmname"];
            dropTable.SelectedValue = ename;

            #region 表单
            DataTable dt = BLL.DataBaseHelper.instance.GetModel("Form", "*", "FormID=" + ename);
            if (dt.Rows.Count > 0)
            {
                txtPageSize.Text = dt.Rows[0]["pagesize"].ToString();
                chkAutoColumn.Checked = Convert.ToBoolean(dt.Rows[0]["isnumcol"]);
                chkEdit.Checked = Convert.ToBoolean(dt.Rows[0]["isedit"]);
                chkDelete.Checked = Convert.ToBoolean(dt.Rows[0]["isdelete"]);
                chkUpdate.Checked = Convert.ToBoolean(dt.Rows[0]["isupdate"]);
                tablekey = dt.Rows[0]["tablekey"].ToString();
                
            }
            #endregion
        }
       


    }

    private void BindGvList()
    {    
        gvList.Columns.Clear();
        string strwhere = "isshow=True and formID=" + dropTable.SelectedValue;
        DataSet ds=BLL.DataBaseHelper.instance.GetList("FormParas", "*", 0, strwhere, "listpx,id");
        string rows="";
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            //添加自增列
            if (chkAutoColumn.Checked)
            {
                TemplateField f = new TemplateField();                
                gvList.Columns.Add(new TemplateField());               
            }      

            tablename=ds.Tables[0].Rows[0]["tablename"].ToString();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var row = ds.Tables[0].Rows[i];
                rows+=row["columnname"].ToString()+",";              
                BoundField filed = new BoundField();
                filed.DataField = row["columnname"].ToString();
                filed.HeaderText = row["cname"].ToString();
                var listwidth = row["listwidth"].ToString();
                int listw=0;
                int.TryParse(listwidth,out listw);
                if(listw>0)
                {
                    filed.ItemStyle.Width = Unit.Pixel(listw);
                }
                gvList.Columns.Add(filed);
            }  
            if(rows.Length>0)
            rows=rows.Remove(rows.Length-1,1);
            //string sql = string.Format("select {0} from {1}", rows, tablename);
            int psize = 0;
            int.TryParse(txtPageSize.Text.Trim(), out psize);
            if (psize == 0)
                gvList.AllowPaging = false;
            else
            {
                gvList.AllowPaging = true;
                gvList.PageSize = psize;
            }
       
            if(chkEdit.Checked || chkDelete.Checked)
            {  
                CommandField colfiled = new CommandField();
                colfiled.ItemStyle.Width = Unit.Pixel(100);
                colfiled.ShowEditButton = chkEdit.Checked;
             
                colfiled.ShowDeleteButton = chkDelete.Checked;
                gvList.Columns.Add(colfiled);
            }
           
            gvList.DataSource = BLL.DataBaseHelper.instance.GetList(tablename, rows, 0, "", "");            
            gvList.DataBind();
            
            
            //gvList.Columns[0].HeaderText = "我是你的";
            //gvList.AutoGenerateColumns = false;
        }
     

        //gvList.DataSource = 
        //gvList.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditArticle.aspx");
    }
    protected void dropTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        string frmname = dropTable.SelectedValue;
        if (frmname != "")
        {         
            #region 表单
            DataTable dt = BLL.DataBaseHelper.instance.GetModel("Form", "*", "FormID=" + frmname);
            if (dt.Rows.Count > 0)
            {
                txtPageSize.Text = dt.Rows[0]["pagesize"].ToString();
                chkAutoColumn.Checked = Convert.ToBoolean(dt.Rows[0]["isnumcol"]);
                chkEdit.Checked = Convert.ToBoolean(dt.Rows[0]["isedit"]);
                chkDelete.Checked = Convert.ToBoolean(dt.Rows[0]["isdelete"]);
                chkUpdate.Checked = Convert.ToBoolean(dt.Rows[0]["isupdate"]);
            }
            #endregion
            BindGvList();
        }
    }

    protected void rptColumnList_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {       
        BindGvList();
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindGvList();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();       
        hs.Add("isnumcol", chkAutoColumn.Checked);
        hs.Add("isedit", chkEdit.Checked);
        hs.Add("isdelete", chkDelete.Checked);
        hs.Add("isupdate", chkUpdate.Checked);
        int pages = 0;
        int.TryParse(txtPageSize.Text.Trim(), out pages);
        hs.Add("pagesize", pages);

        BLL.DataBaseHelper.instance.Update(hs, "Form", "FormID=" + dropTable.SelectedValue);
        BindGvList();
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvList.EditIndex = e.NewEditIndex;
        BindGvList();
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       string keyid= gvList.Rows[e.RowIndex].Cells[0].ToString();
        lbltip.Text = "正在删除!";
        BLL.DataBaseHelper.instance.Delete(tablename, tablekey + "=" + keyid);
    }
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvList.EditIndex = -1;
        BindGvList();
    }
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}
