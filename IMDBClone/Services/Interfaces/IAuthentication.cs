using IMDBClone.Dtos;
using IMDBClone.Types;
using System.Threading.Tasks;

namespace IMDBClone.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<RegistrationMassages> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}
