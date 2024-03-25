using Client.Services.Interfaces;
using IdentityModel.Client;

namespace Client;

public partial class LoadingPage : ContentPage
{
    private IAuthorizationHandler _authorizationHandler;
	public LoadingPage(IAuthorizationHandler authorizationHandler)
	{
        _authorizationHandler = authorizationHandler;
		InitializeComponent();
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await _authorizationHandler.IsAuthenticatedAsync())
        {
            var httpClient = new HttpClient();
            var accessToken=await _authorizationHandler.GetAccessTokenAsync();
            httpClient.SetBearerToken(accessToken);
            await Shell.Current.GoToAsync($"///{nameof(InventoriesPage)}");
#if !DEBUG
            var answer = await httpClient.GetAsync("http://192.168.0.105:45455/checkUserCreated");
#endif
            
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        base.OnNavigatedTo(args);
    }
}