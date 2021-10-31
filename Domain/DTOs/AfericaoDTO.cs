using System;

namespace Domain.DTOs
{
    public class AfericaoDTO
    {
        public Guid ID { get; set; }
        public int IntervaloDias { get; set; }
        public DateTime? DataUltima { get; set; }
    }
}
