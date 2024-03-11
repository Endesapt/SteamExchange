using Client.Models.ResponseModels;
using Client.Services.Interfaces;
using Client.ViewModel;

namespace Client;

public partial class ProfilePage : ContentPage
{
	private readonly IRequestService _requestService;
	private readonly ProfileViewModel _vm;
	public ProfilePage(ProfileViewModel vm,IRequestService requestService)
	{
		BindingContext = vm;
		_requestService = requestService;
		_vm = vm;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var response=await _requestService.GetAsync<ProfileResponseModel>($"/getProfile?userId={_vm.UserId}");
		if (response == null) { 
			await DisplayAlert("Error", $"Cannot get profile of user with Id {_vm.UserId}","OK");
			return;
		}
		if (!_vm.IsUserProfile) {
			TitleLabel.Text = response.User.UserName; 
		}
		_vm.Weapons=response.Weapons;
		_vm.ShowedWeapons = response.Weapons.OrderByDescending(w=>w.Price).ToList();
		ProfileImage.Source = $"https://avatars.steamstatic.com/{response.User.AvatarHash}_full.jpg";
		UserNameLabel.Text = response.User.UserName;

    }
}