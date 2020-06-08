using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsServices.Models {

    public class IdentityDataContext : IdentityDbContext<IdentityUser> {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options) { }
    }
}
