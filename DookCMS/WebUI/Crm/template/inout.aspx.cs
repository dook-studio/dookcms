using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Common;
using System.Text;
using System.Xml;
using System.Collections;

public partial class template_data_inout : System.Web.UI.Page
{
    public string title = "导入数据包";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["id"].IsEmpty())
            {
                title = "编辑表单";
            }
        }
    }
    protected void btnCreateXML_Click(object sender, EventArgs e)//生成xml
    {
        //生成栏目表数据
        StringBuilder str = new StringBuilder();
        str.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
        str.Append("<config>\r\n");
        GetTableXmlData(ref str, "webconfig");
        GetTableXmlData(ref str, "dictionary");
        GetTableXmlData(ref str, "channel");
        GetTableXmlData(ref str, "ad");
        GetTableXmlData(ref str, "adtype");
        str.Append("</config>\r\n");
        txtData.Value = str.ToString();
        Common.MessageBox.Show(this, "成功生成数据xml!");
        //FileHelper.WriteFile(Server.MapPath("~/templets/data.xml"), str.ToString(), "utf-8");
    }

    private void GetTableXmlData(ref StringBuilder str, string tablename)
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetList(tablename);
        if (ds.Tables.Count > 0)
        {
            str.Append("<" + tablename + ">\r\n");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                str.Append("\r\n<item>\r\n");
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    str.Append("<" + col.ColumnName + ">");
                    str.Append("<![CDATA[" + item[col.ColumnName].ObjToStr() + "]]>");
                    str.Append("</" + col.ColumnName + ">\r\n");
                }
                str.Append("</item>");
            }
            str.Append("\r\n</" + tablename + ">\r\n");
        }
    }
    protected void btnImportData_Click(object sender, EventArgs e)//导入数据库
    {
        string xml = txtData.Value.Trim();
        if (string.IsNullOrEmpty(xml))
        {
            Common.MessageBox.Show(this, "文本框为空!");
            return;
        }
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml); //加载XML文档

            XmlNode root = xmlDoc.SelectSingleNode("//config");
            InsertTableData(xmlDoc, "webconfig");
            InsertTableData(xmlDoc, "dictionary");
            InsertTableData(xmlDoc, "channel");
            InsertTableData(xmlDoc, "ad");
            InsertTableData(xmlDoc, "adtype");
            Common.MessageBox.Show(this, "成功导入数据库!");
        }
        catch (Exception ex)
        {
            Common.MessageBox.Show(this, ex.Message);
        }
    }

    private static void InsertTableData(XmlDocument xmlDoc, string tablename)
    {
        //清理数据表
        //插入之前清理数据库
        BLL.DataBaseHelper.instance.Delete(tablename, "");

        XmlNodeList list = xmlDoc.SelectNodes("//config/" + tablename + "/item");
        foreach (XmlNode item in list)
        {
            Hashtable hs = new Hashtable();
            foreach (XmlNode citem in item.ChildNodes)
            {
                hs.Add(citem.Name, citem.InnerText);
            }
            BLL.DataBaseHelper.instance.Insert(hs, tablename);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string xml = txtData.Value.Trim();
        if (string.IsNullOrEmpty(xml))
        {
            Common.MessageBox.Show(this, "文本框不能为空!");
            return;
        }
        string folder = BLL.DataBaseHelper.instance.GetSingle("Template", "folder", "id=" + Request["id"], "").ToString();
        FileHelper.WriteFile(Server.MapPath("~/templets/" + folder + "/data.xml"), txtData.Value.Trim(), "utf-8");
        Common.MessageBox.Show(this, "成功导出到xml!");
    }
    protected void btnLoadxml_Click(object sender, EventArgs e)//加载xml
    {
        string folder = BLL.DataBaseHelper.instance.GetSingle("Template", "folder", "id=" + Request["id"], "").ToString();
        if (!System.IO.File.Exists(Server.MapPath("~/templets/" + folder + "/data.xml")))
        {
            Common.MessageBox.Show(this, "没有找到data.xml!");
            return;
        }
        txtData.Value = FileHelper.ReadFile(Server.MapPath("~/templets/" + folder + "/data.xml"), "utf-8");
        Common.MessageBox.Show(this, "成功加载xml!");
    }
}