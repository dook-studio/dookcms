using System;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Data.Common;
namespace Dukey.IDAL
{
    public interface IDataBaseHelper
    {
        DataTable GetShemaTable();
        DataTable GetShemaColumnName(string tablename);
        DataTable GetPrimaryKey();
        DataTable GetForeignKey();
        int CreateTable(string tablename);
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        int DeleteTable(string tablename);
        DataSet GetList(string tablename, string fieldkey, string fieldshow, int pageSize, int pageIndex, string strWhere, string orderstr, out int total, out int pageCount);

        int Insert(Hashtable hs, string tableName);
        int Update(Hashtable hs, string tableName, string strWhere);

        object GetColumnValue(string tablename, string columnName, string strWhere);

        DataTable GetModel(string tablename, string fieldStr, string strWhere);
        int Delete(string tablename, string strWhere);

        DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr);
        DataSet GetList(string tablename, string fieldshow, int top, string strWhere, string orderstr, Hashtable hs);
        DataSet GetList(string tablename);
        void AddFormParas(int formID, string tablename, string columnname);

        int GetMaxID(string FieldName, string TableName);
        int ExecuteSql(string sql, Hashtable hs);
        object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder);
        object GetSingle(string tablename, string fieldStr, string strWhere, string strOrder, Hashtable hs);

        void RenameTableName(string oldname, string newname);//重命名表
        DataSet GetBySql(string sql);

        int ResetTable(string tablename);//重置表

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
        bool IsExistTable(string tbname);
        bool AddColumn(string tablename, string colname, string type, bool isnull, bool ispk, string defaultvalue, string length);
        bool DeleteColumn(string tablename, string colname);
        bool UpdateColumn(string tablename, string colname_old, string colname_new, string type, bool isnull, bool ispk, string defvalue, string length, string desc);
        
    }
}
