namespace Entities.DTOs.User
{
    public record UserUpdateForDTO:UserDTO
    {
        public HashSet<string> UserRoles { get; set; } = new HashSet<string>();
    }
}