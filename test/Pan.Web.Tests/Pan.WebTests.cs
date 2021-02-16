using FluentAssertions;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Pan.Web.Tests
{
    public class TestRequest
    {
        public string Msg { get; set; }
    }

    public class PanWebTest : IClassFixture<MockServerFixture>
    {
        private const string GetApi = "/get";
        private const string PostApi = "/post";
        private const string PutApi = "/put";
        private const string DeleteApi = "/delete";
        private const string ExpectedRequest = @"{ ""msg"": ""success"" }";
        private const string ExpectedResponse = @"{ ""msg"": ""success"" }";
        private readonly MockServerFixture _mockServerFixture;
        private readonly object _request = new TestRequest {Msg = "success"};
        private readonly IRestClient _restClient;

        public PanWebTest(
            MockServerFixture mockServerFixture,
            IRestClient restClient)
        {
            _mockServerFixture = mockServerFixture;
            _restClient = restClient;
            _restClient.Host(_mockServerFixture.Server.Urls[0]);
        }

        [Fact]
        public async void Get()
        {
            _mockServerFixture
                .Server
                .Given(Request
                    .Create()
                    .WithPath(GetApi)
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithBody(ExpectedResponse)
                );

            var response = await _restClient.Get(GetApi, default, default, default);
            response.Should().Be(ExpectedResponse);
        }

        [Fact]
        public async void Post()
        {
            _mockServerFixture
                .Server
                .Given(Request
                    .Create()
                    .WithPath(PostApi)
                    .WithBody(new JsonMatcher(ExpectedRequest, true))
                    .UsingPost())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithBody(ExpectedResponse)
                );

            var response = await _restClient.Post(PostApi, _request, default, default);
            response.Should().Be(ExpectedResponse);
        }

        [Fact]
        public async void Put()
        {
            _mockServerFixture
                .Server
                .Given(Request
                    .Create()
                    .WithPath(PutApi)
                    .WithBody(new JsonMatcher(ExpectedRequest, true))
                    .UsingPut())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                );

            var response = await _restClient.Put(PutApi, _request, default, default);
            response.Should().Be(true);
        }

        [Fact]
        public async void Delete()
        {
            _mockServerFixture
                .Server
                .Given(Request
                    .Create()
                    .WithPath(DeleteApi)
                    .UsingDelete())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                );

            var response = await _restClient.Delete(DeleteApi, _request, default, default);
            response.Should().Be(true);
        }
    }
}