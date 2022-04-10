using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages;

public partial class Address : ComponentBase
{

  [Inject]
  public IAddressService? AddressService { get; set; }
  [Inject]
  public ILogger<Address>? logger { get; set; }

  private CancellationToken _cancellationToken () => new CancellationTokenSource (TimeSpan.FromMinutes (1)).Token;

  public IpInfo IpInfo { get; set; } = new IpInfo ();

  protected override async Task OnInitializedAsync ()
  {
    try
    {
      IpInfo = (await AddressService!.GetIpInfoAsync (_cancellationToken ())) ?? IpInfo;
      StateHasChanged ();
    }
    catch ( Exception exc )
    {
      logger!.LogError (exc, "Error getting IP info {message}", exc.Message);
    }
  }
}
