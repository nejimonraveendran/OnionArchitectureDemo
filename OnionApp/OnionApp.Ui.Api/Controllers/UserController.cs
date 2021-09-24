using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApp.AppServices.Common.ApiServices.DTOs.Output;
using OnionApp.AppServices.Common.ApiServices.Interfaces;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("users")]
        public IEnumerable<UserOutputDto> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
    }
}
