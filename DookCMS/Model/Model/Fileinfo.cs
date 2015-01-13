using System;
using System.Collections.Generic;
using System.Text;

namespace Dukey.Model
{
    [Serializable]
    public class Fileinfo
    {
        public string filetype { get; set; }
        public string filename { get; set; }
        public string filesize { get; set; }
        public string updatetime { get; set; }
        public string url { get; set; }
    }
}
