using OnionApp.AppServices.Common.DbInterfaces;
using OnionApp.Domain.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Repository
{
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private readonly IMainDbContext _dbContext;

        public RepositoryBase(IMainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
