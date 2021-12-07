using System.Collections.Generic;

namespace Infrastructure.Models.Paginations
{
    public class ResultPaginated<T>
    {
        public List<T> Result { get; set; }
        public Pagination Pagination { get; set; }
    }
}
