using AdventureWorksProject.Services;
using AdventureWorksProject.Utils;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var productService = new ProductService();
        var orderService = new OrderService();

        while (true)
        {
            try
            {
                Logger.Log("Showing menu");
                Console.Clear();
                Console.WriteLine("AdventureWorks Menu");
                Console.WriteLine("1. List products by category");
                Console.WriteLine("2. Show order details");
                Console.WriteLine("3. Add new order");
                Console.WriteLine("4. Sales report by category");
                Console.WriteLine("5. Exit");
                Console.Write("\nYour choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        var categories = await productService.GetCategoriesAsync();
                        Console.WriteLine("Categories:");
                        foreach (var category in categories)
                        {
                            if (category != null)
                            {
                                Console.WriteLine($"{category.ProductCategoryID}. {category.Name}");
                            }
                        }
                        Console.Write("\nCategory ID (leave empty for all products): ");
                        var categoryInput = Console.ReadLine();
                        int? categoryId = string.IsNullOrEmpty(categoryInput) ? null : int.Parse(categoryInput);

                        var products = await productService.GetProductsByCategoryAsync(categoryId);
                        Console.WriteLine("\nProducts:");
                        foreach (var product in products)
                        {
                            if (product != null)
                            {
                                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: {product.ListPrice:C}");
                            }
                        }
                        break;

                    case "2":
                        Console.Clear();
                        var orders = await orderService.GetOrderDetailsAsync();
                        Console.Write("Order ID girin: ");
                        var selectedOrderInput = Console.ReadLine();
                        if (!int.TryParse(selectedOrderInput, out int selectedOrderId))
                        {
                            Console.WriteLine("Invalid Order ID!");
                            break;
                        }
                        var selectedOrder = orders.FirstOrDefault(o => o.SalesOrderID == selectedOrderId);
                        if (selectedOrder == null)
                        {
                            Console.WriteLine("Order not found!");
                            break;
                        }
                        Console.WriteLine($"\nOrder ID: {selectedOrder.SalesOrderID}");
                        Console.WriteLine($"Customer ID: {selectedOrder.CustomerID}");
                        Console.WriteLine($"Order Date: {selectedOrder.OrderDate}");
                        Console.WriteLine($"Total: {selectedOrder.TotalDue:C}");
                        Console.WriteLine("Products:");
                        foreach (var detail in selectedOrder.SalesOrderDetails)
                        {
                            string productName = null;
                            try
                            {
                                using (var context = new AdventureWorksProject.Data.AdventureWorksContext())
                                {
                                    var product = context.Products.FirstOrDefault(p => p.ProductID == detail.ProductID);
                                    if (product != null)
                                        productName = product.Name;
                                }
                            }
                            catch { }
                            if (string.IsNullOrEmpty(productName))
                                productName = $"ProductID: {detail.ProductID} (Product not found!)";

                            var tutar = detail.LineTotal == 0 ? detail.UnitPrice * detail.OrderQty : detail.LineTotal;
                            Console.WriteLine($"  - {productName} x {detail.OrderQty} = {tutar:C}");
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("Customer ID: ");
                        var customerIdInput = Console.ReadLine();
                        if (string.IsNullOrEmpty(customerIdInput))
                        {
                            Console.WriteLine("Invalid customer ID!");
                            break;
                        }
                        var customerId = int.Parse(customerIdInput);

                        var orderItems = new List<(int productId, short quantity)>();
                        while (true)
                        {
                            Console.Write("\nProduct ID (leave empty to finish): ");
                            var productInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(productInput)) break;

                            Console.Write("Quantity: ");
                            var quantityInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(quantityInput))
                            {
                                Console.WriteLine("Invalid quantity!");
                                continue;
                            }
                            var quantity = short.Parse(quantityInput);

                            if (int.TryParse(productInput, out int productId))
                            {
                                orderItems.Add((productId, quantity));
                            }
                            else
                            {
                                Console.WriteLine("Invalid product ID!");
                            }
                        }

                        var newOrder = await orderService.CreateOrderAsync(customerId, orderItems);
                        Console.WriteLine($"\nOrder created! Order ID: {newOrder.SalesOrderID}");
                        break;

                    case "4":
                        Console.Clear();
                        var salesReport = await orderService.GetSalesByCategory();
                        Console.WriteLine("Sales Report by Category:");
                        foreach (var sale in salesReport)
                        {
                            Console.WriteLine($"{sale.Key}: {sale.Value:C}");
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
