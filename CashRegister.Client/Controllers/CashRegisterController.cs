using AutoMapper;
using CashRegister.Services.Abstract;
using CashRegister.Web.Models.DTO;
using CashRegister.Web.Models.Request;
using CashRegister.Web.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashRegisterController : ControllerBase
    {
        private readonly IServiceProvider _container;
        private readonly IMapper _mapper;
        private readonly ICashRegisterManager _manager;

        public CashRegisterController(IServiceProvider provider, IMapper mapper)
        {
            _container = provider;
            _mapper = mapper;
            _manager = _container.GetService<ICashRegisterManager>();
        }

        [HttpGet]
        public ActionResult GetAmounts()
        {
            var amounts = _manager.GetAmountInCashRegister();
            var responseAmounts = _mapper.Map<CurrencyAmounts>(amounts);
            var response = new GetAmountsResponse()
            {
                CurrencyAmounts = responseAmounts
            };

            return Ok(response);
        }

        [HttpPost("cash")]
        public ActionResult AddCash(AddCashRequest request)
        {
            var amounts = _manager.AddCashToCashRegister(_mapper.Map<Domain.Model.CurrencyAmounts>(request.CurrencyAmounts));
            var response = new AddCashResponse()
            {
                CurrencyAmounts = _mapper.Map<CurrencyAmounts>(amounts)
            };

            return Ok(response);
        }

        [HttpPost("payment")]
        public ActionResult MakePayment(MakePaymentRequest request)
        {
            var amounts = _manager.HandlePayment(_mapper.Map<Transaction, Domain.Model.Transaction>(request.Transaction));
            var response =  new MakePaymentResponse()
            {
                CurrencyAmounts = _mapper.Map<CurrencyAmounts>(amounts)
            };

            return Ok(response);
        }
    }
}
