using System;
using System.Reflection;
using System.Configuration;
using Dukey.IDAL;
namespace Dukey.DALFactory
{
    /// <summary>
    /// Abstract Factory pattern to create the DAL��
    /// ��������ﴴ�����󱨴�����web.config���Ƿ��޸���<add key="DAL" value="Maticsoft.SQLServerDAL" />��
    /// </summary>
    public sealed class DataAccess
    {

        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        public DataAccess()
        { }

        #region ���ݹ������ʽӿ�

        //��ʹ�û���
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// ��¼������־
                return null;
            }
        }

        //ʹ�û���
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType =Common.DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    Common.DataCache.SetCache(classNamespace, objType);// д�뻺��
                }
                catch(System.Exception ex)
                {
                    throw ex;
                    //string str=ex.Message;// ��¼������־
                }
            }
            return objType;
        }
        #endregion


        /// <summary>
        /// ����Product���ݲ�ӿ�
        /// </summary>
        public static IDataBaseHelper CreateDataBaseHelper()
        {
            string ClassNamespace = AssemblyPath + ".DataBaseHelper";
            return CreateObject(AssemblyPath, ClassNamespace) as IDataBaseHelper;            
        }

        /// <summary>
        /// ����Photos���ݲ�ӿ�
        /// </summary>
        public static IPhotos CreatePhotos()
        {

            string ClassNamespace = AssemblyPath + ".Photos";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IPhotos)objType;
        }

        	/// <summary>
		/// ����Sys_Admin���ݲ�ӿ�
		/// </summary>
		public static ISys_Admin CreateSys_Admin()
		{

			string ClassNamespace = AssemblyPath +".Sys_Admin";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (ISys_Admin)objType;
		}
        /// <summary>
        /// ����SysTable���ݲ�ӿ�
        /// </summary>
        public static ISysDictionary CreateSysDictionay()
        {
            string ClassNamespace = AssemblyPath + ".SysDictionary";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ISysDictionary)objType;
        }
    }
}