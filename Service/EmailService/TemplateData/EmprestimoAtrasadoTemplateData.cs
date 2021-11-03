using Newtonsoft.Json;

namespace Service.EmailService
{
    public class EmprestimoAtrasadoTemplateData : BaseTemplateData
    {
        [JsonProperty("Ferramenta")]
        public string Ferramenta { get; set; }

        [JsonProperty("Quantidade")]
        public string Quantidade { get; set; }
    }
}
