using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Web.Util
{
    public class Resender
    {
        public static WebRequest Resend(HttpRequestMessage requestMessage, bool api)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string uri = api?appSettings["ServerApi"]:appSettings["Server"];
            WebRequest resultRequest = WebRequest.Create(uri + requestMessage.RequestUri.AbsolutePath);
            resultRequest.Method = requestMessage.Method.Method;
            if (requestMessage.Content.Headers.ContentType != null)
            {
                resultRequest.ContentType = requestMessage.Content.Headers.ContentType.ToString();
            }
            AuthenticationHeaderValue authorization = requestMessage.Headers.Authorization;
            if (authorization != null)
            {
                resultRequest.Headers.Add("Authorization","Bearer "+authorization.Parameter);
            }
            string content = requestMessage.Content.ReadAsStringAsync().Result;
            byte[] data = Encoding.UTF8.GetBytes(content);
            Stream streamWriter = resultRequest.GetRequestStream();
            streamWriter.Write(data, 0, data.Length);
            streamWriter.Close();
            return resultRequest;
        }

        public static WebResponse GetResponse(bool api)
        {
            HttpRequestMessage requestMessage =
                (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
            WebRequest request = Resend(requestMessage, api);
            WebResponse response = request.GetResponse();
            return response;
        }

        public static T ReadResponse<T>(bool api)
        {
            WebResponse response = GetResponse(api);
            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                string text;
                using (var sr = new StreamReader(responseStream))
                {
                    text = sr.ReadToEnd();
                }
                T obj = (T)JsonConvert.DeserializeObject(text);
                responseStream.Close();
                return obj;
            }
            return default(T);
        }
    }
}