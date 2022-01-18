using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.DTOs.Positions;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetPositionDTO position = await this.positionService.GetByIdAsync<GetPositionDTO>(id);

            if (position == null)
            {
                return this.NotFound();
            }

            return this.Ok(position);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllPositionsDTO positions = await this.positionService.GetAllAsync<GetAllPositionsDTO>();

            return this.Ok(positions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPositionDTO model)
        {
            GetPositionDTO createdPosition = await this.positionService.AddAsync<GetPositionDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdPosition);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, PutPositionDTO model)
        {
            bool resultFromUpdate = await this.positionService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.positionService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok();
        }
    }
}
