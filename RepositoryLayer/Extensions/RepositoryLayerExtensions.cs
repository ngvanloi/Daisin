using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Extensions
{
	public static class RepositoryLayerExtensions
	{
		public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services, IConfiguration config) 
		{
			services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Daisin")));
			return services;
		}
	}
}
