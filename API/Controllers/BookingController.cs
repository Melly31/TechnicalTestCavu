using System.Net;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Mediation.Orchestrator;
using CavuTechTest.Models.Exceptions;
using CavuTechTest.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CavuTechTest.API.Controllers
{
    [ApiController]
    [Route("BookingController")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IMediator mediator, ILogger<BookingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        ///     Creates a customer booking
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new CreateBookingCommand(request), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while updating booking {@request}", request);
                throw new Exception("Unexpected error while creating your reservation", ex);
            }
        }

        /// <summary>
        ///    Updates a customer booking
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                {
                    _logger.LogError("Invalid booking Id, cannot update booking {@request}", request);
                    return BadRequest(request);
                }
                var result = await _mediator.Send(new UpdateBookingCommand(request), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while updating booking {@request}", request);
                throw new Exception("Unexpected error while updating booking", ex);
            }

        }

        /// <summary>
        ///     Removes a customer booking
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BookingNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteBookingOrchestrator(bookingId), cancellationToken);
                return Ok();
            }
            catch (BookingNotFoundException ex)
            {
                _logger.LogError("Booking Id {bookingId} provided to delete booking was not found", bookingId);
                throw new BookingNotFoundException("Booking Id provided to delete booking was not found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while removing booking {bookingId}", bookingId);
                throw new Exception("Unexpected error while removing booking", ex);
            }

        }
    }
}
