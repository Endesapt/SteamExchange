using Client.Services.Interfaces;
using Client.ViewModel;
using ModelLibrary;

namespace Client;

public partial class DialogPage : ContentPage
{
	public IRequestService _requestService;
    private readonly DialogViewModel _viewModel;
    public DialogPage(DialogViewModel vm,IRequestService requestService)
	{
		_requestService= requestService;
		InitializeComponent();
        _viewModel = vm;
		BindingContext = vm;
	}
	protected override async void OnAppearing()
	{
		await Shell.Current.DisplayAlert("Chat", $"You are speaking in chat {_viewModel.Chat.Id}","OKI DOKI");
    }
}