using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Shared.Components;

public partial class AddressComponentForUSA : ComponentBase
{

  [Parameter] public RegisterModel RegisterModel { get; set; } = new RegisterModel ();
  [Parameter] public  EventCallback<RegisterModel> OnRegisterModelChanged { get; set; }

  [Inject]
  public IAddressService? AddressService { get; set; }
  [Inject]
  public ILogger<AddressComponentForUSA>? logger { get; set; }

  private CancellationToken _cancellationToken () => new CancellationTokenSource (TimeSpan.FromMinutes (1)).Token;

  public IpInfo IpInfo { get; set; } = new IpInfo ();

  protected override async Task OnInitializedAsync ()
  {
    try
    {
      IpInfo = (await AddressService!.GetIpInfoAsync (_cancellationToken ())) ?? IpInfo;
      MapRegisterModel ();
      StateHasChanged ();

      RegisterModel.OnChange += async () => await NotifyChange ();
    }
    catch ( Exception exc )
    {
      logger!.LogError (exc, "Error getting IP info {message}", exc.Message);
    }
  }

  protected async Task NotifyChange () => await OnRegisterModelChanged.InvokeAsync (RegisterModel);


  private void MapRegisterModel ()
  {
    RegisterModel.Region = IpInfo.Region;
    RegisterModel.City = IpInfo.City;
    RegisterModel.Country = IpInfo.Country;
    RegisterModel.Postal = IpInfo.Postal;
  }
}
