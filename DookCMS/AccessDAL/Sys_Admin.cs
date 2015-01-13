using System;
using System.Collections.Generic;
using System.Text;
using Dukey.IDAL;
using Dukey.DBUtility;
using System.Data;
using System.Data.OleDb;

namespace Dukey.AccessDAL
{
    /// <summary>
    /// 数据访问类Photos。
    /// </summary>
    public class Sys_Admin : ISys_Admin
    {
        public object IsLoginAdmin(string username, string password)
        {
            OleDbParameter[] paras =
            {
                new OleDbParameter("@UserName",username),
                new OleDbParameter("@Password",password)
            };

            string sql = "select [ID] from [Sys_Admin] where UserName=@UserName and Password=@Password";
            return DbHelper.GetSingle(sql, paras);
        }


    }

}
