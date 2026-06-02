using System.Collections.Generic;
using System;
using System.Web;

#if NET40
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
#else
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace GhasedakSms.Framework
{
    public static class Helper
    {
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
            return AddQueryString(baseUrl, queryParams);
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
#if NET40
        // .NET 4.0: use Newtonsoft.Json (System.Text.Json is not supported on net40)
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters =
            {
                new StringEnumConverter { CamelCaseText = true },
                new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.ffffffZ" }
            },
            Formatting = Formatting.Indented
        };

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }
#else
        // .NET 4.7 / 4.8: use System.Text.Json (same as before)
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
#endif
    }
}
