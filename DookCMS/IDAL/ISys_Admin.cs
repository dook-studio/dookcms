using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// �ӿڲ�ISys_Admin ��ժҪ˵����
	/// </summary>
	public interface ISys_Admin
	{
		#region  ��Ա����
	
        /// <summary>
        /// ��¼��̨.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>��¼ID</returns>
        object IsLoginAdmin(string username, string password);
		#endregion  ��Ա����
	}
}
