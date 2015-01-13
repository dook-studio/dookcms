using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Dukey.Model
{
    [Serializable]
    public class Image
    {
        ///<summary>
        ///编号
        ///</summary>
        public int id { get; set; }

 

        ///<summary>
        ///内容
        ///</summary>
        public string body { get; set; }

        ///<summary>
        ///图片地址
        ///</summary>
        public string picurl { get; set; }

        ///<summary>
        ///版块编号
        ///</summary>
        public int typeid { get; set; }

        ///<summary>
        ///是否显示
        ///</summary>
        public bool isshow { get; set; }

        ///<summary>
        ///点击率
        ///</summary>
        public int click { get; set; }

        ///<summary>
        ///排序值 顺序
        ///</summary>
        public int px { get; set; }

        ///<summary>
        ///文章等级
        ///</summary>
        public int level { get; set; }
        

        ///<summary>
        ///发布时间
        ///</summary>
        public DateTime addtime { get; set; }

        ///<summary>
        ///更新时间
        ///</summary>
        public DateTime uptime { get; set; }

        ///<summary>
        ///标题(列表显示)
        ///</summary>
        public string title { get; set; }

        ///<summary>
        ///剪短标题
        ///</summary>
        public string stitle { get; set; }


        ///<summary>
        ///颜色
        ///</summary>
        public string color { get; set; }

        ///<summary>
        ///标签,关键字,用空格分隔
        ///</summary>
        public string tags { get; set; }

        ///<summary>
        ///link
        ///</summary>
        public string link { get; set; }

        ///<summary>
        ///链接
        ///</summary>
        public string url { get; set; }

        ///<summary>
        ///频道名称
        ///</summary>
        public string cname { get; set; }

        ///<summary>
        ///频道链接地址
        ///</summary>
        public string typeurl { get; set; }
    }
}
