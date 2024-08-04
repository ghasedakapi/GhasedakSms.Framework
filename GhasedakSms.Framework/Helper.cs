using System.Collections.Generic;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GhasedakSms.Framework
{
    public static class Helper
    {
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public static T Deserialize<T>(string json)
        {
            return JsonHelper.Deserialize<T>(json);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonHelper.Serialize(obj);
        }

        public static string BuildQueryString(string baseUrl, Dictionary<string, string> queryParams)
        {
            var queryString = AddQueryString(baseUrl, queryParams);
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
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new DateTimeConverter()
        },
            WriteIndented = true
        };

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }

        private class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ"));
            }
        }
    }
}
