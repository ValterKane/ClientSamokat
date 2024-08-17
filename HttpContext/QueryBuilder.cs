using System.Configuration;
using System.Web;

namespace ClientSamokat.HttpContext
{
    public class QueryBuilder : IQueryBuilder
    {
        public string GetQueryString(KeyValuePair<string, string>[] QueryParams, string NameOfConnection)
        {
            if (QueryParams != null)
            {
                string uri_base = ConfigurationManager.AppSettings[NameOfConnection];
                UriBuilder builder = new(uri_base);
                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

                for (int i = 0; i < QueryParams.Length; i++)
                {
                    query[QueryParams[i].Key] = QueryParams[i].Value;
                }
                builder.Query = query.ToString();
                return builder.ToString();
            }
            else
            {
                string uri_base = ConfigurationManager.AppSettings[NameOfConnection];
                UriBuilder builder = new(uri_base);
                return builder.ToString();
            }

        }

        public string GetRESTString(string param, string NameOfConnection)
        {
            if (param == null)
                throw new ArgumentNullException("Строка параметра не может быть Null в REST запросе. Для параметризированного запроса используйте GetQueryString");
            else
            {
                string uri_base = $"{ConfigurationManager.AppSettings[NameOfConnection]}/{param}";
                UriBuilder builder = new(uri_base);
                return builder.ToString();
            }
        }
    }
}
