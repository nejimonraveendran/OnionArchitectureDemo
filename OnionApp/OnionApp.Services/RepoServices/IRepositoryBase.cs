using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Domain.Services.RepoServices
{
    public interface IRepositoryBase
    {
        int SaveChanges();
    }
}
