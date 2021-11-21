using AutoMapper;
using Contabilita.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contabilita.Mapper
{
    public class FatturaProfile : Profile
    {
        public FatturaProfile()
        {
            CreateMap<Infrastructure.Models.Fattura, Fattura>();
            CreateMap<Fattura, Infrastructure.Models.Fattura>();
        }
    }
}
