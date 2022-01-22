using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class VeterinarianController : BaseAPIController
    {
        private readonly IVeterinarianService veterinarianService;
        private readonly IVeterinarianPositionMappingService veterinarianPositionMappingService;

        public VeterinarianController(IVeterinarianService veterinarianService, IVeterinarianPositionMappingService veterinarianPositionMappingService)
        {
            this.veterinarianService = veterinarianService;
            this.veterinarianPositionMappingService = veterinarianPositionMappingService;
        }

        /// <summary>
        ///  Get veterinarian by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the veterinarian entity by the given id</returns>
        /// <response code="200">Returns the veterinarian entity by the given id</response>
        /// <response code="404">If the veterinarian is null</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetVeterinarianDTO veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(id);

            if (veterinarian == null)
            {
                return this.NotFound();
            }

            return this.Ok(veterinarian);
        }

        /// <summary>
        /// Get all veterinarians
        /// </summary>
        /// <returns>Returns all veterinarians </returns>
        /// <response code="200">Returns all veterinarians </response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllVeterinariansDTO veterinarians = await this.veterinarianService.GetAllAsync<GetAllVeterinariansDTO>();

            return this.Ok(veterinarians);
        }

        /// <summary>
        /// Create veterinarian
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Veterinarian
        ///     {
        ///        "firstName": "VeterinarianFirstName",
        ///        "lastName": "VeterinarianLastName",
        ///        "phoneNumber": "VeterinarianPhoneNumber",
        ///        "email": "VeterinarianEmail",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The veterinarian that is created</returns>
        /// <response code="200">If the veterinarian is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        public async Task<IActionResult> Post(PostVeterinarianDTO model)
        {
            Veterinarian createdVeterinarian = await this.veterinarianService.AddAsync(model);

            return this.CreatedAtRoute(this.RouteData, createdVeterinarian);
        }

        /// <summary>
        /// Update veterinarian
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Veterinarian
        ///     {
        ///        "firstName": "VeterinarianFirstName",
        ///        "lastName": "VeterinarianLastName",
        ///        "phoneNumber": "VeterinarianPhoneNumber",
        ///        "email": "VeterinarianEmail",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The veterinarian Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The result form update action</returns>
        /// <response code="200">If the veterinarian is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, PutVeterinarianDTO model)
        {
            bool resultFromUpdate = await this.veterinarianService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok();
        }


        /// <summary>
        /// Partial update veterinarian
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Veterinarian
        ///     {
        ///        "firstName": "VeterinarianFirstName",
        ///        "lastName": "VeterinarianLastName",
        ///        "phoneNumber": "VeterinarianPhoneNumber",
        ///        "email": "VeterinarianEmail",
        ///        "PositionsId"[
        ///         "PositionId"
        ///           ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The veterinarian Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The result form update action</returns>
        /// <response code="200">If the veterinarian is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(Guid id, PatchVeterinarianDTO model)
        {
            bool resultFormPartialUpdate = await this.veterinarianService.PartialUpdateAsync(id, model);
            if (this.ModelState.IsValid == false)
            {
                IEnumerable<ModelError> errors = this.ModelState.Values.SelectMany(v => v.Errors);
                throw new ModelException(errors);
            }
            if (resultFormPartialUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }

        /// <summary>
		/// Delete veterinarian by Id
		/// </summary>
		/// <param name="id">The veterinarian id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the veterinarian is deleted successfully</response>
		/// <response code="400">If the veterinarian is null</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.veterinarianService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromDelete);
        }


        /// <summary>
        /// Delete position from veterinarian
        /// </summary>
        /// <param name="veterinarianId">The veterinarian id</param>
        /// <param name="positionId">The position id</param>
        /// <returns>The result from the delete action</returns>
        /// <response code="200">If the relation is deleted successfully</response>
        /// <response code="400">If there is no relation</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid veterinarianId, Guid positionId)
        {
            bool resultFromDelete = await this.veterinarianPositionMappingService.DeleteAsync(veterinarianId, positionId);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromDelete);
        }

    }
}
