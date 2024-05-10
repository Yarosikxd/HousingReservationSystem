using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Users
{
    public record RegisterUserRequest(
    [Required] string UserName,
    [Required] string Password,
    [Required] string Email);
}
