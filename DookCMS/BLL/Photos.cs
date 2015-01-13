using System;
using System.Collections.Generic;
using System.Text;
using Dukey.DALFactory;
using System.Data;
using Dukey.IDAL;


namespace BLL
{
    public class Photos
    {
        private readonly IPhotos dal = DataAccess.CreatePhotos();
        public Photos()
        { }
        #region  成员方法
    

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Dukey.Model.Photos model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Dukey.Model.Photos model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Dukey.Model.Photos GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Dukey.Model.Photos GetModelByCache(int ID)
        {           
            object objModel = Common.DataCache.GetCache("getmodelPhoto-" + ID);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModel(ID);
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache("getmodelPhoto-" + ID, objModel);
                    }
                }
                catch { }
            }
            return objModel as Dukey.Model.Photos;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Dukey.Model.Photos> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Dukey.Model.Photos> DataTableToList(DataTable dt)
        {
            List<Dukey.Model.Photos> modelList = new List<Dukey.Model.Photos>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Dukey.Model.Photos model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Dukey.Model.Photos();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.title = dt.Rows[n]["title"].ToString();
                    model.brief = dt.Rows[n]["brief"].ToString();
                    model.imgurl = dt.Rows[n]["imgurl"].ToString();
                    if (dt.Rows[n]["bid"].ToString() != "")
                    {
                        model.bid = int.Parse(dt.Rows[n]["bid"].ToString());
                    }
                    if (dt.Rows[n]["isshow"].ToString() != "")
                    {
                        if ((dt.Rows[n]["isshow"].ToString() == "1") || (dt.Rows[n]["isshow"].ToString().ToLower() == "true"))
                        {
                            model.isshow = true;
                        }
                        else
                        {
                            model.isshow = false;
                        }
                    }
                    if (dt.Rows[n]["dots"].ToString() != "")
                    {
                        model.dots = int.Parse(dt.Rows[n]["dots"].ToString());
                    }
                    if (dt.Rows[n]["px"].ToString() != "")
                    {
                        model.px = int.Parse(dt.Rows[n]["px"].ToString());
                    }
                    if (dt.Rows[n]["iscomment"].ToString() != "")
                    {
                        if ((dt.Rows[n]["iscomment"].ToString() == "1") || (dt.Rows[n]["iscomment"].ToString().ToLower() == "true"))
                        {
                            model.iscomment = true;
                        }
                        else
                        {
                            model.iscomment = false;
                        }
                    }
                    if (dt.Rows[n]["addtime"].ToString() != "")
                    {
                        model.addtime = DateTime.Parse(dt.Rows[n]["addtime"].ToString());
                    }
                    if (dt.Rows[n]["uptime"].ToString() != "")
                    {
                        model.uptime = DateTime.Parse(dt.Rows[n]["uptime"].ToString());
                    }
                    if (dt.Rows[n]["adminId"].ToString() != "")
                    {
                        model.adminId = int.Parse(dt.Rows[n]["adminId"].ToString());
                    }
                    if (dt.Rows[n]["userId"].ToString() != "")
                    {
                        model.userId = int.Parse(dt.Rows[n]["userId"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion
    }
}
