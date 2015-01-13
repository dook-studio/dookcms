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
    public class SysTable
    {
        public static readonly SysTable instance = new SysTable();
        private readonly ISysDictionary dal = DataAccess.CreateSysDictionay();
        public Dukey.Model.Dictionary GetModel(int id)
        {
            return dal.GetModel(id);
        }
        public List<Dukey.Model.Dictionary> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }

}
