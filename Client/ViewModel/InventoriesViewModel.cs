using Client.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
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
        public InventoriesViewModel(IAuthorizationHandler authorizationHandler)
        {
          _authorizationHandler = authorizationHandler;
        }
        [RelayCommand]
        async Task GoToDialog(string person)
        {
            await Shell.Current.DisplayAlert("you are going to page", person, "OK");
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
