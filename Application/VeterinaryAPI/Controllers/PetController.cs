using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Pet;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetPetDTO pet = await this.petService.GetByIdAsync<GetPetDTO>(id);

            if (pet == null)
            {
                return this.NotFound();
            }

            return this.Ok(pet);
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            GetAllPetsDTO pets = await this.petService.GetAllAsync<GetAllPetsDTO>();

            return this.Ok(pets);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPetDTO model)
        {
            GetPetDTO createdPet = await this.petService.AddAsync<GetPetDTO>(model);

            return this.CreatedAtRoute(this.RouteData, createdPet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, PutPetDTO model)
        {
            bool resultFromUpdate = await this.petService.UpdateAsync(id, model);

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
            bool resultFromDelete = await this.petService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok();
        }
    }
}
