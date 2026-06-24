namespace eShop.ClientApp.Services.Navigation;

public interface INavigationService
{
    Task InitializeAsync();
    Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);
    Task PopAsync();
}

public class ConsoleNavigationService : INavigationService
{
    public Task InitializeAsync() => Task.CompletedTask;

    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        Console.WriteLine($"  [Nav] → {route}");
        return Task.CompletedTask;
    }

    public Task PopAsync()
    {
        Console.WriteLine($"  [Nav] ← Back");
        return Task.CompletedTask;
    }
}
