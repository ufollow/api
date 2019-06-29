using WebMarkupMin.Core;

namespace ufollow.Infrastructure.Mailing
{
    public sealed class HtmlMessage
    {
        private readonly string _html;

        public HtmlMessage(string html)
        {
            _html = html;
        }

        public string Minified()
        {
            var settings = new HtmlMinificationSettings
            {
                AttributeQuotesRemovalMode = HtmlAttributeQuotesRemovalMode.KeepQuotes,
                RemoveOptionalEndTags = false
            };

            var minifier = new HtmlMinifier(settings);

            return minifier.Minify(_html).MinifiedContent;
        }
    }

}
