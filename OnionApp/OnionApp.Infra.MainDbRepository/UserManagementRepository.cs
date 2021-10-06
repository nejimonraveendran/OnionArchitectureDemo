using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Models.Repos;
using OnionApp.Infra.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionApp.Infra.MainDbRepository
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly MainDbContext _dataContext;

        public UserManagementRepository(MainDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User GetUserById(int id)
        {
            return _dataContext.Set<User>().SingleOrDefault(x => x.Id == id);
        }

        public User AddUser(User user)
        {
            var entry = _dataContext.Set<User>().Add(user);
            return entry.Entity;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dataContext.Set<User>().ToList();
        }

        public User UpdateUser(User user)
        {
            var entry = _dataContext.Set<User>().Update(user);
            return entry.Entity;
        }

        public User DeleteUser(User user)
        {
            var entry = _dataContext.Set<User>().Remove(user);
            return entry.Entity;
        }

        public Role GetRoleById(int roleId)
        {
            return _dataContext.Set<Role>().SingleOrDefault(x => x.Id == roleId);
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges(true);
        }
    }

}
