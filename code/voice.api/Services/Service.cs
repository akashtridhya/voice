using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using voice.models;
using voice.models.callapi;

namespace voice.api.Services
{
    public static class Service
    {
        private static readonly HttpClient client = new HttpClient();

        #region Post API Without Token
        public static async Task<dynamic> PostAPIWithoutToken(string url, StringContent stringContent = null)
        {
            try
            {
                var httpResponseMessage = await client.PostAsync(url, stringContent);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return JsonResponseList;
                }
                else
                {
                    var JsonResponse = JsonConvert.DeserializeObject<dynamic>(response);
                    return new CallAPI
                    {
                        data = JsonResponse,
                        error = null,
                    };
                }
            }
            catch (Exception)
            {
                return new CallAPI
                {
                    error = new ResponseMetaCallAPI
                    {
                        message = "Exception!",
                        statusCode = 1
                    }
                };
            }
        }
        #endregion Post API Without Token

        #region Post API With Token
        public static async Task<dynamic> PostAPIWithToken(string url, StringContent stringContent = null)
        {
            try
            {
                var httpResponseMessage = await client.PostAsync(url, stringContent);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                IEnumerable<string> cookies = httpResponseMessage.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                var sessionCorrelationId = cookies.ElementAt(0);
                var userCorrelationId = cookies.ElementAt(1);
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return JsonResponseList;
                }
                else
                {
                    var JsonResponse = JsonConvert.DeserializeObject<dynamic>(response);
                    return new CallAPI
                    {
                        data = JsonResponse,
                        error = null,
                        SessionCorrelationId = sessionCorrelationId,
                        UserCorrelationId = userCorrelationId
                    };
                }
            }
            catch (Exception)
            {
                return new CallAPI
                {
                    error = new ResponseMetaCallAPI
                    {
                        message = "Exception!",
                        statusCode = 1
                    }
                };
            }
        }
        #endregion Post API With Token

        #region Post Upload Image
        public static async Task<dynamic> PostUploadFile(string url, string type, IFormFile formDataContent = null)
        {
            try
            {
                var Token = (Contexts.AppContext.Current.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

                byte[] data;
                using (var br = new BinaryReader(formDataContent.OpenReadStream()))
                    data = br.ReadBytes((int)formDataContent.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);

                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                multiContent.Add(bytes, "file", formDataContent.FileName);
                multiContent.Add(new StringContent(type), "type");

                var httpResponseMessage = await client.PostAsync(url, multiContent);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return new CallAPIList
                    {
                        error = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.error.message,
                            statusCode = JsonResponseList.error.statusCode
                        }
                    };
                }
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    error = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.error.message,
                        statusCode = JsonResponse.error.statusCode
                    }
                };
            }
            catch (Exception)
            {
                return new CallAPI
                {
                    error = new ResponseMetaCallAPI
                    {
                        message = "Exception!",
                        statusCode = 1
                    }
                };
            }
        }
        #endregion Post Upload Image

        #region Get API Without Token
        public static async Task<dynamic> GetAPIWithoutToken(string url)
        {
            try
            {
                var httpResponseMessage = await client.GetAsync(url);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    error = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.error.message,
                        statusCode = JsonResponse.error.statusCode
                    }
                };
            }
            catch (Exception ex)
            {
                return new CallAPI
                {
                    error = new ResponseMetaCallAPI
                    {
                        message = "Exception!",
                        statusCode = 1
                    }
                };
            }
        }
        #endregion Get API Without Token

        #region Get API With Token
        public static async Task<dynamic> GetAPIWithToken(string url)
        {
            try
            {
                //var Token = (Contexts.AppContext.Current.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", "testKey"); 
                var httpResponseMessage = await client.GetAsync(url);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                return response;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #endregion Get API With Token
    }
}