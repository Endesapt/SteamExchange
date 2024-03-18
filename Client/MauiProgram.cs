
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
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<ProfilePage>();
            //ViewModels
            builder.Services.AddSingleton<InventoriesViewModel>();
            builder.Services.AddScoped<DialogViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<ChatViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddSingleton<AuctionViewModel>();
            //Services
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<IAuthorizationHandler,AuthorizationHandlerService>();
            builder.Services.AddTransient<IRequestService,RequestService>();

            builder.Services.AddTransient<MauiAuthenticationBrowser>();
            builder.Services.AddSingleton(sp =>
       new OidcClient(new()
       {
           Authority = "https://settling-maggot-finally.ngrok-free.app",
           ClientId = "steamid",
           Scope = "openid profile",
           RedirectUri = "myapp://",
           PostLogoutRedirectUri = "myapp://",
           ClientSecret= "f5K8Y2aVa2F1HRhlvOe",
           Browser = new MauiAuthenticationBrowser()
       }));
#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
