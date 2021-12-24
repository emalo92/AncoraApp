using AutoMapper;
using ContabilitaWeb.Models;

namespace ContabilitaWeb.Mapper
{
    public class MovimentiProfile : Profile
    {
        public MovimentiProfile()
        {
            CreateMap<Infrastructure.Models.InputRicercaMovimenti, InputRicercaMovimenti>();
            CreateMap<InputRicercaMovimenti, Infrastructure.Models.InputRicercaMovimenti>();

            CreateMap<Infrastructure.Models.Movimento, Movimento>();
            CreateMap<Movimento, Infrastructure.Models.Movimento>();
        }
    }
}
