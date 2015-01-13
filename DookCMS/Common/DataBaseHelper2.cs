using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
namespace Common
{
    /// <summary> 
    /// 对数据库操作的类，包括附加、还原、备份、分离、压缩、创建、修改等常见操作.版本1.0 
    /// 最新版本主要在命名规范上做了完善,同时增加了还原时重命名数据库功能 
    /// </summary> 
    public class DataBaseHelper2
    {
        /// <summary> 
        /// 数据库连接字符串 
        /// </summary> 
        public string ConnectionString;
        /// <summary> 
        /// SQL操作语句 
        /// </summary> 
        public string StrSQL;
        /// <summary> 
        /// 数据库连接对象 
        /// </summary> 
        private SqlConnection Conn;
        /// <summary> 
        /// 数据库操作对象Comm 
        /// </summary> 
        private SqlCommand Comm;
        /// <summary> 
        /// 要操作的数据库名称 
        /// </summary> 
        public string DataBaseName;
        /// <summary> 
        /// 数据库逻辑名 
        /// 逻辑名为数据库创建后的初始文件名，以后不会随着数据库的操作(如备份还原)而改变 
        /// 此处要区别于DataBaseName 
        /// </summary> 
        public string LogicalDataBaseName;
        /// <summary> 
        /// 数据库文件完整地址 
        /// </summary> 
        public string DataBase_MDF_Address;
        /// <summary> 
        /// 数据库日志文件完整地址 
        /// </summary> 
        public string DataBase_LDF_Address;
        /// <summary> 
        /// 备份文件名 
        /// </summary> 
        public string DataBaseOfBackupName;
        /// <summary> 
        /// 备份文件路径 
        /// </summary> 
        public string DataBaseOfBackupPath;
        /// <summary> 
        /// 数据库压缩比，1%－99% 
        /// </summary> 
        public string Percent;
        /// <summary> 
        /// 执行创建/修改数据库和表的通用操作 
        /// </summary> 
        public void DataBaseAndTableOperate()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = StrSQL;
                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 附加数据库 
        /// </summary> 
        public void AppendDataBase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "sp_attach_db";
                Comm.Parameters.Add(new SqlParameter(@"dbname", SqlDbType.NVarChar));
                Comm.Parameters[@"dbname"].Value = DataBaseName;
                Comm.Parameters.Add(new SqlParameter(@"filename1", SqlDbType.NVarChar));
                Comm.Parameters[@"filename1"].Value = DataBase_MDF_Address;
                Comm.Parameters.Add(new SqlParameter(@"filename2", SqlDbType.NVarChar));
                Comm.Parameters[@"filename2"].Value = DataBase_LDF_Address;
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 分离数据库 
        /// </summary> 
        public void DetachDataBase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = @"sp_detach_db";
                Comm.Parameters.Add(new SqlParameter(@"dbname", SqlDbType.NVarChar));
                Comm.Parameters[@"dbname"].Value = DataBaseName;
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 备份数据库 
        /// </summary> 
        public void BackupDataBase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "use master;backup database @dbname to disk = @backupname;";
                Comm.Parameters.Add(new SqlParameter(@"dbname", SqlDbType.NVarChar));
                Comm.Parameters[@"dbname"].Value = DataBaseName;
                Comm.Parameters.Add(new SqlParameter(@"backupname", SqlDbType.NVarChar));
                Comm.Parameters[@"backupname"].Value = @DataBaseOfBackupPath + @DataBaseOfBackupName;

                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();

            }
        }
        /// <summary> 
        /// 还原数据库 
        /// </summary> 
        public void RestoreDataBase()
        {
            try
            {
                string BackupFile = @DataBaseOfBackupPath + @DataBaseOfBackupName;
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "use master;restore filelistonly from disk = @BackupFile;restore database @DataBaseName from disk = @BackupFile with move '" + LogicalDataBaseName + "' to '" + DataBase_MDF_Address + "',move 'ajgl_baseunit_Log' to '" + DataBase_LDF_Address + "',stats = 10, replace;";
                Comm.Parameters.Add(new SqlParameter(@"DataBaseName", SqlDbType.NVarChar));
                Comm.Parameters[@"DataBaseName"].Value = DataBaseName;
                Comm.Parameters.Add(new SqlParameter(@"BackupFile", SqlDbType.NVarChar));
                Comm.Parameters[@"BackupFile"].Value = BackupFile;
                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 压缩数据库 
        /// </summary> 
        public void CompressDatabase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "DBCC SHRINKDATABASE (" + DataBaseName + "," + Percent + ")";
                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 设置数据库为只读 
        /// </summary> 
        /// <param name=""></param> 
        public void ReadOnlyDatabase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "USE master; EXEC sp_dboption '" + DataBaseName + "', 'read only', 'TRUE'";

                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }
        /// <summary> 
        /// 设置数据库为脱机状态 
        /// </summary> 
        public void OfflineDatabase()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                Comm = new SqlCommand();
                Comm.Connection = Conn;
                Comm.CommandText = "USE master; EXEC sp_dboption '" + DataBaseName + "', 'offline', 'TRUE'";

                Comm.CommandType = CommandType.Text;
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                Conn.Close();
            }
        }

        #region SQL恢复数据库
        /// <summary> 
        /// SQL恢复数据库 
        /// </summary> 
        /// <param name="ServerIP">SQL服务器IP或(Localhost)</param> 
        /// <param name="LoginName">数据库登录名</param> 
        /// <param name="LoginPass">数据库登录密码</param> 
        /// <param name="DBName">要还原的数据库名</param> 
        /// <param name="BackPath">数据库备份的路径</param> 
        public static void SQLDbRestore(string ServerIP, string LoginName, string LoginPass, string DBName, string BackPath)
        {
            SQLDMO.Restore orestore = new SQLDMO.RestoreClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(ServerIP, LoginName, LoginPass);
                orestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                orestore.Database = DBName;
                orestore.Files = BackPath;
                orestore.FileNumber = 1;
                orestore.ReplaceDatabase = true;
                orestore.SQLRestore(oSQLServer);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }
        #endregion
    }
}
//#region 调用示例 
//#region btnremove_ServerClick 分离数据库 
///// <summary> 
///// 分离数据库 
///// </summary> 
///// <param name="sender"></param> 
///// <param name="e"></param> 
//protected void btndetach_ServerClick(object sender, EventArgs e) 
//{ 
//try 
//{ 
//DataBaseHelper dbh = new DataBaseHelper(); 
//dbh.ConnectionString = "Data Source=(local);User id=sa;Password=123456; Initial Catalog=master"; 
//dbh.DataBaseName = "DBName"; 
//dbh.DetachDataBase(); 
//} 
//catch (Exception ex) 
//{ 
//throw (ex); 
//} 
//} 
//#endregion 
//#region btnadddb_ServerClick 附加数据库 
///// <summary> 
///// 附加数据库 
///// </summary> 
///// <param name="sender"></param> 
///// <param name="e"></param> 
//protected void btnappenddb_ServerClick(object sender, EventArgs e) 
//{ 
//try 
//{ 
//DataBaseHelper dbh = new DataBaseHelper(); 
//dbh.ConnectionString = "Data Source=(local);User id=sa;Password=123456; Initial Catalog=master"; 
//dbh.DataBaseName = "DBName"; 
//dbh.DataBase_MDF_Address = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\DBName.MDF"; 
//dbh.DataBase_LDF_Address = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\DBName_Log.LDF"; 
//dbh.AppendDataBase(); 
//} 
//catch (Exception ex) 
//{ 
//throw (ex); 
//} 
//} 
//#endregion 
//#region btnbackup_ServerClick 备份数据库 
///// <summary> 
///// 备份数据库 
///// </summary> 
///// <param name="sender"></param> 
///// <param name="e"></param> 
//protected void btnbackup_ServerClick(object sender, EventArgs e) 
//{ 
//try 
//{ 
//DataBaseHelper dbh = new DataBaseHelper(); 
//dbh.ConnectionString = "Data Source=(local);User id=sa;Password=123456; Initial Catalog=master";

