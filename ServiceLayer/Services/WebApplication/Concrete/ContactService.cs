using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactVM;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Contact> _repo;

        public ContactService(IGenericRepository<Contact> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Contact>();
        }

        public async Task<List<ContactListVM>> GetAllAsync()
        {
            var contactListVM = await _repo.GetAll()
                .ProjectTo<ContactListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return contactListVM;
        }

        public async Task AddContactAsync(ContactAddVM request)
        {
            var contact = _mapper.Map<Contact>(request);
            await _repo.AddEntityAsync(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateContactAsync(ContactUpdateVM request)
        {
            var contactUpdate = _mapper.Map<Contact>(request);
            _repo.UpdateEntity(contactUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ContactUpdateVM> GetContactById(int Id)
        {
            var contact = await _repo.Where(x => x.Id == Id)
                .ProjectTo<ContactUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return contact;
        }
    }
}
