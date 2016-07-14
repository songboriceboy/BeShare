using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using CRDB.Model;
namespace CRDB.BLL
{
	/// <summary>
	/// 业务逻辑类crdb_rsssource 的摘要说明。
	/// </summary>
	public class crdb_rsssource
	{
		private readonly CRDB.DAL.crdb_rsssource dal=new CRDB.DAL.crdb_rsssource();
		public crdb_rsssource()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CRDB.Model.crdb_rsssource model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(CRDB.Model.crdb_rsssource model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CRDB.Model.crdb_rsssource GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public CRDB.Model.crdb_rsssource GetModelByCache(int id)
		{
			
			string CacheKey = "crdb_rsssourceModel-" + id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CRDB.Model.crdb_rsssource)objModel;
		}
        public CRDB.Model.crdb_rsssource GetOneTask(string strWhere)
        {
            return dal.GetOneTask(strWhere);
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
		public List<CRDB.Model.crdb_rsssource> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CRDB.Model.crdb_rsssource> DataTableToList(DataTable dt)
		{
			List<CRDB.Model.crdb_rsssource> modelList = new List<CRDB.Model.crdb_rsssource>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CRDB.Model.crdb_rsssource model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CRDB.Model.crdb_rsssource();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.site_name=dt.Rows[n]["site_name"].ToString();
					model.site_code=dt.Rows[n]["site_code"].ToString();
					model.site_url=dt.Rows[n]["site_url"].ToString();
					model.article_url_pattern=dt.Rows[n]["article_url_pattern"].ToString();
					model.article_url_range=dt.Rows[n]["article_url_range"].ToString();
					if(dt.Rows[n]["gather_interval"].ToString()!="")
					{
						model.gather_interval=int.Parse(dt.Rows[n]["gather_interval"].ToString());
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

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

