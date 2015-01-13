using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// 接口层IWebConfig 的摘要说明。
	/// </summary>
	public interface IWebConfig
	{
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void Add(Dukey.Model.WebConfig model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		void Update(Dukey.Model.WebConfig model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		void Delete();
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Dukey.Model.WebConfig GetModel();
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	}
}
