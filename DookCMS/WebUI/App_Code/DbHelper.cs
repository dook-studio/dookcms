using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
namespace MdbWatch
{
    /// <summary>
    /// 2010年3月18日 zj添加
    /// 数据访问基础类(基于OleDb)
    /// </summary>
    /// 
    public abstract class DbHelper
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        //public static readonly string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath("~/DataFile/lsclient.mdb") + "";              

        public static readonly string webstr = System.Configuration.ConfigurationManager.ConnectionStrings["strWeb"].ConnectionString;

        #region 公用方法

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName;
            object obj = DbHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public static bool Exists(string strSql)
        {
            object obj = DbHelper.GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Exists(string strSql, params OleDbParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.OleDb.OleDbException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OleDbConnection conn = new OleDbConnection(webstr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                OleDbTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand(SQLString, connection);
                System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@content", OleDbType.VarChar);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);
                System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@fs", OleDbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(string strSQL)
        {
            OleDbConnection connection = new OleDbConnection(webstr);
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);
            try
            {
                connection.Open();
                OleDbDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                connection.Close();
                throw new Exception(e.Message);
            }
            finally
            {
                //connection.Close();
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                    command.Fill(ds);
                }
                catch (System.Data.OleDb.OleDbException ex)
                {
                    connection.Close();
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.OleDb.OleDbException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OleDbParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OleDbConnection conn = new OleDbConnection(webstr))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    OleDbCommand cmd = new OleDbCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OleDbParameter[] cmdParms = (OleDbParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(string SQLString, params OleDbParameter[] cmdParms)
        {
            OleDbConnection connection = new OleDbConnection(webstr);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OleDbDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                connection.Close();
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 对表的操作方法
        /// <summary>
        /// 返回Mdb数据库中所有表信息
        /// </summary>
        public static DataTable GetShemaTable()
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, "", null);
                DataTable shemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                return shemaTable;
            }
        }
        /// <summary>
        /// 返回主键
        /// </summary>
        public static DataTable GetPrimaryKey()
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, "", null);
                DataTable shemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
                return shemaTable;
            }
        }

        public static Hashtable GetPrimaryKeys()
        {
            Hashtable htPrimaryKey = new Hashtable();
            foreach (DataRow dr in GetPrimaryKey().Rows)
            {
                htPrimaryKey.Add(dr["TABLE_NAME"].ToString().ToLower(), dr["COLUMN_NAME"].ToString().ToLower());
            }
            return htPrimaryKey;
        }
        /// <summary>
        /// 返回外键
        /// </summary>
        public static DataTable GetForeignKey()
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, "", null);
                DataTable shemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
                return shemaTable;
            }
        }
        /// <summary>
        /// 返回Mdb数据库中所有表表名
        /// </summary>
        public static DataTable GetShemaColumnName(string tablename)
        {
            using (OleDbConnection connection = new OleDbConnection(webstr))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, "", null);
                return connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tablename, null });
            }
        }

        public static int CreateTable(string tablename)
        {
            string sql = string.Format("create table {0}(id AUTOINCREMENT '自增编号',userid int default 3,addtime datetime default now(),PRIMARY KEY (id))", tablename);         
            return DbHelper.ExecuteSql(sql);
        }

        public static int DeleteTable(string tablename)
        {
            string sql = string.Format("drop table {0} ", tablename);
            return DbHelper.ExecuteSql(sql);
        }

        public static DataSet GetList(string tablename, string fieldkey, string showString, int pageSize, int pageIndex, string strWhere, string orderString, out int recordCount, out int pageCount)
        {
            pageCount = 1;
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";

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

            return DbHelper.Query(sql);
        }
        private static string recordID(string query, int passCount)
        {

            string result = string.Empty;
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
        public static int Update(Hashtable hs, string tableName, string strWhere)
        {
            OleDbParameter[] sqlParas = new OleDbParameter[hs.Count];
            string sql = "update [" + tableName + "] set ";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            while (str.MoveNext())
            {
                sqlParas[i] = new OleDbParameter("@" + str.Key.ToString(), str.Value);
                //if (strWhere.IndexOf(str.Key.ToString()) == -1)
                //{
                sql += ("" + str.Key + "" + "=@" + str.Key.ToString() + ",");
                //}
                i++;
            }

            sql = sql.Remove(sql.Length - 1, 1);
            if (strWhere != "")
            {
                sql += " where " + strWhere;
            }
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
        public static int Insert(Hashtable hs, string tableName)
        {
            OleDbParameter[] sqlParas = new OleDbParameter[hs.Count];
            string sql = "insert into [" + tableName + "](";
            IDictionaryEnumerator str = hs.GetEnumerator();
            int i = 0;
            string strColumn = "";
            string strValues = "";
            while (str.MoveNext())
            {
                strColumn += "[" + str.Key.ToString() + "],";
                strValues += "@" + str.Key.ToString() + ",";
                sqlParas[i] = new OleDbParameter("@" + str.Key.ToString(), str.Value);
                i++;
            }
            strColumn = strColumn.Remove(strColumn.Length - 1, 1);
            strValues = strValues.Remove(strValues.Length - 1, 1);
            sql += strColumn + ") values(";
            sql += strValues + ")";

            return DbHelper.ExecuteSql(sql, sqlParas);
        }
        #endregion

        #region 获取表单个数据
        public static object GetColumnValue(string tablename, string columnName, string strWhere)
        {
            string sql = string.Format("select top 1 {0} from {1}", columnName, tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            return DbHelper.GetSingle(sql);
        }
        #endregion


        #region 获取单条记录
        public static DataTable GetModel(string tablename, string fieldStr, string strWhere)
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
            DataSet ds = DbHelper.Query(sql);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion

        #region 获取单条记录
        public static object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder)
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
            return DbHelper.GetSingle(sql);
        }
        #endregion



        public static int Delete(string tablename, string strWhere)
        {
            string sql = string.Format("delete from {0} ", tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += " where " + strWhere;
            }
            return DbHelper.ExecuteSql(sql);
        }

        public static DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr)
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
            //sql = StringHelper.ReplaceBadSQL(sql);
            return DbHelper.Query(sql);
        }

        public static DataSet GetList(string tablename)
        {
            string sql = "select * from [" + tablename + "]";
            return DbHelper.Query(sql);
        }
        public static int ExecuteSql(string sql, Hashtable hs)
        {
            OleDbParameter[] paras = new OleDbParameter[hs.Count];
            int i = 0;
            foreach (object objKeys in hs.Keys)
            {
                paras[i] = new OleDbParameter("@" + objKeys.ToString(), hs[objKeys].ToString());
                i++;
            }

            return DbHelper.ExecuteSql(sql, paras);
        }

        #region 添加表单数据列
        public static void AddFormParas(int formID, string tablename, string columnname)
        {
            string sql = string.Format("select 1 from FormParas where (formID={0} and tablename='{1}' and columnname='{2}')", formID, tablename, columnname);
            if (DbHelper.GetSingle(sql) == null)
            {
                string sql1 = string.Format("insert into FormParas(formId,[tablename],columnname,cname,para_name,[datatype],[maxlength],[width]) values({0},'{1}','{2}','{2}','{2}','text',50,50)", formID, tablename, columnname);
                DbHelper.ExecuteSql(sql1);
            }
        }
        #endregion

        #region 重命名表
        public static void RenameTableName(string oldname, string newname)
        {
            ArrayList list = new ArrayList();
            list.Add(string.Format("SELECT   *   INTO   {0}   FROM   {1}", newname, oldname));
            list.Add(string.Format("drop table {0}", oldname));
            DbHelper.ExecuteSqlTran(list);
        }
        #endregion
        #endregion


        
    }
}


