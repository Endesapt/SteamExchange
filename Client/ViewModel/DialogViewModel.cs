﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public partial class DialogViewModel:BaseViewModel
    {
        [RelayCommand]
        async Task GoBack(string str)
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
