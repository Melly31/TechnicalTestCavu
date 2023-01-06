using CavuTechTest.Models;
using CavuTechTest.Models.Request;
using CavuTechTest.Models.Response;
using MediatR;

namespace CavuTechTest.Mediation.Commands
{
    /// <summary>
    ///     Command to update a customer booking
    /// </summary>
    /// <param name="Request"></param>
    public record UpdateBookingCommand(UpdateBookingRequest Request) : IRequest<UpdateBookingResponse>;
}