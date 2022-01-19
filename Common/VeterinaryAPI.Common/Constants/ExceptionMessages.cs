using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Common.Constants
{
    public static class ExceptionMessages
    {
        public const string SOMETHING_WENT_WRONG_MESSAGE = "Something went wrong!";
        public const string VETERINARIAN_DOES_NOT_EXIST_MESSAGE = "Veterinarian with this id dosen't exist.";
        public const string PET_DOES_NOT_EXIST_MESSAGE = "Pet with this id dosen't exist.";
        public const string OWNER_DOES_NOT_EXIST_MESSAGE = "Owner with this id dosen't exist.";
        public const string POSITION_DOES_NOT_EXIST_MESSAGE = "Position with this id dosen't exist.";
        public const string PET_ALREADY_ADDED_MESSAGE = "Pet is already added to this owner. ({0})";
        public const string POSITION_ALREADY_ADDED_MESSAGE = "Position is already added to this veterinarian. ({0})";
        public const string VETERINARIAN_PET_MAPPING_DOES_NOT_EXIST_MESSAGE = "There is no relation between this veterinarian and pet";
        public const string OWNER_PET_MAPPING_DOES_NOT_EXIST_MESSAGE = "There is no relation between this owner and pet";
        public const string VETERINARIAN_POSITION_MAPPING_DOES_NOT_EXIST_MESSAGE = "There is no relation between this veterinarian and position";
    }
}