//dbh.DataBaseName = "DBName"; 
//dbh.DataBaseOfBackupName = "back.bak"; 
//dbh.DataBaseOfBackupPath = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Back\"; 
//dbh.BackupDataBase(); 
//} 
//catch (Exception ex) 
//{ 
//throw (ex); 
//} 
//} 
//#endregion 
//#region btnrestore_Click 还原数据库 
///// <summary> 
///// 还原数据库 
///// </summary> 
///// <param name="sender"></param> 
///// <param name="e"></param> 
//protected void btnrestore_Click(object sender, EventArgs e) 
//{ 
//try 
//{ 
//DataBaseHelper dbh = new DataBaseHelper(); 
//dbh.ConnectionString = "Data Source=(local);User id=sa;Password=123456; Initial Catalog=master"; 
//dbh.LogicalDataBaseName = "DBName"; 
//dbh.DataBaseName = "DBName"; 
//dbh.DataBaseOfBackupName = "back.bak"; 
//dbh.DataBase_MDF_Address = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\DBName.MDF"; 
//dbh.DataBase_LDF_Address = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\DBName_Log.LDF"; 
//dbh.DataBaseOfBackupPath = @"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Back\"; 
//dbh.RestoreDataBase(); 
//} 
//catch (Exception ex) 
//{ 
//throw (ex); 
//} 
//} 
//#endregion 
//#endregion

