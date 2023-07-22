using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using backend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence
{
  public class IdentityDbContext : IdentityDbContext<User>
  {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }
  }
}
