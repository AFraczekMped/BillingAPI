using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using BillingAPI.BillingService.Requests.OrdersActions;

namespace BillingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public partial class OrderController : BaseController
    {

        public OrderController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor) { }


        /// <summary>
        /// Processing order
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Order processed successfuly - return receipt in PDF(base64)</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ProcessOrder(ProcessOrderCommand command)
        {
            try
            {
                return HandleResponse(await Mediator.Send(command), true);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

