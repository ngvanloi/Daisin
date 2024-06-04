using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configuration
{
	public class CategoryConfig : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
			builder.Property(x => x.UpdatedDate).HasMaxLength(10);

			builder.Property(x => x.RowVersion).IsRowVersion();

			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

			builder.HasData(new Category
			{
				Id = 1,
				Name = "Projects",
			},
			new Category
			{
				Id = 2,
				Name = "SiteWorks",
			});
		}
	}
}
