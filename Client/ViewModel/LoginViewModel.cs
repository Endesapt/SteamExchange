using Client;
using Client.Services.Interfaces;
using Client.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace Client.ViewModel;

public partial class LoginViewModel:BaseViewModel
{
    protected readonly IAuthorizationHandler _authHandler;
    protected readonly IConnectivity _connectivity;

    public LoginViewModel(IAuthorizationHandler authHandler, IConnectivity connectivity)
    {
        _authHandler = authHandler;
        _connectivity = connectivity;
    }

    [RelayCommand]
    async Task LoginAsync()
    {

        try
        {
            if (_connectivity.NetworkAccess is not NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Offline", "Check your internet connection", "Ok");
                return;
            }

            var accessToken = await _authHandler.LoginAsync();
            if (accessToken != null) {
                await Shell.Current.GoToAsync($"///{nameof(LoadingPage)}"); 
            }
            else await Shell.Current.DisplayAlert("Error", "Error while authenticating process, please,try later", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
        }
    }
}