using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// �ӿڲ�IBoards ��ժҪ˵����
	/// </summary>
	public interface IBoards
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int bid);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(Dukey.Model.Channel model);
		/// <summary>
		/// ����һ������
		/// </summary>
		void Update(Dukey.Model.Channel model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		void Delete(int bid);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Dukey.Model.Channel GetModel(int bid);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  ��Ա����
	}
}
