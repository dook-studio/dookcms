using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// 接口层ISys_Admin 的摘要说明。
	/// </summary>
	public interface ISys_Admin
	{
		#region  成员方法
	
        /// <summary>
        /// 登录后台.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>登录ID</returns>
        object IsLoginAdmin(string username, string password);
		#endregion  成员方法
	}
}
