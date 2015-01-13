using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// 接口层IBoards 的摘要说明。
	/// </summary>
	public interface IBoards
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int bid);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Dukey.Model.Channel model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		void Update(Dukey.Model.Channel model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		void Delete(int bid);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Dukey.Model.Channel GetModel(int bid);
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
