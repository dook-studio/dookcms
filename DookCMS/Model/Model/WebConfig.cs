using System;
namespace Dukey.Model
{
    [Serializable]
    public class WebConfig
    {
        ///<summary>
        ///
        ///</summary>
        public int ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string sitename { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string title { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string brief { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string keywords { get; set; }

        ///<summary>
        ///
        ///</summary>
        public bool islock { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int templateid { get; set; }

        /// <summary>
        /// 显示方式0=正常,1=url重写,2=生成静态页面
        /// </summary>
        public int showtype { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string cname { get; set; }
        /// <summary>
        /// 模板英文名称
        /// </summary>
        public string ename { get; set; }
        /// <summary>
        /// 模板文件夹
        /// </summary>
        public string folder { get; set; }

        public string coverimg { get; set; }
        public string closepage { get; set; }

    }
}
