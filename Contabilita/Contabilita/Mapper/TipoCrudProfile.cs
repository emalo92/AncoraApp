using AutoMapper;
using Contabilita.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contabilita.Mapper
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
