using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace Client
{
    internal class MauiAuthenticationBrowser:IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(options.StartUrl),
                    new Uri(options.EndUrl));

                var url = new RequestUrl("myapp://")
                    .Create(new Parameters(result.Properties));

                return new BrowserResult
                {
                    Response = url,
                    ResultType = BrowserResultType.Success
                };
            }
            catch (TaskCanceledException)
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UserCancel
                };
            }
        }

        public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
        {
            IEnumerable<string> parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
            var values = string.Join("&", parameters);

            return $"{redirectUrl}#{values}";
        }
    }
}
