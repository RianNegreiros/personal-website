﻿using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Models;

public class User : IdentityUser
{
  public string PersistentToken { get; set; }
}