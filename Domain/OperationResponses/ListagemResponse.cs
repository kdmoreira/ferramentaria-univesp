using Domain.DTOs;
using System.Collections.Generic;

namespace Domain.OperationResponses
{
    public class ListagemResponse<T> where T : ListagemDTO
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
