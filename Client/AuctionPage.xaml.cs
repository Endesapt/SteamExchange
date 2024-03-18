using Client.Services.Interfaces;
using Client.ViewModel;
using ModelLibrary;

namespace Client;

public partial class AuctionPage : ContentPage
{
	private readonly IRequestService _requestService;
	private readonly AuctionViewModel _viewModel; 
	private readonly IAuthorizationHandler _authorizationHandler;
	public AuctionPage(IRequestService requestService, AuctionViewModel vm
, IAuthorizationHandler authorizationHandler)
    {
        _requestService = requestService;
        _viewModel = vm;
        BindingContext = vm;
        InitializeComponent();
        _authorizationHandler = authorizationHandler;
    }
    protected override async void OnAppearing(
        )
	{
		base.OnAppearing();
        base.OnAppearing();
        var imageSource = _authorizationHandler.UserClaims.FirstOrDefault((c) => c.Type == "picture")?.Value;
        if (imageSource != null)
        {
            ProfileImage.Source = imageSource;
        }
        var response =await _requestService.GetAsync<IEnumerable<Offer>>("/getOffers",8,true);
        _viewModel.Offers = new(response);
		
	}
}