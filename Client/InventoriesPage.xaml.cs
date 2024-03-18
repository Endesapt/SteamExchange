using Client.Services.Interfaces;
using Client.ViewModel;
using IdentityModel.Client;
using Microsoft.Maui.Controls.PlatformConfiguration;
using ModelLibrary;
using System.Net.Http.Headers;

namespace Client;

public partial class InventoriesPage : ContentPage
{
	private readonly IAuthorizationHandler _authorizationHandler;
    private readonly IRequestService _requestService;
	private readonly InventoriesViewModel _viewModel;
    public InventoriesPage(IAuthorizationHandler authorizationHandler
		,InventoriesViewModel vm,IRequestService requestService)
	{
		_authorizationHandler = authorizationHandler;
        _viewModel = vm;
		_requestService = requestService;
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
		var response = await _requestService.GetAsync<IEnumerable<User>>("/getInventories",8,true);
		if (response != null)
		{
			_viewModel.Inventories = response.ToList();
		}
    }
}