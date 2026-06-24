namespace eShop.ClientApp.Models.Basket;

public class BasketItem
{
    private int _quantity;

    public string Id { get; set; } = string.Empty;

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public decimal OldUnitPrice { get; set; }

    public bool HasNewPrice => OldUnitPrice != 0.0m;

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
    }

    public string PictureUrl { get; set; } = string.Empty;

    public decimal Total => Quantity * UnitPrice;

    public override string ToString()
    {
        return $"Product Id: {ProductId}, Quantity: {Quantity}";
    }
}
