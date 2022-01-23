using Microsoft.AspNetCore.Mvc;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class BaseAPIController : ControllerBase
    {

    }
}
