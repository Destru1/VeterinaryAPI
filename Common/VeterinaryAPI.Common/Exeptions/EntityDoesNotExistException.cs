using System;

namespace VeterinaryAPI.Common.Exeptions
{
    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(string message)
            : base(message)
        {

        }
    }
}
