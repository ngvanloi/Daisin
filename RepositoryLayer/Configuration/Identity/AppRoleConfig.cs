using EntityLayer.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configuration.Identity
{
	public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasData(
				new AppRole
				{
					Id = Guid.Parse("AA3D9336-4414-4FB6-B5DA-D12DFC30E2EF").ToString(),
					Name = "SuperAdmin",
					NormalizedName = "SUPERADMIN",
					ConcurrencyStamp = Guid.NewGuid().ToString(),
				},
				new AppRole
				{
					Id = Guid.Parse("0F1C16F7-6D56-4DAD-9761-D03A63B42E87").ToString(),
					Name = "Member",
					NormalizedName = "MEMBER",
					ConcurrencyStamp = Guid.NewGuid().ToString(),
				}
			);
		}
	}
}
