using Client.Services.Interfaces;
using Client.ViewModel;
using MonkeyCache.FileStore;
using System.Runtime.CompilerServices;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            MainPage = new AppShell();
        }
    }
}
