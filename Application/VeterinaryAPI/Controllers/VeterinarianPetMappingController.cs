using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.Pet;
using VeterinaryAPI.DTOs.Veterinarian;
using VeterinaryAPI.DTOs.VeterinarianPetMapping;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/book-appointment")]
    public class VeterinarianPetMappingController : BaseAPIController
    {
        private readonly IVeterinarianService veterinarianService;
        private readonly IPetService petService;
        private readonly IVeterinarianPetMappingService veterinarianPetMappingService;

        public VeterinarianPetMappingController(IVeterinarianService veterinarianService, 
            IPetService petService, 
            IVeterinarianPetMappingService veterinarianPetMappingService)
        {
            this.veterinarianService = veterinarianService;
            this.petService = petService;
            this.veterinarianPetMappingService = veterinarianPetMappingService;
        }
        /// <summary>
        ///  Get appointment by veterinarian id and pet id
        /// </summary>
        /// <param name="veterinarianId"></param>
        /// <param name="petId"></param>
        /// <returns></returns>
        /// <returns>Returns the appointment entity by the given id's</returns>
        /// <response code="200">Returns the appointment entity by the given id's</response>
        /// <response code="404">If the appointment is null</response>
        [HttpGet]
        public async Task<IActionResult> Get(Guid veterinarianId, Guid petId)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(veterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(petId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.GetModelByVeterinarianIdAndPetIdAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id);

            return this.Ok(veterinarianPetMapping);
        }
        /// <summary>
        /// Create appointment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/book-apointment
        ///     {
        ///        "veterinarianId": "VeterinarianId",
        ///        "petId": "PetId",
        ///        "appointmentDate": "Date"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The appointment that is created</returns>
        /// <response code="200">If the appointment is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        public async Task<IActionResult> Post(PostVeterinarianPetDTO model)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(model.VeterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(model.PetId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.CreateRelationAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id, model.AppointmentDate);

            return this.Ok(veterinarianPetMapping);
        }


        /// <summary>
        /// Update appointment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/book-apointment
        ///     {
        ///        "veterinarianId": "VeterinarianId",
        ///        "petId": "PetId",
        ///        "appointmentDate": "Date"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The appointment that is updated</returns>
        /// <response code="200">If the appointment is updated successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPatch]
        public async Task<IActionResult> Patch(PatchVeterinarianPetDTO model)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(model.VeterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(model.PetId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.UpdateApointmentDateAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id, model.AppointmentDate);

            return this.Ok(veterinarianPetMapping);
        }

        /// <summary>
		/// Delete appointment by veterinarian id and pet id
		/// </summary>
		/// <param name="veterinarianId">The veterinarian id</param>
		/// <param name="petId">The pet id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the appointment is deleted successfully</response>
		/// <response code="400">If the appointment is null</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid veterinarianId, Guid petId)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(veterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(petId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.DeleteRalationAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id);

            return this.Ok(veterinarianPetMapping);
        }
    }
}
