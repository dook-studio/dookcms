using System;
using System.Data;
namespace Dukey.IDAL
{
	/// <summary>
	/// �ӿڲ�IWebConfig ��ժҪ˵����
	/// </summary>
	public interface IWebConfig
	{
		#region  ��Ա����
		/// <summary>
		/// ����һ������
		/// </summary>
		void Add(Dukey.Model.WebConfig model);
		/// <summary>
		/// ����һ������
		/// </summary>
		void Update(Dukey.Model.WebConfig model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		void Delete();
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Dukey.Model.WebConfig GetModel();
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
