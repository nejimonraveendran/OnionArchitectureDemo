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
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAppLogger _logger;

        public UserService(IUserRepository userRepository,  
            IRoleRepository roleRepository,
            IAppLogger logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public void AddUser(AddUserRequest request)
        {
            var role = _roleRepository.GetById(request.RoleId);
            if (role == null)
                throw new InvalidOperationException("Invalid role");

            var newUser = new UserEntity 
            { 
                Name = request.Name, 
                Role = role, 
                DateCreated = DateTime.Now 
            };

            var user = _userRepository.Add(newUser);
            _userRepository.SaveChanges();

            _logger.Info($"User created, Id: {user.Id}, name: {request.Name}");
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new InvalidOperationException("Invalid user");

            _userRepository.Delete(user);
            _userRepository.SaveChanges();

        }

        public IEnumerable<GetAllUsersResponse> GetAllUsers()
        {
            _logger.Info("GetAllUsers called");
            return _userRepository.GetAll()
                .Select(x => new GetAllUsersResponse { Id = x.Id, UserName = x.Name,  DateCreated = x.DateCreated});
        }

      
        public void UpdateUser(UpdateUserRequest request)
        {
            var user = _userRepository.GetById(request.Id);

            if (user == null)
                throw new InvalidOperationException("Invalid user");

            var role = _roleRepository.GetById(request.Id);
            if (role == null)
                throw new InvalidOperationException("Invalid role");

            user.Name = request.Name;
            user.DateCreated = DateTime.Now;
            user.Role = role;
            
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }
    }
}
