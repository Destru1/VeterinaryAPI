using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.DTOs.Pet;
using VeterinaryAPI.Infastructure.Filters;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class PetController : BaseAPIController
    {
        private readonly IPetService petService;

        public PetController(IPetService petService)
        {
            this.petService = petService;
        }

        /// <summary>
        ///  Get pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the pet entity by the given id</returns>
        /// <response code="200">Returns the pet entity by the given id</response>
        /// <response code="404">If the pet is null</response>
        [HttpGet]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get(Guid id)
        {
            GetPetDTO pet = await this.petService.GetByIdAsync<GetPetDTO>(id);

            if (pet == null)
            {
                return this.NotFound();
            }

            return this.Ok(pet);
        }


        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns>Returns all pets </returns>
        /// <response code="200">Returns all pets </response>
        [HttpGet]
        [JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Get()
        {
            GetAllPetsDTO pets = await this.petService.GetAllAsync<GetAllPetsDTO>();

            return this.Ok(pets);
        }



        /// <summary>
        /// Create pet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Pet
        ///     {
        ///        "name": "PetName",
        ///        "type": "PetType",
        ///        "breed": "PetBreed"
        ///        "age": "PetAge"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The pet that is created</returns>
        /// <response code="200">If the pet is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Post(PostPetDTO model)
        {
            GetPetDTO createdPet = await this.petService.AddAsync<GetPetDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdPet);
        }

        /// <summary>
        /// Update pet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Pet
        ///     {
        ///        "name": "PetName",
        ///        "type": "PetType",
        ///        "breed": "PetBreed"
        ///        "age": "PetAge"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The pet Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The pet that is created</returns>
        /// <response code="200">If the pet is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Put(Guid id, PutPetDTO model)
        {
            bool resultFromUpdate = await this.petService.UpdateAsync(id, model);

            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok();
        }


        /// <summary>
        /// Partial update pet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Pet
        ///     {
        ///        "name": "PetName",
        ///        "type": "PetType",
        ///        "breed": "PetBreed"
        ///        "age": "PetAge"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The owner Id</param>
        /// <param name="model">Body model with data</param>
        /// <returns>The pet that is created</returns>
        /// <response code="200">If the pet is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Patch(Guid id, PatchPetDTO model)
        {
            bool resultFromUpdate = await this.petService.PartialUpdateAsync(id, model);
            if (resultFromUpdate == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok(resultFromUpdate);
        }


        /// <summary>
        /// Delete pet by Id
        /// </summary>
        /// <param name="id">The pet id</param>
        /// <returns>The result from the delete action</returns>
        /// <response code="200">If the pet is deleted successfully</response>
        /// <response code="400">If the pet is null</response>
        [HttpDelete]
        [Route("{id}")]
        [JwtAuthorize(Roles = new[] { GlobalConstants.ADMIN_ROLE_NAME })]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool resultFromDelete = await this.petService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }
    }
}
