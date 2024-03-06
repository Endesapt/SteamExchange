namespace Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DialogPage), typeof(DialogPage));
            //Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            //Routing.RegisterRoute(nameof(InventoriesPage), typeof(InventoriesPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
