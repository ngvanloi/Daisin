using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
    public class AppUserClaimConfig : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.HasData(new AppUserClaim
            {
                Id = 1,
                UserId = Guid.Parse("E137111E-77B7-40F8-9318-099522BA68AF").ToString(),
                ClaimType = "AdminObserverExpireDate",
                ClaimValue = "06/10/2023",
            });
        }
    }
}
