using System;
namespace Dukey.Model
{
    /// <summary>
    /// 实体类Photos 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Photos
    {
        public Photos()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _brief;
        private string _imgurl;
        private int _bid;
        private bool _isshow;
        private int _dots;
        private int _px;
        private bool _iscomment;
        private DateTime _addtime;
        private DateTime _uptime;
        private int _adminid;
        private int _userid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgurl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int bid
        {
            set { _bid = value; }
            get { return _bid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isshow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int dots
        {
            set { _dots = value; }
            get { return _dots; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int px
        {
            set { _px = value; }
            get { return _px; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool iscomment
        {
            set { _iscomment = value; }
            get { return _iscomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime uptime
        {
            set { _uptime = value; }
            get { return _uptime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int adminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        #endregion Model

    }
}

