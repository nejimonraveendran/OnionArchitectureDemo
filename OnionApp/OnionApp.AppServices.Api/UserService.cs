using OnionApp.AppServices.Common.ApiServices.DTOs.Input;
using OnionApp.AppServices.Common.ApiServices.DTOs.Output;
using OnionApp.AppServices.Common.ApiServices.Interfaces;
using OnionApp.CrossCutting.Logging.Interfaces;
using OnionApp.Domain.Entities;
using OnionApp.Domain.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Api
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger _logger;

        public UserService(IUserRepository userRepository, IAppLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void AddUser(UserInputDto user)
        {
            _userRepository.AddUser(new UserEntity { Id = user.Id, Name = user.Name });
            _userRepository.SaveChanges();

            _logger.Info($"User created, Id: {user.Id}, name: {user.Name}");
        }

        public IQueryable<UserOutputDto> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userRepository.GetAllUsers().Select(x => new UserOutputDto { Id = x.Id, UserName = x.Name });
        }
    }
}
