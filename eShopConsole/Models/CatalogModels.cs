namespace eShop.ClientApp.Models.Catalog;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureUri { get; set; } = string.Empty;
    public int CatalogBrandId { get; set; }
    public int CatalogTypeId { get; set; }
}

public class CatalogBrand
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
}

public class CatalogType
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}
