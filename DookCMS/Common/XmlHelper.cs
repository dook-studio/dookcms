﻿//======================================================================
//
//        Copyright(C) 2009-2010 连林SoftWare工作室    
//        All Rights Reserved
//
//        FileName: XMLHelper 
//        Description: XML文档帮助类,静态方法,实现对
//                     XML文档的创建,及节点和属性的增、删、改、查
//
//          Author: Wang Lian Lin(王连林)
//          CLR版本: 2.0.50727.42
//        MachineName: WLL
//          注册组织名: WLL
//        Created By Wang Lian Lin(王连林) at 2009-3-22 16:50:44 
//        Email: LianLin.Wang@163.com
//        http://chnboy.cnblogs.com
//
//======================================================================
using System;
using System.Xml;

/**/
/// <summary>
/// XMLHelper XML文档操作管理器
/// </summary>
public class XMLHelper
{
    public XMLHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    //XML文档节点查询和读取#region XML文档节点查询和读取
    /**/
    /// <summary>
    /// 选择匹配XPath表达式的第一个节点XmlNode.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
    /// <returns>返回XmlNode</returns>
    public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            return xmlNode;
        }
        catch (Exception ex)
        {
            return null;
            //throw ex; //这里可以定义你自己的异常处理
        }
    }

    /**/
    /// <summary>
    /// 选择匹配XPath表达式的节点列表XmlNodeList.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
    /// <returns>返回XmlNodeList</returns>
    public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);
            return xmlNodeList;
        }
        catch (Exception ex)
        {
            return null;
            //throw ex; //这里可以定义你自己的异常处理
        }
    }

    /**/
    /// <summary>
    /// 选择匹配XPath表达式的第一个节点的匹配xmlAttributeName的属性XmlAttribute.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <returns>返回xmlAttributeName</returns>
    public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
    {
        string content = string.Empty;
        XmlDocument xmlDoc = new XmlDocument();
        XmlAttribute xmlAttribute = null;
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.Count > 0)
                {
                    xmlAttribute = xmlNode.Attributes[xmlAttributeName];
                }
            }
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return xmlAttribute;
    }
    //#endregion

    //XML文档创建和节点或属性的添加、修改#region XML文档创建和节点或属性的添加、修改
    /**/
    /// <summary>
    /// 创建一个XML文档
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="rootNodeName">XML文档根节点名称(须指定一个根节点名称)</param>
    /// <param name="version">XML文档版本号(必须为:"1.0")</param>
    /// <param name="encoding">XML文档编码方式</param>
    /// <param name="standalone">该值必须是"yes"或"no",如果为null,Save方法不在XML声明上写出独立属性</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
    {
        bool isSuccess = false;
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(xmlDeclaration);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlFileName);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /**/
    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建它的子节点(如果此节点已存在则追加一个新的同名节点
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <param name="value">属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //存不存在此节点都创建
                XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                subElement.InnerXml = innerText;

                //如果属性和值参数都不为空则在此新节点上新增属性
                if (!string.IsNullOrEmpty(xmlAttributeName) && !string.IsNullOrEmpty(value))
                {
                    XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                    xmlAttribute.Value = value;
                    subElement.Attributes.Append(xmlAttribute);
                }

                xmlNode.AppendChild(subElement);
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    public static void CreateNodeByXpath(string xmlFileName, string xpath)
    {
         XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFileName); //加载XML文档
        XmlNode xmlnode = xmlDoc.SelectSingleNode(xpath);
        if(xmlnode==null)
        {
            //创建父节点
            //CreateNodeByXpath(xmlFileName
           //创建本节点
            //XmlElement subElement = xmlDoc.CreateElement(xpath.Substring(xpath.LastIndexOf("/"), xpath.Length - xpath.LastIndexOf("/")));
            //XmlNode xmlnode = xmlDoc.SelectSingleNode(xpath.Remove(subElement.Name));
            //xmlNode.AppendChild(subElement);
        }
            
    }
    /**/
    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建或更新它的子节点(如果节点存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
    {
        bool isSuccess = false;
        bool isExistsNode = false;//标识节点是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点下的所有子节点
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (node.Name.ToLower() == xmlNodeName.ToLower())
                    {
                        //存在此节点则更新
                        node.InnerXml = innerText;
                        isExistsNode = true;
                        break;
                    }
                }
                if (!isExistsNode)
                {
                    //不存在此节点则创建
                    XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;
                    xmlNode.AppendChild(subElement);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /**/
    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建或更新它的属性(如果属性存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <param name="value">属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
    {
        bool isSuccess = false;
        bool isExistsAttribute = false;//标识属性是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                    {
                        //节点中存在此属性则更新
                        attribute.Value = value;
                        isExistsAttribute = true;
                        break;
                    }
                }
                if (!isExistsAttribute)
                {
                    //节点中不存在此属性则创建
                    XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                    xmlAttribute.Value = value;
                    xmlNode.Attributes.Append(xmlAttribute);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }
    //#endregion

    // XML文档节点或属性的删除#region XML文档节点或属性的删除
    /**/
    /// <summary>
    /// 删除匹配XPath表达式的第一个节点(节点中的子元素同时会被删除)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //删除节点
                xmlNode.ParentNode.RemoveChild(xmlNode);
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /**/
    /// <summary>
    /// 删除匹配XPath表达式的第一个节点中的匹配参数xmlAttributeName的属性
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要删除的xmlAttributeName的属性名称</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
    {
        bool isSuccess = false;
        bool isExistsAttribute = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            XmlAttribute xmlAttribute = null;
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                    {
                        //节点中存在此属性
                        xmlAttribute = attribute;
                        isExistsAttribute = true;
                        break;
                    }
                }
                if (isExistsAttribute)
                {
                    //删除节点中的属性
                    xmlNode.Attributes.Remove(xmlAttribute);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /**/
    /// <summary>
    /// 删除匹配XPath表达式的第一个节点中的所有属性
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                xmlNode.Attributes.RemoveAll();
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }
    //#endregion

}
