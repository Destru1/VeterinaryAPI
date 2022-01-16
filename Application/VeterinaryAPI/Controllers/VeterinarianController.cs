using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class VeterinarianController : BaseAPIController
    {
        private readonly IVeterinarianService veterinarianService;

        public VeterinarianController(IVeterinarianService veterinarianService)
        {
            this.veterinarianService = veterinarianService;
        }

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllVeterinariansDTO veterinarians = await this.veterinarianService.GetAllAsync<GetAllVeterinariansDTO>();

            return this.Ok(veterinarians);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostVeterinarianDTO model)
        {
            Veterinarian createdVeterinarian = await this.veterinarianService.AddAsync(model);

            return this.CreatedAtRoute(this.RouteData, createdVeterinarian);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, PutVeterinarianDTO model)
        {
            bool resultFromUpdate = await this.veterinarianService.UpdateAsync(id, model);

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
            bool resultFromDelete = await this.veterinarianService.DeleteAsync(id);

            if (resultFromDelete == false)
            {
                return this.BadRequest(ExeptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(resultFromDelete);
        }

    }
}
