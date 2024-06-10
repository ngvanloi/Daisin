using CoreLayer.BaseEntities;
using EntityLayer.Identity.Entities;
using EntityLayer.WebApplication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public AppDbContext()
		{
		}

		public DbSet<HomePage> HomePages { get; set; }
		public DbSet<About> Abouts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Service> Services{ get; set; }
		public DbSet<Testimonal> Testimonals{ get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<SocialMedia> SocialMedias{ get; set; }
		public DbSet<Portfolio> Portfolios{ get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var item in ChangeTracker.Entries())
			{
				if (item.Entity is BaseEntity entity)
				{
					switch (item.State)
					{
						case EntityState.Added:
							entity.CreatedDate = DateTime.Now.ToString("d");
							break;
						case EntityState.Modified:
							Entry(entity).Property(x => x.CreatedDate).IsModified = false;
							entity.UpdatedDate = DateTime.Now.ToString("d");
							break;
						default:
							break;
					}
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
