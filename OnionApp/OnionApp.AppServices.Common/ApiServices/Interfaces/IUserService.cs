using OnionApp.AppServices.Common.ApiServices.DTOs.Input;
using OnionApp.AppServices.Common.ApiServices.DTOs.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Common.ApiServices.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserInputDto seat);
        IQueryable<UserOutputDto> GetAllUsers();
    }
}
