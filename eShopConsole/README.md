# eShopConsole

A console-based shopping cart app with an intentional bug in the delete flow.

## Bug

After deleting items from the cart, the basket badge count stays at the old value instead of going to zero. The service basket is correctly updated and shows 0 items, but the local collection is not updated.

## Repro Steps

1. Build and launch eShop-Console.exe
2. The app adds 3 items to the cart
3. The app deletes all items from the cart
4. Observe the basket badge count still shows 3 even though all items were removed

## Root Cause

In `BasketViewModel.DeleteAsync`, the `_basketItems.Remove(item)` call is missing, so the local collection stays stale after deletion. The service correctly removes the item, but the local state is never updated.
