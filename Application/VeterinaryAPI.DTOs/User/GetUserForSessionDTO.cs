using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.Roles;

namespace VeterinaryAPI.DTOs.User
{
    public class GetUserForSessionDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IEnumerable<GetRolesForSessionDTO> Roles { get; set; }

    }
}
