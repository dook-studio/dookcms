using System;
using System.Collections.Generic;
using System.Text;

namespace Dukey.Model
{
    [Serializable]
    public class FormParas
    {

        ///<summary>
        ///
        ///</summary>
        public int ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        public DateTime addtime { get; set; }

        ///<summary>
        ///
        ///</summary>
        public DateTime updatetime { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int px { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string htmlstr { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string defaultvalue { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int formID { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string tablename { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string columnname { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string cname { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string para_name { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string datatype { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int maxlength { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int width { get; set; }
    } 
}
