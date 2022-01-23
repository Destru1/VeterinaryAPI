using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Owner;
using VeterinaryAPI.Infastructure.Filters;
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
        /// <summary>
        ///  Get owner by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the owner entity by the given id</returns>
        /// <response code="200">Returns the owner entity by the given id</response>
        /// <response code="404">If the owner is null</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get(Guid id)
        {
            GetOwnerDTO owner = await this.ownerService.GetByIdAsync<GetOwnerDTO>(id);

            if (owner == null)
            {
                return this.NotFound();
            }

            return this.Ok(owner);
        }
        /// <summary>
        /// Get all owners
        /// </summary>
        /// <returns>Returns all owners </returns>
        /// <response code="200">Returns all owners </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get()
        {
            GetAllOwnersDTO owners = await this.ownerService.GetAllAsync<GetAllOwnersDTO>();

            return this.Ok(owners);
        }


        /// <summary>
        /// Create owner
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Owner
        ///     {
        ///        "firstName": "OwnerFirstName",
        ///        "lastName": "OwnerLastName",
        ///        "phoneNumber": "OwnerPhoneNumber"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The owner that is created</returns>
        /// <response code="200">If the owner is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Post(PostOwnerDTO model)
        {
            GetOwnerDTO createdOwner = await this.ownerService.AddAsync<GetOwnerDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdOwner);
        }

        /// <summary>
        /// Update owner
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Owner
        ///     {
        ///        "firstName": "OwnerFirstName",
        ///        "lastName": "OwnerLastName",
        ///        "phoneNumber": "OwnerPhoneNumber"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The owner Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The result form update action</returns>
        /// <response code="200">If the owner is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Put(Guid id, PutOwnerDTO model)
        {
            bool resultFromUpdate = await this.ownerService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromUpdate);
        }

        /// <summary>
        /// Partial update owner
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Owner
        ///     {
        ///        "firstName": "OwnerFirstName",
        ///        "lastName": "OwnerLastName",
        ///        "phoneNumber": "OwnerPhoneNumber",
        ///        "petsId"[
        ///         "PetId"
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The owner Id</param>
        /// <param name="model">Body model with data to partial</param>
        /// <returns>The result form update action</returns>
        /// <response code="200">If the owner is updated successfully</response>
        /// <response code="400">If the body is not correct</response>

        [HttpPatch]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Patch(Guid id, PatchOwnerDTO model)
        {
            bool resultFormUpdate = await this.ownerService.PartialUpdateAsync(id, model);

            if (this.ModelState.IsValid == false)
            {
                IEnumerable<ModelError> errors = this.ModelState.Values.SelectMany(v => v.Errors);
                throw new ModelException(errors);
            }

            if (resultFormUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }

        /// <summary>
		/// Delete owner by Id
		/// </summary>
		/// <param name="id">The owner id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the owner is deleted successfully</response>
		/// <response code="400">If the owner is null</response>
        [HttpDelete]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.ownerService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromDelete);
        }

        /// <summary>
		/// Delete pet from owner
		/// </summary>
		/// <param name="ownerId">The owner id</param>
		/// <param name="petId">The pet id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the relation is deleted successfully</response>
		/// <response code="400">If there is no relation</response>
        [HttpDelete]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Delete(Guid ownerId, Guid petId)
        {
            bool resultFormDelete = await this.ownerPetMappingService.DeleteAsync(ownerId, petId);

            if (resultFormDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFormDelete);
        }
    }
}
