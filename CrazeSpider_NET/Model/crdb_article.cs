using System;
namespace CRDB.Model
{
	/// <summary>
	/// 实体类crdb_article 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class crdb_article
	{
		public crdb_article()
		{}
		#region Model
		private int _id;
		private string _article_link;
		private string _article_title;
		private string _article_content;
		private int _article_time;
		private int _bloom_offset1;
		private int _bloom_offset2;
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
		public string article_link
		{
			set{ _article_link=value;}
			get{return _article_link;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string article_title
		{
			set{ _article_title=value;}
			get{return _article_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string article_content
		{
			set{ _article_content=value;}
			get{return _article_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int article_time
		{
			set{ _article_time=value;}
			get{return _article_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int bloom_offset1
		{
			set{ _bloom_offset1=value;}
			get{return _bloom_offset1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int bloom_offset2
		{
			set{ _bloom_offset2=value;}
			get{return _bloom_offset2;}
		}
		#endregion Model

	}
}

