using Microsoft.EntityFrameworkCore;
using OnionApp.AppServices.Common.ApiServices.Results;
using OnionApp.AppServices.Common.DbInterfaces;
using OnionApp.AppServices.Common.Queries.Commands;
using OnionApp.AppServices.Common.Queries.Interfaces;
using OnionApp.AppServices.Common.Queries.Results;
using OnionApp.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionApp.AppServices.Queries
{
    public class ReportQueries : IReportQueries
    {
        private readonly IMainDbContext _mainDbContext;

        public ReportQueries(IMainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        IEnumerable<GetAllUsersAddedFromToResult> IReportQueries.GetAllUsersAddedFromTo(GetAllUsersAddedFromToCommand command)
        {
            return _mainDbContext.Set<UserEntity>().AsNoTracking()
                .Where(x => x.DateCreated >= command.FromDate && x.DateCreated <= command.ToDate)
                .Select(x => new GetAllUsersAddedFromToResult { Id = x.Id, Name = x.Name, DateCreated = x.DateCreated })
                .ToList();
        }
    }
}
