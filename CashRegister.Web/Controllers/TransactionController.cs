using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CashRegister.Domain.Abstract;

namespace CashRegister.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceProvider _container;

        public TransactionController(IServiceProvider provider)
        {
            _container = provider;
        }

            [HttpGet("amounts")]
        public ActionResult GetAmounts()
        {
            var register = _container.GetService<ICashRegister>();
            return Ok();
        }
    }
}
