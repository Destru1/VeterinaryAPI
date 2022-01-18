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

        [HttpGet]
        public async Task<IActionResult> Get(Guid veterinarianId, Guid petId)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(veterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(petId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.GetModelByVeterinarianIdAndPetIdAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id);

            return this.Ok(veterinarianPetMapping);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostVeterinarianPetDTO model)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(model.VeterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(model.PetId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.CreateRelationAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id, model.AppointmentDate);

            return this.Ok(veterinarianPetMapping);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(PatchVeterinarianPetDTO model)
        {
            var veterinarian = await this.veterinarianService.GetByIdAsync<GetVeterinarianDTO>(model.VeterinarianId);
            var pet = await this.petService.GetByIdAsync<GetPetDTO>(model.PetId);
            var veterinarianPetMapping = await this.veterinarianPetMappingService.UpdateApointmentDateAsync<GetVeterinarianPetDTO>(veterinarian.Id, pet.Id, model.AppointmentDate);

            return this.Ok(veterinarianPetMapping);
        }

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
