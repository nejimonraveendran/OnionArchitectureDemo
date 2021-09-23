using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Repository.DataContextInterfaces;
using OnionApp.Domain.Entities;
using OnionApp.Domain.Services.RepoServices;
using System;
using System.Linq;

namespace OnionApp.AppServices.Repository
{
    public class UserSqlRepository : RepositoryBase, IUserRepository
    {
        private readonly IMainDbContext _dbContext;

        public UserSqlRepository(IMainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(UserEntity seat)
        {          
            var entity = _dbContext.Set<UserEntity>().Add(new UserEntity { Name = seat.Name });
            entity.State = EntityState.Added;
        }

        public IQueryable<UserEntity> GetAllUsers()
        {
            return _dbContext.Set<UserEntity>().AsQueryable();
        }

      
    }
}
