using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApp.AppServices.Common.ApiServices.Commands;
using OnionApp.AppServices.Common.ApiServices.Interfaces;
using OnionApp.AppServices.Common.ApiServices.Results;
using OnionApp.AppServices.Common.Queries.Commands;
using OnionApp.AppServices.Common.Queries.Results;
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

        [HttpPost("users")]
        public ActionResult AddUser(AddUserCommand command)
        {
            _userService.AddUser(command);
            return Ok("User added");
        }

        [HttpPut("users")]
        public ActionResult UpdateUser(UpdateUserCommand command)
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
        public ActionResult<IEnumerable<GetAllUsersResult>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("users/created/dateRange")]
        public ActionResult<IEnumerable<GetAllUsersAddedFromToResult>> GetAllUsersAddedFromTo(DateTime fromDate, DateTime toDate)
        {
            return Ok(_userService.GetAllUsersAddedFromDateToDate(new GetAllUsersAddedFromToCommand { FromDate = fromDate, ToDate = toDate }));
        }
    }
}
