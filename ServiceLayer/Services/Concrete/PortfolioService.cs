using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Concrete
{
	public class PortfolioService : IPortfolioService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Portfolio> _repo;

		public PortfolioService(IGenericRepository<Portfolio> repo, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.GetGenericRepository<Portfolio>();
		}

		public async Task<List<PortfolioListVM>> GetAllAsync()
		{
			var portfolioListVM = await _repo.GetAll()
				.ProjectTo<PortfolioListVM>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return portfolioListVM;
		}

		public async Task AddPortfolioAsync(PortfolioAddVM request)
		{
			var portfolio = _mapper.Map<Portfolio>(request);
			await _repo.AddEntityAsync(portfolio);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeletePortfolioAsync(int id)
		{
			var portfolio = await _repo.GetEntityByIdAsync(id);
			_repo.DeleteEntity(portfolio);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdatePortfolioAsync(PortfolioUpdateVM request)
		{
			var portfolioUpdate = _mapper.Map<Portfolio>(request);
			_repo.UpdateEntity(portfolioUpdate);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PortfolioUpdateVM> GetPortfolioById(int Id)
		{
			var portfolio = await _repo.Where(x => x.Id == Id)
				.ProjectTo<PortfolioUpdateVM>(_mapper.ConfigurationProvider)
				.SingleAsync();

			return portfolio;
		}
	}
}
