using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.DbInterfaces
{
    /// <summary>
    /// Base Interface for any Application DBs.  If there are multiple Application DBs, there needs to be a separate interface for each for DI purposes, but they all can inherit this interface to avoid defining the DbSet and SaveChanges again and again
    /// </summary>
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
