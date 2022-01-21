using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.User
{
    public class GetTokenDTO
    {
        public GetTokenDTO(string token)
        {
            this.Token = token;
        }

        public string Token { get; set; }
    }
}
