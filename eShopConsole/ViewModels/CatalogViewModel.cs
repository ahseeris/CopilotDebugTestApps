using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using eShop.ClientApp.Messages;
using eShop.ClientApp.Models.Basket;
using eShop.ClientApp.Models.Catalog;
using eShop.ClientApp.Services.AppEnvironment;
using eShop.ClientApp.Services.Navigation;
using eShop.ClientApp.ViewModels.Base;

namespace eShop.ClientApp.ViewModels;

public partial class CatalogViewModel : ViewModelBase
{
    private readonly IAppEnvironmentService _appEnvironmentService;
    private readonly ObservableCollectionEx<CatalogItem> _products = new();

    [ObservableProperty] private int _badgeCount;

    private bool _initialized;

    public CatalogViewModel(
        IAppEnvironmentService appEnvironmentService,
        INavigationService navigationService)
        : base(navigationService)
    {
        _appEnvironmentService = appEnvironmentService;

        WeakReferenceMessenger.Default
            .Register<CatalogViewModel, ProductCountChangedMessage>(
                this,
                (_, message) =>
                {
                    BadgeCount = message.Value;
                });
    }

    public IReadOnlyList<CatalogItem> Products => _products;

    public override async Task InitializeAsync()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        await IsBusyFor(
            async () =>
            {
                var products = await _appEnvironmentService.CatalogService.GetCatalogAsync();
                var basket = await _appEnvironmentService.BasketService.GetBasketAsync();

                BadgeCount = basket.ItemCount;

                _products.ReloadData(products);
            });
    }

    [RelayCommand]
    public async Task AddToCartAsync(CatalogItem catalogItem)
    {
        if (catalogItem is null)
        {
            return;
        }

        var basket = await _appEnvironmentService.BasketService.GetBasketAsync();
        if (basket is not null)
        {
            basket.AddItemToBasket(
                new BasketItem
                {
                    ProductId = catalogItem.Id,
                    ProductName = catalogItem.Name,
                    PictureUrl = catalogItem.PictureUri,
                    UnitPrice = catalogItem.Price,
                    Quantity = 1
                });

            var basketUpdate = await _appEnvironmentService.BasketService.UpdateBasketAsync(basket);

            WeakReferenceMessenger.Default
                .Send(new ProductCountChangedMessage(basketUpdate.ItemCount));
        }
    }

    [RelayCommand]
    private async Task ViewBasket()
    {
        await NavigationService.NavigateToAsync("Basket");
    }
}
