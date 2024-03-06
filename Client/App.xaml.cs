using Client.Services.Interfaces;
using Client.ViewModel;
using System.Runtime.CompilerServices;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
