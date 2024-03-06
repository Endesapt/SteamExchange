using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public partial class ChatViewModel:BaseViewModel
    {
        [RelayCommand]
        async Task GoToDialog(string str)
        {
            await Shell.Current.GoToAsync($"{nameof(DialogPage)}");
        }
    }
}
