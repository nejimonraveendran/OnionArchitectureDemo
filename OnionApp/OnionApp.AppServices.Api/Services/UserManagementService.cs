using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Results;
using OnionApp.CrossCutting.Logging.Interfaces;
using OnionApp.Domain.Models.Entities;
using OnionApp.Domain.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionApp.AppServices.Api.Services
{
    public class UserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IAppLogger _logger;

        public UserManagementService(IUserManagementRepository userRepository,  
            IAppLogger logger)
        {
            _userManagementRepository = userRepository;
            _logger = logger;
        }

        public void AddUser(AddUserRequest request)
        {
            var role = _userManagementRepository.GetRoleById(request.RoleId);
            if (role == null)
                throw new InvalidOperationException("Invalid role");

            var newUser = new User 
            { 
                Name = request.Name, 
                Role = role, 
                DateCreated = DateTime.Now 
            };

            var user = _userManagementRepository.AddUser(newUser);
            _userManagementRepository.SaveChanges();

            _logger.Info($"User created, Id: {user.Id}, name: {request.Name}");
        }

        public void DeleteUser(int id)
        {
            var user = _userManagementRepository.GetUserById(id);
            if (user == null)
                throw new InvalidOperationException("Invalid user");

            _userManagementRepository.DeleteUser(user);
            _userManagementRepository.SaveChanges();

        }

        public IEnumerable<GetAllUsersResponse> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userManagementRepository.GetAllUsers()
                .Select(x => new GetAllUsersResponse { Id = x.Id, UserName = x.Name,  DateCreated = x.DateCreated});
        }

      
        public void UpdateUser(UpdateUserRequest request)
        {
            var user = _userManagementRepository.GetUserById(request.Id);

            if (user == null)
                throw new InvalidOperationException("Invalid user");

            var role = _userManagementRepository.GetRoleById(request.Id);
            if (role == null)
                throw new InvalidOperationException("Invalid role");

            user.Name = request.Name;
            user.DateCreated = DateTime.Now;
            user.Role = role;
            
            _userManagementRepository.UpdateUser(user);
            _userManagementRepository.SaveChanges();
        }
    }
}
