using System.Runtime.CompilerServices;
using System.Text.Json;
[assembly: InternalsVisibleTo ("BlazorAppTest")]
namespace BlazorApp.Services;

public interface IHttpService
{
  Task<T?> GetAsync<T> ( string url, CancellationToken cancellationToken = default );
}

public class HttpService : IHttpService
{
  private readonly HttpClient _httpClient;

  public HttpService ( HttpClient httpClient )
  {
    _httpClient = httpClient;
  }
  public async Task<T?> GetAsync<T> ( string url, CancellationToken cancellationToken = default )
  {
    var response = await _httpClient.GetAsync (url, cancellationToken);
    var json = await response.Content.ReadAsStringAsync ();
    return JsonSerializer.Deserialize<T> (json);
  }
}
