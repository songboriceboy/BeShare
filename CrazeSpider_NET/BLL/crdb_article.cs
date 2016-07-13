using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using CRDB.Model;
namespace CRDB.BLL
{
	/// <summary>
	/// 业务逻辑类crdb_article 的摘要说明。
	/// </summary>
	public class crdb_article
	{
		private readonly CRDB.DAL.crdb_article dal=new CRDB.DAL.crdb_article();
		public crdb_article()
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
		public void Add(CRDB.Model.crdb_article model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(CRDB.Model.crdb_article model)
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
		public CRDB.Model.crdb_article GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public CRDB.Model.crdb_article GetModelByCache(int id)
		{
			
			string CacheKey = "crdb_articleModel-" + id;
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
			return (CRDB.Model.crdb_article)objModel;
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
		public List<CRDB.Model.crdb_article> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CRDB.Model.crdb_article> DataTableToList(DataTable dt)
		{
			List<CRDB.Model.crdb_article> modelList = new List<CRDB.Model.crdb_article>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CRDB.Model.crdb_article model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CRDB.Model.crdb_article();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.article_link=dt.Rows[n]["article_link"].ToString();
					model.article_title=dt.Rows[n]["article_title"].ToString();
					model.article_content=dt.Rows[n]["article_content"].ToString();
					if(dt.Rows[n]["article_time"].ToString()!="")
					{
						model.article_time=int.Parse(dt.Rows[n]["article_time"].ToString());
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

