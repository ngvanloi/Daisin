﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TeamVM;
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
	public class TeamService : ITeamService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Team> _repo;

		public TeamService(IGenericRepository<Team> repo, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = _unitOfWork.GetGenericRepository<Team>();
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
			var team = _mapper.Map<Team>(request);
			await _repo.AddEntityAsync(team);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteTeamAsync(int id)
		{
			var team = await _repo.GetEntityByIdAsync(id);
			_repo.DeleteEntity(team);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateTeamAsync(TeamUpdateVM request)
		{
			var teamUpdate = _mapper.Map<Team>(request);
			_repo.UpdateEntity(teamUpdate);
			await _unitOfWork.CommitAsync();
		}

		public async Task<TeamUpdateVM> GetTeamById(int Id)
		{
			var team = await _repo.Where(x => x.Id == Id)
				.ProjectTo<TeamUpdateVM>(_mapper.ConfigurationProvider)
				.SingleAsync();

			return team;
		}
	}
}
