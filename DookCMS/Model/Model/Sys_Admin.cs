using System;
namespace Dukey.Model
{
	/// <summary>
	/// 实体类Sys_Admin 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Sys_Admin
	{
		public Sys_Admin()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _password;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		#endregion Model

	}
}

