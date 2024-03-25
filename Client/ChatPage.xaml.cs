using Client.Services.Interfaces;
using Client.ViewModel;
using ModelLibrary;

namespace Client;

public partial class ChatPage : ContentPage
{
    private readonly IRequestService _requestService;
    private readonly ChatViewModel _viewModel;
	public ChatPage(ChatViewModel vm,IRequestService requestService)
	{
		InitializeComponent();
        _requestService = requestService;
		BindingContext = vm;
        _viewModel = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var response = await _requestService.GetAsync<IEnumerable<Chat>>("/getChats", 8, true);
        if (response != null)
        {
            _viewModel.Chats = response.ToList();
        }
    }
}