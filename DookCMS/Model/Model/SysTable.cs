using System;
using System.Collections.Generic;
using System.Text;

namespace Dukey.Model
{
  /// <summary>
	/// 实体类Sys_Admin 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public class Sys_Dictionary
    {
        public int id { get; set; }
        public string keytext { get; set; }
        public string keyvalue { get; set; }
        public int px { get; set; }
        public int pid { get; set; }
        public DateTime addtime { get; set; }
        public DateTime updatetime { get; set; }
        public string remark { get; set; }
    }
}
