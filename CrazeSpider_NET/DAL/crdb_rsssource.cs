using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace CRDB.DAL
{
	/// <summary>
	/// 数据访问类crdb_rsssource。
	/// </summary>
	public class crdb_rsssource
	{
		public crdb_rsssource()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "crdb_rsssource"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from crdb_rsssource");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CRDB.Model.crdb_rsssource model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into crdb_rsssource(");
			strSql.Append("site_name,site_code,site_url,article_url_pattern,article_url_range,gather_interval)");
			strSql.Append(" values (");
			strSql.Append("@site_name,@site_code,@site_url,@article_url_pattern,@article_url_range,@gather_interval)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@site_name", MySqlDbType.VarChar,64),
					new MySqlParameter("@site_code", MySqlDbType.VarChar,32),
					new MySqlParameter("@site_url", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_url_pattern", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_url_range", MySqlDbType.VarChar,64),
					new MySqlParameter("@gather_interval", MySqlDbType.Int32,10)};
			parameters[0].Value = model.site_name;
			parameters[1].Value = model.site_code;
			parameters[2].Value = model.site_url;
			parameters[3].Value = model.article_url_pattern;
			parameters[4].Value = model.article_url_range;
			parameters[5].Value = model.gather_interval;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(CRDB.Model.crdb_rsssource model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update crdb_rsssource set ");
			strSql.Append("site_name=@site_name,");
			strSql.Append("site_code=@site_code,");
			strSql.Append("site_url=@site_url,");
			strSql.Append("article_url_pattern=@article_url_pattern,");
			strSql.Append("article_url_range=@article_url_range,");
			strSql.Append("gather_interval=@gather_interval");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@site_name", MySqlDbType.VarChar,64),
					new MySqlParameter("@site_code", MySqlDbType.VarChar,32),
					new MySqlParameter("@site_url", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_url_pattern", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_url_range", MySqlDbType.VarChar,64),
					new MySqlParameter("@gather_interval", MySqlDbType.Int32,10)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.site_name;
			parameters[2].Value = model.site_code;
			parameters[3].Value = model.site_url;
			parameters[4].Value = model.article_url_pattern;
			parameters[5].Value = model.article_url_range;
			parameters[6].Value = model.gather_interval;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from crdb_rsssource ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CRDB.Model.crdb_rsssource GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,site_name,site_code,site_url,article_url_pattern,article_url_range,gather_interval from crdb_rsssource ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			CRDB.Model.crdb_rsssource model=new CRDB.Model.crdb_rsssource();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.site_name=ds.Tables[0].Rows[0]["site_name"].ToString();
				model.site_code=ds.Tables[0].Rows[0]["site_code"].ToString();
				model.site_url=ds.Tables[0].Rows[0]["site_url"].ToString();
				model.article_url_pattern=ds.Tables[0].Rows[0]["article_url_pattern"].ToString();
				model.article_url_range=ds.Tables[0].Rows[0]["article_url_range"].ToString();
				if(ds.Tables[0].Rows[0]["gather_interval"].ToString()!="")
				{
					model.gather_interval=int.Parse(ds.Tables[0].Rows[0]["gather_interval"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,site_name,site_code,site_url,article_url_pattern,article_url_range,gather_interval ");
			strSql.Append(" FROM crdb_rsssource ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "crdb_rsssource";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

