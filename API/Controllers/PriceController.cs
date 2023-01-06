using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CavuTechTest.Mediation.Queries;

namespace CavuTechTest.API.Controllers
{
    [ApiController]
    [Route("PriceController")]
    public class PriceController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PriceController> _logger;

        public PriceController(IMediator mediator, ILogger<PriceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        ///     Get method for summer pricing per day
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("SummerPrice")]
        public async Task<IActionResult> GetSummerPricePerDay(CancellationToken cancellationToken)
        {
            try
            {
                var price = await _mediator.Send(new SummerPriceQuery(), cancellationToken);
                return Ok(price.ToString("0.00"));

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while getting summer price");
                throw new Exception("Unexpected error while getting summer price", ex);
            }

        }

        /// <summary>
        ///     Get method for winter pricing per day
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("WinterPrice")]
        public async Task<IActionResult> GetWinterPricePerDay(CancellationToken cancellationToken)
        {
            try
            {
                var price = await _mediator.Send(new WinterPriceQuery(), cancellationToken);
                return Ok(price.ToString("0.00"));

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected error while getting summer price");
                throw new Exception("Unexpected error while getting summer price", ex);
            }

        }
    }
}