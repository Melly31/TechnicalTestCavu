using CavuTechTest.Models.Request;
using CavuTechTest.Models.Response;
using MediatR;

namespace CavuTechTest.Mediation.Commands
{
    /// <summary>
    ///     Command to create a customer booking
    /// </summary>
    /// <param name="Request"></param>
    public record CreateBookingCommand(CreateBookingRequest Request) : IRequest<CreateBookingResponse>;
}