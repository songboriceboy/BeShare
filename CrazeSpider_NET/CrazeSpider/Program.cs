using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using System.Data;


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
    class Program
    {
        private static CRDB.BLL.crdb_rsssource m_bll = new CRDB.BLL.crdb_rsssource();
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
        private static void GetSiteLinks(RssSource rs, string strContent)
        {
            string strUrlRule = rs.strArticleUrlPattern;;
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

            List<string> lstRevomeSame = new List<string>();


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

                    Console.WriteLine(normalizedLink);
                    if (lstRevomeSame.Contains(normalizedLink))
                        continue;
                    else
                        lstRevomeSame.Add(normalizedLink);

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


        public static void timerGetLinks_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("timerGetLinks_Elapsed");
        }
        public static void timerGetArticle_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("timerGetArticle_Elapsed");
        } 

        static void Main(string[] args)
        {
            m_timerGetLinks.Interval = 20000;
            m_timerGetLinks.Enabled = true;
            m_timerGetLinks.Elapsed += new System.Timers.ElapsedEventHandler(timerGetLinks_Elapsed);

            m_timerGetArticle.Interval = 30000;
            m_timerGetArticle.Enabled = true;
            m_timerGetArticle.Elapsed += new System.Timers.ElapsedEventHandler(timerGetArticle_Elapsed); 
           
            DataSet ds = m_bll.GetAllList();
            RssSource rs = new RssSource();
            rs.strSiteUrl = "http://www.cnblogs.com/";
            rs.strArticleUrlPattern = "www.cnblogs.com/*/p/*.html$";
            rs.strArticleUrlRangeCssPath = "div#post_list";
          
            WebDownloader wd = new WebDownloader();
            Encoding ec = Encoding.GetEncoding("UTF-8");
            string strContent = wd.GetPageByHttpWebRequest(rs.strSiteUrl, ec, "");
            GetSiteLinks(rs, strContent);


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
 
            Console.ReadLine();
        }
    }
}
