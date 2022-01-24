using AutoMapper;
using CashRegister.Client.Models.DTO;
using CashRegister.Client.Models.Request;
using CashRegister.Client.Models.Response;
using CashRegister.Services.Abstract;
using CashRegister.Web.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Client.Controllers
{
    [Route("[controller]")]
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

        [HttpGet("amounts")]
        public ActionResult GetAmounts()
        {
            try
            {
                var currentAmounts = _manager.GetAmountInCashRegister();
                var response = new GetAmountsResponse()
                {
                    Amounts = _mapper.Map<List<DenominationAmount>>(currentAmounts.Amounts),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("denominations")]
        public ActionResult GetDenominations()
        {
            try {
                var denominations = _manager.GetDenominations();
                var response = new GetDenominationsResponse()
                {
                    Denominations = _mapper.Map<List<DenominationValue>>(denominations)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cash")]
        public ActionResult AddCash(AddCashRequest request)
        {
            try
            {
                var amounts =
                    _manager.AddCashToCashRegister(_mapper.Map<Domain.Model.CurrencyAmounts>(request.CurrencyAmounts));
                var response = new AddCashResponse()
                {
                    CurrencyAmounts = _mapper.Map<CurrencyAmounts>(amounts)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("payment")]
        public ActionResult MakePayment(MakePaymentRequest request)
        {
            try {
                var amounts = _manager.HandlePayment(_mapper.Map<Transaction, Domain.Model.Transaction>(request.Transaction));
                var response =  new MakePaymentResponse()
                {
                    CurrencyAmounts = _mapper.Map<CurrencyAmounts>(amounts)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
