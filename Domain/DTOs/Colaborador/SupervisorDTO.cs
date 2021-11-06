using System;

namespace Domain.DTOs
{
    public class SupervisorDTO
    {
        public Guid ID { get; set; }
        public string Matricula { get; set; }
        public string NomeCompleto { get; set; }
        public string Empresa { get; set; }
    }
}
