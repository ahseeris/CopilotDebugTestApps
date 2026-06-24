using eShop.ClientApp.Models.Basket;
using eShop.ClientApp.Services.AppEnvironment;
using eShop.ClientApp.Services.Basket;
using eShop.ClientApp.Services.Catalog;
using eShop.ClientApp.Services.Navigation;
using eShop.ClientApp.ViewModels;

namespace eShop.ClientApp;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      eShop Client Application       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.WriteLine();

        // Set up services (mock backend)
        var basketService = new BasketMockService();
        var catalogService = new CatalogMockService();
        var appEnvironment = new AppEnvironmentService(basketService, catalogService);
        var navigationService = new ConsoleNavigationService();

        // Create view models (simulating app pages)
        var catalogViewModel = new CatalogViewModel(appEnvironment, navigationService);
        var basketViewModel = new BasketViewModel(appEnvironment, navigationService);

        // === User Flow: Browse Catalog ===
        Console.WriteLine("📱 Opening Catalog page...");
        await catalogViewModel.InitializeAsync();
        Console.WriteLine($"   Products loaded: {catalogViewModel.Products.Count}");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");
        Console.WriteLine();

        // === User Flow: Add items to cart ===
        Console.WriteLine("🛒 Adding items to cart...");
        var item1 = catalogViewModel.Products[0]; // .NET Bot Blue Sweatshirt
        var item2 = catalogViewModel.Products[1]; // .NET Black & White Mug
        var item3 = catalogViewModel.Products[3]; // .NET Foundation Notebook

        await catalogViewModel.AddToCartAsync(item1);
        Console.WriteLine($"   Added: {item1.Name} (${item1.Price})");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");

        await catalogViewModel.AddToCartAsync(item2);
        Console.WriteLine($"   Added: {item2.Name} (${item2.Price})");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");

        await catalogViewModel.AddToCartAsync(item3);
        Console.WriteLine($"   Added: {item3.Name} (${item3.Price})");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");
        Console.WriteLine();

        // === User Flow: Navigate back to Catalog after adding ===
        Console.WriteLine("📱 Navigating back to Catalog page...");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");
        Console.WriteLine();

        // === User Flow: Navigate to Basket ===
        Console.WriteLine("📱 Opening Basket page...");
        await basketViewModel.InitializeAsync();
        Console.WriteLine($"   Items in basket: {basketViewModel.BasketItems.Count}");
        Console.WriteLine($"   Basket total: ${basketViewModel.Total}");
        Console.WriteLine($"   Basket badge: {basketViewModel.BadgeCount}");
        Console.WriteLine();

        // === User Flow: Delete items from cart ===
        Console.WriteLine("🗑️  Removing items from cart...");
        var itemsToDelete = basketViewModel.BasketItems.ToList();
        foreach (var item in itemsToDelete)
        {
            await basketViewModel.DeleteAsync(item);
            Console.WriteLine($"   Removed: {item.ProductName}");
            Console.WriteLine($"   Basket badge: {basketViewModel.BadgeCount}");
        }
        Console.WriteLine();

        // === User Flow: Navigate back to Catalog ===
        Console.WriteLine("📱 Navigating back to Catalog page...");
        Console.WriteLine($"   Cart badge: {catalogViewModel.BadgeCount}");
        Console.WriteLine();
        Console.WriteLine("Done.");

        // Keep the process alive so the debugger can attach, set breakpoints, and re-run.
        // The test runner kills the process when the simulation ends.
        Console.ReadLine();
    }
}
