using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DataCollector.API
{
    public static class PostData
    {
        public static List<string> AllResults { get; private set; }
        public static List<string> AllErrors { get; private set; }

        public static PostResults PostRequest(string url, string data)
        {
            AllResults = new List<string>();
            AllErrors = new List<string>();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    if (httpWebRequest.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            AllResults.Add(reader.ReadToEnd());
                        }
                    } else
                    {
                        AllErrors.Add("Missing response from API");
                    }
                }
                
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            AllErrors.Add(reader.ReadToEnd());
                        }
                    }

                } else
                {
                    AllErrors.Add(e.Message);
                }
            }
            return new PostResults()
            {
                Results = AllResults,
                Errors = AllErrors
            };
        }
    }

    public class PostResults
    {
        public List<string> Results { get; set; }
        public List<string> Errors { get; set; }
    }
}