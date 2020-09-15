using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Queries;
using CQRS.CarRental.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Dispatchers;

namespace CQRS.CarRental.RESTAPI.Controllers
{
    [Route("api/Drivers")]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DriversController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMapper _mapper;

        public DriversController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IMapper mapper)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Get a list of drivers
        /// </summary>
        /// <returns>An ActionResult of type List of DriverResult</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound,StatusCode = 404)]
        public ActionResult<List<DriverResult>> GetDrivers()
        {
            var query = new GetAllDriversQuery();

            var result = _queryDispatcher.Send<GetAllDriversQuery, List<DriverResult>>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        /// <summary>
        /// Returns a driver by specyfic id
        /// </summary>
        /// <param name="driverId">The id of the driver</param>
        /// <returns>An ActionResult of DriverResult</returns>
        /// <response code="200">Returns the requested driver</response>
        [HttpGet("{driverId}", Name = "GetDriver")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<DriverResult> GetDriver(Guid driverId)
        {
            var query = new GetDriverByIdQuery(driverId);

            var result = _queryDispatcher.Send<GetDriverByIdQuery, DriverResult>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create a driver
        /// </summary>
        /// <param name="createDriverCommand">The driver to create</param>
        /// <returns>An IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, StatusCode = 406)]
        public IActionResult PostDriver([FromBody] CreateDriverCommand createDriverCommand)
        {
            if (createDriverCommand == null)
            {
                return BadRequest();
            }
            Guid driverId = Guid.Empty;

            _commandDispatcher.Send(createDriverCommand, out driverId);

            var driverToReturn = _mapper.Map<DriverResult>(createDriverCommand);
            driverToReturn.DriverId = driverId;

            return CreatedAtRoute("GetDriver", new { driverId = driverId }, driverToReturn);
        }

        /// <summary>
        /// Delete driver within specified Id
        /// </summary>
        /// <param name="driverId">Id of a driver to delete</param>
        /// <returns>An IActionResult</returns>
        [HttpDelete("{driverId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteDriver(Guid driverId)
        {
            if (driverId == Guid.Empty)
            {
                return BadRequest();
            }

            var command = new DeleteDriverCommand(driverId);

            _commandDispatcher.Send(command);

            return NoContent();
        }

    }
}