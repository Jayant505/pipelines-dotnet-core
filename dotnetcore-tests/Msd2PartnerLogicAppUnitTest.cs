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
    public class Msd2PartnerLogicAppUnitTest
    {
        [TestMethod]
        public void Msd2PartnerEdiTest()
        {
            #region call Logic app

            string baseUrl = "https://prod-19.eastasia.logic.azure.com:443/";
            string requestUrl = "/workflows/2cbfde6f37ce46b0bda464513fc578c2/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=vRt6YR5hx5Z0yg7zLYy8S9mGARwjfZxyVPALbWFcYAQ";

            string jsonString = TestData.msd2partnerforEdiJsonString;
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var responseResult = CommonMethod.PostResource(baseUrl, requestUrl, jsonString, requestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            //to call msd system.
            string msdBaseUrl = "https://apim-test-api-au.azure-api.net/";
            string msdrequestUrl = "/echo/msd2partnerforedi";
            Dictionary<string, string> partnerRequestHeader = new Dictionary<string, string>();
            partnerRequestHeader.Add("ocp-apim-subscription-key", "0aa02a70a75f479d88d73e6e3c011a91");
            partnerRequestHeader.Add("Message-ID", "300000033");
      
            string msdJsonString = TestData.outPutmsd2partnerfroEdiString;
             
            var responseResult1 = CommonMethod.PostResource(msdBaseUrl, msdrequestUrl, msdJsonString, partnerRequestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            #endregion

            #region get cache value
            string msdRequestUrl = "/echo/resource?param1=sample";
            string cacheKey = "msd2partner300000033";
            string subscriptionkey = "0aa02a70a75f479d88d73e6e3c011a91";
            var responseResult2 = CommonMethod.GetResource(msdBaseUrl, msdRequestUrl, subscriptionkey, cacheKey);
            Assert.AreNotSame("non-data", responseResult2);

            #endregion


            #region test case
            //if (responseResult2 != "non-data")
            //{
            //    JObject inPutBody = JObject.Parse(TestData.partner2msdJsonJsonString);
            //    JObject outPutBody = JObject.Parse(responseResult2);

            //}
            #endregion

        }


        [TestMethod]
        public void Msd2PartnerJsonTest()
        {
            #region call Logic app

            string baseUrl = "https://prod-19.eastasia.logic.azure.com:443/";
            string requestUrl = "/workflows/2cbfde6f37ce46b0bda464513fc578c2/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=vRt6YR5hx5Z0yg7zLYy8S9mGARwjfZxyVPALbWFcYAQ";

            string jsonString = TestData.msd2partnerforJsonJsonString;
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var responseResult = CommonMethod.PostResource(baseUrl, requestUrl, jsonString, requestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            //to call msd system.
            string msdBaseUrl = " https://apim-test-api-au.azure-api.net/";
            string msdrequestUrl = "/echo/msd2partnerforjson";
            Dictionary<string, string> partnerRequestHeader = new Dictionary<string, string>();
            partnerRequestHeader.Add("ocp-apim-subscription-key", "0aa02a70a75f479d88d73e6e3c011a91");
            partnerRequestHeader.Add("Message-ID", "SECC2");

            string msdJsonString = TestData.outPutmsd2partforJsonString;

            var responseResult1 = CommonMethod.PostResource(msdBaseUrl, msdrequestUrl, msdJsonString, partnerRequestHeader);
            Assert.AreEqual(responseResult.StatusCode, System.Net.HttpStatusCode.OK);

            #endregion

            //#region get cache value
            string msdRequestUrl = "/echo/resource?param1=sample";
            string cacheKey = "msd2partnerSECC2";
            string subscriptionkey = "0aa02a70a75f479d88d73e6e3c011a91";
            var responseResult2 = CommonMethod.GetResource(msdBaseUrl, msdRequestUrl, subscriptionkey, cacheKey);
            Assert.AreNotSame("non-data", responseResult2);

            //#endregion


            //#region test case
            ////if (responseResult2 != "non-data")
            ////{
            ////    JObject inPutBody = JObject.Parse(TestData.partner2msdJsonJsonString);
            ////    JObject outPutBody = JObject.Parse(responseResult2);

            ////}
            //#endregion

        }
    }
}