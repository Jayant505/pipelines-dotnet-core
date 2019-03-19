using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnetcore_sample.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;

namespace dotnetcore_tests
{
    public class CommonMethod
    {

        public static string GetResource(string baseUrl, string requestUrl,string subscriptionkey =null, string cachekey =null)
        {
            String resultContent = String.Empty;
            HttpResponseMessage responseMsg;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add
                                (
                                   new MediaTypeWithQualityHeaderValue("application/json")
                                );
               if(!String.IsNullOrEmpty(subscriptionkey))  client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", subscriptionkey);
               if(!String.IsNullOrEmpty(cachekey)) client.DefaultRequestHeaders.Add("cache-key", cachekey);

                var postTokenTask = client.GetAsync(requestUrl);
                postTokenTask.Wait();

                responseMsg = postTokenTask.Result;

                var readStringTask = postTokenTask.Result.Content.ReadAsStringAsync();
                readStringTask.Wait();

                resultContent = readStringTask.Result;


            }
            return resultContent;
        }

        public static HttpResponseMessage PostResource(string baseUrl, string requestUrl, string jsonString, Dictionary<string, string> requestHeader, string medaiyType = "application/json")
        {
            String resultContent = String.Empty;
            HttpResponseMessage responseMsg;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add
                                (
                                   new MediaTypeWithQualityHeaderValue("*/*")
                                );

                if (requestHeader.Count > 0)
                {
                    foreach (var item in requestHeader)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }


                //string jsonString = "{ \"name\" : \"Tom\", \"address\" : \"Tom\" , \"title\" : \"Tom\" }";
                var content = new StringContent(jsonString, null, medaiyType);

                var postTokenTask = client.PostAsync(requestUrl, content);

                postTokenTask.Wait();

                responseMsg = postTokenTask.Result;
                //var readStringTask = postTokenTask.Result.Content.ReadAsStringAsync();
                
                //readStringTask.Wait();

                //resultContent = readStringTask.Result;
            }
            return responseMsg;
        }

    }
}
