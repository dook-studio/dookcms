using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using Dukey.DBUtility;

namespace BLL
{
    /// <summary>
    /// 版块表
    /// </summary>
    public class WebConfig
    {
        public static readonly WebConfig instance = new WebConfig();

       #region  成员方法
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Dukey.Model.WebConfig model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebConfig set ");
            strSql.Append("sitename=@sitename,");
            strSql.Append("title=@title,");
            strSql.Append("brief=@brief,");
            strSql.Append("keywords=@keywords,");
            strSql.Append("islock=@islock,");
            strSql.Append("showtype=@showtype");

            OleDbParameter[] parameters = {
					new OleDbParameter("@sitename", OleDbType.VarChar,255),
					new OleDbParameter("@title", OleDbType.VarChar,255),
					new OleDbParameter("@brief", OleDbType.VarChar,255),
					new OleDbParameter("@keywords", OleDbType.VarChar,255),
					new OleDbParameter("@islock", OleDbType.Boolean,1),
					new OleDbParameter("@showtype", OleDbType.Integer,4)
                                          };
            parameters[0].Value = model.sitename;
            parameters[1].Value = model.title;
            parameters[2].Value = model.brief;
            parameters[3].Value = model.keywords;
            parameters[4].Value = model.islock;
            parameters[5].Value = model.showtype;

            DbHelper.ExecuteSql(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Dukey.Model.WebConfig GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select top 1 * from viewMyWeb ");
			Dukey.Model.WebConfig model=new Dukey.Model.WebConfig();
			DataSet ds=DbHelper.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.sitename=ds.Tables[0].Rows[0]["sitename"].ToString();
				model.title=ds.Tables[0].Rows[0]["title"].ToString();
				model.brief=ds.Tables[0].Rows[0]["brief"].ToString();
				model.keywords=ds.Tables[0].Rows[0]["keywords"].ToString();
				if(ds.Tables[0].Rows[0]["islock"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["islock"].ToString()=="1")||(ds.Tables[0].Rows[0]["islock"].ToString().ToLower()=="true"))
					{
						model.islock=true;
					}
					else
					{
						model.islock=false;
					}
				}
				if(ds.Tables[0].Rows[0]["templateid"].ToString()!="")
				{
					model.templateid=int.Parse(ds.Tables[0].Rows[0]["templateid"].ToString());
				}
                model.cname = ds.Tables[0].Rows[0]["cname"].ToString();
                model.ename = ds.Tables[0].Rows[0]["ename"].ToString();
                model.coverimg = ds.Tables[0].Rows[0]["coverimg"].ToString();
                model.folder = ds.Tables[0].Rows[0]["folder"].ToString();
                model.closepage = ds.Tables[0].Rows[0]["closepage"].ToString();
                model.showtype =int.Parse(ds.Tables[0].Rows[0]["showtype"].ToString());               
				return model;
			}
			else
			{
				return null;
			}
		}

        public Dukey.Model.WebConfig GetModelByCache()
        {         
            object objModel = Common.DataCache.GetCache("webconfig_model");
            if (objModel == null)
            {
                try
                {
                    objModel = GetModel();
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache("webconfig_model", objModel);
                    }
                }
                catch { }
            }
            return objModel as Dukey.Model.WebConfig;
        }
		#endregion  成员方法
    }


}
