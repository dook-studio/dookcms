using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Collections;
namespace Dukey.DBUtility
{

    public class DataOp
    {
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
                if (strWhere.IndexOf(str.Key.ToString()) == -1)
                {
                    sql += ("" + str.Key + "" + "=@" + str.Key.ToString() + ",");
                }
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
    }

}