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
        [RelayCommand]
        async Task GoToDialog(string person)
        {
            await Shell.Current.DisplayAlert("you are going to page", person, "OK");
            await Shell.Current.GoToAsync($"{nameof(DialogPage)}");
        }
    }
}
