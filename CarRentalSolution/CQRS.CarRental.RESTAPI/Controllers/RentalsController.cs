using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Queries;
using CQRS.CarRental.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedKernel.Dispatchers;

namespace CQRS.CarRental.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMapper _mapper;

        public RentalsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IMapper mapper)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<RentalResult>> GetRentals()
        {
            var query = new GetAllRentalsQuery();

            var result = _queryDispatcher.Send<GetAllRentalsQuery, List<RentalResult>>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        [HttpGet("{rentalId}", Name = "GetRental")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RentalModelResult> GetRental(Guid rentalId)
        {
            var query = new GetRentalByIdQuery(rentalId);

            var result = _queryDispatcher.Send<GetRentalByIdQuery, RentalModelResult>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create rental of a specyfic Car for a specyfic Driver.
        /// </summary>
        /// <param name="createRentCommand">The rent to create.</param>
        /// <returns>An ActionResult of type RentalModelResult.</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RentalModelResult> PostRental([FromBody] CreateRentCommand createRentCommand)
        {
            Guid rentId = Guid.Empty;

            _commandDispatcher.Send(createRentCommand, out rentId);

            var modelToReturn = _mapper.Map<RentalModelResult>(createRentCommand);
            modelToReturn.RentalId = rentId;

            return CreatedAtRoute("GetRent", new { rentId = rentId }, modelToReturn);
        }

        /// <summary>
        /// Stop renting car
        /// </summary>
        /// <param name="rentalId">The id of rental to update.</param>
        /// <param name="stopRentingCarCommand">The object with updated values.</param>
        /// <returns>An IActionResult</returns>
        /// <response code="422">Valdation error</response>
        [HttpPut("{rentalId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult UpdateRental(Guid rentalId, StopRentingCarCommand stopRentingCarCommand)
        {
            _commandDispatcher.Send(stopRentingCarCommand);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteRental(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
            {
                return NotFound();
            }

            var command = new DeleteRentCommand(rentalId);

            return NoContent();
        }

        /// <summary>
        /// Shows allowed options to use in controller.
        /// </summary>
        /// <returns>An IActionResult</returns>
        [HttpOptions]
        public IActionResult GetRentalOption()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}