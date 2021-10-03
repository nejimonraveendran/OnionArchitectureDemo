using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Common.ApiServices.Interfaces
{
    public interface IUserService
    {
        void AddUser(AddUserCommand command);
        IQueryable<GetAllUsersResult> GetAllUsers();
    }
}
