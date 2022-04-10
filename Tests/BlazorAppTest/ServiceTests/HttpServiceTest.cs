using BlazorApp.Services;
using FluentAssertions;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorAppTest.ServiceTests;

[TestFixture]
public class HttpServiceTest
{
  private Mock<HttpMessageHandler> _handlerMock = default!;
  private HttpClient _httpClient = default!;
  private HttpService _service = default!;

  [SetUp]
  public void Setup ()
  {
    //Create MockHttpClient
    _handlerMock = new Mock<HttpMessageHandler> ();
    _httpClient = new HttpClient (_handlerMock.Object);
    _service = new HttpService (_httpClient);
  }

  [TestCase]
  public async Task GetAsyncShouldReturn_Response_Casted ()
  {
    _handlerMock.Protected ().Setup<Task<HttpResponseMessage>> (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage> (),
                    ItExpr.IsAny<CancellationToken> ()
                )
                .ReturnsAsync (new HttpResponseMessage
                {
                  StatusCode = HttpStatusCode.OK,
                  Content = new StringContent ("{\"Name\":\"test\"}")
                });

    var result = await _service.GetAsync<MockResponse> ("http://test.com");
    result.Should ().NotBeNull ();
    result!.Name.Should ().Be ("test");
  }
}


 class MockResponse
{
  public string Name { get; set; }
}
