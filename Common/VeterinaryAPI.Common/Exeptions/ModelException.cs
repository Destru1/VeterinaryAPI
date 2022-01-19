using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Common.Exeptions
{
    public class ModelException : Exception
    {
        public ModelException(IEnumerable<ModelError> errorMessage)
            :base(null)
        {
            this.ErrorMessage = errorMessage;
        }

        public ModelException(IEnumerable<ModelError> errorMessage, Exception inner)
        {
            this.ErrorMessage = errorMessage;
        }

        public IEnumerable<ModelError> ErrorMessage { get; }
    }
}
