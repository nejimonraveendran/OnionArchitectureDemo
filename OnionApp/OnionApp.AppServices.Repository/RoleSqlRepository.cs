using OnionApp.AppServices.Repository.DataInterfaces;
using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Repository
{
    public class RoleSqlRepository : RepositoryBase, IRoleRepository
    {
        private readonly IDataContext _dataContext;

        public RoleSqlRepository(IDataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public RoleEntity GetById(int id)
        {
            return _dataContext.Set<RoleEntity>().SingleOrDefault(x => x.Id == id);
        }
    }
}
