using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TeamVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
	public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Team> _repo;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private readonly string Section = "Team section";
        public TeamService(IGenericRepository<Team> repo, IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Team>();
            _imageHelper = imageHelper;
            _toasty = toasty;
        }

        public async Task<List<TeamListVM>> GetAllAsync()
        {
            var teamListVM = await _repo.GetAll()
                .ProjectTo<TeamListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return teamListVM;
        }

        public async Task AddTeamAsync(TeamAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.team, null);
            if (imageResult.Error != null)
            {
                _toasty.AddErrorToastMessage(imageResult.Error, new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle });
                return;
            }
            request.FileName = imageResult.FileName!;
            request.FileType = imageResult.FileType!;

            var team = _mapper.Map<Team>(request);
            await _repo.AddEntityAsync(team);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(team);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(team.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateTeamAsync(TeamUpdateVM request)
        {
            var oldTeam = await _repo.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();

            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.team, null);
                if (imageResult.Error != null)
                {
                    _toasty.AddErrorToastMessage(imageResult.Error, new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle });
                    return;
                }
                request.FileName = imageResult.FileName!;
                request.FileType = imageResult.FileType!;
            }

            var teamUpdate = _mapper.Map<Team>(request);
            _repo.UpdateEntity(teamUpdate);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				if (request.Photo != null)
				{
					_imageHelper.DeleteImage(request.FileName);
				}
				throw new ClientSideExceptions(ExceptionMessage.ConcurencyException);
			}
			if (request.Photo != null)
				_imageHelper.DeleteImage(oldTeam.FileName);
			_toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
		}

        public async Task<TeamUpdateVM> GetTeamById(int Id)
        {
            var team = await _repo.Where(x => x.Id == Id)
                .ProjectTo<TeamUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return team;
        }

		//UI Service Methods
		public async Task<List<TeamUI>> GetAllListForUI()
		{
			var uiList = await _repo.GetAll().ProjectTo<TeamUI>(_mapper.ConfigurationProvider).ToListAsync();
			return uiList;
		}
	}
}
