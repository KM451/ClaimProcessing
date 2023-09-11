using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
