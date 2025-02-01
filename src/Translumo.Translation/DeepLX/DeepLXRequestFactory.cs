using Translumo.Translation.DeepLX;

namespace Translumo.Translation.DeepLX
{
    internal static class DeepLXRequestFactory
    {
        public static DeepLXRequest CreateRequest(DeepLXContainer container, string text, string sourceLangCode, string targetLangCode)
        {
            return new DeepLXRequest()
            {
                Body = new DeepLXRequest.DeepLXRequestBody()
                {
                    source_lang = sourceLangCode,
                    target_lang = targetLangCode,
                    text = text
                }
            };
        }
    }
}
