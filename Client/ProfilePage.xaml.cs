using Client.Services.Interfaces;
using Client.ViewModel;
using ModelLibrary;

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
        var response=await _requestService.GetAsync<User>($"/getProfile?userId={_vm.UserId}",8,true);
		if (response == null) { 
			await DisplayAlert("Error", $"Cannot get profile of user with Id {_vm.UserId}","OK");
			return;
		}
		if (_vm.ProfileCurrentState==ProfilePageState.ShowSelfPage) {
			WeaponsCollectionView.SelectionMode=SelectionMode.None;
		}
		if(_vm.ProfileCurrentState!=ProfilePageState.ShowSelfPage &&
			_vm.ProfileCurrentState != ProfilePageState.ShowOtherPage)
		{
			_vm.ShowProfileMenu = false;
		}
		else
		{
            TitleLabel.Text = response.UserName;
        }
		_vm.Weapons=response.Weapons;
		_vm.ShowedWeapons = response.Weapons.OrderByDescending(w=>w.Weapon.Price).ToList();
		ProfileImage.Source = $"https://avatars.steamstatic.com/{response.AvatarHash}_full.jpg";
		UserNameLabel.Text = response.UserName;

    }
}