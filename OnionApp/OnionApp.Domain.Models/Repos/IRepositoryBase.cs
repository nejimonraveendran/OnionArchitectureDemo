using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Domain.Models.Repos
{
    public interface IRepositoryBase
    {
        int SaveChanges();
    }
}
