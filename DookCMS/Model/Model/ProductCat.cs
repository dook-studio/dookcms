using System;
using System.Collections.Generic;
using System.Text;

namespace Dukey.Model
{
    [Serializable]
    public class ProductCat
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 中文版块名称
        /// </summary>
        public string cname { get; set; }
      
        /// <summary>
        /// 父级编号
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 层级关系,如:3,20
        /// </summary>
        public string xpath { get; set; }
    } 
}
