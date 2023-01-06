using System.Net;
using CavuTechTest.Mediation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CavuTechTest.API.Controllers
{
    [ApiController]
    [Route("AvailabilityController")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AvailabilityController> _logger;

        public AvailabilityController(IMediator mediator, ILogger<AvailabilityController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        ///     Get method for car parking space availability
        /// </summary>
        /// <param name="toDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCarSpaceAvailability(DateTime toDate, DateTime fromDate, CancellationToken cancellationToken)
        {
            try
            {
                var availability = await _mediator.Send(new AvailableSpacesQuery(toDate, fromDate), cancellationToken);
                if (availability.Any())
                {
                    return Ok(availability);
                }

                _logger.LogWarning("No content found for car parking space availability");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while getting car space availability");
                throw new Exception("Unexpected error while getting car space availability", ex);
            }

        }
    }
}