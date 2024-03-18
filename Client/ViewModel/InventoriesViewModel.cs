using Client.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public partial class InventoriesViewModel:BaseViewModel
    {
        private readonly IAuthorizationHandler _authorizationHandler;
        [ObservableProperty]
        public List<User> inventories=new();
        public InventoriesViewModel(IAuthorizationHandler authorizationHandler)
        {
          _authorizationHandler = authorizationHandler;
        }
        [RelayCommand]
        async Task GoToDialog(long userId)
        {
            await Shell.Current.DisplayAlert("you are going to page", userId.ToString(), "OK");
            await Shell.Current.GoToAsync($"{nameof(DialogPage)}");
        }
        [RelayCommand]
        async Task GoToProfile(long userId)
        {
            if (userId > 0)
            {
                await Shell.Current.GoToAsync(nameof(ProfilePage), new Dictionary<string, object> {
                    {"userId",userId},
                    {"isUserProfile",false}
                });
            }
            else
            {
                userId = _authorizationHandler.GetUserId()??0;
                await Shell.Current.GoToAsync(nameof(ProfilePage), new Dictionary<string, object>
                {
                    {"userId",userId},
                    {"isUserProfile",true}
                });
            }
        }
    
    }
}
