using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Users
{
    public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);
}
