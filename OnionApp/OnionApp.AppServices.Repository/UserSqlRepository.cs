using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Repository.DataInterfaces;
using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionApp.AppServices.Repository
{
    public class UserSqlRepository : RepositoryBase, IUserRepository
    {
        private readonly IDataContext _dataContext;

        public UserSqlRepository(IDataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public UserEntity GetById(int id)
        {
           return _dataContext.Set<UserEntity>().SingleOrDefault(x => x.Id == id);
        }

        public UserEntity Add(UserEntity entity)
        {          
            var entry = _dataContext.Set<UserEntity>().Add(entity);
            return entry.Entity;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _dataContext.Set<UserEntity>().ToList();
        }

        public UserEntity Update(UserEntity entity)
        {
           var entry = _dataContext.Set<UserEntity>().Update(entity);
           return entry.Entity;
        }

        public UserEntity Delete(UserEntity entity)
        {
            var entry = _dataContext.Set<UserEntity>().Remove(entity);
            return entry.Entity;
        }
    }
}
