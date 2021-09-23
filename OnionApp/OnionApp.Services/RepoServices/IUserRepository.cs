using OnionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.Domain.Services.RepoServices
{
    public interface IUserRepository : IRepositoryBase
    {
        void AddUser(UserEntity seat);
        IQueryable<UserEntity> GetAllUsers();
    }
}
