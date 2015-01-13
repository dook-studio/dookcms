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

public class imageManager : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string[] paths = { "/upload" }; //需要遍历的目录列表，最好使用缩略图地址，否则当网速慢时可能会造成严重的延时
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                //文件允许格式

        string action = context.Server.HtmlEncode(context.Request["action"]);

        if (action == "get")
        {
            String str = String.Empty;

            foreach (string path in paths)
            {
                DirectoryInfo info = new DirectoryInfo(context.Server.MapPath(path));
                if (info.Exists)
                {
                    System.Collections.Generic.List<DirectoryInfo> list = new System.Collections.Generic.List<DirectoryInfo>();
                    GetAllDir(info, ref list);
                    foreach (DirectoryInfo item in list)
                    {
                        foreach (FileInfo fi in item.GetFiles())
                        {
                            if (Array.IndexOf(filetype, fi.Extension) != -1)
                            {
                                //这里遍历不全,还有缺陷钟健2013年7月4日
                                str += path + "/" + item.Parent.Parent.Name + "/" + item.Parent.Name + "/" + item.Name + "/" + fi.Name + "ue_separate_ue";
                            }
                        }
                    }
                    //本目录的文件
                    foreach (FileInfo fi in info.GetFiles())
                    {
                        if (Array.IndexOf(filetype, fi.Extension) != -1)
                        {
                            str += path + "/" + fi.Name + "ue_separate_ue";
                        }
                    }

                }
                //目录验证
                //if (info.Exists)
                //{
                //    DirectoryInfo[] infoArr = info.GetDirectories();
                //    foreach (DirectoryInfo tmpInfo in infoArr)
                //    {
                //        foreach (FileInfo fi in tmpInfo.GetFiles())
                //        {
                //            if (Array.IndexOf(filetype, fi.Extension) != -1)
                //            {
                //                str += path+"/" + tmpInfo.Name + "/" + fi.Name + "ue_separate_ue";
                //            }
                //        }
                //    }
                //}
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


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}