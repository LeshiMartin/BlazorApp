using BlazorApp.Models;
using BlazorApp.Pages;
using BlazorApp.Services;
using BlazorApp.Shared.Components;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace BlazorAppTest.Components;
[TestFixture]
public class AddressComponentTest
{
  private Bunit.TestContext ctx = default!;
  private Mock<IAddressService> _addressService = default!;

  [SetUp]
  public void Setup ()
  {
    ctx = new Bunit.TestContext ();
    _addressService = new Mock<IAddressService> ();
    _addressService.Setup (x => x.GetIpInfoAsync (It.IsAny<CancellationToken> ())).ReturnsAsync (
      new IpInfo ()
      {
        City = "City",
        Country = "Country",
        Region = "Region",
        Postal = "Postal"
      });
    ctx.Services.AddScoped (x => _addressService.Object);
  }
  [TestCase]
  public void Verify_Address_Component_Is_Rendered ()
  {
    var component = ctx.RenderComponent<Address> ();
    component.Should ().NotBeNull ();
  }


  [TestCase]
  public void Verify_Address_Component_Has_Four_Text_Inputs ()
  {

    var component = ctx.RenderComponent<Address> ();
    var textInputs = component.FindComponents<TextInput> ();
    textInputs.Should ().HaveCount (4);
  }

  [TestCase]
  public void Verify_All_TextInputs_DoesnT_Have_Default_Values ()
  {
    var component = ctx.RenderComponent<Address> ();
    var textInputs = component.FindComponents<TextInput> ();
    textInputs.All (x => !string.IsNullOrEmpty (x.Instance.Value)).Should ().BeTrue ();
  }
}
