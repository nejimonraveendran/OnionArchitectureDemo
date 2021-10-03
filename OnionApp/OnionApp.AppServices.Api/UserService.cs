using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Interfaces;
using OnionApp.AppServices.Common.ApiServices.Results;
using OnionApp.AppServices.Common.Queries.Commands;
using OnionApp.AppServices.Common.Queries.Interfaces;
using OnionApp.AppServices.Common.Queries.Results;
using OnionApp.CrossCutting.Logging.Interfaces;
using OnionApp.Domain.Models.Entities;
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
        private readonly IReportQueries _reportQueries;
        private readonly IAppLogger _logger;

        public UserService(IUserRepository userRepository, IReportQueries reportQueries, IAppLogger logger)
        {
            _userRepository = userRepository;
            _reportQueries = reportQueries;
            _logger = logger;
        }

        public void AddUser(AddUserCommand command)
        {
            var newUser = new UserEntity { Id = command.Id, Name = command.Name, DateCreated = DateTime.Now };

            _userRepository.Add(newUser);
            _userRepository.SaveChanges();

            _logger.Info($"User created, Id: {command.Id}, name: {command.Name}");
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new InvalidOperationException("Invalid user");

            _userRepository.Delete(user);
            _userRepository.SaveChanges();

        }

        public IEnumerable<GetAllUsersResult> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userRepository.GetAll()
                .Select(x => new GetAllUsersResult { Id = x.Id, UserName = x.Name,  DateCreated = x.DateCreated});
        }

        public IEnumerable<GetAllUsersAddedFromToResult> GetAllUsersAddedFromDateToDate(GetAllUsersAddedFromToCommand command)
        {
            return _reportQueries.GetAllUsersAddedFromTo(command);
        }

        public void UpdateUser(UpdateUserCommand command)
        {
            var user = _userRepository.GetById(command.Id);

            if (user == null)
                throw new InvalidOperationException("Invalid user");

            user.Name = command.Name;
            user.DateCreated = DateTime.Now;

            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }
    }
}
