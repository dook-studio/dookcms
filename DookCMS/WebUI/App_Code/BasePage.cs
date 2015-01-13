using System;
using System.Collections.Generic;
using System.Web;
using Dukey.Engine;
using System.Text;
using System.IO;
using System.Web.SessionState;
using System.Diagnostics;
using System.Collections;
using Common;
/// <summary>
/// 页面基类
/// </summary>
public abstract class BasePage : IHttpHandler, IRequiresSessionState
{
    #region IHttpHandler 成员
    /// <summary>
    /// 
    /// </summary>
    public bool IsReusable
    {
        get { return false; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        this.InitContext(context);

        //输出数据
        this.LoadCurrentTemplate();
        this.InitPageTemplate();

        #region 生成静态页面
        if (!string.IsNullOrEmpty(Request.QueryString["cpage"]))
        {
            string cpage = Request.QueryString["cpage"];
            switch (cpage)
            {
                case "channel"://生成栏目.
                    CreateChannelHTML();
                    break;
                case "article"://生成文章
                    CreateArticleHTML();
                    break;
                case "product"://生成产品页
                    break;
            }
        }
        else
        {
            this.Document.Render(this.Response.Output);
        }
        #endregion

    }

    #region 生成文章
    private void CreateArticleHTML()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["aid"]))//生成文章
        {
            lock (this)
            {
                try
                {
                    string filehtm = Request.QueryString["filehtm"];
                    string aid = Request.QueryString["aid"];

                    if (!Directory.Exists(Server.MapPath(filehtm)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filehtm));
                    }
                    string file = filehtm + aid + ".html";
                    if (!File.Exists(Server.MapPath(file)))
                    {
                        using (StreamWriter sw = File.CreateText(Server.MapPath(file)))
                        {
                            //sw.WriteLine("/*新文件建立于 " + DateTime.Now + "*/");
                        }
                    }
                    System.Threading.Thread.Sleep(500);
                    this.Document.RenderTo(Server.MapPath(file));
                    Hashtable hs = new Hashtable();
                    hs.Add("link", file);
                    BLL.DataBaseHelper.instance.Update(hs, "article", "id=" + Request.QueryString["aid"]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
    #endregion

    #region 生成栏目
    private void CreateChannelHTML()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["bid"]))//生成文章
        {
            lock (this)
            {
                try
                {
                    string file = Request.QueryString["filehtm"];
                    file = "/" + file + ".html";
                    if (!File.Exists(Server.MapPath(file)))
                    {
                        using (StreamWriter sw = File.CreateText(Server.MapPath(file)))
                        {
                            //sw.WriteLine("/*新文件建立于 " + DateTime.Now + "*/");
                        }
                    }
                    System.Threading.Thread.Sleep(500);
                    this.Document.RenderTo(Server.MapPath(file));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
    #endregion
    protected HttpContext Context { get; private set; }
    protected HttpApplicationState Application { get; private set; }
    protected HttpRequest Request { get; private set; }
    protected HttpResponse Response { get; private set; }
    protected HttpServerUtility Server { get; private set; }
    protected HttpSessionState Session { get; private set; }

    /// <summary>
    /// 初始化上下文数据
    /// </summary>
    /// <param name="context"></param>
    private void InitContext(HttpContext context)
    {
        this.Context = context;
        this.Application = context.Application;
        this.Request = context.Request;
        this.Response = context.Response;
        this.Server = context.Server;
        this.Session = context.Session;
    }
    #endregion



    /// <summary>
    /// 当前页面的模板文档对象
    /// </summary>
    protected TemplateDocument Document
    {
        get;
        private set;
    }
    /// <summary>
    /// 当前页面的模板文档的配置参数
    /// </summary>
    protected virtual TemplateDocumentConfig DocumentConfig
    {
        get
        {
            return TemplateDocumentConfig.Default;
        }
    }
    /// <summary>
    /// 是否读取缓存模板
    /// </summary>
    protected virtual bool IsLoadCacheTemplate
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// 装载当前页面的模板文档
    /// </summary>
    public virtual void LoadCurrentTemplate()
    {
        //string fileName = Path.GetFileNameWithoutExtension(this.Request.FilePath);
        this.LoadTemplateFile(this.Server.MapPath("~/template/default/index.htm"));
    }
    /// <summary>
    /// 装载模板文件
    /// </summary>
    /// <param name="fileName"></param>
    protected virtual void LoadTemplateFile(string fileName)
    {
        this.Document = null;       
        try
        {
            this.Document = TemplateDocument.FromFileCache(fileName, Encoding.UTF8, this.DocumentConfig);
        }
        catch
        {
            this.Document = new TemplateDocument(fileName, Encoding.UTF8, this.DocumentConfig);
        }
    }

    /// <summary>
    /// 初始化当前页面模板数据
    /// </summary>
    protected abstract void InitPageTemplate();
}

