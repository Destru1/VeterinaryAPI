using System.Text.Json;

namespace VeterinaryAPI.Common
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public dynamic Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
