using System;
using System.Data;
using System.Collections.Generic;
namespace Dukey.IDAL
{
	/// <summary>
	/// �ӿڲ�IPhotos ��ժҪ˵����
	/// </summary>
    public interface ISysDictionary
	{
		#region  ��Ա����
	
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        Dukey.Model.Dictionary GetModel(int id);
		/// <summary>
		/// ��������б�
		/// </summary>
        List<Dukey.Model.Dictionary> GetList(string strWhere);

		#endregion  ��Ա����
	}
}
