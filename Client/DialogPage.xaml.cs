using Client.ViewModel;

namespace Client;

public partial class DialogPage : ContentPage
{
	public DialogPage(DialogViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}