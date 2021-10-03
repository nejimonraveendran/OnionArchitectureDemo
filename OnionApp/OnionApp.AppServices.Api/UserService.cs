using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Interfaces;
using OnionApp.AppServices.Common.ApiServices.Results;
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

        public void AddUser(AddUserCommand command)
        {
            _userRepository.AddUser(new UserEntity { Id = command.Id, Name = command.Name });
            _userRepository.SaveChanges();

            _logger.Info($"User created, Id: {command.Id}, name: {command.Name}");
        }

        public IQueryable<GetAllUsersResult> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userRepository.GetAllUsers().Select(x => new GetAllUsersResult { Id = x.Id, UserName = x.Name });
        }
    }
}
