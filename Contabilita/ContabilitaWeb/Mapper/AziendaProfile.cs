using AutoMapper;
using ContabilitaWeb.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilitaWeb.Mapper
{
    public class AziendaProfile : Profile
    {
        public AziendaProfile()
        {
            CreateMap<Infrastructure.Models.Azienda, Azienda>();
            CreateMap<Azienda, Infrastructure.Models.Azienda>();
        }
    }
}
