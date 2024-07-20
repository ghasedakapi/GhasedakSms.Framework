using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using GhasedakSms.Fremework.Dto;
using GhasedakSms.Framework.Dto;
using Newtonsoft.Json;
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
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            _client.DefaultRequestHeaders.Add("ApiKey", apiKey);
        }

        public async Task<ResponseDto<List<SmsStatusResponseItems>>> CheckSmsStatus(CheckSmsStatusInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString($"{_url}CheckSmsStatus", new Dictionary<string, string>
            {
                { "Type", query.Type.ToString() }
            }, "Ids", query.Ids);

            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(queryString, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<List<SmsStatusResponseItems>>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<List<SmsStatusResponseItems>>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<List<SmsStatusResponseItems>>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<List<SmsStatusResponseItems>>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<AccountInformationResponse>> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync($"{_url}GetAccountInformation", cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<AccountInformationResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<AccountInformationResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<AccountInformationResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<AccountInformationResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<ReceivedSmsesResponse>> GetReceivedSmses(GetReceivedSmsInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString($"{_url}GetReceivedSmses", new Dictionary<string, string>
            {
                { "LineNumber", query.LineNumber },
                { "IsRead", query.IsRead.ToString() }
            });
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(queryString, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<ReceivedSmsesResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<ReceivedSmsesResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<ReceivedSmsesResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        StatusCode = (int)result.StatusCode,
                        Message = result.Message
                    };
                }
                catch
                {
                    return new ResponseDto<ReceivedSmsesResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<ReceivedSmsesPagingResponse>> GetReceivedSmsesPaging(GetReceivedSmsPagingInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString($"{_url}GetReceivedSmsesPaging", new Dictionary<string, string>
            {
                {"LineNumber" , query.LineNumber},
                {"IsRead" , query.IsRead.ToString()},
                {"StartDate" , query.StartDate.ToString("yyyy-MM-ddTHH:mm:ss")},
                {"EndDate" , query.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")},
                {"PageIndex" , query.PageIndex.ToString()},
                {"PageSize" , query.PageSize.ToString() },
            });

            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(queryString, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<ReceivedSmsesPagingResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<ReceivedSmsesPagingResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<ReceivedSmsesPagingResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        StatusCode = result.StatusCode,
                        Message = result.Message
                    };
                }
                catch
                {
                    return new ResponseDto<ReceivedSmsesPagingResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<CheckOtpTemplateResponse>> GetOtpParameters(GetOtpParametersInput query, CancellationToken cancellationToken = default)
        {
            var queryString = Helper.BuildQueryString($"{_url}GetOtpTemplateParameters", new Dictionary<string, string>
            {
                { "TemplateName", query.TemplateName },
            });
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(queryString, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<CheckOtpTemplateResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<CheckOtpTemplateResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<CheckOtpTemplateResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<CheckOtpTemplateResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }

        }

        public async Task<ResponseDto<SendSingleResponse>> SendSingleSMS(SendSingleSmsInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"{_url}SendSingleSMS", content, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendSingleResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<SendSingleResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<SendSingleResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<SendSingleResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<SendBulkResponse>> SendBulkSMS(SendBulkInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"{_url}SendBulkSMS", content, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendBulkResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<SendBulkResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<SendBulkResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<SendBulkResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<SendPairToPairResponse>> SendPairToPairSMS(SendPairToPairInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"{_url}SendPairToPairSMS", content, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendPairToPairResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<SendPairToPairResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<SendPairToPairResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<SendPairToPairResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<SendOtpResponse>> SendOtpWithParams(SendOldOtpInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"{_url}SendOtpWithParams", content, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendOtpResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<SendOtpResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<SendOtpResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<SendOtpResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }

        public async Task<ResponseDto<SendOtpResponse>> SendOtpSMS(SendOtpInput command, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(command);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"{_url}SendOtpSMS", content, cancellationToken);
            }
            catch (WebException ex)
            {
                return new ResponseDto<SendOtpResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)(ex.Status == WebExceptionStatus.Timeout ? HttpStatusCode.RequestTimeout : HttpStatusCode.InternalServerError)
                };
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto<SendOtpResponse>>(content);
                return result;
            }
            else
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return new ResponseDto<SendOtpResponse>()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode
                    };
                }
                catch
                {
                    return new ResponseDto<SendOtpResponse>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    };
                }
            }
        }
    }
}
