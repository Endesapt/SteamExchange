namespace Client;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await SecureStorage.GetAsync("access_token") !=null)
        {
            await Shell.Current.GoToAsync($"///{nameof(InventoriesPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        base.OnNavigatedTo(args);
    }
}