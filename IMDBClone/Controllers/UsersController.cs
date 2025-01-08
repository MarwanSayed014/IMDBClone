using IMDBClone.Dtos;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBClone.Controllers
{
    [Route("IMDBClone/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IAuthentication _authentication { get; }

        public UsersController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<string>> RegisterAsync(RegisterDto model)
        {
            try
            {
                var massage = await _authentication.RegisterAsync(model);
                switch (massage)
                {
                    case RegistrationMassages.Succeeded:
                        return "The user is registered successfully.";
                    case RegistrationMassages.UserNameAlreadyExists:
                        return "The user name already exists please change it and try again.";
                    case RegistrationMassages.Failed:
                    default:
                        return "The registration failed please try again.";
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto model)
        {
            try
            {
                var result = await _authentication.LoginAsync(model);
                if (result == null)
                    return Unauthorized();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
