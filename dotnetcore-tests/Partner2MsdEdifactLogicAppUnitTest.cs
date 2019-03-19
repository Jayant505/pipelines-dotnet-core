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
    public class Partner2MsdEdifactLogicAppUnitTest
    {
        [TestMethod]
        public void Partner2MsdEdifactAPITest()
        {
        #region call Logic app

       
            string baseUrl = "https://prod-02.eastasia.logic.azure.com:443/";
            string requestUrl = "/workflows/2a6c49422313438b812644ee305dace3/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Qs6js5QZp-ACntSyHdGgBBlnlPS36GJJf5lIe8pcepA";

            string jsonString = TestData.partner2msdEdifactJsonString;
            Dictionary<string, string> logicAppRequestHeader = new Dictionary<string, string>();

            logicAppRequestHeader.Add("Message-Id", "1");
            logicAppRequestHeader.Add("AS2-TO", "662424795MSDT");
            logicAppRequestHeader.Add("AS2-FROM", "ZZT657257051WIH");
            logicAppRequestHeader.Add("Accept-Encoding", "gzip,deflate");
            logicAppRequestHeader.Add("Disposition-Notification-To", "localhost");
         
            string mediaType = "text/plain";
            var responseResult = CommonMethod.PostResource(baseUrl, requestUrl, jsonString, logicAppRequestHeader, mediaType);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            //to call msd system.
            string msdBaseUrl = "https://apim-test-api-au.azure-api.net/";
            string msdrequestUrl = "/echo/partner2msdEdifact";
            string subscriptionkey = "0aa02a70a75f479d88d73e6e3c011a91";
            string msdJsonString = TestData.msdEdifactString;
            Dictionary<string, string> msdAppRequestHeader = new Dictionary<string, string>();
            msdAppRequestHeader.Add("ocp-apim-subscription-key", "0aa02a70a75f479d88d73e6e3c011a91");
            var responseResult1 = CommonMethod.PostResource(msdBaseUrl, msdrequestUrl, msdJsonString, msdAppRequestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            #endregion

            #region get cache value
            string msdRequestUrl = "/echo/resource?param1=sample";
            string cacheKey = "parner2msdEdifact1";                           
            var responseResult2 = CommonMethod.GetResource(msdBaseUrl, msdRequestUrl, subscriptionkey, cacheKey);
            Assert.AreNotSame("non-data", responseResult2);

            #endregion


            #region test case
            if (responseResult2 != "non-data")
            {
                //JObject inPutBody = JObject.Parse(TestData.partner2msdJsonJsonString);
                JObject outPutBody = JObject.Parse(responseResult2);

            }
            #endregion

        }





    }
}
