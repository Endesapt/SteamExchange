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
    public partial class InventoriesViewModel:BaseViewModel
    {
        private readonly IAuthorizationHandler _authorizationHandler;
        private readonly IRequestService _requestService;
        [ObservableProperty]
        public ObservableCollection<User> inventories=new();
        public InventoriesViewModel(IAuthorizationHandler authorizationHandler, IRequestService requestService)
        {
            _authorizationHandler = authorizationHandler;
            _requestService = requestService;
        }
        [RelayCommand]
        async Task GoToDialog(long toUserId)
        {
            var chat = await _requestService.GetAsync<Chat>($"/getChat?toUserId={toUserId}",24,true);
            await Shell.Current.GoToAsync($"{nameof(DialogPage)}",new Dictionary<string, object>()
            {
                {"chat",chat}
            });
        }
        [RelayCommand]
        async Task GoToProfile(long userId)
        {
            if (userId > 0)
            {
                await Shell.Current.GoToAsync(nameof(ProfilePage), new Dictionary<string, object> {
                    {"userId",userId},
                    {"pageState",ProfilePageState.ShowOtherPage}
                });
            }
            else
            {
                userId = _authorizationHandler.GetUserId()??0;
                await Shell.Current.GoToAsync(nameof(ProfilePage), new Dictionary<string, object>
                {
                    {"userId",userId},
                    {"pageState",ProfilePageState.ShowSelfPage}
                });
            }
        }
        [RelayCommand]
        async Task Scrolled(ItemsViewScrolledEventArgs e)
        {
            if(IsBusy) return;
            if(e.LastVisibleItemIndex>=Inventories.Count-5) 
            {
                IsBusy = true;
                var fromTime = Inventories.Last().InventoryUpgradeTime.ToString("yyyyMMddHHmmssfff");
                var response = await _requestService.GetAsync<IEnumerable<User>>($"/getInventories?count=10&fromTimeString={fromTime}", 15, true);
                if (response != null)
                {
                    foreach(var user in response)
                    {
                        Inventories.Add(user);
                    }
                }
                IsBusy = false;
            }
        }
    
    }
}
