using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.Domain.Models.Repos
{
    public interface IUserManagementRepository : IRepositoryBase
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        User AddUser(User entity);
        User UpdateUser(User entity);
        User DeleteUser(User entity);

        Role GetRoleById(int roleId);

    }
}
