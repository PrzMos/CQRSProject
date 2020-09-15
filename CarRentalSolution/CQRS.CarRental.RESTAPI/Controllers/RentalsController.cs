using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Models.Write;
using CQRS.CarRental.Core.Queries;
using CQRS.CarRental.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedKernel.Dispatchers;

namespace CQRS.CarRental.RESTAPI.Controllers
{
    /// <summary>
    /// Controller which contains actions on Rental model
    /// </summary>
    [Route("api/Rentals")]
    [Produces("application/json","application/xml")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, StatusCode = 415)]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMapper _mapper;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public RentalsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IMapper mapper)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Returns a list of rentals
        /// </summary>
        /// <returns>An ActionResult of List of RentalResult</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound ,StatusCode = 404)]
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
        /// Returns Rental with a specyfic Id
        /// </summary>
        /// <param name="rentalId">Id of Rent to get.</param>
        /// <returns>An ActionResult of RentalModelResult</returns>
        [HttpGet("{rentalId}", Name = "GetRental")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode =200)]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404)]
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
        [ProducesResponseType(StatusCodes.Status201Created, StatusCode = 201)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, StatusCode = 406)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult UpdateRental(Guid rentalId, StopRentingCarCommand stopRentingCarCommand)
        {
            if (rentalId == stopRentingCarCommand.RentId)
            {
                _commandDispatcher.Send(stopRentingCarCommand);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Delete a rental with a specified Id
        /// </summary>
        /// <param name="rentalId">Id of a specyfic Rental to delete</param>
        /// <returns>An IActionResult</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound,StatusCode =404)]
        [ProducesResponseType(StatusCodes.Status204NoContent,StatusCode =204)]
        public IActionResult DeleteRental(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
            {
                return NotFound();
            }

            var command = new DeleteRentCommand(rentalId);

            _commandDispatcher.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Shows allowed options to use in controller.
        /// </summary>
        /// <returns>An IActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK,StatusCode =200)]
        public IActionResult GetRentalOption()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}