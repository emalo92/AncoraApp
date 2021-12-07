using AutoMapper;
using ContabilitaWeb.Models.Paginate;
using Route = ContabilitaWeb.Models.Paginate.Route;

namespace ContabilitaWeb.Mapper
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<Pagination, Infrastructure.Models.Paginations.Pagination>();
            CreateMap<Infrastructure.Models.Paginations.Pagination, Pagination>();

            CreateMap<Infrastructure.Models.Paginations.Route, Route>();
            CreateMap<Route, Infrastructure.Models.Paginations.Route>();
        }
    }
}
