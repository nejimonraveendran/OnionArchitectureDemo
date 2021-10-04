using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Repository.DataInterfaces;
using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionApp.AppServices.Repository
{
    public class UserManagementRepository : RepositoryBase, IUserManagementRepository
    {
        private readonly IDataContext _dataContext;

        public UserManagementRepository(IDataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public User GetUserById(int id)
        {
           return _dataContext.Set<User>().SingleOrDefault(x => x.Id == id);
        }

        public User AddUser(User entity)
        {          
            var entry = _dataContext.Set<User>().Add(entity);
            return entry.Entity;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dataContext.Set<User>().ToList();
        }

        public User UpdateUser(User entity)
        {
           var entry = _dataContext.Set<User>().Update(entity);
           return entry.Entity;
        }

        public User DeleteUser(User entity)
        {
            var entry = _dataContext.Set<User>().Remove(entity);
            return entry.Entity;
        }

        public Role GetRoleById(int roleId)
        {
            return _dataContext.Set<Role>().SingleOrDefault(x => x.Id == roleId);
        }
    }
}
