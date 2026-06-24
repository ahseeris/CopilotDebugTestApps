using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eShop.ClientApp.Services.Navigation;

namespace eShop.ClientApp.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject
{
    private long _isBusy;

    [ObservableProperty] private bool _isInitialized;

    public ViewModelBase(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;

    public INavigationService NavigationService { get; }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    protected async Task IsBusyFor(Func<Task> unitOfWork)
    {
        Interlocked.Increment(ref _isBusy);
        OnPropertyChanged(nameof(IsBusy));

        try
        {
            await unitOfWork();
        }
        finally
        {
            Interlocked.Decrement(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));
        }
    }
}
