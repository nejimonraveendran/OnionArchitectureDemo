using OnionApp.AppServices.Common.Queries.Commands;
using OnionApp.AppServices.Common.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.Queries.Interfaces
{
    public interface IReportQueries
    {
        IEnumerable<GetAllUsersAddedFromToResult> GetAllUsersAddedFromTo(GetAllUsersAddedFromToCommand command);
    }
}
