using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using Common;

public partial class Crm_FetchDataEdit : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        //解析xml
        string id = Request["id"];
        if (StringHelper.isNum(id))
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xmlstr = BLL.DataBaseHelper.instance.GetSingle("collectdata", "xmlstr", "id=" + id, "").ObjToStr();
            if (xmlstr != string.Empty)
            {
                xmlDoc.LoadXml(xmlstr);

                //更新页面中文显示名称
                XmlNode listurls = xmlDoc.SelectSingleNode("//collect/listurls");
                if (listurls != null)
                {
                    txthtmls.Value = xmlDoc.SelectSingleNode("//collect/urlrules").InnerText;
                    txthtmle.Value = xmlDoc.SelectSingleNode("//collect/urlrulee").InnerText;
                    radlagent.SelectedValue = xmlDoc.SelectSingleNode("//collect/isagent").InnerXml;
                    txtCookies.Value = xmlDoc.SelectSingleNode("//collect/cookies").InnerText;
                    txtlisturl.Value = listurls.InnerText.Replace(",", "\r\n");
                    cname.Value = xmlDoc.SelectSingleNode("//collect/cname").InnerXml;
                    radlType.SelectedValue = xmlDoc.SelectSingleNode("//collect/type").InnerXml;
                    radlEncoding.SelectedValue = xmlDoc.SelectSingleNode("//collect/encoding").InnerXml;
                    radlDoorchain.SelectedValue = xmlDoc.SelectSingleNode("//collect/isdoorchain").InnerXml;
                    radllisttype.SelectedValue = xmlDoc.SelectSingleNode("//collect/getlisttype").InnerXml;
                    radlHasThumb.SelectedValue = xmlDoc.SelectSingleNode("//collect/hasthumb").InnerXml;
                    radlDoorchain.SelectedValue = xmlDoc.SelectSingleNode("//collect/isdoorchain").InnerXml;
                    txtbodys.Value = xmlDoc.SelectSingleNode("//collect/bodys").InnerText;
                    txtbodye.Value = xmlDoc.SelectSingleNode("//collect/bodye").InnerText;
                    txtinurl.Value = xmlDoc.SelectSingleNode("//collect/inurl").InnerText;//url包含
                    txtouturl.Value = xmlDoc.SelectSingleNode("//collect/outurl").InnerText;//url不包含
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("cname", StringHelper.ReplaceBadChar(cname.Value));
        hs.Add("type", radlType.SelectedValue);
        hs.Add("encoding", radlEncoding.SelectedValue);

        //生成xml配置文件
        StringBuilder str = new StringBuilder();
        str.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
        str.Append("<collect>\r\n");
        str.AppendFormat("<cname>{0}</cname>", StringHelper.ReplaceBadChar(cname.Value));
        str.AppendFormat("<type>{0}</type>", radlType.SelectedValue);
        str.AppendFormat("<encoding>{0}</encoding>", radlEncoding.SelectedValue);
        str.AppendFormat("<isdoorchain>{0}</isdoorchain>", radlDoorchain.SelectedValue);
        str.AppendFormat("<getlisttype>{0}</getlisttype>", radllisttype.SelectedValue);
        str.AppendFormat("<listurls><![CDATA[{0}]]></listurls>", txtlisturl.Value.Replace("\r\n", ","));
        str.AppendFormat("<urlrules><![CDATA[{0}]]></urlrules>", txthtmls.Value);
        str.AppendFormat("<urlrulee><![CDATA[{0}]]></urlrulee>", txthtmle.Value);
        str.AppendFormat("<hasthumb>{0}</hasthumb>", radlHasThumb.SelectedValue);  
        str.AppendFormat("<isagent>{0}</isagent>", radlagent.SelectedValue);
        str.AppendFormat("<cookies>{0}</cookies>", txtCookies.Value);
        str.AppendFormat("<bodys><![CDATA[{0}]]></bodys>", txtbodys.Value);
        str.AppendFormat("<bodye><![CDATA[{0}]]></bodye>", txtbodye.Value);
        str.AppendFormat("<inurl><![CDATA[{0}]]></inurl>", txtinurl.Value);
        str.AppendFormat("<outurl><![CDATA[{0}]]></outurl>", txtouturl.Value);
        str.Append("</collect>");
        hs.Add("xmlstr", str.ToString());
        
        try
        {
            if (!StringHelper.isNum(Request.QueryString["id"]))
            {
                BLL.DataBaseHelper.instance.Insert(hs, "collectdata");
                Common.MessageBox.ShowAndRedirect(this, "操作成功!", "list.aspx");
            }
            else
            {
                string id = Request.QueryString["id"];
                BLL.DataBaseHelper.instance.Update(hs, "collectdata", "id=" + id);
                Common.MessageBox.ShowAndRedirect(this, "操作成功!", "creaturllist.aspx?id=" + id);
            }

           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}