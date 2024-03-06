using Client.Services.Interfaces;
using Client.ViewModel;

namespace Client;

public partial class InventoriesPage : ContentPage
{
	private readonly IAuthorizationHandler _authorizationHandler;
	private readonly HttpClient _httpClient;
	public InventoriesPage(IAuthorizationHandler authorizationHandler,InventoriesViewModel vm)
	{
		_authorizationHandler = authorizationHandler;
		_httpClient = new HttpClient();
		BindingContext = vm;
		InitializeComponent();
	}
}