using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
	{
		public void Configure(EntityTypeBuilder<AppUserRole> builder)
		{
			builder.HasData(new AppUserRole
			{
				UserId = Guid.Parse("8CC8635C-47C6-4B98-98C4-A26894B18D24").ToString(),
				RoleId = Guid.Parse("AA3D9336-4414-4FB6-B5DA-D12DFC30E2EF").ToString(),
			},
			new AppUserRole
			{
				UserId = Guid.Parse("E137111E-77B7-40F8-9318-099522BA68AF").ToString(),
				RoleId = Guid.Parse("0F1C16F7-6D56-4DAD-9761-D03A63B42E87").ToString(),
			}) ;

		}
	}
}
