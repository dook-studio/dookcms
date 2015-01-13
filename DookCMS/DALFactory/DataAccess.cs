using System;
using System.Reflection;
using System.Configuration;
using Dukey.IDAL;
namespace Dukey.DALFactory
{
    /// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
    /// </summary>
    public sealed class DataAccess
    {

        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        public DataAccess()
        { }

        #region 数据工厂访问接口

        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }
        }

        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType =Common.DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    Common.DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch(System.Exception ex)
                {
                    throw ex;
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion


        /// <summary>
        /// 创建Product数据层接口
        /// </summary>
        public static IDataBaseHelper CreateDataBaseHelper()
        {
            string ClassNamespace = AssemblyPath + ".DataBaseHelper";
            return CreateObject(AssemblyPath, ClassNamespace) as IDataBaseHelper;            
        }

        /// <summary>
        /// 创建Photos数据层接口
        /// </summary>
        public static IPhotos CreatePhotos()
        {

            string ClassNamespace = AssemblyPath + ".Photos";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IPhotos)objType;
        }

        	/// <summary>
		/// 创建Sys_Admin数据层接口
		/// </summary>
		public static ISys_Admin CreateSys_Admin()
		{

			string ClassNamespace = AssemblyPath +".Sys_Admin";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (ISys_Admin)objType;
		}
        /// <summary>
        /// 创建SysTable数据层接口
        /// </summary>
        public static ISysDictionary CreateSysDictionay()
        {
            string ClassNamespace = AssemblyPath + ".SysDictionary";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ISysDictionary)objType;
        }
    }
}