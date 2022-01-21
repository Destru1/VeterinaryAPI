using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Common
{
    public class ApplicationSettings
    {
        public string DbConnectionString { get; set; }

        public string JwtApiSecret { get; set; }
    }
}
