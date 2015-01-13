using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// 接口层IPhotos 的摘要说明。
	/// </summary>
	public interface IPhotos
	{
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void Add(Dukey.Model.Photos model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		void Update(Dukey.Model.Photos model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		void Delete(int ID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Dukey.Model.Photos GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		//DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	}
}
