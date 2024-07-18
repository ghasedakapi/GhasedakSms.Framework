using System.Collections.Generic;
using System;
using System.Web;

namespace GhasedakSms.Framework
{
    public class Helper
    {
        public static string BuildQueryString(string baseUrl, Dictionary<string, string> queryParams, string arrayKey = null, List<string> arrayValues = null)
        {
            var queryString = AddQueryString(baseUrl, queryParams);
            if (arrayKey != null && arrayValues != null)
            {
                foreach (var value in arrayValues)
                {
                    queryString = AddQueryString(queryString, new Dictionary<string, string> { { arrayKey, value } });
                }
            }
            return queryString;
        }

        private static string AddQueryString(string baseUrl, Dictionary<string, string> queryParams)
        {
            var uriBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var param in queryParams)
            {
                query[param.Key] = param.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
