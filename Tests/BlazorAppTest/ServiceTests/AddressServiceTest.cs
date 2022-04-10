using BlazorApp.Models;
using BlazorApp.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorAppTest.ServiceTests;
[TestFixture]
public class AddressServiceTest
{
  private Mock<IHttpService> _httpService = default!;
  private AddressService _addressService = default!;

  [SetUp]
  public void Setup ()
  {
    _httpService = new Mock<IHttpService> ();
    var configuration = new Mock<IConfiguration> ();
    configuration.Setup (x => x[ "IpInfoBaseUrl" ]).Returns ("");
    _addressService = new AddressService (_httpService.Object, configuration.Object);
  }

  [TestCase]
  public async Task GetIpInfoAsync_Should_Return_Instance_Of_IpInfo ()
  {
    var ipInfo = new IpInfo ();
    _httpService.Setup (x => x.GetAsync<IpInfo> (It.IsAny<string> (), It.IsAny<CancellationToken> ()))
      .ReturnsAsync (ipInfo);

    var result = await _addressService.GetIpInfoAsync ();
    result.Should ().NotBeNull ();
    result.Should ().BeOfType<IpInfo> ();

  }
}
