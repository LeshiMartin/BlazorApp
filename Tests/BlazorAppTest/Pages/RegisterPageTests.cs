using BlazorApp.Pages;
using BlazorApp.Services;
using BlazorApp.Shared.Components;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace BlazorAppTest.Pages;

[TestFixture]
public class RegisterPageTests
{
  private Bunit.TestContext ctx = default!;
  [SetUp]
  public void Setup ()
  {
    ctx = new Bunit.TestContext ();
    var _addressService = new Mock<IAddressService> ();
    ctx.Services.AddScoped (x => _addressService.Object);
  }

  [TestCase]
  public void Verify_Register_Component_Is_Rendered ()
  {
    var comp = ctx.RenderComponent<Register> ();
    comp.Should ().NotBeNull ();
  }

  [TestCase]
  public void Verify_Register_Should_Have_Six_Text_Inputs_And_OneAddress_ForUsa_Component ()
  {
    var comp = ctx.RenderComponent<Register> ();
    var textInputs = comp.FindComponents<TextInput> ();
    var addressForUsa = comp.FindComponent<AddressComponentForUSA> ();

    textInputs.Should ().HaveCount (6);
    addressForUsa.Should ().NotBeNull ();
  }
}
