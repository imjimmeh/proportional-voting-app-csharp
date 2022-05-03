using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Extensions;
using Jim.Core.Shared.APIs;
using Jim.Core.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserManagerService _usersService;

        public UsersController(IUserManagerService usersService, ILogger<UsersController> logger) : base(logger)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        //[Authorize(Roles = "GetUsers")]
        //[HttpGet]
        //public async Task<IActionResult> GetUsers(long take, long skip)
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserDTO newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(new CreateUserResponse(ModelState.GetErrorMessages().ToList()));

            try
            {
                var result = await _usersService.CreateNewUser(newUser);

                return result.HasValue ?
                    Ok(new CreateUserResponse(result.Value)) :
                    BadRequest(new CreateUserResponse(new ResponseError[] { new ResponseError("Error creating new user") } ));
            }
            catch(Exception ex)
            {
                return BadRequest(new CreateUserResponse(new ResponseError[] { new ResponseError("Error creating new user"), new ResponseError(ex.Message) }));
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "JimCustom")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(new GetUserDTO
            {
                Claims = new List<IClaim>
                {
                    new ClaimDTO
                    {
                        Type = "Test",
                        Value = "Test"
                    }
                }
            });
        }
    }
}
