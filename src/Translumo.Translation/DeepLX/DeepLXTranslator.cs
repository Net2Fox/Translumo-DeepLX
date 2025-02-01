using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.Logging;
using Translumo.Infrastructure.Language;
using Translumo.Translation.Configuration;
using Translumo.Translation.Exceptions;
using Translumo.Utils.Extensions;

namespace Translumo.Translation.DeepLX
{
    public sealed class DeepLXTranslator : BaseTranslator<DeepLXContainer>
    {
        private readonly AutoResetEvent _sync;
        private readonly HashSet<Languages> _unsupportedLanguages = new(new[]
        {
            Languages.Turkish, Languages.Arabic, Languages.PortugueseBrazil, Languages.Greek
        });

        public DeepLXTranslator(TranslationConfiguration translationConfiguration, LanguageService languageService, ILogger logger) : 
            base(translationConfiguration, languageService, logger)
        {
            this._sync = new AutoResetEvent(true);
        }

        public override Task<string> TranslateTextAsync(string sourceText)
        {
            //TODO: Temp implementation for specific lang
            if (_unsupportedLanguages.Contains(TargetLangDescriptor.Language))
            {
                throw new TransactionException("DeepLX translator is unavailable for this language");
            }

            return base.TranslateTextAsync(sourceText);
        }


        protected override async Task<string> TranslateTextInternal(DeepLXContainer container, string sourceText)
        {
            var request = DeepLXRequestFactory.CreateRequest(container, sourceText, SourceLangDescriptor.IsoCode,
                TargetLangDescriptor.IsoCode);

            return await container.Reader.RequestTranslationAsync(request);
        }

        protected override IList<DeepLXContainer> CreateContainers(TranslationConfiguration configuration)
        {
            var result = configuration.ProxySettings.Select(proxy => new DeepLXContainer(proxy)).ToList();
            result.Add(new DeepLXContainer(isPrimary: true));

            return result;
        }
    }
}
