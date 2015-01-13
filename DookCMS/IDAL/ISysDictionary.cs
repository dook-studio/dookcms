using System;
using System.Data;
using System.Collections.Generic;
namespace Dukey.IDAL
{
	/// <summary>
	/// 接口层IPhotos 的摘要说明。
	/// </summary>
    public interface ISysDictionary
	{
		#region  成员方法
	
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Dukey.Model.Dictionary GetModel(int id);
		/// <summary>
		/// 获得数据列表
		/// </summary>
        List<Dukey.Model.Dictionary> GetList(string strWhere);

		#endregion  成员方法
	}
}
