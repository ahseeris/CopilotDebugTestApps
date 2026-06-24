using eShop.ClientApp.Services.Basket;
using eShop.ClientApp.Services.Catalog;

namespace eShop.ClientApp.Services.AppEnvironment;

public interface IAppEnvironmentService
{
    IBasketService BasketService { get; }
    ICatalogService CatalogService { get; }
}

public class AppEnvironmentService : IAppEnvironmentService
{
    public AppEnvironmentService(IBasketService basketService, ICatalogService catalogService)
    {
        BasketService = basketService;
        CatalogService = catalogService;
    }

    public IBasketService BasketService { get; }
    public ICatalogService CatalogService { get; }
}
