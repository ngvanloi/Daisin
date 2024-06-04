using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
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
	public class SocialMediaService : ISocialMediaService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<SocialMedia> _repo;

		public SocialMediaService(IGenericRepository<SocialMedia> repo, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.GetGenericRepository<SocialMedia>();
		}

		public async Task<List<SocialMediaListVM>> GetAllAsync()
		{
			var socialMediaListVM = await _repo.GetAll()
				.ProjectTo<SocialMediaListVM>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return socialMediaListVM;
		}

		public async Task AddSocialMediaAsync(SocialMediaAddVM request)
		{
			var socialMedia = _mapper.Map<SocialMedia>(request);
			await _repo.AddEntityAsync(socialMedia);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteSocialMediaAsync(int id)
		{
			var socialMedia = await _repo.GetEntityByIdAsync(id);
			_repo.DeleteEntity(socialMedia);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateSocialMediaAsync(SocialMediaUpdateVM request)
		{
			var socialMediaUpdate = _mapper.Map<SocialMedia>(request);
			_repo.UpdateEntity(socialMediaUpdate);
			await _unitOfWork.CommitAsync();
		}

		public async Task<SocialMediaUpdateVM> GetSocialMediaById(int Id)
		{
			var socialMedia = await _repo.Where(x => x.Id == Id)
				.ProjectTo<SocialMediaUpdateVM>(_mapper.ConfigurationProvider)
				.SingleAsync();

			return socialMedia;
		}
	}
}
