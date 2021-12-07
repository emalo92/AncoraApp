using System.Collections.Generic;

namespace ContabilitaWeb.Models.Paginate
{
    public class ResultPaginated<T>
    {
        public List<T> Result { get; set; }
        public Pagination Pagination { get; set; }
    }
}
