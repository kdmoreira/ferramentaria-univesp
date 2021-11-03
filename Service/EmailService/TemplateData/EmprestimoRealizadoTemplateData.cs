using Newtonsoft.Json;

namespace Service.EmailService
{
    public class EmprestimoRealizadoTemplateData : BaseTemplateData
    {
        [JsonProperty("Ferramenta")]
        public string Ferramenta { get; set; }

        [JsonProperty("Quantidade")]
        public string Quantidade { get; set; }

        [JsonProperty("Devolucao")]
        public string Devolucao { get; set; }
    }
}
