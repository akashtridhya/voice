using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using voice.middleware.Models.Account;
using voice.middleware.Models.Consts;

namespace voice.middleware.Services
{
    public class Service
    {
        private static readonly HttpClient client = new HttpClient();

        #region Post API Without Token
        public static async Task<dynamic> PostAPIWithoutToken(string url, StringContent stringContent = null)
        {
            try
            {
                var httpResponseMessage = client.PostAsync(url, stringContent).Result;
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    meta = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.meta.message,
                        statusCode = JsonResponse.meta.statusCode
                    }
                };
            }
            catch (Exception ex)
            {
                return new CallAPI
                {
                    meta = new ResponseMetaCallAPI
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
                var Token = (Contexts.AppContext.Current.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                var httpResponseMessage = await client.PostAsync(url, stringContent);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    meta = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.meta.message,
                        statusCode = JsonResponse.meta.statusCode
                    }
                };
            }
            catch (Exception)
            {
                return new CallAPI
                {
                    meta = new ResponseMetaCallAPI
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
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    meta = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.meta.message,
                        statusCode = JsonResponse.meta.statusCode
                    }
                };
            }
            catch (Exception)
            {
                return new CallAPI
                {
                    meta = new ResponseMetaCallAPI
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
                    meta = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.meta.message,
                        statusCode = JsonResponse.meta.statusCode
                    }
                };
            }
            catch (Exception ex)
            {
                return new CallAPI
                {
                    meta = new ResponseMetaCallAPI
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
                var Token = (Contexts.AppContext.Current.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                var httpResponseMessage = await client.GetAsync(url);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
                if (statusCode != StatusCodeConsts.Success)
                {
                    var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
                var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
                return new CallAPI
                {
                    data = JsonResponse.GetJsonData(),
                    meta = new ResponseMetaCallAPI
                    {
                        message = JsonResponse.meta.message,
                        statusCode = JsonResponse.meta.statusCode
                    }
                };
            }
            catch (Exception ex)
            {
                return new CallAPI
                {
                    meta = new ResponseMetaCallAPI
                    {
                        message = "Exception!",
                        statusCode = 1
                    }
                };
            }
        }
        #endregion Get API With Token
    }
}