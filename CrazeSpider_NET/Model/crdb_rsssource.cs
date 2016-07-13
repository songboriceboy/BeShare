using System;
namespace CRDB.Model
{
	/// <summary>
	/// 实体类crdb_rsssource 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class crdb_rsssource
	{
		public crdb_rsssource()
		{}
		#region Model
		private int _id;
		private string _site_name;
		private string _site_code;
		private string _site_url;
		private string _article_url_pattern;
		private string _article_url_range;
		private int _gather_interval;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string site_name
		{
			set{ _site_name=value;}
			get{return _site_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string site_code
		{
			set{ _site_code=value;}
			get{return _site_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string site_url
		{
			set{ _site_url=value;}
			get{return _site_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string article_url_pattern
		{
			set{ _article_url_pattern=value;}
			get{return _article_url_pattern;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string article_url_range
		{
			set{ _article_url_range=value;}
			get{return _article_url_range;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int gather_interval
		{
			set{ _gather_interval=value;}
			get{return _gather_interval;}
		}
		#endregion Model

	}
}

