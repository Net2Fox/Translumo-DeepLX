using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Translumo.Translation.Configuration;
using Translumo.Translation.Deepl;
using Translumo.Translation.Exceptions;
using Translumo.Utils;
using Translumo.Utils.Http;

namespace Translumo.Translation.DeepLX
{
    public class DeepLXReaderProxy
    {
        public HttpReader HttpReader { get; protected set; }

        private const string TRANSLATE_URL = "http://localhost:1188/translate"; //"https://papago.naver.com/apis/n2mt/translate";

        public DeepLXReaderProxy(Proxy proxy = null)
        {
            HttpReader = CreateReader(proxy);
        }

        public async Task<string> RequestTranslationAsync(DeepLXRequest request)
        {
            var response = await HttpReader
                .RequestWebDataAsync(TRANSLATE_URL, HttpMethods.POST, HttpHelper.BuildJSONFormData(request.Body), true)
                .ConfigureAwait(false);

            if (response.IsSuccessful)
            {
                var DeepLXResponse = JsonSerializer.Deserialize<DeepLXResponse>(response.Body);
                if (DeepLXResponse == null)
                {
                    throw new TranslationException($"Unexpected response: '{response.Body}'");
                }

                return DeepLXResponse.Data;
            }
            else
            {
                throw new TranslationException($"Bad response by translator: '{response.Body}'");
            }
        }

        private HttpReader CreateReader(Proxy proxy)
        {
            var httpReader = new HttpReader();
            httpReader.Accept = "application/json";
            httpReader.Accept = "*/*";
            httpReader.Proxy = proxy?.ToWebProxy();

            return httpReader;
        }
    }
}
