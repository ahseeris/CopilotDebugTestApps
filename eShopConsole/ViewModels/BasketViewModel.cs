using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eShop.ClientApp.Models.Basket;
using eShop.ClientApp.Services.AppEnvironment;
using eShop.ClientApp.Services.Navigation;
using eShop.ClientApp.ViewModels.Base;

namespace eShop.ClientApp.ViewModels;

public partial class BasketViewModel : ViewModelBase
{
    private readonly IAppEnvironmentService _appEnvironmentService;
    private readonly ObservableCollectionEx<BasketItem> _basketItems = new();

    public BasketViewModel(
        IAppEnvironmentService appEnvironmentService,
        INavigationService navigationService)
        : base(navigationService)
    {
        _appEnvironmentService = appEnvironmentService;
    }

    public int BadgeCount => _basketItems?.Sum(basketItem => basketItem.Quantity) ?? 0;

    public decimal Total => _basketItems?.Sum(basketItem => basketItem.Quantity * basketItem.UnitPrice) ?? 0m;

    public IReadOnlyList<BasketItem> BasketItems => _basketItems;

    public override async Task InitializeAsync()
    {
        var basket = await _appEnvironmentService.BasketService.GetBasketAsync();

        if ((basket?.Items?.Count ?? 0) > 0)
        {
            await _basketItems.ReloadDataAsync(
                async innerList =>
                {
                    foreach (var basketItem in basket!.Items.ToArray())
                    {
                        basketItem.ProductName = basketItem.ProductName;
                        innerList.Add(basketItem);
                    }
                });
        }
    }

    [RelayCommand]
    public async Task DeleteAsync(BasketItem item)
    {
        var basket = await _appEnvironmentService.BasketService.GetBasketAsync();
        if (basket != null)
        {
            basket.RemoveItemFromBasket(item);
            await _appEnvironmentService.BasketService.UpdateBasketAsync(basket);
        }

        ReCalculateTotal();
    }

    [RelayCommand]
    private async Task CheckoutAsync()
    {
        if (_basketItems?.Any() ?? false)
        {
            _appEnvironmentService.BasketService.LocalBasketItems = _basketItems;
            await NavigationService.NavigateToAsync("Checkout");
        }
    }

    [RelayCommand]
    private Task BackAsync()
    {
        return NavigationService.PopAsync();
    }

    private void ReCalculateTotal()
    {
        OnPropertyChanged(nameof(BadgeCount));
        OnPropertyChanged(nameof(Total));
    }
}
