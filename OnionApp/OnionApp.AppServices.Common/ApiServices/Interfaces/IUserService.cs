using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Results;
using OnionApp.AppServices.Common.Queries.Commands;
using OnionApp.AppServices.Common.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Common.ApiServices.Interfaces
{
    public interface IUserService
    {
        void AddUser(AddUserCommand command);
        void UpdateUser(UpdateUserCommand command);
        void DeleteUser(int id);

        IEnumerable<GetAllUsersResult> GetAllUsers();
        IEnumerable<GetAllUsersAddedFromToResult> GetAllUsersAddedFromDateToDate(GetAllUsersAddedFromToCommand command);


    }
}
