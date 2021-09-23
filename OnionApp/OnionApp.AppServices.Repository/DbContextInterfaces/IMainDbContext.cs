using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Repository.DbContextInterfaces;
using OnionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Repository.DataContextInterfaces
{
    /// <summary>
    /// Application's Main DB Context
    /// </summary>
    public interface IMainDbContext : IDbContext { }
}
