using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Common.Constants
{
    public class ExeptionMessages
    {
        public const string SOMETHING_WENT_WRONG_MESSAGE = "Something went wrong!";
        public const string VETERINARIAN_DOES_NOT_EXIST_MESSAGE = "Veterinarian with this id dosen't exist ({0})";
        public const string PET_DOES_NOT_EXIST_MESSAGE = "Pet with this id dosen't exist ({0})";
        public const string OWNER_DOES_NOT_EXIST_MESSAGE = "Owner with this id dosen't exist ({0})";
    }
}
