using CommunityToolkit.Mvvm.ComponentModel;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    [QueryProperty(nameof(UserId), "userId")]
    [QueryProperty(nameof(IsUserProfile), "isUserProfile")]
    public partial class ProfileViewModel:BaseViewModel
    {
        [ObservableProperty]
        public List<UserWeapon> showedWeapons=new();
        public List<UserWeapon> Weapons = new();
        [ObservableProperty]
        long userId;
        [ObservableProperty]
        bool isUserProfile;
    }
}
