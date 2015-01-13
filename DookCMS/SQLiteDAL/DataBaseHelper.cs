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
using System.Data.SQLite;


namespace Dukey.SQLiteDAL
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public DataBaseHelper() { }
        /// <summary>
        /// 获取数据库所有数据表
        /// </summar>y
        public DataTable GetShemaTable()
        {
            //return DbHelperSQLite.GetShemaTable();
            return DbHelperSQLite.Query("select * from sqlite_master where type='table' and name!='sqlite_sequence' order by name").Tables[0];
            //return null;
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
            string sql = string.Format("delete from {0} ;DELETE FROM sqlite_sequence WHERE name='{0}';", tablename);
            return DbHelperSQLite.ExecuteSql(sql);
        }


        public DataTable GetShemaColumnName(string tablename)
        {
            //tablename = StringHelper.ReplaceBadSQL(tablename);
            //return DbHelperSQLite.ExecuteTable("GetShemaColumnName", new SQLiteParameter[] { new SQLiteParameter("@tablename", tablename) });
            string sql = "pragma table_info ('" + tablename + "') ";
            return DbHelperSQLite.Query(sql, null).Tables[0];
        }

        public int GetMaxID(string FieldName, string TableName)
        {
            FieldName = StringHelper.ReplaceBadSQL(FieldName);
            TableName = StringHelper.ReplaceBadSQL(TableName);
            return DbHelperSQLite.GetMaxID(FieldName, TableName);
        }

        public int CreateTable(string tablename)
        {
            string sql = string.Format("create table {0} ( id int)", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.ExecuteSql(sql);
        }

        public int DeleteTable(string tablename)
        {
            string sql = string.Format("drop table {0} ", tablename);
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.ExecuteSql(sql);
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
            string sqltotal = string.Format("select count(1) as mytotal from {0}", tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " where " + strWhere;
                sqltotal += strWhere;
            }
            recordCount = Convert.ToInt32(DbHelperSQLite.GetSingle(sqltotal));
            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            string sql = "";
            if (pageIndex == 1)//第一页
            {
                sql = string.Format("select  {1} from {2} {3} order by {4} limit {0}", pageSize, showString, tablename, strWhere, orderString);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                sql = string.Format("select 1 from {0} {1}", tablename, "where 1=2");
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                sql = string.Format("select {0} from {1} {2} order by {3} limit {4},{5} ", showString, tablename, strWhere, orderString, pageUpperBound, pageSize);
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.Query(sql);
        }

        private string recordID(string query, int passCount)
        {
            query = StringHelper.ReplaceBadSQL(query);
            string result = string.Empty;
            using (IDataReader dr = DbHelperSQLite.ExecuteReader(query))
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
            SQLiteParameter[] sqlParas = new SQLiteParameter[hs.Count];
            string sql = "update [" + tableName + "] set ";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            while (str.MoveNext())
            {
                sqlParas[i] = new SQLiteParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
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
            return DbHelperSQLite.ExecuteSql(sql, sqlParas);
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
            SQLiteParameter[] sqlParas = new SQLiteParameter[hs.Count];
            string sql = "insert into [" + tableName + "](";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            string strColumn = "";
            string strValues = "";
            while (str.MoveNext())
            {
                strColumn += "[" + str.Key.ToString().Replace("[", "").Replace("]", "") + "],";
                strValues += "@" + str.Key.ToString().Replace("[", "").Replace("]", "") + ",";
                sqlParas[i] = new SQLiteParameter("@" + str.Key.ToString().Replace("[", "").Replace("]", ""), str.Value);
                i++;
            }
            strColumn = strColumn.Remove(strColumn.Length - 1, 1);
            strValues = strValues.Remove(strValues.Length - 1, 1);
            sql += strColumn + ") values(";
            sql += strValues + ")";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.ExecuteSql(sql, sqlParas);
        }
        #endregion

        #region 获取表单个数据
        public object GetColumnValue(string tablename, string columnName, string strWhere)
        {
            string sql = string.Format("select  {0} from {1} ", columnName, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql += " limit 1";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.GetSingle(sql);
        }
        #endregion


        #region 获取单条记录
        public DataTable GetModel(string tablename, string fieldStr, string strWhere)
        {
            if (string.IsNullOrEmpty(fieldStr))
            {
                fieldStr = "*";
            }
            string sql = string.Format("select {0} from [{1}] ", fieldStr, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            sql += " limit 1";
            sql = StringHelper.ReplaceBadSQL(sql);
            DataSet ds = DbHelperSQLite.Query(sql);
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
            string sql = string.Format("select  {0} from [{1}] ", fieldStr, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            if (!string.IsNullOrEmpty(strOrder))
            {
                sql += " order by " + strOrder;
            }
            sql += " limit 1";
            sql = StringHelper.ReplaceBadSQL(sql);
            object o = DbHelperSQLite.GetSingle(sql, Hash2Parameter(hs));
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
            return DbHelperSQLite.ExecuteSql(sql);
        }

        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr, Hashtable hs)
        {
            string sql = "select ";
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
            if (top > 0)
            {
                sql += " limit " + top + " ";
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.Query(sql, Hash2Parameter(hs));
        }

        public DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr)
        {
            return GetList(tablename, fieldshow, top, strWhere, orderstr, null);
        }

        public DataSet GetList(string tablename)
        {
            string sql = "select * from [" + tablename + "]";
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.Query(sql);
        }



        #region 添加表单数据列
        public void AddFormParas(int formID, string tablename, string columnname)
        {
            string sql = string.Format("select 1 from FormParas where (formID={0} and tablename='{1}' and columnname='{2}')", formID, tablename, columnname);
            sql = StringHelper.ReplaceBadSQL(sql);
            if (DbHelperSQLite.GetSingle(sql) == null)
            {
                string sql1 = string.Format("insert into FormParas(formId,[tablename],columnname,paraId,para_name,[datatype],[maxlength],[width]) values({0},'{1}','{2}','{2}','{2}','text',50,50)", formID, tablename, columnname);
                sql1 = StringHelper.ReplaceBadSQL(sql1);
                DbHelperSQLite.ExecuteSql(sql1);
            }
        }
        #endregion

        public int ExecuteSql(string sql, Hashtable hs)
        {
            SQLiteParameter[] paras = null;
            int i = 0;
            if (hs != null)
            {
                paras = new SQLiteParameter[hs.Count];
                foreach (object objKeys in hs.Keys)
                {
                    paras[i] = new SQLiteParameter("@" + objKeys.ToString(), hs[objKeys].ToString());
                    i++;
                }
            }
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.ExecuteSql(sql, paras);
        }

        #region 重命名表
        public void RenameTableName(string oldname, string newname)
        {
            //使用adox
            string sql = string.Format("alter table {0} rename to {1}", oldname, newname);
            sql = StringHelper.ReplaceBadSQL(sql);
            DbHelperSQLite.ExecuteSql(sql);
        }
        #endregion

        public DataSet GetBySql(string sql)
        {
            sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelperSQLite.Query(sql);
        }

        private SQLiteParameter[] Hash2Parameter(Hashtable hs) //转换参数
        {
            if (hs != null && hs.Count > 0)
            {
                SQLiteParameter[] sqlParas = new SQLiteParameter[hs.Count];
                IDictionaryEnumerator str = hs.GetEnumerator();
                int i = 0;
                while (str.MoveNext())
                {
                    sqlParas[i] = new SQLiteParameter("@" + str.Key.ToString(), str.Value);
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
            string sql = string.Format("SELECT COUNT(1)  as CNT FROM sqlite_master where type='table' and name='{0}'", tbname);
            object o = DbHelperSQLite.GetSingle(sql);
            if (o != null && o.ToString() != "0")
            {
                return true;
            }
            return false;
        }

        public bool AddColumn(string tablename, string colname, string type, bool isnull, bool ispk, string defaultvalue, string length)
        {
            string isempty = string.Empty;
            if (isnull == false) isempty = "NOT NULL";
            string pk = string.Empty;
            if (ispk) { pk = "PRIMARY KEY AUTOINCREMENT "; isempty = "NOT NULL"; }
            if (defaultvalue != string.Empty) defaultvalue = "DEFAULT " + defaultvalue;
            if (int.Parse(length) > 0) type = type + "(" + length + ")";
            string sql = string.Format("ALTER TABLE {0} ADD COLUMN {1} {2} {3} {4} {5}", tablename, colname, type, pk, isempty, defaultvalue);
            int i = DbHelperSQLite.ExecuteSql(sql);
            if (i > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除表字段
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="colname"></param>
        /// <returns></returns>
        public bool DeleteColumn(string tablename, string colname)
        {
            DataTable dt = GetShemaColumnName(tablename);
            string sql = string.Format("alter table {0} rename to {1};", tablename, "tmp_" + tablename);
            sql += "CREATE TABLE " + tablename + " (";
            string cols = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["name"].ToString().ToLower() != colname.ToLower())
                {

                    string pk = string.Empty;
                    if (dt.Rows[i]["pk"].ToString() == "1") pk = "PRIMARY KEY AUTOINCREMENT NOT NULL";
                    string defaultvalue = string.Empty;
                    if (dt.Rows[i]["dflt_value"].ToString() != "") defaultvalue = "DEFAULT " + dt.Rows[i]["dflt_value"].ToString();
                    if (i == 0)
                    {
                        sql += dt.Rows[i]["name"] + " " + dt.Rows[i]["type"] + " " + pk + " " + defaultvalue;
                        cols += dt.Rows[i]["name"].ToString();
                    }
                    else
                    {
                        sql += "," + dt.Rows[i]["name"] + " " + dt.Rows[i]["type"] + " " + pk + " " + defaultvalue;
                        cols += "," + dt.Rows[i]["name"].ToString();
                    }

                }
            }
            sql += ");";
            sql += "INSERT INTO " + tablename + " (" + cols + ")  SELECT " + cols + " FROM tmp_" + tablename + ";";
            sql += "drop table tmp_" + tablename + ";";
            sql += "UPDATE  sqlite_sequence SET seq = 0 WHERE name = '" + tablename + "';";
            int o = DbHelperSQLite.ExecuteSql(sql);
            if (o > 0)
            {
                return true;
            }
            return false;
        }

        //更新表列
        public bool UpdateColumn(string tablename, string colname_old, string colname_new, string type, bool isnull, bool ispk, string defvalue, string length, string desc)
        {
            DataTable dt = GetShemaColumnName(tablename);
            string sql = string.Format("alter table {0} rename to {1};", tablename, "tmp_" + tablename);
            sql += "CREATE TABLE " + tablename + " (";
            string cols = string.Empty;
            string colsold = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["name"].ToString().ToLower() != colname_old.ToLower())
                {

                    string pk = string.Empty;
                    if (dt.Rows[i]["pk"].ToString() == "1") pk = "PRIMARY KEY AUTOINCREMENT NOT NULL";
                    string defaultvalue = string.Empty;
                    if (dt.Rows[i]["dflt_value"].ToString() != "") defaultvalue = "DEFAULT " + dt.Rows[i]["dflt_value"].ToString();

                    if (i == 0)
                    {
                        sql += dt.Rows[i]["name"] + " " + dt.Rows[i]["type"] + " " + pk + " " + defaultvalue;
                        colsold += dt.Rows[i]["name"].ToString();
                        cols += dt.Rows[i]["name"].ToString();
                    }
                    else
                    {
                        sql += "," + dt.Rows[i]["name"] + " " + dt.Rows[i]["type"] + " " + pk + " " + defaultvalue;
                        colsold += "," + dt.Rows[i]["name"].ToString();
                        cols += "," + dt.Rows[i]["name"].ToString();
                    }
                }
                else
                {
                    string isempty = string.Empty;
                    if (isnull == false) isempty = "NOT NULL";
                    string pk = string.Empty;
                    if (ispk) { pk = "PRIMARY KEY AUTOINCREMENT "; isempty = "NOT NULL"; }
                    if (defvalue != string.Empty) defvalue = "DEFAULT " + defvalue;
                    if (int.Parse(length) > 0) type = type + "(" + length + ")";
                    if (i == 0)
                    {
                        sql += colname_new + " " + type + " " + pk + " " + isempty + " " + defvalue;
                        colsold += dt.Rows[i]["name"].ToString();
                        cols += colname_new;
                    }
                    else
                    {
                        sql += "," + colname_new+ " " + type + " " + pk + " " + " " + isempty + " " + defvalue;
                        colsold += "," + dt.Rows[i]["name"].ToString();
                        cols += "," + colname_new;
                    }
                }
            }
            sql += ");";
            sql += "INSERT INTO " + tablename + " (" + cols + ")  SELECT " + colsold + " FROM tmp_" + tablename + ";";
            sql += "drop table tmp_" + tablename + ";";
            int o = DbHelperSQLite.ExecuteSql(sql);
            if (o > 0)
            {
                return true;
            }
            return false;
        }
    }
}
