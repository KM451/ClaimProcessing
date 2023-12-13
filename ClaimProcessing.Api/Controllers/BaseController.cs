using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
 
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private IIntamiClient _intami;
        protected IIntamiClient Intami => _intami ??= HttpContext.RequestServices.GetService<IIntamiClient>();
    }
}
