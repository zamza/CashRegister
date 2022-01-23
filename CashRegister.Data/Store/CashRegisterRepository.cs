using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Data.Model;

namespace CashRegister.Data.Store
{
    public class CashRegisterRepository
    {
        private readonly IMapper _mapper;

        public CashRegisterRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
