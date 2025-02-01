namespace Translumo.Translation.DeepLX
{
    public class DeepLXRequest
    {
        public DeepLXRequestBody Body { get; set; }

        public sealed class DeepLXRequestBody
        {
            public string text { get; set; }
            public string source_lang { get; set; }

            public string target_lang { get; set; }
        }
    }
}
