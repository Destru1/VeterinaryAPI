using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Owner;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class OwnerController : BaseAPIController
    {
        private readonly IOwnerService ownerService;
        private readonly IOwnerPetMappingService ownerPetMappingService;


        public OwnerController(IOwnerService ownerService, IOwnerPetMappingService ownerPetMappingService)
        {
            this.ownerService = ownerService;
            this.ownerPetMappingService = ownerPetMappingService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetOwnerDTO owner = await this.ownerService.GetByIdAsync<GetOwnerDTO>(id);

            if (owner == null)
            {
                return this.NotFound();
            }

            return this.Ok(owner);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllOwnersDTO owners = await this.ownerService.GetAllAsync<GetAllOwnersDTO>();

            return this.Ok(owners);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostOwnerDTO model)
        {
            GetOwnerDTO createdOwner = await this.ownerService.AddAsync<GetOwnerDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdOwner);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, PutOwnerDTO model)
        {
            bool resultFromUpdate = await this.ownerService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromUpdate);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(Guid id, PatchOwnerDTO model)
        {
            bool resultFormUpdate = await this.ownerService.PartialUpdateAsync(id, model);

            if (this.ModelState.IsValid == false)
            {
                //TODO Model error
            }

            if (resultFormUpdate == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.ownerService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromDelete);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid ownerId, Guid petId)
        {
            bool resultFormDelete = await this.ownerPetMappingService.DeleteAsync(ownerId, petId);

            if (resultFormDelete == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFormDelete);
        }
    }
}
