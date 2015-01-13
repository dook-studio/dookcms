using System;
using System.Collections.Generic;
using System.Text;
using Dukey.IDAL;
using Dukey.DBUtility;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Dukey.SQLiteDAL
{
    /// <summary>
    /// 数据访问类Photos。
    /// </summary>
    public class SysDictionary : ISysDictionary
    {
        public static SysDictionary instance = new SysDictionary();

        public Dukey.Model.Dictionary GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,pid,keytext,keyvalue,px,addtime,updatetime,remark from Sys_Dictionary ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Dukey.Model.Dictionary model = new Dukey.Model.Dictionary();

            SqlDataReader dr = DbHelperSQL.ExecuteReader(strSql.ToString(),parameters);
            if (dr.Read())
            {
                if (dr["id"].ToString() != "")
                {
                    model.id = int.Parse(dr["id"].ToString());
                }
                model.keytext = dr["keytext"].ToString();
                model.keyvalue = dr["keyvalue"].ToString();
                if (dr["px"].ToString() != "")
                {
                    model.px = int.Parse(dr["px"].ToString());
                }
                if (dr["pid"].ToString() != "")
                {
                    model.pid = int.Parse(dr["pid"].ToString());
                }
                if (dr["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(dr["addtime"].ToString());
                }
                if (dr["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(dr["updatetime"].ToString());
                }
                dr.Dispose();
                return model;
            }
            else
            {
                dr.Dispose();
                return null;
            }
        }

        public List<Dukey.Model.Dictionary> GetList(string strWhere)
        {
            List<Dukey.Model.Dictionary> list = new List<Dukey.Model.Dictionary>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  id,pid,keytext,keyvalue,px,addtime,updatetime,remark from Sys_Dictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by px");
            SqlDataReader dr = DbHelperSQL.ExecuteReader(strSql.ToString());
            while (dr.Read())
            {
                Dukey.Model.Dictionary model = new Dukey.Model.Dictionary();
                if (dr["id"].ToString() != "")
                {
                    model.id = int.Parse(dr["id"].ToString());
                }
                model.keytext = dr["keytext"].ToString();
                model.keyvalue = dr["keyvalue"].ToString();
                if (dr["px"].ToString() != "")
                {
                    model.px = int.Parse(dr["px"].ToString());
                }
                if (dr["pid"].ToString() != "")
                {
                    model.pid = int.Parse(dr["pid"].ToString());
                }
                if (dr["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(dr["addtime"].ToString());
                }
                if (dr["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(dr["updatetime"].ToString());
                }
                list.Add(model);
            }
            dr.Dispose();
            return list;
        }
    }
}
