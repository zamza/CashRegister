using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CashRegister.Domain;
using CashRegister.Domain.Abstract;
using CashRegister.Domain.Models;
using CashRegister.Web.Models.Request;
using CashRegister.Web.Models.Response;

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

        [HttpGet("AmountsPaid")]
        public ActionResult GetAmounts()
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var amounts = cashRegister.GetAmounts();
            var response = new GetAmountsResponse();
            response.Amounts = amounts;

            return Ok(response);
        }

        [HttpPost("cash")]
        public ActionResult AddCash(AddCashRequest request)
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var amounts = cashRegister.AddCash(request.Amounts);
            var response = new AddCashResponse();
            response.Amounts = amounts;

            return Ok(response);
        }

        [HttpPost("payment")]
        public ActionResult MakePayment(MakePaymentRequest request)
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var transaction = new Transaction(request.AmountsPaid, request.Cost);
            var amounts = cashRegister.TakePayment(transaction);
            var response = new MakePaymentResponse();
            response.Amounts = amounts;

            return Ok(response);
        }
    }
}
