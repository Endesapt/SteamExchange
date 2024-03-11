using Client.Services.Interfaces;
using Client.ViewModel;
using IdentityModel.Client;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net.Http.Headers;

namespace Client;

public partial class InventoriesPage : ContentPage
{
	private readonly IAuthorizationHandler _authorizationHandler;
	public InventoriesPage(IAuthorizationHandler authorizationHandler,InventoriesViewModel vm)
	{
		_authorizationHandler = authorizationHandler;
        //settting AccessToken
        BindingContext = vm;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		var imageSource = _authorizationHandler.UserClaims.FirstOrDefault((c) => c.Type == "picture")?.Value;
		if (imageSource != null)
		{
			ProfileImage.Source = imageSource;
		}
    }
}