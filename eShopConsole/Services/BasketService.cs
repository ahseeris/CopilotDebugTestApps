using eShop.ClientApp.Models.Basket;

namespace eShop.ClientApp.Services.Basket;

public interface IBasketService
{
    IEnumerable<BasketItem> LocalBasketItems { get; set; }
    Task<CustomerBasket> GetBasketAsync();
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
    Task ClearBasketAsync();
}

public class BasketMockService : IBasketService
{
    private CustomerBasket _mockCustomBasket;

    public BasketMockService()
    {
        _mockCustomBasket = new CustomerBasket { BuyerId = "9245fe4a-d402-451c-b9ed-9c1a04247482" };
    }

    public IEnumerable<BasketItem> LocalBasketItems { get; set; } = Array.Empty<BasketItem>();

    public async Task<CustomerBasket> GetBasketAsync()
    {
        await Task.Delay(10);
        return _mockCustomBasket;
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
    {
        await Task.Delay(10);
        _mockCustomBasket = customerBasket;
        return _mockCustomBasket;
    }

    public async Task ClearBasketAsync()
    {
        await Task.Delay(10);
        _mockCustomBasket.ClearBasket();
        LocalBasketItems = null!;
    }
}
