using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nitto1320LegendsServer.Models;

namespace Nitto1320LegendsServer.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
