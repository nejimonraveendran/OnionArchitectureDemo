using OnionApp.AppServices.Repository.DataInterfaces;
using OnionApp.Domain.Models.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Repository
{
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private readonly IDataContext _dataContext;

        public RepositoryBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
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
            return _dataContext.SaveChanges();
        }
    }
}
