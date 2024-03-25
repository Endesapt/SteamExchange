
using Client.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public enum ProfilePageState
    {
        ShowSelfPage,
        ShowOtherPage,
        ChooseSelfTradeWeapons,
        ChooseOtherTradeWeapons,
        ChooseAuctionWeapons
    }
    [QueryProperty(nameof(UserId), "userId")]
    [QueryProperty(nameof(ProfileCurrentState), "pageState")]
    [QueryProperty(nameof(GetWeapons),"weapons")]
    public partial class ProfileViewModel:BaseViewModel
    {
        private IAuthorizationHandler _authorizationHandler;
        private IRequestService _requestService;
        public ProfileViewModel(IAuthorizationHandler authorizationHandler, IRequestService requestService)
        {
            _authorizationHandler = authorizationHandler;
            _requestService = requestService;
        }
        [ObservableProperty]
        public List<UserWeapon> getWeapons = new();
        [ObservableProperty]
        public List<UserWeapon> showedWeapons=new();
        public List<UserWeapon> Weapons = new();
        [ObservableProperty]
        public List<UserWeapon> checkedWeapons = new();
        [ObservableProperty]
        long userId;
        [ObservableProperty]
        bool showProfileMenu=true;
        [ObservableProperty]
        bool showTradeFlyout;
        [ObservableProperty]
        ProfilePageState profileCurrentState;
        [RelayCommand]
        public void OnCollectionSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) { ShowTradeFlyout = false; return; }
            if (e.CurrentSelection.Count >= 1 && !ShowTradeFlyout) { 
                ShowTradeFlyout = true; 
            }
            CheckedWeapons=e.CurrentSelection.Select(c=>c as UserWeapon).ToList()!;
            

        }
        [RelayCommand]
        public async Task OnContinueAsync()
        {
            if (ProfileCurrentState == ProfilePageState.ShowOtherPage)
            {
                await Shell.Current.GoToAsync($"./{nameof(ProfilePage)}", new Dictionary<string, object>
                {
                    {"userId",_authorizationHandler.GetUserId()!},
                    {"pageState",ProfilePageState.ChooseOtherTradeWeapons},
                    {"weapons",CheckedWeapons}
                });
            }
            if (ProfileCurrentState == ProfilePageState.ChooseAuctionWeapons)
            {
                await CreateAuction();
                await Shell.Current.GoToAsync($"///{nameof(AuctionPage)}");
            } 
        }
        public async Task CreateAuction()
        {
            bool created=await _requestService.CreateOfferAsync(CheckedWeapons);
            if (!created) await Shell.Current.DisplayAlert("Error", "Auction was not created", "OK");
        }
    }
    
}
