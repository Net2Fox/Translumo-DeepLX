using System.Net;
using Translumo.Translation.Configuration;

namespace Translumo.Translation.DeepLX
{
    public sealed class DeepLXContainer : TranslationContainer
    {
        public DeepLXReaderProxy Reader { get; set; }

        public DeepLXContainer(Proxy proxy = null, bool isPrimary = false) : base(proxy, isPrimary)
        {
            Reader = new DeepLXReaderProxy(proxy);
        }

        public override void Reset()
        {
            base.Reset();
            Reader.HttpReader.Cookies = new CookieContainer();
        }
    }
}
