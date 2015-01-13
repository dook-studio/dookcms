using System;
using System.Collections.Generic;
using System.Text;

namespace Dukey.Model
{
    [Serializable]
    public class Channel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int bid { get; set; }
        /// <summary>
        /// 中文版块名称
        /// </summary>
        public string cname { get; set; }
        /// <summary>
        /// 英文版块名称
        /// </summary>
        public string ename { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public int fid { get; set; }
        /// <summary>
        /// 层级关系,如:3,20
        /// </summary>
        public string xpath { get; set; }
        /// <summary>
        /// 子类数目
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 链接路径
        /// </summary>
        public string linkto { get; set; }
        /// <summary>
        /// 栏目内容
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 生成静态页面链接地址
        /// </summary>
        public string linktohtml { get; set; }
        /// <summary>
        /// 显示方式,
        /// </summary>
        public int showid { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int px { get; set; }
        /// <summary>
        /// 网页标题
        /// </summary>
        public string seotitle { get; set; }
        /// <summary>
        /// seo关键字
        /// </summary>
        public string seokeywords { get; set; }
        /// <summary>
        /// seo描述
        /// </summary>
        public string seodesc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 0=不显示,1=显示
        /// </summary>
        public bool isshow { get; set; }
        /// <summary>
        /// 是否是主版块
        /// </summary>
        public bool ismain { get; set; }
        /// <summary>
        /// addtime
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 是否生成静态页面
        /// </summary>
        public bool iscreatehtml { get; set; }
        /// <summary>
        /// contentlink
        /// </summary>
        public string contentlink { get; set; }

        public string link { get; set; }

        /// <summary>
        /// 栏目类型 0=封面,1=列表,2=外链
        /// </summary>
        public int channeltype { get; set; }

        /// <summary>
        /// 文章生成规则
        /// </summary>
        public string itemrule { get; set; }

        /// <summary>
        /// 模型编号.0=文章,1=商品,2=图片集.3=分类信息,4=软件
        /// </summary>
        public int patternid { get; set; }

    } 
}
