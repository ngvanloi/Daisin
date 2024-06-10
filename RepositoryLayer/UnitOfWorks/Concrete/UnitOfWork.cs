using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
using RepositoryLayer.UnitOfWorks.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.UnitOfWorks.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public async Task<bool> CommitAsync()
		{
			try
			{
				await _context.SaveChangesAsync();
				return true;
			}
			catch (DbUpdateConcurrencyException)
			{
				return false;
			}
		}

		public ValueTask DisposeAsync()
		{
			return _context.DisposeAsync();
		}

		IGenericRepository<T> IUnitOfWork.GetGenericRepository<T>()
		{
			return new GenericRepositories<T>(_context);
		}
	}
}
