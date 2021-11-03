using Newtonsoft.Json;

namespace Service.EmailService
{
    public class EmprestimoPorColaboradorTemplateData : BaseTemplateData
    {
        [JsonProperty("Colaborador")]
        public string Colaborador { get; set; }

        [JsonProperty("Ferramenta")]
        public string Ferramenta { get; set; }

        [JsonProperty("Quantidade")]
        public string Quantidade { get; set; }

        [JsonProperty("Devolucao")]
        public string Devolucao { get; set; }
    }
}
