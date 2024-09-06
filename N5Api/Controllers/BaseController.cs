using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Application.Models;

namespace N5Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IMediator Mediator => _mediator;

        protected IActionResult Result<T>(Response<T> response)
        {
            if (!response.IsValid)
            {
                return Error(response);
            }

            return Success(response);
        }

        protected IActionResult Success<T>(Response<T> response)
        {
            return new JsonResult(response.Content)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        protected IActionResult Error<T>(Response<T> response)
        {
            return new JsonResult(response.Notifications)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}