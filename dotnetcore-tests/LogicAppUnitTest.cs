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
    [TestClass]
    public class LogicAppUnitTest
    {
        [TestMethod]
        public void APITest1()
        {
            String resultContent = String.Empty;
            using (var client = new HttpClient())
            {
            
                client.BaseAddress = new Uri("https://prod-12.eastasia.logic.azure.com:443/");
                client.DefaultRequestHeaders.Accept.Add
                                (
                                   new MediaTypeWithQualityHeaderValue("application/json")
                                );


                //other acount 
                //var content = new FormUrlEncodedContent(new[]
                //  {
                //  new KeyValuePair<string, string>("grant_type", "client_credentials"),
                //  new KeyValuePair<string, string>("client_id", "0B17F60A-6D69-426B-9ABD-79F35A6E9F7B"),
                //  new KeyValuePair<string, string>("client_secret", "53b8b19adffa41a3e87dbbd8858977ae")
                //   });



                //my account
                //var content = new FormUrlEncodedContent(new[]
                //{
                //   new KeyValuePair<string, string>("name", "Tom"),
                //    new KeyValuePair<string, string>("address", "Tom"),
                //     new KeyValuePair<string, string>("title", "Tom"),
                //});
                string jsonString = "{ \"name\" : \"Tom\", \"address\" : \"Tom\" , \"title\" : \"Tom\" }";
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                //var result = await client.PostAsync()
                var postTokenTask = client.PostAsync("/workflows/73a53d06ff174e68ac9af6b86064e56f/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=hGnPGgTu4Wn9CfXafGGtGzBhnidY8zd73v5-TA8aYEo", content);
                //var postTokenTask = client.GetAsync("/workflows/73a53d06ff174e68ac9af6b86064e56f/triggers/manual/paths/invoke/customers/Tom?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=hGnPGgTu4Wn9CfXafGGtGzBhnidY8zd73v5-TA8aYEo");
                postTokenTask.Wait();

                var readStringTask = postTokenTask.Result.Content.ReadAsStringAsync();
                readStringTask.Wait();

                resultContent = readStringTask.Result;

                //Assert.AreEqual("Your contact page.", );
                Assert.IsNotNull(resultContent);
                Assert.AreEqual("Hi Tom", resultContent);
               

            }
         
        }

      
    }
}
