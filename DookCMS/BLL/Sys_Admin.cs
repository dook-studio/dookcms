using System;
using System.Collections.Generic;
using System.Text;
using Dukey.IDAL;
using Dukey.DBUtility;
using System.Data;
using System.Data.OleDb;
using Dukey.DALFactory;

namespace BLL
{
    /// <summary>
    /// 数据访问类Photos。
    /// </summary>
    public class Sys_Admin
    {
        private readonly ISys_Admin dal = DataAccess.CreateSys_Admin();
        public object IsLoginAdmin(string username, string password)
        {
            return dal.IsLoginAdmin(username, password);
        }
    }

}
