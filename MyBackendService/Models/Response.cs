using Newtonsoft.Json;

namespace MyBackendService.Models
{
    public record Response
    {
        [JsonProperty("error")]
        public string Error { get; init; }

        [JsonProperty("warning")]
        public string Warning { get; init; }

        [JsonProperty("data")]
        public object Data { get; init; }
    }
}