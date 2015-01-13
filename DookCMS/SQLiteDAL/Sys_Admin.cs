using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Dukey.IDAL;
using Dukey.DBUtility;
using System.Data.SQLite;//请先添加引用
namespace Dukey.SQLiteDAL
{
	/// <summary>
	/// 数据访问类Sys_Admin。
	/// </summary>
	public class Sys_Admin:ISys_Admin
	{
        public object IsLoginAdmin(string username, string password)
        {
            SQLiteParameter[] paras =
            {
                new SQLiteParameter("@UserName",username),
                new SQLiteParameter("@Password",password)
            };
            string sql = "select [ID] from Sys_Admin where (UserName=@UserName and Password=@Password) limit 1";
            return DbHelperSQLite.GetSingle(sql, paras);
        }
	}
}

