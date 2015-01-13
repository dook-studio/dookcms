using System;
using System.Collections.Generic;
using System.Text;
using Dukey.DBUtility;
using System.Data;
using Dukey.IDAL;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Data.Common;


namespace Dukey.SQLServerDAL
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public DataBaseHelper() { }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public DataTable GetShemaTable()
        {
            return DbHelperSQL.GetShemaTable();
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


        public DataTable GetShemaColumnName(string tablename)
        {
            tablename = StringHelper.ReplaceBadSQL(tablename);
            return DbHelperSQL.ExecuteTable("GetShemaColumnName", new SqlParameter[] { new SqlParameter("@tablename", tablename) });
        }

        public int GetMaxID(string FieldName, string TableName)
        {
            FieldName = StringHelper.ReplaceBadSQL(FieldName);
            TableName = StringHelper.ReplaceBadSQL(TableName);
            return DbHelperSQL.GetMaxID(FieldName, TableName);
        }

        public int CreateTable(string tablename)
        {
            string sql = string.Format("create table {0} ( id int)", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.ExecuteSql(sql);
        }
        public int ResetTable(string tablename)
        {
            string sql = string.Format("truncata table  {0}", tablename);
            return DbHelperSQL.ExecuteSql(sql);
        }
        public int DeleteTable(string tablename)
        {
            string sql = string.Format("drop table {0} ", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.ExecuteSql(sql);
        }

        public DataSet GetList(string tablename, string fieldkey, string showString, int pageSize, int pageIndex, string strWhere, string orderString, out int recordCount, out int pageCount)
        {
            pageCount = 1;
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Sql",SqlDbType.VarChar),//0
                new SqlParameter("@Order",SqlDbType.VarChar),//1
                new SqlParameter("@CurrentPage",SqlDbType.Int),//2
                new SqlParameter("@PageSize",SqlDbType.Int),//3          
                new SqlParameter("@TotalCount",SqlDbType.Int)//8                    
            };
            string sql = "select " + showString + " from " + tablename;
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            parameters[0].Value = sql;
            parameters[1].Value = orderString;
            parameters[2].Value = pageIndex;
            parameters[3].Value = pageSize;
            parameters[4].Direction = ParameterDirection.Output;
            DataSet dr = DbHelperSQL.ExecuteDataSet("MyPageList", parameters);
            recordCount = Convert.ToInt32(parameters[4].Value.ToString());
            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            return dr;
        }
        private string recordID(string query, int passCount)
        {
            query = StringHelper.ReplaceBadSQL(query);
            string result = string.Empty;
            using (IDataReader dr = DbHelperSQL.ExecuteReader(query))
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
            SqlParameter[] sqlParas = new SqlParameter[hs.Count];
            string sql = "update [" + tableName + "] set ";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            while (str.MoveNext())
            {
                sqlParas[i] = new SqlParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
                //if (strWhere.IndexOf(str.Key.ToString()) == -1)
                //{
                sql += ("" + str.Key + "" + "=@" + str.Key.ToString().Replace("[", "").Replace("]", "") + ",");
                //}
                i++;
            }

            sql = sql.Remove(sql.Length - 1, 1);
            if (strWhere != "")
            {
                sql += " where " + strWhere;
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.ExecuteSql(sql, sqlParas);
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
            SqlParameter[] sqlParas = new SqlParameter[hs.Count];
            string sql = "insert into [" + tableName + "](";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            string strColumn = "";
            string strValues = "";
            while (str.MoveNext())
            {
                strColumn += "[" + str.Key.ToString().Replace("[", "").Replace("]", "") + "],";
                strValues += "@" + str.Key.ToString().Replace("[", "").Replace("]", "") + ",";
                sqlParas[i] = new SqlParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
                i++;
            }
            strColumn = strColumn.Remove(strColumn.Length - 1, 1);
            strValues = strValues.Remove(strValues.Length - 1, 1);
            sql += strColumn + ") values(";
            sql += strValues + ")";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.ExecuteSql(sql, sqlParas);
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
            return DbHelperSQL.GetSingle(sql);
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
            DataSet ds = DbHelperSQL.Query(sql);
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
            object o = DbHelperSQL.GetSingle(sql, Hash2Parameter(hs));
            if (o != null)
                return o;
            return "";
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
            return DbHelperSQL.ExecuteSql(sql);
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
            return DbHelperSQL.Query(sql, Hash2Parameter(hs));
        }

        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr)
        {
            return GetList(tablename, fieldshow, top, strWhere, orderstr, null);
        }

        public DataSet GetList(string tablename)
        {
            string sql = "select * from [" + tablename + "]";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.Query(sql);
        }



        #region 添加表单数据列
        public void AddFormParas(int formID, string tablename, string columnname)
        {
            string sql = string.Format("select 1 from FormParas where (formID={0} and tablename='{1}' and columnname='{2}')", formID, tablename, columnname);
            sql = StringHelper.ReplaceBadSQL(sql);
            if (DbHelperSQL.GetSingle(sql) == null)
            {
                string sql1 = string.Format("insert into FormParas(formId,[tablename],columnname,paraId,para_name,[datatype],[maxlength],[width]) values({0},'{1}','{2}','{2}','{2}','text',50,50)", formID, tablename, columnname);
                sql1 = StringHelper.ReplaceBadSQL(sql1);
                DbHelperSQL.ExecuteSql(sql1);
            }
        }
        #endregion

        public int ExecuteSql(string sql, Hashtable hs)
        {
            SqlParameter[] paras = null;
            int i = 0;
            if (hs != null)
            {
                paras = new SqlParameter[hs.Count];
                foreach (object objKeys in hs.Keys)
                {
                    paras[i] = new SqlParameter("@" + objKeys.ToString(), hs[objKeys].ToString());
                    i++;
                }
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.ExecuteSql(sql, paras);
        }

        #region 重命名表
        public void RenameTableName(string oldname, string newname)
        {
            //使用adox
            string sql = string.Format("exec sp_rename  '{0}','{1}'", oldname, newname);
            sql = StringHelper.ReplaceBadSQL(sql);
            DbHelperSQL.ExecuteSql(sql);
        }
        #endregion

        public DataSet GetBySql(string sql)
        {
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQL.Query(sql);
        }

        private SqlParameter[] Hash2Parameter(Hashtable hs) //转换参数
        {
            if (hs != null && hs.Count > 0)
            {
                SqlParameter[] sqlParas = new SqlParameter[hs.Count];
                IDictionaryEnumerator str = hs.GetEnumerator();
                int i = 0;
                while (str.MoveNext())
                {
                    sqlParas[i] = new SqlParameter("@" + str.Key.ToString(), str.Value);
                    i++;
                }
                return sqlParas;
            }
            return null;
        }

        /// <summary>
        /// 是否存在数据表
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public bool IsExistTable(string tbname)
        {
            string sql = string.Format("SELECT COUNT(*)  as CNT FROM sqlite_master where type='table' and name='{0}'", tbname);
            object o = DbHelperSQL.GetSingle(sql);
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
