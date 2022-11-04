using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BillingAPI.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// BaseController - context accessor
        /// </summary>
        /// <param name="contextAccessor"></param>
        protected BaseController(IHttpContextAccessor contextAccessor) =>
            _contextAccessor = contextAccessor;

        private IMediator _mediator;

        /// <summary>
        /// Getting mediator instance from contextAccessor
        /// </summary>
        protected IMediator Mediator =>
            _mediator ?? (_mediator = _contextAccessor.HttpContext.RequestServices.GetRequiredService<IMediator>());

        protected IActionResult HandleResponse<T>(T content, bool withContent = false)
        {
            if (withContent)
                return Ok(content);

            var type = Request.Method;

            var methodType = type.ToEnum<MethodType>();

            return methodType switch
            {
                MethodType.GET => Ok(content),
                _ => NoContent(),
            };
        }

        private enum MethodType
        {
            GET, POST, PUT, DELETE
        }
    }
}
