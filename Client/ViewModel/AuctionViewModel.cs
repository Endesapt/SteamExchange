﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        [ObservableProperty]
        ObservableCollection<Offer> offers;

    }
}
