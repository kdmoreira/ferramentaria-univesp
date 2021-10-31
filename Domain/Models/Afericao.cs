using System;

namespace Domain.Models
{
    public class Afericao : BaseModel
    {
        public Guid FerramentaID { get; set; }
        public Ferramenta Ferramenta { get; set; }
        public int IntervaloDias { get; set; }
        public DateTime? DataUltima { get; set; }
    }
}
