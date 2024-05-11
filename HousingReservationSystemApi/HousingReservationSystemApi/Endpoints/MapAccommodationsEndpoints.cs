using HousingReservationSystemApi.Contracts.Accommodation;
using HousingReservationSystemApplication.Services;
using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApi.Endpoints
{
    public static class MapAccommodationsEndpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPut("update/{id}", UpdateAccommodation);
            app.MapDelete("delete/{id}", DeleteAccommodation);

            return app;
        }

        private static async Task<IResult> UpdateAccommodation(Guid id, UpdateAccommodationRequest request, AccommodationService accommodationService)
        {
            await accommodationService.UpdateAccommodationAsync(id, request.Name, request.Location);
            return Results.Ok();
        }

        private static async Task<IResult> DeleteAccommodation(Guid id, AccommodationService accommodationService)
        {
            await accommodationService.DeleteAccommodationAsync(id);
            return Results.Ok();
        }
    }
}
