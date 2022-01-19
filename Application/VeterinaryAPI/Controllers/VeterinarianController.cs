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
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }
            return this.Ok();
        }

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
