using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Common.DbInterfaces;
using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Services.RepoServices;
using System;
using System.Collections.Generic;
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

        public UserEntity GetById(int id)
        {
           return _dbContext.Set<UserEntity>().SingleOrDefault(x => x.Id == id);
        }

        public UserEntity Add(UserEntity entity)
        {          
            var entry = _dbContext.Set<UserEntity>().Add(entity);
            //entry.State = EntityState.Added;
            return entry.Entity;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _dbContext.Set<UserEntity>().ToList();
        }

        public UserEntity Update(UserEntity entity)
        {
           var entry = _dbContext.Set<UserEntity>().Update(entity);
           return entry.Entity;
        }

        public UserEntity Delete(UserEntity entity)
        {
            var entry = _dbContext.Set<UserEntity>().Remove(entity);
            return entry.Entity;
        }
    }
}
