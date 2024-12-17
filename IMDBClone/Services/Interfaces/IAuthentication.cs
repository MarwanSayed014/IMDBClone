using IMDBClone.DTOs;
using IMDBClone.Types;
using System.Threading.Tasks;

namespace IMDBClone.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<RegistrationMassages> RegisterAsync(RegisterDTO model);
        Task<string> LoginAsync(LoginDTO model);
    }
}
