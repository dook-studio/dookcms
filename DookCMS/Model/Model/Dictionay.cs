
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Dukey.Model
{
    [Serializable]
    public class Dictionary
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
