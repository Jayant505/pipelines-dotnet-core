using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnetcore_sample.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace dotnetcore_tests
{
    [TestClass]
    public class Partner2MsdJsonLogicAppUnitTest
    {
        [TestMethod]
        public void Partner2MsdJsonAPiTest()
        {
            #region call Logic app

            string baseUrl = "https://prod-21.eastasia.logic.azure.com:443/";
            string requestUrl = "/workflows/603f285bd3934dc68e003327c01144a9/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=y2N8tP0npFHNbEIiBlrCgnvlmIjTUBS7GqCrKTNvF0s";

            string jsonString = TestData.partner2msdJsonJsonString;
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var responseResult = CommonMethod.PostResource(baseUrl, requestUrl, jsonString, requestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            //to call msd system.
            string msdBaseUrl = " https://apim-test-api-au.azure-api.net/";
            string msdrequestUrl = "/echo/partner2msdJson";

            string subscriptionkey = "0aa02a70a75f479d88d73e6e3c011a91";
            string msdJsonString = TestData.msdJsonString;
            Dictionary<string, string> msdRequestHeader = new Dictionary<string, string>();
            msdRequestHeader.Add("ocp-apim-subscription-key", "0aa02a70a75f479d88d73e6e3c011a91");
            var responseResult1 = CommonMethod.PostResource(msdBaseUrl, msdrequestUrl, msdJsonString, msdRequestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            #endregion

            #region get cache value
            string msdRequestUrl = "/echo/resource?param1=sample";
            string cacheKey = "parner2msdjson1247286586";
            var responseResult2 = CommonMethod.GetResource(msdBaseUrl, msdRequestUrl, subscriptionkey, cacheKey);
            Assert.AreNotSame("non-data", responseResult2);

            #endregion

            #region test case
            if (responseResult2 != "non-data")
            {
                JObject inPutBody = JObject.Parse(TestData.partner2msdJsonJsonString);
                JObject outPutBody = JObject.Parse(responseResult2);

            }
            #endregion

        }
    }
}

