using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SS_Services.Models {

    public class IdentityDataContext : IdentityDbContext<IdentityUser> {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options) { }
    }
}
