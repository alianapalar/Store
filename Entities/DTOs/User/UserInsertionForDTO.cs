using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Entities.DTOs.User
{
    public record UserInsertionForDTO:UserDTO
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}