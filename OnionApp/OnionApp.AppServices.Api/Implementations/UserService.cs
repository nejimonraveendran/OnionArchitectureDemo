using OnionApp.AppServices.Api.Interfaces;
using OnionApp.AppServices.Api.ViewModels.Input;
using OnionApp.AppServices.Api.ViewModels.Output;
using OnionApp.CrossCutting.Logging.Interfaces;
using OnionApp.Domain.Entities;
using OnionApp.Domain.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Api.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebLogger _logger;

        public UserService(IUserRepository userRepository, IWebLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void AddUser(UserInputVM user)
        {
            _userRepository.AddUser(new UserEntity { Id = user.Id, Name = user.Name });
            _userRepository.SaveChanges();

            _logger.Info($"User created, Id: {user.Id}, name: {user.Name}");
        }

        public IQueryable<UserOutputVM> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userRepository.GetAllUsers().Select(x => new UserOutputVM { Id = x.Id, UserName = x.Name });
        }
    }
}
