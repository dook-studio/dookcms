using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// �ӿڲ�IPhotos ��ժҪ˵����
	/// </summary>
	public interface IPhotos
	{
		#region  ��Ա����
		/// <summary>
		/// ����һ������
		/// </summary>
		void Add(Dukey.Model.Photos model);
		/// <summary>
		/// ����һ������
		/// </summary>
		void Update(Dukey.Model.Photos model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		void Delete(int ID);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Dukey.Model.Photos GetModel(int ID);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		//DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  ��Ա����
	}
}
