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
    [QueryProperty(nameof(Chat),"chat")]
    public partial class DialogViewModel:BaseViewModel
    {
        [ObservableProperty]
        public Chat chat;
        [RelayCommand]
        async Task GoBack(string str)
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
