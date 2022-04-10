namespace BlazorApp.Models;

public class AddressModel
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public RegisterModel RegisterModel { get; set; } = new RegisterModel ();
}
public class RegisterModel
{
  public event Action OnChange = delegate { };

  private string? _country;

  public string? Country
  {
    get { return _country; }
    set
    {
      _country = value;
      OnChange ();
    }
  }

  private string? _city;

  public string? City
  {
    get { return _city; }
    set
    {
      _city = value;
      OnChange ();
    }
  }


  private string? _postal;

  public string? Postal
  {
    get { return _postal; }
    set
    {
      _postal = value;
      OnChange ();
    }
  }

  private string? _region;

  public string? Region
  {
    get { return _region; }
    set
    {
      _region = value;
      OnChange ();
    }
  }



}