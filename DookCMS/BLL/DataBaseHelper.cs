using System;
using System.Collections.Generic;
using System.Text;
using Dukey.DALFactory;
using System.Data;
using Dukey.IDAL;
using System.Collections;
using System.Data.OleDb;

namespace BLL
{
    public class DataBaseHelper
    {
        private IDataBaseHelper dal;
        public DataBaseHelper()
        {
            switch (Common.MyWeb.DbType)
            {
                case 0: dal = new Dukey.AccessDAL.DataBaseHelper(); break;
                case 1: dal = new Dukey.SQLServerDAL.DataBaseHelper(); break;
                case 2: dal = new Dukey.SQLiteDAL.DataBaseHelper(); break;
            }
        }
        public static readonly DataBaseHelper instance = new DataBaseHelper();
        //private IDataBaseHelper dal = DataAccess.CreateDataBaseHelper();

        //private readonly IDataBaseHelper dal = new Dukey.AccessDAL.DataBaseHelper();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public DataTable GetShemaTable()
        {
            return dal.GetShemaTable();
        }

        /// <summary>
        /// 返回表字段列表
        /// </summary>
        public DataTable GetShemaColumnName(string tablename)
        {
            return dal.GetShemaColumnName(tablename);
        }
        public Hashtable GetPrimaryKeys()
        {
            Hashtable htPrimaryKey = new Hashtable();
            foreach (DataRow dr in GetPrimaryKey().Rows)
            {
                htPrimaryKey.Add(dr["TABLE_NAME"].ToString().ToLower(), dr["COLUMN_NAME"].ToString().ToLower());
            }
            return htPrimaryKey;
        }

        public DataTable GetForeignKey()
        {
            return dal.GetForeignKey();
        }
        public DataTable GetPrimaryKey()
        {
            return dal.GetPrimaryKey();
        }

        public int GetMaxID(string FieldName, string TableName)
        {
            return dal.GetMaxID(FieldName, TableName);
        }

        public int CreateTable(string tablename)
        {
            return dal.CreateTable(tablename);
        }
        public int DeleteTable(string tablename)
        {
            return dal.DeleteTable(tablename);
        }

        public DataSet GetList(string tablename, string fieldkey, string fieldshow, int pageSize, int pageIndex, string strWhere, string orderstr, out int total, out int pageCount)
        {
            return dal.GetList(tablename, fieldkey, fieldshow, pageSize, pageIndex, strWhere, orderstr, out total, out pageCount);
        }
        public DataSet GetList(string tablename)
        {
            return dal.GetList(tablename);
        }


        public int Insert(Hashtable hs, string tableName)
        {
            return dal.Insert(hs, tableName);
        }

        public int Update(Hashtable hs, string tableName, string strWhere)
        {
            return dal.Update(hs, tableName, strWhere);
        }

        /// <summary>
        /// 获取单个表字段值
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="columnName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public object GetColumnValue(string tablename, string columnName, string strWhere)
        {
            return dal.GetColumnValue(tablename, columnName, strWhere);
        }
        public DataSet GetBySql(string sql)
        {
            return dal.GetBySql(sql);
        }
        #region 获取单条记录
        public DataTable GetModel(string tablename, string fieldStr, string strWhere)
        {
            return dal.GetModel(tablename, fieldStr, strWhere);
        }
        #endregion

        #region 获取单条记录
        public DataRowView GetModelView(string tablename, string fieldStr, string strWhere)
        {
            DataTable dt = dal.GetModel(tablename, fieldStr, strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.DefaultView[0];
            }
            return null;
        }
        #endregion

        public DataRowView GetModelViewByCache(string tablename, string fieldStr, string strWhere, string cacheKey)
        {
            object objModel = Common.DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelView(tablename, fieldStr, strWhere);
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache(cacheKey, objModel);
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return objModel as DataRowView;
        }

        public int Delete(string tablename, string strWhere)
        {
            return dal.Delete(tablename, strWhere);
        }
        public int ResetTable(string tablename)
        {
            return dal.ResetTable(tablename);
        }

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldshow">显示的字段,如果为空.则全部显示</param>
        /// <param name="top">默认0</param>
        /// <param name="strWhere">条件</param>
        /// <param name="orderstr">排序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr, Hashtable hs)
        {
            return dal.GetList(tablename, fieldshow, top, strWhere, orderstr, hs);
        }
        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr)
        {
            return dal.GetList(tablename, fieldshow, top, strWhere, orderstr);
        }
        #endregion

        public DataSet GetListByCache(string tablename, string fieldshow, int top, string strWhere, string orderstr, string cachekey, Hashtable hs)
        {

            object objModel = Common.DataCache.GetCache(cachekey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(tablename, fieldshow, top, strWhere, orderstr, hs);
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache(cachekey, objModel);
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return objModel as DataSet;
        }
        public DataSet GetListByCache(string tablename, string fieldshow, int top, string strWhere, string orderstr, string cachekey)
        {
            return GetListByCache(tablename, fieldshow, top, strWhere, orderstr, cachekey, null);
        }
        public DataSet GetListByCache(string tablename, string cachekey)
        {
            object objModel = Common.DataCache.GetCache(cachekey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(tablename);
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache(cachekey, objModel);
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return objModel as DataSet;
        }
        #region 添加表单数据列
        public void AddFormParas(int formID, string tablename, string columnname)
        {
            dal.AddFormParas(formID, tablename, columnname);
        }
        #endregion

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="hs">参数表key value</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, Hashtable hs)
        {
            return dal.ExecuteSql(sql, hs);
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="fieldStr"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder, Hashtable hs)
        {
            return dal.GetSingle(tablename, fieldStr, strWhere, strOrder, hs);
        }
        public object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder)
        {
            return dal.GetSingle(tablename, fieldStr, strWhere, strOrder, null);
        }
        public void RenameTableName(string oldname, string newname)
        {
            dal.RenameTableName(oldname, newname);
        }
        /// <summary>
        /// 是否存在数据表
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public bool IsExistTable(string tbname)
        {
            return dal.IsExistTable(tbname);
        }

        public bool AddColumn(string tablename, string colname, string type, bool isnull, bool ispk, string defaultvalue, string desc)
        {
            return dal.AddColumn(tablename, colname, type, isnull, ispk, defaultvalue, desc);
        }
        public bool DeleteColumn(string tablename, string colname)
        {
            return dal.DeleteColumn(tablename, colname);
        }
        public bool UpdateColumn(string tablename, string colname_old, string colname_new, string type, bool isnull, bool ispk, string defvalue, string length, string desc)
        {
            return dal.UpdateColumn(tablename, colname_old, colname_new, type, isnull, ispk, defvalue, length, desc);
        }
    }
}
