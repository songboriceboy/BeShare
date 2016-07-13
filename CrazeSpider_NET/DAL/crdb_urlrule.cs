using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace CRDB.DAL
{
	/// <summary>
	/// 数据访问类crdb_urlrule。
	/// </summary>
	public class crdb_urlrule
	{
		public crdb_urlrule()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "crdb_urlrule"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from crdb_urlrule");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CRDB.Model.crdb_urlrule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into crdb_urlrule(");
			strSql.Append("article_url_pattern,article_content_csspath)");
			strSql.Append(" values (");
			strSql.Append("@article_url_pattern,@article_content_csspath)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@article_url_pattern", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_content_csspath", MySqlDbType.VarChar,64)};
			parameters[0].Value = model.article_url_pattern;
			parameters[1].Value = model.article_content_csspath;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(CRDB.Model.crdb_urlrule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update crdb_urlrule set ");
			strSql.Append("article_url_pattern=@article_url_pattern,");
			strSql.Append("article_content_csspath=@article_content_csspath");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@article_url_pattern", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_content_csspath", MySqlDbType.VarChar,64)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.article_url_pattern;
			parameters[2].Value = model.article_content_csspath;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from crdb_urlrule ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CRDB.Model.crdb_urlrule GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,article_url_pattern,article_content_csspath from crdb_urlrule ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			CRDB.Model.crdb_urlrule model=new CRDB.Model.crdb_urlrule();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.article_url_pattern=ds.Tables[0].Rows[0]["article_url_pattern"].ToString();
				model.article_content_csspath=ds.Tables[0].Rows[0]["article_content_csspath"].ToString();
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
			strSql.Append("select id,article_url_pattern,article_content_csspath ");
			strSql.Append(" FROM crdb_urlrule ");
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
			parameters[0].Value = "crdb_urlrule";
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

