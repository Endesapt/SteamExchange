
using Client.Services;
using Client.Services.Interfaces;
using Client.ViewModel;
using CommunityToolkit.Maui;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Client
{
    public static class MauiProgram
    {
        private static HttpClient GetInsecureHttpClient()
        {

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(handler);
            return client;
        }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.ttf", "FASolid");
                });
            //Pages
            builder.Services.AddScoped<DialogPage>();
            builder.Services.AddSingleton<ChatPage>();
            builder.Services.AddSingleton<AuctionPage>();
            builder.Services.AddSingleton<InventoriesPage>();
            builder.Services.AddSingleton<LoginPage>();
            //ViewModels
            builder.Services.AddSingleton<InventoriesViewModel>();
            builder.Services.AddScoped<DialogViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<ChatViewModel>();
            //Services
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IAuthorizationHandler,AuthorizationHandlerService>();

            builder.Services.AddTransient<WebAuthenticationBrowser>();
            builder.Services.AddTransient<OidcClient>(sp =>
       new OidcClient(new OidcClientOptions
       {
           Authority = "https://bb1f-87-252-236-142.ngrok-free.app",
           ClientId = "steamid",
           Scope = "openid profile",
           RedirectUri = "myapp://",
           PostLogoutRedirectUri = "myapp://",
           //Test client secret

           ClientSecret = "f5K8Y2aVa2F1HRhlvOe",
           HttpClientFactory = options => GetInsecureHttpClient(),
           Browser = sp.GetRequiredService<WebAuthenticationBrowser>()
       }));
#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
