using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.Domain.Models.Repos
{
    public interface IUserRepository : IRepositoryBase
    {
        UserEntity GetById(int id);
        IEnumerable<UserEntity> GetAll();
        UserEntity Add(UserEntity entity);
        UserEntity Update(UserEntity entity);
        UserEntity Delete(UserEntity entity);
        
    }
}
