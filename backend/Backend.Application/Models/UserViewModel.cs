namespace Backend.Application.Models;

public class UserViewModel
{
  public string Id { get; set; }
  public string Email { get; set; }
  public string Username { get; set; }
  public string Token { get; set; }
  public bool IsAdmin { get; set; }
  public bool RememberMe { get; set; }
}
