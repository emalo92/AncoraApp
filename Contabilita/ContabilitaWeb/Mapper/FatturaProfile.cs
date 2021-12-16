using AutoMapper;
using ContabilitaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilitaWeb.Mapper
{
    public class FatturaProfile : Profile
    {
        public FatturaProfile()
        {
            CreateMap<Infrastructure.Models.Fattura, Fattura>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo != null ? src.Tipo.Trim() : null))
                ;
            CreateMap<Fattura, Infrastructure.Models.Fattura>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo != null ? src.Tipo.Trim() : null))
                ;
        }
    }
}
