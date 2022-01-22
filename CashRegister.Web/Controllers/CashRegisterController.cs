using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using CashRegister.Domain;
using CashRegister.Domain.Abstract;
using CashRegister.Web.Models.DTO;
using CashRegister.Web.Models.Request;
using CashRegister.Web.Models.Response;

namespace CashRegister.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashRegisterController : ControllerBase
    {
        private readonly IServiceProvider _container;
        private readonly IMapper _mapper;

        public CashRegisterController(IServiceProvider provider, IMapper mapper)
        {
            _container = provider;
            _mapper = mapper;
        }

        [HttpGet("amounts")]
        public ActionResult GetAmounts()
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var amounts = cashRegister.GetAmounts();
            var responseAmounts = _mapper.Map<Dictionary<Currencies, int>>(amounts);
            var response = new GetAmountsResponse()
            {
                Amounts = responseAmounts
            };

            return Ok(response);
        }

        [HttpPost("cash")]
        public ActionResult AddCash(AddCashRequest request)
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var amounts = cashRegister.AddCash(_mapper.Map<Dictionary<Domain.Models.Currencies, int>>(request.Amounts));
            var response = new AddCashResponse()
            {
                Amounts = _mapper.Map<Dictionary<Currencies, int>>(amounts)
            };

            return Ok(response);
        }

        [HttpPost("payment")]
        public ActionResult MakePayment(MakePaymentRequest request)
        {
            var cashRegister = _container.GetService<ICashRegister>();
            var transaction = _mapper.Map<Transaction, Domain.Models.Transaction>(request.Transaction);
            var amounts = cashRegister.TakePayment(transaction);
            var response = new MakePaymentResponse()
            {
                Amounts = _mapper.Map<Dictionary<Currencies, int>>(amounts)
            };

            return Ok(response);
        }
    }
}
