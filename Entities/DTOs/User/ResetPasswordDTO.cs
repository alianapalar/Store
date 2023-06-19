using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.User
{
    public record ResetPasswordDTO
    {
        public string Username { get; init; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; init; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="ConfirmPassword is required.")]
        [Compare("Password",ErrorMessage ="Password and ConfirmPassword must be match.")]
        public string ConfirmPassword { get; init; }
    }
}