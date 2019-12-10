using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using TUTA_Automation.Entities.JsonObjects;

namespace TUTA_Automation.Steps
{
    [Binding]
    public class TutaAPITestingSteps
    {
        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }
        internal static PostCodeObject PostCodeResult { get => postCodeResult; set => postCodeResult = value; }

        private static PostCodeObject postCodeResult = new PostCodeObject() { };

        private static string _postCodeUri;

        private int responseCodeValue;

        public static void SwapOutHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        [BeforeScenario]
        public void Before()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
        }

        [Given(@"I am using the base url '(.*)' value")]
        public void GivenIAmUsingTheBaseUrlHttpApi(string baseUrl)
        {
            if (HttpClient.BaseAddress == null)
            {
                HttpClient.BaseAddress = new Uri(baseUrl);
            }
        }

        [Given(@"I setup the request to GET for resource '(.*)' value")]
        public void GivenISetupTheRequestToGETForResource(string postCode)
        {
            HttpRequestMessage = new HttpRequestMessage();
            HttpRequestMessage.Method = new HttpMethod("GET");
            _postCodeUri = postCode;
        }

        [When(@"I send the request")]
        public void WhenISendTheRequest()
        {
            HttpRequestMessage.RequestUri = new Uri(_postCodeUri, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }

        [Then(@"I should receive a response")]
        public void ThenIShouldReceiveAResponse()
        {
            string json = @"" + HttpResponseMessage.Content.ReadAsStringAsync().Result + "";

            postCodeResult = JsonConvert.DeserializeObject<PostCodeObject>(json);
        }

        [Then(@"I should have a status code of (.*)")]
        public void ThenIShouldHaveAStatusCodeOf(int responseCode)
        {
            responseCodeValue = responseCode;

            Assert.That((int)HttpResponseMessage.StatusCode, Is.EqualTo(responseCode));
        }

        [Then(@"I validate '(.*)' should have '(.*)' value")]
        public void ThenIValidateAdmind_DistrictShouldHaveValue(string responseVariableName, string responseValue)
        {
            switch (responseVariableName)
            {
                case "admind_district":
                    Assert.That(postCodeResult.result.admin_district == responseValue,
                        "Someting went wrong! \n" +
                        "Expected result is: '" + responseValue + "' \n" +
                        "Actual result is: '" + postCodeResult.result.admin_district + "'.");
                    break;
                case "region":
                    Assert.That(postCodeResult.result.region == responseValue,
                        "Someting went wrong! \n" +
                        "Expected result is: '" + responseValue + "' \n" +
                        "Actual result is: '" + postCodeResult.result.region + "'.");
                    break;
                case "error":
                    Assert.That(postCodeResult.error == responseValue,
                        "Someting went wrong! \n" +
                        "Expected result is: '" + responseValue + "' \n" +
                        "Actual result is: '" + postCodeResult.error + "'.");
                    break;
            }

            //if (responseCodeValue != 200)
            //{
            //    Assert.That(postCodeResult.error == responseValue,
            //        "Someting went wrong! \n" +
            //        "Expected result is: " + responseValue + " \n" +
            //        "Actual result is: '" + postCodeResult.result.region + "'.");
            //}
            //else {
            //    Assert.That(postCodeResult.result.region == responseValue,
            //        "Someting went wrong! \n" +
            //        "Expected result is: " + responseValue + " \n" +
            //        "Actual result is: '" + postCodeResult.result.region + "'.");
            //}
        }

        [Then(@"I validate region should have '(.*)' value")]
        public void ThenIValidateRegionShouldHaveValue(string responseValue)
        {
            Assert.That(postCodeResult.result.region == responseValue,
                "Someting went wrong! \n" +
                "Expected result is: " + responseValue + " \n" +
                "Actual result is: '" + postCodeResult.result.region + "'.");
        }

    }
}
