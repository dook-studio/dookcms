using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

public partial class upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            // Get the data
            string file_id = Request.QueryString["file_id"] as string;
            HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];

            //string photobid = Request.Cookies["photobid"].Value;          
            string UploadPath = Server.MapPath("~/upload/photos/" + DateTime.Now.ToString("yyyy\\/MM\\/dd")) + "/";
            string httpPath = "/upload/photos/" + DateTime.Now.ToString("yyyy\\/MM\\/dd") + "/";

            if(!Directory.Exists(UploadPath))
                Directory.CreateDirectory(UploadPath);
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

            int thisWidth = originalImage.Width;
            int thisHeight = originalImage.Height;

            int solidW =thisWidth>670?670:thisWidth;
            int solidH =thisHeight>503?503:thisHeight;
            string mode="W";
            mode = thisWidth>thisHeight ? "W" : "H";


            string picId = DateTime.Now.ToString("yyMMddHHssfff");
            string filename = picId + ".jpg";
            StringHelper.MakeThumbnail(originalImage, filename, UploadPath, solidW, solidH,mode);

            
            ////生成直方图500*500
            //System.Drawing.Image bigImage = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
            //string upBigPath = Server.MapPath("/upload/") + MyWeb.instance.userId + "/pic/";
            //if (!Directory.Exists(upBigPath))
            //    Directory.CreateDirectory(upBigPath);
            //int solidW2 = thisWidth > 500 ? 500 : thisWidth;
            //int solidH2 = thisHeight > 500 ? 500 : thisHeight;           
            //StringHelper.MakeThumbnail(bigImage, filename, upBigPath, solidW2, solidH2, mode);

            ////生成缩略图85*85
            //System.Drawing.Image thumbImage = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
            //string upThumbPath = Server.MapPath("/upload/") + MyWeb.instance.userId + "/thumb/";
            //if (!Directory.Exists(upThumbPath))
            //    Directory.CreateDirectory(upThumbPath);

            //int solidW3 = thisWidth > 85 ? 85 : thisWidth;
            //int solidH3 = thisHeight > 85 ? 85 : thisHeight;            
            //StringHelper.MakeThumbnail(thumbImage, filename, upThumbPath, solidW3, solidH3, mode); 

            ////查询记录
            //string[] paras = new string[7];
            //paras[0] = picId;
            //paras[1] = MyWeb.instance.userId;
            //paras[2] = jpeg_image_upload.FileName;
            //paras[3] = filename;
            //paras[4] = "";
            //paras[5] = "";
            //paras[6] = albumId;
            //BOL.Photos.instance.Insert(paras);

            //}
            //    Session.Clear();
            //}
            int photobid =0;
            if(Request.Cookies["photobid"]!=null)
            {
                int.TryParse(Request.Cookies["photobid"].Value,out photobid);
            }
           
            Dukey.Model.Photos model = new Dukey.Model.Photos();
            model.title = jpeg_image_upload.FileName;
            model.brief = "";
            model.imgurl = httpPath + filename;
            model.bid = photobid;
            model.isshow = true;
            model.dots = 0;
            model.px = 0;
            model.iscomment = true;
            model.addtime = DateTime.Now;
            model.uptime = DateTime.Now;
            model.adminId = BaseWeb.instance.AdminId;
            model.userId = 0;
            new BLL.Photos().Add(model);


        }
        catch (Exception ex)
        {
            // If any kind of error occurs return a 500 Internal Server error
            Response.StatusCode = 500;
            Response.Write(ex.ToString());
            Response.End();
        }
		
	}
}
//public class Thumbnail
//{
//    public Thumbnail(string file_id, byte[] data)
//    {
//        this.FileID = file_id;
//        this.Data = data;
//    }


//    private string file_id;
//    public string FileID
//    {
//        get
//        {
//            return this.file_id;
//        }
//        set
//        {
//            this.file_id = value;
//        }
//    }

//    private byte[] thumbnail_data;
//    public byte[] Data
//    {
//        get
//        {
//            return this.thumbnail_data;
//        }
//        set
//        {
//            this.thumbnail_data = value;
//        }
//    }


//}
