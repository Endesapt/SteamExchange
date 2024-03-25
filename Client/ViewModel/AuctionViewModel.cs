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
    public partial class AuctionViewModel:BaseViewModel
    {
        private readonly IAuthorizationHandler _authorizationHandler;
        public AuctionViewModel(IAuthorizationHandler authorizationHandler)
        {
            _authorizationHandler = authorizationHandler;
        }
        [ObservableProperty]
        ObservableCollection<Offer> offers=new();
        [RelayCommand]
        public async Task ToCreateAuctionAsync()
        {
            await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}", new Dictionary<string, object> {
                {"userId",_authorizationHandler.GetUserId()!},
                {"pageState",ProfilePageState.ChooseAuctionWeapons}
            });
        }
    }
    
}
