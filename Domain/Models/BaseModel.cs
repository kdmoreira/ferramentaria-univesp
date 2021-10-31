using Domain.Audits;
using System;

namespace Domain.Models
{
    public abstract class BaseModel
    {
        public Guid ID { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }
        public Guid CriadoPor { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public Guid? AtualizadoPor { get; set; }

        public virtual void Ativar()
        {
            Ativo = true;
        }

        public virtual void Inativar()
        {
            Ativo = false;
        }
    }
}