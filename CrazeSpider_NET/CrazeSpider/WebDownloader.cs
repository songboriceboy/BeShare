using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection;

namespace CrazeSpider
{
    public class WebDownloader
    {

        public string GetPageByHttpWebRequest(string url, Encoding encoding, string strRefer)
        {

            string result = null;

            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;

                if (!string.IsNullOrEmpty(strRefer))
                {
                    Uri u = new Uri(strRefer);

                    request.Referer = u.Host;
                }
                else
                {
                    request.Referer = strRefer;
                }
                request.Method = "GET";
             
                response = request.GetResponse();

      
            
                reader = new StreamReader(response.GetResponseStream(), encoding);
         

     
                result = reader.ReadToEnd();
                HttpWebResponse hwr = response as HttpWebResponse;
              

            }
            catch (Exception ex)
            {
                result = "";
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();

            }
            return result;
        }
 
    }
}
