<%@ WebHandler Language="C#" Class="imageManager" %>
/**
 * Created by visual studio2010
 * User: xuheng
 * Date: 12-3-7
 * Time: 下午16:29
 * To change this template use File | Settings | File Templates.
 */
using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Linq;

public class imageManager : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string[] paths = { "/upload" }; //需要遍历的目录列表，最好使用缩略图地址，否则当网速慢时可能会造成严重的延时
        if (context.Session["UserId"] != null)
        {
            paths=new string[]{"/upload/user/"+context.Session["UserId"]};
        }
      
        
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                //文件允许格式

        string action = context.Server.HtmlEncode(context.Request["action"]);

        if (action == "get")
        {
            String str = String.Empty;

            foreach (string path in paths)
            {
                DirectoryInfo info = new DirectoryInfo(context.Server.MapPath(path));
                List<FileInfo> list=new List<FileInfo>();
                getAllFiles(context.Server.MapPath(path),ref list);
                 var sortedList = list.OrderByDescending(a => a.CreationTime);
                //这个时候会排序
                foreach (FileInfo fi in sortedList.ToList())
                {
                    if (Array.IndexOf(filetype, fi.Extension) != -1)
                    {                      
                       string dir = context.Server.MapPath(path);
                        string gg=fi.FullName.Replace(dir,"").Replace(@"\","/");
                        str +=path+ gg + "ue_separate_ue";
                        //这里遍历不全,还有缺陷钟健2013年7月4日
                        //str += path + "/" + item.Parent.Parent.Name + "/" + item.Parent.Name + "/" + item.Name + "/" + fi.Name + "ue_separate_ue";
                    }
                }
            }

            context.Response.Write(str);
        }
    }

    private void GetAllDir(DirectoryInfo rootdir, ref System.Collections.Generic.List<DirectoryInfo> list)
    {
        DirectoryInfo[] dirs = rootdir.GetDirectories();
        if (dirs.Length > 0)
        {
            foreach (var item in dirs)
            {
                list.Add(item);
                GetAllDir(item, ref list);
            }
        }
    }    
    public void getAllFiles(string directory,ref List<System.IO.FileInfo> list) //获取指定的目录中的所有文件（包括文件夹）
    {
        getFiles(directory,ref list);//获取指定的目录中的所有文件（不包括文件夹）
        getDirectory(directory,ref list);//获取指定的目录中的所有目录（文件夹）
    }
        
    public void getFiles(string directory,ref List<System.IO.FileInfo> list) //获取指定的目录中的所有文件（不包括文件夹）
    {
        string[] path = System.IO.Directory.GetFiles(directory);
        FileInfo[] fs=new DirectoryInfo(directory).GetFiles();
        foreach (FileInfo item in fs)
	    {
		     list.Add(item);
	    }
    }

    public void getDirectory(string directory, ref List<System.IO.FileInfo> list) //获取指定的目录中的所有目录（文件夹）
    {
        string[] directorys = System.IO.Directory.GetDirectories(directory);
        if (directorys.Length <= 0) //如果该目录总没有其他文件夹
            return;
        else
        {
            for (int i = 0; i < directorys.Length; i++)
                getAllFiles(directorys[i],ref list);
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}