using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace CrazeSpider
{
    public class RssSource
    {
        public string strSiteName;
        public string strSiteCode;
        public string strSiteUrl;
        public string strArticleUrlPattern;
        public string strArticleUrlRangeCssPath;
    }
    public class ContentGatherRule
    {
        public string strArticleUrlPattern;
        public string strArticleContentCssPath;
    }
    public abstract class BaseModel
    {
        /// <summary>
        /// 将Model对象转换为json字符串
        /// </summary>
        /// <returns>返回json字符串</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    public class Article : BaseModel
    {
        public string article_link;
        public string article_title;
        public string article_content;
        public int article_time;
        public int bloom_offset1;
        public int bloom_offset2;
    }
    class Program
    {
        private static string strApiUrl = "http://localhost:808";
        private static Dictionary<string, string> m_dicSiteRules = new Dictionary<string, string>();
        private static System.Timers.Timer m_timerGetLinks = new System.Timers.Timer();
        private static System.Timers.Timer m_timerGetArticle = new System.Timers.Timer(); 
        private static HtmlAgilityPack.HtmlDocument GetHtmlDocument(string strPage)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument
            {
                OptionAddDebuggingAttributes = false,
                OptionAutoCloseOnEnd = true,
                OptionFixNestedTags = true,
                OptionReadEncoding = true
            };
            htmlDoc.LoadHtml(strPage);


            return htmlDoc;
        }
        private static string NormalizeLink(string baseUrl, string link)
        {
            return link.NormalizeUrl(baseUrl);
        }
        private static string GetNormalizedLink(string baseUrl, string decodedLink)
        {
            string normalizedLink = NormalizeLink(baseUrl, decodedLink);

            return normalizedLink;
        }
        private static string GetUrlLeftPart(string strPath)
        {
            int n = strPath.LastIndexOf("/");
            if (n > -1)
            {
                return strPath.Substring(0, n + 1);
            }
            else
                return new Uri(strPath).GetLeftPart(UriPartial.Authority);
        }
        public static string GenerateTimeStamp(DateTime dt)
        {
            // Default implementation of UNIX time of the current UTC time  
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static int IsLinkExist(int nOffset1, int nOffset2)
        {
            string loginUrl = strApiUrl + "/index.php/api/index/is_link_exist";

            RssSource rs = new RssSource();
            Encoding encoding = Encoding.GetEncoding("utf-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("offset1", nOffset1.ToString());
            parameters.Add("offset2", nOffset2.ToString());

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);



            string result = reader.ReadToEnd();
            JObject jo = JObject.Parse(result);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();


            return int.Parse(values[1]);
            //Console.WriteLine("timerGetLinks_Elapsed");
        }
        protected static void SaveUrlToDB(string strUrl, string strLinkText)
        {
           
            BloomFilter m_bf = new BloomFilter(10485760);
            int[] nArrayOffset = new int[2];
            nArrayOffset = m_bf.getOffset(strUrl);
            int nExist = IsLinkExist(nArrayOffset[0], nArrayOffset[1]);
            if (nExist > 0)
                return;


            Article modelArticle = new Article();
    
            modelArticle.article_content = "";
            modelArticle.article_time = int.Parse(GenerateTimeStamp(System.DateTime.Now));

            modelArticle.article_link = strUrl;
    
            string strTitle = "";
            if (!string.IsNullOrEmpty(strLinkText))
            {
                strTitle = Regex.Replace(strLinkText, @"[|•/\;.':*?<>-]", "").ToString();
                strTitle = Regex.Replace(strTitle, "[\"]", "").ToString();
                strTitle = Regex.Replace(strTitle, @"\s", "");
                strTitle = strTitle.RemovePathInvalidChars().RemoveFileNameInvalidChars();
            }

            if (strTitle == "")
            {
                strTitle = System.Guid.NewGuid().ToString().Replace("-", "");
            }
            if (strTitle.Length > 35)
                strTitle = strTitle.Substring(0, 35);

            modelArticle.article_title = strTitle;
            modelArticle.bloom_offset1 = nArrayOffset[0];
            modelArticle.bloom_offset2 = nArrayOffset[1];

            AddArticle(modelArticle.ToString());
            //m_bll.Add(modelArticle);
            return;

        }
        public static void AddArticle(string strContent)
        {
            string loginUrl = strApiUrl + "/index.php/api/index/add_article";

            RssSource rs = new RssSource();
            Encoding encoding = Encoding.GetEncoding("utf-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("article_content", strContent.ToString());
           

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);


            return;
            //Console.WriteLine("timerGetLinks_Elapsed");
        }
        private static void GetSiteLinks(RssSource rs, string strContent)
        {
            string strUrlRule = rs.strArticleUrlPattern;
            strUrlRule = strUrlRule.Replace(".", "\\.");
            strUrlRule = strUrlRule.Replace("*", ".*?");

            HtmlAgilityPack.HtmlDocument htmlDoc = GetHtmlDocument(strContent);

            if (rs.strArticleUrlRangeCssPath != "")
            {
                IEnumerable<HtmlNode> NodesUrlContent = htmlDoc.DocumentNode.QuerySelectorAll(rs.strArticleUrlRangeCssPath);
                if (NodesUrlContent.Count() > 0)
                {
                    string strReturnPage = NodesUrlContent.ToArray()[0].InnerHtml;//进一步缩小范围
                    htmlDoc = GetHtmlDocument(strReturnPage);
                }
            }

            string baseUrl = GetUrlLeftPart(rs.strSiteUrl);
            DocumentWithLinks links = htmlDoc.GetLinks();




            foreach (string link in links.Links.Union(links.References))
            {

                if (string.IsNullOrEmpty(link))
                {
                    continue;
                }


                string decodedLink = link;

                string normalizedLink = GetNormalizedLink(baseUrl, decodedLink);


                if (string.IsNullOrEmpty(normalizedLink))
                {
                    continue;
                }

                MatchCollection matchs = Regex.Matches(normalizedLink, strUrlRule, RegexOptions.Singleline);
                if (matchs.Count > 0)
                {
                    string strLinkText = "";

                    foreach (string strTemp in links.m_dicLink2Text.Keys)
                    {
                        if (strTemp.Contains(normalizedLink))
                        {
                            strLinkText = links.m_dicLink2Text[strTemp];
                            break;
                        }
                    }

                    if (strLinkText == "")
                    {
                        if (links.m_dicLink2Text.Keys.Contains(link))
                            strLinkText = links.m_dicLink2Text[link].TrimEnd().TrimStart();
                        if (links.m_dicLink2Text.Keys.Contains(link.ToLower()))
                            strLinkText = links.m_dicLink2Text[link.ToLower()].TrimEnd().TrimStart();
                    }
                    SaveUrlToDB(normalizedLink, strLinkText);
                    Console.WriteLine(normalizedLink);


                }

            }
            return;

        }

        protected static string GetPageContent(string strMainContentElementRules, string strReturnPage)
        {

            string strMainContent = "";

            HtmlAgilityPack.HtmlDocument htmlDoc = GetHtmlDocument(strReturnPage);
            if (htmlDoc == null)
            {
                return "下载失败！";
            }

            string strTitle = "";
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//title");
            // Extract Title
            if (!Equals(nodes, null))
            {
                strTitle = string.Join(";", nodes.
                    Select(n => n.InnerText).
                    ToArray()).Trim();
            }

            strTitle = Regex.Replace(strTitle, @"[|•/\;.':*?<>-]", "").ToString();
            strTitle = Regex.Replace(strTitle, "[\"]", "").ToString();
            strTitle = Regex.Replace(strTitle, @"\s", "");

            strTitle = strTitle.RemovePathInvalidChars().RemoveFileNameInvalidChars();
            if (strTitle == "")
                strTitle = System.Guid.NewGuid().ToString().Replace("-", "");
            if (strTitle.Length > 35)
                strTitle = strTitle.Substring(0, 35);


            IEnumerable<HtmlNode> NodesMainContent = htmlDoc.DocumentNode.QuerySelectorAll(strMainContentElementRules);

            if (NodesMainContent.Count() > 0)
            {
                strMainContent = NodesMainContent.ToArray()[0].OuterHtml;
                strMainContent = strMainContent.Replace("display: none;", "");

                strMainContent = StripJs(strMainContent);
            }

            return strMainContent;

        }
        static string StripJs(string source)
        {
            try
            {
                string result = source;

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                return result;
            }
            catch
            {
                return source;
            }
        }
        public static void GetAllRules()
        {
            string loginUrl = strApiUrl + "/index.php/api/index/get_all_rules";

            RssSource rs = new RssSource();
            Encoding encoding = Encoding.GetEncoding("utf-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();


            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);



            string result = reader.ReadToEnd();
            JObject jo = JObject.Parse(result);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            JArray ja = (JArray)JsonConvert.DeserializeObject(values[3]);
            foreach (JToken jt in ja)
            {
                string strArticle_url_pattern = jt["article_url_pattern"].ToString();
                string strArticle_content_csspath = jt["article_content_csspath"].ToString();
                m_dicSiteRules.Add(strArticle_url_pattern, strArticle_content_csspath);
            }

            return;
            //Console.WriteLine("timerGetLinks_Elapsed");
        }
        public static RssSource GetOneTask()
        {
            string loginUrl = strApiUrl + "/index.php/api/index/get_one_task";

            RssSource rs = new RssSource();
            Encoding encoding = Encoding.GetEncoding("utf-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
       

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);



            string result = reader.ReadToEnd();
            JObject jo = JObject.Parse(result);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            JArray ja = (JArray)JsonConvert.DeserializeObject(values[3]);
            foreach (JToken jt in ja)
            {
                rs.strSiteName = jt["site_name"].ToString();
                rs.strSiteCode = jt["site_code"].ToString();
                rs.strSiteUrl = jt["site_url"].ToString();
                rs.strArticleUrlPattern = jt["article_url_pattern"].ToString();
                rs.strArticleUrlRangeCssPath = jt["article_url_range"].ToString();
            }

            return rs;
            //Console.WriteLine("timerGetLinks_Elapsed");
        }

        public static void timerGetLinks_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RssSource rs = GetOneTask();
            WebDownloader wd = new WebDownloader();
            Encoding ec = Encoding.GetEncoding("UTF-8");
            string strContent = wd.GetPageByHttpWebRequest(rs.strSiteUrl, ec, "");
            GetSiteLinks(rs, strContent);

            Console.WriteLine("timerGetLinks_Elapsed");
        }
        public static void timerGetArticle_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("timerGetArticle_Elapsed");
        } 
        
        static void Main(string[] args)
        {
            //GetOneTask();
            GetAllRules();

            m_timerGetLinks.Interval = 20000;
            m_timerGetLinks.Enabled = true;
            m_timerGetLinks.Elapsed += new System.Timers.ElapsedEventHandler(timerGetLinks_Elapsed);

            //m_timerGetArticle.Interval = 30000;
            //m_timerGetArticle.Enabled = true;
            //m_timerGetArticle.Elapsed += new System.Timers.ElapsedEventHandler(timerGetArticle_Elapsed); 

            Console.ReadLine();
            //RssSource rs = new RssSource();
            //rs.strSiteUrl = "http://www.cnblogs.com/";
            //rs.strArticleUrlPattern = "www.cnblogs.com/*/p/*.html$";
            //rs.strArticleUrlRangeCssPath = "div#post_list";
          
            WebDownloader wd = new WebDownloader();
            Encoding ec = Encoding.GetEncoding("UTF-8");
            //string strContent = wd.GetPageByHttpWebRequest(rs.strSiteUrl, ec, "");
            //GetSiteLinks(rs, strContent);


            ContentGatherRule cgr = new ContentGatherRule();
            cgr.strArticleContentCssPath = "div#cnblogs_post_body";
            cgr.strArticleUrlPattern = "www.cnblogs.com/*/p/*.html$";
    
            string strUrlArticle = "http://www.cnblogs.com/chejiangyi/p/5666250.html";


            //string strUrlRule = cgr.strArticleUrlPattern; ;
            //strUrlRule = strUrlRule.Replace(".", "\\.");
            //strUrlRule = strUrlRule.Replace("*", ".*?");
            //MatchCollection matchs = Regex.Matches(strUrlArticle, strUrlRule, RegexOptions.Singleline);
            //int nMatchCount = matchs.Count;

            string strArticle = wd.GetPageByHttpWebRequest(strUrlArticle, ec, "");
         

            Console.WriteLine("--------------正文-----------------------");
            strArticle = GetPageContent(cgr.strArticleContentCssPath, strArticle);
            string loginUrl = "http://localhost:808/index.php/api/index/user_login";
            

            Encoding encoding = Encoding.GetEncoding("utf-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            strArticle = System.Web.HttpUtility.HtmlDecode(strArticle);
            parameters.Add("content", strArticle);
            //parameters.Add("password", password);

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            Console.ReadLine();
        }
    }
}
