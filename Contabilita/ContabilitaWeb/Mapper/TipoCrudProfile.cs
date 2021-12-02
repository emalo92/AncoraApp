using AutoMapper;
using ContabilitaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilitaWeb.Mapper
{
    public class TipoCrudProfile : Profile
    {
        public TipoCrudProfile()
        {
            CreateMap<Infrastructure.Models.TipoCrud, TipoCrud>();
            CreateMap<TipoCrud, Infrastructure.Models.TipoCrud>();
        }
    }
}
