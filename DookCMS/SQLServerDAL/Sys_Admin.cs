using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Dukey.IDAL;
using Dukey.DBUtility;//请先添加引用
namespace Dukey.SQLServerDAL
{
	/// <summary>
	/// 数据访问类Sys_Admin。
	/// </summary>
	public class Sys_Admin:ISys_Admin
	{
        public object IsLoginAdmin(string username, string password)
        {
            SqlParameter[] paras =
            {
                new SqlParameter("@UserName",username),
                new SqlParameter("@Password",password)
            };
            string sql = "select [ID] from [Sys_Admin] where UserName=@UserName and Password=@Password";
            return DbHelperSQL.GetSingle(sql, paras);
        }
	}
}

