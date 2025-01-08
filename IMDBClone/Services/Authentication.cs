using IMDBClone.Dtos;
using IMDBClone.Helpers;
using IMDBClone.Helpers.Interfaces;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace IMDBClone.Services
{
    public class Authentication : IAuthentication
    {
        public IUserManager _userManager { get; }
        public IRoleManager _roleManager { get; }
        public IPasswordManager _passwordManager { get; }
        public JWTHelper _jWTHelper { get; }

        public Authentication(IUserManager userManager, IRoleManager roleManager,
            IPasswordManager passwordManager, JWTHelper jWTHelper )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordManager = passwordManager;
            _jWTHelper = jWTHelper;
        }

        public async Task<string> LoginAsync(LoginDto model)
        {
            try
            {
                var hashedPassword = await _passwordManager.Hash(model.Password);

                var user = await _userManager.GetUserAsync(model.UserName, hashedPassword);
                if (user == null)
                    return null;

                var roles = await _roleManager.GetUserRolesAsync(user.Id);

                return await _jWTHelper.GenerateAcessToken(await _jWTHelper.GenerateUserClaims(user, roles));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RegistrationMassages> RegisterAsync(RegisterDto model)
        {
            try
            {
                if (await IsRegisterModelValidAsync(model) == RegistrationMassages.UserNameNotExists)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Password = await _passwordManager.Hash(model.Password),
                        Name = model.UserName
                    };

                     
                    await _userManager.CreateUserAsync(user);
                    await _roleManager.AddUserToRoleAsync(user.Id, RoleTypes.User);
                    
                    return RegistrationMassages.Succeeded;
                }
                return RegistrationMassages.UserNameAlreadyExists;
            }
            catch (Exception ex)
            {
                return RegistrationMassages.Failed;
            }
        }

        private async Task<RegistrationMassages> IsRegisterModelValidAsync(RegisterDto model)
        {
            try
            {
                if (await _userManager.UserNameExistsAsync(model.UserName))
                    return RegistrationMassages.UserNameAlreadyExists;
                return RegistrationMassages.UserNameNotExists;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
