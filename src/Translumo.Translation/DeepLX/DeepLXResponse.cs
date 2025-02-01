using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Translumo.Translation.DeepLX
{
    internal class DeepLXResponse
    {
        [JsonPropertyName("alternatives")]
        public List<string> alternatives { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("id")]
        public long ID { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("source_lang")]
        public string SourceLang {  get; set; }

        [JsonPropertyName("target_lang")]
        public string TargetLang { get; set; }
    }
}
