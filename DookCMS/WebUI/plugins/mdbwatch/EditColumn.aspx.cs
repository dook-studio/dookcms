using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using MdbWatch;


public partial class Mdbw_EditColumn : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!StringHelper.IsBadChar(Request.QueryString["colname"]))
            {
                txtColumnName.Text = Request.QueryString["colname"];
                string tablename = Request.QueryString["tbname"];
                DataTable dt =BLL.DataBaseHelper.instance.GetShemaColumnName(tablename);
                if (dt.Rows.Count > 0)
                {
                    DataView dtv = dt.DefaultView;
                    dtv.RowFilter = string.Format("name='{0}'", txtColumnName.Text);
                    if (dtv.Count == 0) return;
                    DataRowView drs = dtv[0];
                    
                    //dropType.SelectedValue = "TEXT";
                    txtDefaultValue.Text = drs["dflt_value"].ToString();
                    chkIsNull.Checked = drs["notnull"].ToString().ToLower() == "0" ? true : false;
                    txtLength.Text = string.Empty;
                    string type = drs["type"].ToString();
                    int ls=type.IndexOf("(");
                    int le=type.IndexOf(")");
                    if(ls>0)
                    {
                        txtLength.Text = type.Substring(ls+1, le - ls-1);                    
                    }                    
                    txtDesc.Text = string.Empty;
                }
                BindShowType();
            }
        }
    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //提交
        try
        {
            string tablename = Request.QueryString["tbname"];
            string columnName = StringHelper.ReplaceBadChar(txtColumnName.Text);
            string type = dropType.SelectedValue;
            string column_length = StringHelper.ReplaceBadChar(txtLength.Text);
            int collength=0;
            int.TryParse(column_length, out collength);
            string defaultvalue=txtDefaultValue.Text;
            string description = txtDesc.Text.Trim();
            bool isnull = chkIsNull.Checked;
            bool ispk = false;
            if (string.IsNullOrEmpty(Request.QueryString["colname"]))
            {
                //增加
                BLL.DataBaseHelper.instance.AddColumn(tablename, columnName, type, isnull, ispk, defaultvalue, column_length);
            }
            else
            {
                
                string colname_old = Request.QueryString["colname"];
                BLL.DataBaseHelper.instance.UpdateColumn(tablename, colname_old, columnName, type, isnull, ispk, defaultvalue, column_length, description);
            }
            //AdoxHelper ado = new AdoxHelper();
            //bool allowEmpty = chkIsNull.Checked;
            //if (string.IsNullOrEmpty(Request.QueryString["colname"]))
            //{          
            //    ado.ColumnAdd(tablename, columnName, (AdoxHelper.ColType)int.Parse(dropType.SelectedValue), collength, defaultvalue, description,allowEmpty);
            //}
            //else
            //{                
            //    ado.ColumnUpdate(tablename, columnName, (AdoxHelper.ColType)int.Parse(dropType.SelectedValue), collength, defaultvalue, description,allowEmpty);
            //}           
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script>Finish();</script>");
        }
        catch (Exception ex)
        {
            throw ex;
            //Common.MessageBox.Show(this, ex.Message);
        }
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindShowType();
    }

    private void BindShowType()
    {
        //trLength.Visible = false;  
        //switch (dropType.SelectedValue)
        //{
        //    case "2":
        //        {
        //            trLength.Visible = true;
        //            break;
        //        }         
        //}
    }
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
            {
                Response.Write("非法访问!");
                Response.End();
                return;
            }
        }
    }
}
