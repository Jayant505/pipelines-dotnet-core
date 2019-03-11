using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnetcore_sample.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace dotnetcore_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void About()
        {
          // Arrange
          HomeController controller = new HomeController();

          // Act
          ViewResult result = controller.About() as ViewResult;

          // Assert
          Assert.AreEqual("Your application description page.", result.ViewData["Message"]);
        }

        [TestMethod]
        public void Contact()
        {
          // Arrange
          HomeController controller = new HomeController();

          // Act
          ViewResult result = controller.Contact() as ViewResult;

          // Assert
          Assert.AreEqual("Your contact page.", result.ViewData["Message"]);
        }

        [TestMethod]
        public void APITest()
        {
            String resultContent = String.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://login.microsoftonline.com/");
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
                var content = new FormUrlEncodedContent(new[]
                {
                  new KeyValuePair<string, string>("grant_type", "client_credentials"),
                  new KeyValuePair<string, string>("client_id", "a314e658-f6d8-4af8-8afd-e4cf09ad2edb"),
                  new KeyValuePair<string, string>("client_secret", "qZY6oqvHCW5p0QhjjlU0LyxoFx6PPo6yXsVzOoU0W0o="),
                  new KeyValuePair<string, string>("resource", "2adcb359-2123-4716-9580-8dea6524f975")

                   });

                //var result = await client.PostAsync()
                var postTokenTask = client.PostAsync("/5c7d0b28-bdf8-410c-aa93-4df372b16203/oauth2/token", content);
                postTokenTask.Wait();

                var readStringTask = postTokenTask.Result.Content.ReadAsStringAsync();
                readStringTask.Wait();

                resultContent = readStringTask.Result;

                //Assert.AreEqual("Your contact page.", );
                Assert.IsNotNull(resultContent);

            }
         
        }

      
    }
}
