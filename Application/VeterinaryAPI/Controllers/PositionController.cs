using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.DTOs.Positions;
using VeterinaryAPI.Infastructure.Filters;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class PositionController : BaseAPIController
    {
        private readonly IPositionService positionService;
        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        /// <summary>
        ///  Get position by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the position entity by the given id</returns>
        /// <response code="200">Returns the position entity by the given id</response>
        /// <response code="404">If the position is null</response>
        [HttpGet]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get(Guid id)
        {
            GetPositionDTO position = await this.positionService.GetByIdAsync<GetPositionDTO>(id);

            if (position == null)
            {
                return this.NotFound();
            }

            return this.Ok(position);
        }


        /// <summary>
        /// Get all positions
        /// </summary>
        /// <returns>Returns all positions </returns>
        /// <response code="200">Returns all owners </response>
        [HttpGet]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get()
        {
            GetAllPositionsDTO positions = await this.positionService.GetAllAsync<GetAllPositionsDTO>();

            return this.Ok(positions);
        }

        /// <summary>
        /// Create position
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Position
        ///     {
        ///        "name": "PositionName"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The position that is created</returns>
        /// <response code="200">If the position is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Post(PostPositionDTO model)
        {
            GetPositionDTO createdPosition = await this.positionService.AddAsync<GetPositionDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdPosition);
        }

        /// <summary>
        /// Update position
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Position
        ///     {
        ///        "name": "PositionName"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The position Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The result form update action</returns>
        /// <response code="200">If the position is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Put(Guid id, PutPositionDTO model)
        {
            bool resultFromUpdate = await this.positionService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }

        /// <summary>
        /// Delete position by Id
        /// </summary>
        /// <param name="id">The position id</param>
        /// <returns>The result from the delete action</returns>
        /// <response code="200">If the position is deleted successfully</response>
        /// <response code="400">If the position is null</response>
        [HttpDelete]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.positionService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok();
        }
    }
}
