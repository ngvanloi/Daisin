using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppUserConfig : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			var admin = new AppUser
			{
				Id = Guid.Parse("8CC8635C-47C6-4B98-98C4-A26894B18D24").ToString(),
				Email = "nguyenloi.itse@gmail.com",
				NormalizedEmail = "NGUYENLOI.ITSE@GMAIL.COM",
				UserName = "TestAdmin",
				NormalizedUserName = "TESTADMIN",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			var adminPasswordHash = PasswordHash(admin, "Password12**");
			admin.PasswordHash = adminPasswordHash;
			builder.HasData(admin);

			var member = new AppUser
			{
				Id = Guid.Parse("E137111E-77B7-40F8-9318-099522BA68AF").ToString(),
				Email = "nguyenloi.site@gmail.com",
				NormalizedEmail = "NGUYENLOI.SITE@GMAIL.COM",
				UserName = "TestMember",
				NormalizedUserName = "TESTMEMBER",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				SecurityStamp = Guid.NewGuid().ToString(),
			};
			var memberPasswordHash = PasswordHash(member, "Password12**");
			member.PasswordHash = memberPasswordHash;
			builder.HasData(member);
		}

		private string PasswordHash(AppUser user, string password)
		{
			var passwordHasher = new PasswordHasher<AppUser>();
			return passwordHasher.HashPassword(user, password);
		}
	}
}
