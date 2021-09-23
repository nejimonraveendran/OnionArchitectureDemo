using OnionApp.AppServices.Api.ViewModels.Input;
using OnionApp.AppServices.Api.ViewModels.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Api.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserInputVM seat);
        IQueryable<UserOutputVM> GetAllUsers();
    }
}
