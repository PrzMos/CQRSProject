using System;
using System.Collections.Generic;
using System.Composition;
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
    [Route("api/Cars")]
    [ApiController]
    [Produces("application/json","application/xml")]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CarsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMapper _mapper;

        public CarsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IMapper mapper)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _mapper = mapper;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Gets list of all cars
        /// </summary>
        /// <returns>An ActionResult of List of CarResult</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404)]
        public ActionResult<List<CarResult>> GetCars()
        {
            var query = new GetAllCarsQuery();

            var result = _queryDispatcher.Send<GetAllCarsQuery, List<CarResult>>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets car with specified Id
        /// </summary>
        /// <param name="id">The id of specified car</param>
        /// <returns>An ActionResult of CarResult</returns>
        [HttpGet("{id}", Name="GetCar")]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404)]
        public ActionResult<CarResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var query = new GetCarDetailsQuery(id);

            var result =  _queryDispatcher.Send<GetCarDetailsQuery, CarResult>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create new car
        /// </summary>
        /// <param name="createCarCommand">Car to create</param>
        /// <returns>An IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,StatusCode =201)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, StatusCode = 406)]
        public IActionResult Post([FromBody] CreateCarCommand createCarCommand)
        {
            if (createCarCommand == null)
            {
                return BadRequest();
            }

            Guid carId = Guid.Empty;

            _commandDispatcher.Send(createCarCommand, out carId);

            var carToReturn =  _mapper.Map<CarResult>(createCarCommand);
            carToReturn.CarId = carId;

            return CreatedAtRoute("GetCar",new { carId }, carToReturn);
        }

        /// <summary>
        /// Delete car with specyfic Id
        /// </summary>
        /// <param name="id">Id of a car to delete</param>
        /// <returns>An IActionResult</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, StatusCode = 204)]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404)]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var command = new DeleteCarCommand(id);

            _commandDispatcher.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Returns allowed options to use in controller.
        /// </summary>
        /// <returns>An IActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCarsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,DELETE");
            return Ok();
        }
    }
}
