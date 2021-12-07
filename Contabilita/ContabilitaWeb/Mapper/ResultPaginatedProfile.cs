using AutoMapper;
using ContabilitaWeb.Models.Paginate;

namespace ContabilitaWeb.Mapper
{
    public class ResultPaginatedProfile : Profile
    {
        public ResultPaginatedProfile()
        {
            CreateMap(typeof(Infrastructure.Models.Paginations.ResultPaginated<>), typeof(ResultPaginated<>));
            CreateMap(typeof(ResultPaginated<>), typeof(Infrastructure.Models.Paginations.ResultPaginated<>));
        }
    }
}
