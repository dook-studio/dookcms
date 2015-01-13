using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using Dukey.DBUtility;
using ADOX;


public partial class Crm_EditColumn : BaseCrm
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!StringHelper.IsBadChar(Request.QueryString["column_name"]))
            {
                txtColumnName.Text = Request.QueryString["column_name"];
                string tablename = Request.QueryString["tablename"];
                DataTable dt = new BLL.DataBaseHelper().GetShemaColumnName(tablename);
                if (dt.Rows.Count > 0)
                {
                    DataView dtv = dt.DefaultView;
                    dtv.RowFilter = string.Format("COLUMN_NAME='{0}'", txtColumnName.Text);
                    if (dtv.Count == 0) return;
                    DataRowView drs = dtv[0];
                    dropType.SelectedValue = "TEXT";
                    txtDefaultValue.Text = drs["column_default"].ToString();
                    chkIsNull.Checked = drs["is_nullable"].ToString().ToLower() == "true" ? true : false;
                    txtLength.Text = drs["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    txtDesc.Text = drs["description"].ToString();
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
            string tablename = Request.QueryString["tablename"];
            string columnName = StringHelper.ReplaceBadChar(txtColumnName.Text);
            string type = dropType.SelectedValue;
            string column_length = StringHelper.ReplaceBadChar(txtLength.Text);
            string description = txtDesc.Text.Trim();
            if (column_length != string.Empty)
            {
                column_length = "(" + column_length + ")";
            }
            string sql = "";
            if (string.IsNullOrEmpty(Request.QueryString["column_name"]))
            {
                sql = string.Format("alter table [{0}] add column [{1}] {2} {3}", tablename, columnName, type, column_length);
                if (!chkIsNull.Checked)
                {
                    sql += " not null";
                }
                 if (txtDefaultValue.Text.Trim() != string.Empty)
                 {
                     sql += " default " + StringHelper.ReplaceBadChar(txtDefaultValue.Text) + "";
                 }
            }
            else
            {
                sql = string.Format("alter table  [{0}]  rename column [{1}] to '{2}'", tablename, Request.QueryString["column_name"], columnName);
            }

            Response.Write(sql);
            DbHelper.ExecuteSql(sql);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script>OK();</script>");
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
        trLength.Visible = true;
        trDefaultValue.Visible = true;

        switch (dropType.SelectedValue)
        {
            case "TEXT":
                {
                    break;
                }
            case "COUNTER":
                {
                    txtDefaultValue.Text = "";
                    trLength.Visible = false;
                    trDefaultValue.Visible = false;
                    break;
                }
            default:
                {
                    txtLength.Text = "";
                    trLength.Visible = false;
                    break;
                }
        }
    }
}
