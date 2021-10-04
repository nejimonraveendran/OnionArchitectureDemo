using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Domain.Models.Repos
{
    public interface IRoleRepository : IRepositoryBase
    {
        RoleEntity GetById(int id);
    }
}
