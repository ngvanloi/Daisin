using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Portfolio> _repo;
        private readonly IImageHelper _imageHelper;

        public PortfolioService(IGenericRepository<Portfolio> repo, IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Portfolio>();
            _imageHelper = imageHelper;
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

            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.portfolio, null);
            if (imageResult.Error != null)
            {
                return;
            }
            request.FileName = imageResult.FileName!;
            request.FileType = imageResult.FileType!;

            var portfolio = _mapper.Map<Portfolio>(request);
            await _repo.AddEntityAsync(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(portfolio);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(portfolio.FileName);
        }

        public async Task UpdatePortfolioAsync(PortfolioUpdateVM request)
        {
            var oldPortfolio = await _repo.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();

            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.portfolio, null);
                if (imageResult.Error != null)
                {
                    return;
                }
                request.FileName = imageResult.FileName!;
                request.FileType = imageResult.FileType!;
            }

            var portfolioUpdate = _mapper.Map<Portfolio>(request);
            _repo.UpdateEntity(portfolioUpdate);
            await _unitOfWork.CommitAsync();
            if (request.Photo != null)
                _imageHelper.DeleteImage(oldPortfolio.FileName);
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
