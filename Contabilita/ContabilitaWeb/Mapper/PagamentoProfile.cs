using AutoMapper;
using ContabilitaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilitaWeb.Mapper
{
    public class PagamentoProfile : Profile
    {
        public PagamentoProfile()
        {
            CreateMap<Infrastructure.Models.Pagamento, Pagamento>();
            CreateMap<Pagamento, Infrastructure.Models.Pagamento>();
        }
    }
}
