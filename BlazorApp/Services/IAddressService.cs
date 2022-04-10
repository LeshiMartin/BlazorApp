using BlazorApp.Models;

namespace BlazorApp.Services;

public interface IAddressService
{
  Task<IpInfo?> GetIpInfoAsync ( CancellationToken cancellationToken = default );
}

internal class AddressService : IAddressService
{
  private readonly IHttpService _httpService;
  private readonly IConfiguration _configuration;

  public AddressService ( IHttpService httpService, IConfiguration configuration )
  {
    _httpService = httpService;
    _configuration = configuration;
  }
  public async Task<IpInfo?> GetIpInfoAsync ( CancellationToken cancellationToken = default )
  {
    var info = await _httpService.GetAsync<IpInfo> (
     $"{_configuration[ "IpInfoBaseUrl" ]}", cancellationToken
   );
    return info;
  }
}
