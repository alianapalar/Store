using Entities.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles{get;}
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityUser> GetOneUser(string userName);
        Task<UserUpdateForDTO> GetOneUserForUpdate(string userName);
        Task Update(UserUpdateForDTO userDto);
        Task<IdentityResult> CreateUser(UserInsertionForDTO userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDTO model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}