using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//请先添加引用
namespace CRDB.DAL
{
	/// <summary>
	/// 数据访问类crdb_article。
	/// </summary>
	public class crdb_article
	{
		public crdb_article()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "crdb_article"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from crdb_article");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CRDB.Model.crdb_article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into crdb_article(");
			strSql.Append("article_link,article_title,article_content,article_time,bloom_offset1,bloom_offset2)");
			strSql.Append(" values (");
			strSql.Append("@article_link,@article_title,@article_content,@article_time,@bloom_offset1,@bloom_offset2)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@article_link", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_title", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_content", MySqlDbType.Text),
					new MySqlParameter("@article_time", MySqlDbType.Int32,10),
					new MySqlParameter("@bloom_offset1", MySqlDbType.Int32,32),
					new MySqlParameter("@bloom_offset2", MySqlDbType.Int32,32)};
			parameters[0].Value = model.article_link;
			parameters[1].Value = model.article_title;
			parameters[2].Value = model.article_content;
			parameters[3].Value = model.article_time;
			parameters[4].Value = model.bloom_offset1;
			parameters[5].Value = model.bloom_offset2;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(CRDB.Model.crdb_article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update crdb_article set ");
			strSql.Append("article_link=@article_link,");
			strSql.Append("article_title=@article_title,");
			strSql.Append("article_content=@article_content,");
			strSql.Append("article_time=@article_time,");
			strSql.Append("bloom_offset1=@bloom_offset1,");
			strSql.Append("bloom_offset2=@bloom_offset2");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@article_link", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_title", MySqlDbType.VarChar,200),
					new MySqlParameter("@article_content", MySqlDbType.Text),
					new MySqlParameter("@article_time", MySqlDbType.Int32,10),
					new MySqlParameter("@bloom_offset1", MySqlDbType.Int32,32),
					new MySqlParameter("@bloom_offset2", MySqlDbType.Int32,32)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.article_link;
			parameters[2].Value = model.article_title;
			parameters[3].Value = model.article_content;
			parameters[4].Value = model.article_time;
			parameters[5].Value = model.bloom_offset1;
			parameters[6].Value = model.bloom_offset2;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from crdb_article ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CRDB.Model.crdb_article GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,article_link,article_title,article_content,article_time,bloom_offset1,bloom_offset2 from crdb_article ");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)};
			parameters[0].Value = id;

			CRDB.Model.crdb_article model=new CRDB.Model.crdb_article();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.article_link=ds.Tables[0].Rows[0]["article_link"].ToString();
				model.article_title=ds.Tables[0].Rows[0]["article_title"].ToString();
				model.article_content=ds.Tables[0].Rows[0]["article_content"].ToString();
				if(ds.Tables[0].Rows[0]["article_time"].ToString()!="")
				{
					model.article_time=int.Parse(ds.Tables[0].Rows[0]["article_time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["bloom_offset1"].ToString()!="")
				{
					model.bloom_offset1=int.Parse(ds.Tables[0].Rows[0]["bloom_offset1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["bloom_offset2"].ToString()!="")
				{
					model.bloom_offset2=int.Parse(ds.Tables[0].Rows[0]["bloom_offset2"].ToString());
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
			strSql.Append("select id,article_link,article_title,article_content,article_time,bloom_offset1,bloom_offset2 ");
			strSql.Append(" FROM crdb_article ");
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
			parameters[0].Value = "crdb_article";
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

