using eShop.ClientApp.Models.Catalog;

namespace eShop.ClientApp.Services.Catalog;

public interface ICatalogService
{
    Task<IEnumerable<CatalogItem>> GetCatalogAsync();
    Task<CatalogItem> GetCatalogItemAsync(int catalogItemId);
    Task<IEnumerable<CatalogBrand>> GetCatalogBrandAsync();
    Task<IEnumerable<CatalogType>> GetCatalogTypeAsync();
    Task<IEnumerable<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId);
}

public class CatalogMockService : ICatalogService
{
    private static readonly List<CatalogItem> _mockCatalog = new()
    {
        new CatalogItem { Id = 1, Name = ".NET Bot Blue Sweatshirt", Price = 19.50m, PictureUri = "product_01.png", CatalogBrandId = 1, CatalogTypeId = 1 },
        new CatalogItem { Id = 2, Name = ".NET Black & White Mug", Price = 8.50m, PictureUri = "product_02.png", CatalogBrandId = 1, CatalogTypeId = 2 },
        new CatalogItem { Id = 3, Name = "Prism White T-Shirt", Price = 12.00m, PictureUri = "product_03.png", CatalogBrandId = 2, CatalogTypeId = 1 },
        new CatalogItem { Id = 4, Name = ".NET Foundation Notebook", Price = 5.00m, PictureUri = "product_04.png", CatalogBrandId = 1, CatalogTypeId = 3 },
        new CatalogItem { Id = 5, Name = "Roslyn Red Pin", Price = 3.00m, PictureUri = "product_05.png", CatalogBrandId = 3, CatalogTypeId = 4 },
    };

    public Task<IEnumerable<CatalogItem>> GetCatalogAsync() => Task.FromResult<IEnumerable<CatalogItem>>(_mockCatalog);

    public Task<CatalogItem> GetCatalogItemAsync(int catalogItemId) =>
        Task.FromResult(_mockCatalog.First(c => c.Id == catalogItemId));

    public Task<IEnumerable<CatalogBrand>> GetCatalogBrandAsync() =>
        Task.FromResult<IEnumerable<CatalogBrand>>(new List<CatalogBrand>
        {
            new() { Id = 1, Brand = ".NET" },
            new() { Id = 2, Brand = "Prism" },
            new() { Id = 3, Brand = "Roslyn" }
        });

    public Task<IEnumerable<CatalogType>> GetCatalogTypeAsync() =>
        Task.FromResult<IEnumerable<CatalogType>>(new List<CatalogType>
        {
            new() { Id = 1, Type = "Shirt" },
            new() { Id = 2, Type = "Mug" },
            new() { Id = 3, Type = "Notebook" },
            new() { Id = 4, Type = "Pin" }
        });

    public Task<IEnumerable<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId) =>
        Task.FromResult<IEnumerable<CatalogItem>>(
            _mockCatalog.Where(c => c.CatalogBrandId == catalogBrandId && c.CatalogTypeId == catalogTypeId));
}
