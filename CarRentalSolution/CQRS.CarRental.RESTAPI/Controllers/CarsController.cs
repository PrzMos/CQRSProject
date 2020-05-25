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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets list of all cars
        /// </summary>
        /// <returns>An ActionResult of List of CarResult</returns>
        [HttpGet]
        public ActionResult<List<CarResult>> Get()
        {
            var query = new GetAllCarsQuery();

            var result = _queryDispatcher.Send<GetAllCarsQuery, List<CarResult>>(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets car with specified Id
        /// </summary>
        /// <param name="id">The id of specified car</param>
        /// <returns>An ActionResult of CarResult</returns>
        [HttpGet("{id}", Name="GetCar")]
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
        /// 
        /// </summary>
        /// <param name="createCarCommand"></param>
        /// <returns></returns>
        [HttpPost]
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetCarsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,DELETE");
            return Ok();
        }
    }
}
