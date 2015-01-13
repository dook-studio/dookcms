using System;
using System.Collections.Generic;
using System.Text;
using Dukey.DBUtility;
using System.Data;
using Dukey.IDAL;
using System.Data.OleDb;
using System.Collections;
using System.Diagnostics;
using System.Data.Common;


namespace Dukey.AccessDAL
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public DataBaseHelper() { }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public DataTable GetShemaTable()
        {
            return DbHelper.GetShemaTable();
        }
        public DataTable GetPrimaryKey()
        {
            // 字段名   
            return DbHelper.GetPrimaryKey();
            //return columnTable = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
            //foreach (DataRow dr in columnTable.Rows)
            //{
            //    htPrimaryKey.Add(dr["TABLE_NAME"], dr["COLUMN_NAME"]);
            //}
        }

        public DataTable GetForeignKey()
        {
            return DbHelper.GetForeignKey();
        }
        public int ResetTable(string tablename)
        {
            string sql = string.Format("drop table  {0}", tablename);
            return DbHelper.ExecuteSql(sql);
        }


        public DataTable GetShemaColumnName(string tablename)
        {
            return DbHelper.GetShemaColumnName(tablename);
        }

        public int GetMaxID(string FieldName, string TableName)
        {
            return DbHelper.GetMaxID(FieldName, TableName);
        }

        public int CreateTable(string tablename)
        {
            string sql = string.Format("create table {0} ( id int)", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql);
        }

        public int DeleteTable(string tablename)
        {
            string sql = string.Format("drop table {0} ", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql);
        }

        public DataSet GetList(string tablename, string fieldkey, string showString, int pageSize, int pageIndex, string strWhere, string orderString, out int recordCount, out int pageCount)
        {
            pageCount = 1;
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";
            tablename = tablename.Replace("[", "").Replace("]", "");
            tablename = "[" + tablename + "]";
            string sqltotal = string.Format("select count(0) from {0}", tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " where " + strWhere;
                sqltotal += strWhere;
            }
            recordCount = Convert.ToInt32(DbHelper.GetSingle(sqltotal));
            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            string sql = "";
            if (pageIndex == 1)//第一页
            {
                sql = string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, tablename, strWhere, orderString);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                sql = string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, tablename, "where 1=2", orderString);
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                string recordIDs = recordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, fieldkey, tablename, strWhere, orderString), pageUpperBound);
                sql = string.Format("select {0} from {1} where {4} in ({2}) order by {3} ", showString, tablename, recordIDs, orderString, fieldkey);
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.Query(sql);
        }
        private string recordID(string query, int passCount)
        {

            string result = string.Empty;
            query = StringHelper.ReplaceBadSQL(query);
            using (IDataReader dr = DbHelper.ExecuteReader(query))
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        result += "," + dr.GetInt32(0);
                    }
                    passCount--;
                }
            }
            return result.Substring(1);
        }


        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="hs">hs.Add("表字段","值")</param>
        /// <param name="tableName">表名称</param>
        /// <param name="strWhere">查询条件,不用写where</param>
        /// <returns></returns>
        public int Update(Hashtable hs, string tableName, string strWhere)
        {
            OleDbParameter[] sqlParas = new OleDbParameter[hs.Count];
            string sql = "update [" + tableName + "] set ";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            while (str.MoveNext())
            {
                sqlParas[i] = new OleDbParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
                //if (strWhere.IndexOf(str.Key.ToString()) == -1)
                //{
                sql += ("[" + str.Key.ToString().Replace("[", "").Replace("]", "") + "]" + "=@" + str.Key.ToString().Replace("[", "").Replace("]", "") + ",");
                //}
                i++;
            }

            sql = sql.Remove(sql.Length - 1, 1);
            if (strWhere != "")
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql, sqlParas);
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="hs">hs.Add("表字段","值")</param>
        /// <param name="tableName">表名称</param>   
        /// <returns></returns>
        public int Insert(Hashtable hs, string tableName)
        {
            OleDbParameter[] sqlParas = new OleDbParameter[hs.Count];
            string sql = "insert into [" + tableName + "](";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            string strColumn = "";
            string strValues = "";
            while (str.MoveNext())
            {
                strColumn += "[" + str.Key.ToString().Replace("[", "").Replace("]", "") + "],";
                strValues += "@" + str.Key.ToString().Replace("[", "").Replace("]", "") + ",";
                sqlParas[i] = new OleDbParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
                i++;
            }
            strColumn = strColumn.Remove(strColumn.Length - 1, 1);
            strValues = strValues.Remove(strValues.Length - 1, 1);
            sql += strColumn + ") values(";
            sql += strValues + ")";

            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql, sqlParas);
        }
        #endregion

        #region 获取表单个数据
        public object GetColumnValue(string tablename, string columnName, string strWhere)
        {
            string sql = string.Format("select top 1 {0} from {1}", columnName, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.GetSingle(sql);
        }
        #endregion


        #region 获取单条记录
        public DataTable GetModel(string tablename, string fieldStr, string strWhere)
        {
            if (string.IsNullOrEmpty(fieldStr))
            {
                fieldStr = "*";
            }
            string sql = string.Format("select top 1 {0} from [{1}] ", fieldStr, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            DataSet ds = DbHelper.Query(sql);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion

        #region 获取单条记录
        public object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder, Hashtable hs)
        {
            if (string.IsNullOrEmpty(fieldStr))
            {
                fieldStr = "*";
            }
            string sql = string.Format("select top 1 {0} from [{1}] ", fieldStr, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            if (!string.IsNullOrEmpty(strOrder))
            {
                sql += " order by " + strOrder;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.GetSingle(sql, Hash2Parameter(hs));
        }
        public object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder)
        {
            return GetSingle(tablename, fieldStr, strWhere, strOrder, null);
        }
        #endregion



        public int Delete(string tablename, string strWhere)
        {
            string sql = string.Format("delete from {0} ", tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql);
        }

        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr, Hashtable hs)
        {
            string sql = "select ";
            if (top > 0)
            {
                sql += " top " + top + " ";
            }
            if (!string.IsNullOrEmpty(fieldshow))
            {
                sql += fieldshow;
            }
            else
            {
                sql += " * ";
            }

            sql += " from [" + tablename + "]";
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            if (!string.IsNullOrEmpty(orderstr))
            {
                sql += " order by " + orderstr;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.Query(sql, Hash2Parameter(hs));
        }

        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr)
        {
            return GetList(tablename, fieldshow, top, strWhere, orderstr, null);
        }

        private OleDbParameter[] Hash2Parameter(Hashtable hs) //转换参数
        {
            if (hs != null && hs.Count > 0)
            {
                OleDbParameter[] sqlParas = new OleDbParameter[hs.Count];
                IDictionaryEnumerator str = hs.GetEnumerator();
                int i = 0;
                while (str.MoveNext())
                {
                    sqlParas[i] = new OleDbParameter("@" + str.Key.ToString(), str.Value);
                    i++;
                }
                return sqlParas;
            }
            return null;
        }

        public DataSet GetList(string tablename)
        {
            string sql = "select * from [" + tablename + "]";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.Query(sql);
        }
        public DataSet GetBySql(string sql)
        {
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.Query(sql);
        }
        public int ExecuteSql(string sql, Hashtable hs)
        {
            OleDbParameter[] paras = null;
            int i = 0;
            if (hs != null)
            {
                paras = new OleDbParameter[hs.Count];
                foreach (object objKeys in hs.Keys)
                {
                    paras[i] = new OleDbParameter("@" + objKeys.ToString(), hs[objKeys].ToString());
                    i++;
                }
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.ExecuteSql(sql, paras);
        }

        #region 添加表单数据列
        public void AddFormParas(int formID, string tablename, string columnname)
        {
            string sql = string.Format("select 1 from FormParas where (formID={0} and tablename='{1}' and columnname='{2}')", formID, tablename, columnname);
            sql = StringHelper.ReplaceBadSQL(sql);
            if (DbHelper.GetSingle(sql) == null)
            {
                string sql1 = string.Format("insert into FormParas(formId,[tablename],columnname,cname,para_name,[datatype],[maxlength],[width]) values({0},'{1}','{2}','{2}','{2}','text',50,50)", formID, tablename, columnname);
                sql1 = StringHelper.ReplaceBadSQL(sql1);
                DbHelper.ExecuteSql(sql1);
            }
        }
        #endregion

        #region 重命名表
        public void RenameTableName(string oldname, string newname)
        {
            oldname = StringHelper.ReplaceBadSQL(oldname);
            newname = StringHelper.ReplaceBadSQL(newname);
            ArrayList list = new ArrayList();
            list.Add(string.Format("SELECT   *   INTO   {0}   FROM   {1}", newname, oldname));
            list.Add(string.Format("drop table {0}", oldname));
            DbHelper.ExecuteSqlTran(list);
        }
        #endregion


        /// <summary>
        /// 是否存在数据表
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public bool IsExistTable(string tbname)
        {
            string sql = string.Format("SELECT COUNT(*)  as CNT FROM sqlite_master where type='table' and name='{0}'", tbname);
            object o = DbHelper.GetSingle(sql);
            if (o != null && o.ToString() != "0")
            {
                return true;
            }
            return false;
        }
        public bool AddColumn(string tablename, string colname, string type, bool isnull, bool ispk, string defaultvalue, string desc)
        {
            return false;
        }
        public bool DeleteColumn(string tablename, string colname)
        {
            return false;
        }
        public bool UpdateColumn(string tablename, string colname_old, string colname_new, string type, bool isnull, bool ispk, string defvalue, string length, string desc)
        {
            return false;
        }
    }
}
