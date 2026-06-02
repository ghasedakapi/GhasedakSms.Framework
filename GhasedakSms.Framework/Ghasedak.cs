using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using GhasedakSms.Framework.Dto;
using System.Text;

namespace GhasedakSms.Framework
{
    public class Ghasedak
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Ghasedak(string apiKey)
        {
            _url = "https://gateway.ghasedak.me/rest/api/v1/WebService/";
//#if NET40
            System.Net.ServicePointManager.SecurityProtocol =
                (System.Net.SecurityProtocolType)3072; // Tls12
//#endif
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            _client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            _client.DefaultRequestHeaders.Add("Agent", "C#");
        }

        public async Task<ResponseDto<List<SmsStatusResponseItems>>> CheckSmsStatus(CheckSmsStatusInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString(_url + "CheckSmsStatus", new Dictionary<string, string>
            {
                { "Type", query.Type.ToString() },
                { "Ids", string.Join(",", query.Ids) }
            });

            HttpResponseMessage response;
            try
            {
#if NET40
                response = await _client.GetAsync(queryString).ConfigureAwait(false);
#else
                response = await _client.GetAsync(queryString, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<List<SmsStatusResponseItems>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<List<SmsStatusResponseItems>>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<AccountInformationResponse>> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
#if NET40
                response = await _client.GetAsync(_url + "GetAccountInformation").ConfigureAwait(false);
#else
                response = await _client.GetAsync(_url + "GetAccountInformation", cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<AccountInformationResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<AccountInformationResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<ReceivedSmsesResponse>> GetReceivedSmses(GetReceivedSmsInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString(_url + "GetReceivedSmses", new Dictionary<string, string>
            {
                { "LineNumber", query.LineNumber },
                { "IsRead", query.IsRead.ToString() }
            });

            HttpResponseMessage response;
            try
            {
#if NET40
                response = await _client.GetAsync(queryString).ConfigureAwait(false);
#else
                response = await _client.GetAsync(queryString, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<ReceivedSmsesResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<ReceivedSmsesResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<ReceivedSmsesPagingResponse>> GetReceivedSmsesPaging(GetReceivedSmsPagingInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString(_url + "GetReceivedSmsesPaging", new Dictionary<string, string>
            {
                { "LineNumber",  query.LineNumber },
                { "IsRead",      query.IsRead.ToString() },
                { "StartDate",   query.StartDate.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "EndDate",     query.EndDate.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "PageIndex",   query.PageIndex.ToString() },
                { "PageSize",    query.PageSize.ToString() },
            });

            HttpResponseMessage response;
            try
            {
#if NET40
                response = await _client.GetAsync(queryString).ConfigureAwait(false);
#else
                response = await _client.GetAsync(queryString, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<ReceivedSmsesPagingResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<ReceivedSmsesPagingResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<CheckOtpTemplateResponse>> GetOtpParameters(GetOtpParametersInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString(_url + "GetOtpTemplateParameters", new Dictionary<string, string>
            {
                { "TemplateName", query.TemplateName },
            });

            HttpResponseMessage response;
            try
            {
#if NET40
                response = await _client.GetAsync(queryString).ConfigureAwait(false);
#else
                response = await _client.GetAsync(queryString, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<CheckOtpTemplateResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<CheckOtpTemplateResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<SendSingleResponse>> SendSingleSMS(SendSingleSmsInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = Helper.Serialize(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
#if NET40
                response = await _client.PostAsync(_url + "SendSingleSMS", content).ConfigureAwait(false);
#else
                response = await _client.PostAsync(_url + "SendSingleSMS", content, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendSingleResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<SendSingleResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<SendBulkResponse>> SendBulkSMS(SendBulkInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = Helper.Serialize(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
#if NET40
                response = await _client.PostAsync(_url + "SendBulkSMS", content).ConfigureAwait(false);
#else
                response = await _client.PostAsync(_url + "SendBulkSMS", content, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendBulkResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<SendBulkResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<SendPairToPairResponse>> SendPairToPairSMS(SendPairToPairInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = Helper.Serialize(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
#if NET40
                response = await _client.PostAsync(_url + "SendPairToPairSMS", content).ConfigureAwait(false);
#else
                response = await _client.PostAsync(_url + "SendPairToPairSMS", content, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendPairToPairResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<SendPairToPairResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<SendOtpResponse>> SendOtpWithParams(SendOldOtpInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = Helper.Serialize(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
#if NET40
                response = await _client.PostAsync(_url + "SendOtpWithParams", content).ConfigureAwait(false);
#else
                response = await _client.PostAsync(_url + "SendOtpWithParams", content, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendOtpResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<SendOtpResponse>(response).ConfigureAwait(false);
        }

        public async Task<ResponseDto<SendOtpResponse>> SendOtpSMS(SendOtpInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = Helper.Serialize(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
#if NET40
                response = await _client.PostAsync(_url + "SendOtpSMS", content).ConfigureAwait(false);
#else
                response = await _client.PostAsync(_url + "SendOtpSMS", content, cancellationToken).ConfigureAwait(false);
#endif
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendOtpResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            return await ParseResponse<SendOtpResponse>(response).ConfigureAwait(false);
        }

        // ── Shared response parsing ───────────────────────────────────────────────

        private static async Task<ResponseDto<T>> ParseResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Helper.Deserialize<ResponseDto<T>>(content);
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var error = Helper.Deserialize<ResponseDto>(content);
                return new ResponseDto<T>
                {
                    IsSuccess = error.IsSuccess,
                    Message = error.Message,
                    StatusCode = error.StatusCode
                };
            }
            catch
            {
                return new ResponseDto<T>
                {
                    IsSuccess = false,
                    StatusCode = (int)response.StatusCode,
                    Message = response.ReasonPhrase
                };
            }
        }
    }
}
