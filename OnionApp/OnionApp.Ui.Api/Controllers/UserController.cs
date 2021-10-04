using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApp.AppServices.Api;
using OnionApp.AppServices.Api.Services;
using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionApp.Ui.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("users")]
        public ActionResult AddUser(AddUserRequest command)
        {
            _userService.AddUser(command);
            return Ok("User added");
        }

        [HttpPut("users")]
        public ActionResult UpdateUser(UpdateUserRequest command)
        {
            _userService.UpdateUser(command);
            return Ok("User updated");
        }

        [HttpDelete("users/{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok("User deleted");
        }


        [HttpGet("users")]
        public ActionResult<IEnumerable<GetAllUsersResponse>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        
    }
}
