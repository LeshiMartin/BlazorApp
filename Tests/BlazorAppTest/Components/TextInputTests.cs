using AngleSharp.Dom;
using BlazorApp.Shared.Components;
using Bunit;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlazorAppTest.Components;

[TestFixture]
public class TextInputTests
{
  private Bunit.TestContext ctx = default!;

  [SetUp]
  public void Setup ()
  {
    ctx = new Bunit.TestContext ();
  }



  [TestCase]
  public void Verify_Text_Input ()
  {
    var component = ctx.RenderComponent<TextInput> ();
    var div = component.Find (".form-floating");
    div.Should ().NotBeNull ();

  }

  [TestCase]
  public void Verify_Default_Value ()
  {
    var component = ctx.RenderComponent<TextInput> ();
    var input = component.Find ("input");
    input.Should ().NotBeNull ();
    input.Attributes[ "value" ]!.Value.Should ().BeEmpty ();
    input.Id.Should ().NotBeEmpty ();
  }

  [TestCase]
  public void Verify_Input_Value_Should_Be_Set_To_ParameterValue ()
  {
    var val = "TestValue";
    var component = ctx.RenderComponent<TextInput> (ComponentParameter.CreateParameter (nameof (TextInput.Value), val));
    var input = component.Find ("input");
    input.Attributes["value"]!.Value.Should ().Be (val);
    input.Id.Should ().NotBeEmpty ();
  }

  [TestCase]
  public void Verify_Id_Of_Input_Should_Be_Set_To_Parameter_For ()
  {
    var val = "Test_Value";
    var component = ctx.RenderComponent<TextInput> (ComponentParameter.CreateParameter (nameof (TextInput.For), val));
    var input = component.Find ("input");
    input.Id.Should ().Be (val);
  }

  [TestCase]
  public void Verify_Placeholde_Of_Input_Should_Be_Set_To_Parameter_Label ()
  {
    var val = "Test_Value";
    var component = ctx.RenderComponent<TextInput> (ComponentParameter.CreateParameter (nameof (TextInput.Label), val));
    var input = component.Find ("input");
    input.Attributes["placeholder"]!.Value.Should ().Be (val);
  }

  [TestCase]
  public void Verify_Default_Label ()
  {
    var component = ctx.RenderComponent<TextInput> ();
    var label = component.Find ("label");
    label.Should ().NotBeNull ();
    label.TextContent.Should ().Be ("Label");
  }

  [TestCase]
  public void Verify_Label_Text_Content_Set_To_Parameter_Label_Value ()
  {
    var val = "Test_Value";
    var component = ctx.RenderComponent<TextInput> (ComponentParameter.CreateParameter (nameof (TextInput.Label), val));
    var label = component.Find ("label");
    label.TextContent.Should ().Be (val);
  }

  [TestCase]
  public void Verify_Label_For_Attribute_Set_To_Parameter_For_Value ()
  {
    var val = "Test_Value";
    var component = ctx.RenderComponent<TextInput> (ComponentParameter.CreateParameter (nameof (TextInput.For), val));
    var label = component.Find ("label");
    label.Attributes["for"]!.Value.Should ().Be (val);
  }
}
