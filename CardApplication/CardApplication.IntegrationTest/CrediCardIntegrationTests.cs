using System;
using System.Collections.Generic;
using System.Net;
using CardApplication.IntegrationTest.Helpers;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace CardApplication.IntegrationTest
{
    [Trait("Category", "Integration")]
    [TestCaseOrderer("CardApplication.IntegrationTest.Helper.PriorityOrderer", "CardApplication.IntegrationTest")]
    public class CrediCardIntegrationTests
    {
        private AuthConfigOptions _authConfig;
        private ServerOptions _serverOptions;
        private Token _token;
        private readonly string _cardName;
        private readonly string _cardNumber;
        private readonly string _expiryDate;
        private CreditCardResponse _createdCard;
        private const string Resource = "creditcard";
        private const string GetEndpoint = "";
        
        public CrediCardIntegrationTests()
        {
            _authConfig = TestHelper.GetAuthConfig(Environment.CurrentDirectory);
            _serverOptions = TestHelper.GetServerOptions(Environment.CurrentDirectory);
            _cardName = "UserName1";
            _cardNumber = "4539990476967330";
            _expiryDate = "2023-09-09";
            _token = GetAccessToken();
        }
        
        private Token GetAccessToken()
        {
            var client = new RestClient($"https://{_authConfig.Domain}/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            var body =
                $"{{\"client_id\":\"{_authConfig.ClientId}\", \"client_secret\":\"{_authConfig.ClientSecrets}\", \"audience\":\"{_authConfig.Audience}\" ,\"grant_type\":\"{_authConfig.GrandType}\"}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            _token = JsonConvert.DeserializeObject<Token>(response.Content);
            return _token;
        }

        [Fact, TestPriority(1)]
        public void ShouldBeAbleToGetUnauthorized_WhenCallingPost()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact, TestPriority(2)]
        public void ShouldBeAbleToGetUnauthorized_WhenCallingGet()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode); 
        }
        
        [Fact, TestPriority(3)]
        public void ShouldBeAbleToGetUnauthorized_WhenCallingGetById()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);  
        }
        
        [Fact, TestPriority(5)]
        public void ShouldBeAbleToCreateAndGetCreditCard()
        {
            CreateCreditCard();
            var id = AssertCanGetCreditCardList();
            AssertCanGetOneCreditCard(id);
        }

        private void CreateCreditCard()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", $"{_token.token_type} {_token.access_token}");
            var body =
                $"{{\"name\": \"{_cardName}\",\"cardNumber\": \"{_cardNumber}\",\"cvc\": \"721\",\"expiryDate\":\"{_expiryDate}\"}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private long AssertCanGetCreditCardList()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"{_token.token_type} {_token.access_token}");

            IRestResponse response = client.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var list = JsonConvert.DeserializeObject<List<CreditCardResponse>>(response.Content);
            Assert.NotEmpty(list);
            _createdCard = list.Find(
                c => c.Name == _cardName && c.CardNumber == _cardNumber);
            Assert.NotNull(_createdCard);
            return _createdCard.Id;
        }

        private void AssertCanGetOneCreditCard(long id)
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/{id}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"{_token.token_type} {_token.access_token}");

            IRestResponse response = client.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var record = JsonConvert.DeserializeObject<CreditCardResponse>(response.Content);
            Assert.NotNull(record);
        }
    }
}