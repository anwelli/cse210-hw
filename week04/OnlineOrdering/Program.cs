
using System;
using System.Collections.Generic;

public class Address
{
public string StreetAddress { get; set; }
public string City { get; set; }
public string StateProvince { get; set; }
public string Country { get; set; }

public Address(string streetAddress, string city, string stateProvince, string country)
{
    StreetAddress = streetAddress;
    City = city;
    StateProvince = stateProvince;
    Country = country;
}

public bool IsUSA()
{
    return Country.ToLower() == "usa";
}

public string GetFullAddress()
{
    return $"{StreetAddress}\n{City}, {StateProvince}\n{Country}";
}

}

public class Customer
{
public string Name { get; set; }
public Address Address { get; set; }

public Customer(string name, Address address)
{
    Name = name;
    Address = address;
}

public bool IsUSA()
{
    return Address.IsUSA();
}

public string GetCustomerInfo()
{
    return $"{Name}\n{Address.GetFullAddress()}";
}

}

public class Product
{
public string Name { get; set; }
public string ProductId { get; set; }
public decimal Price { get; set; }
public int Quantity { get; set; }

public Product(string name, string productId, decimal price, int quantity)
{
    Name = name;
    ProductId = productId;
    Price = price;
    Quantity = quantity;
}

public decimal GetTotalCost()
{
    return Price * Quantity;
}

public string GetProductInfo()
{
    return $"{Name} ({ProductId})";
}

}

public class Order
{
public Customer Customer { get; set; }
public List<Product> Products { get; set; }
private const decimal USA_SHIPPING_COST = 5.00m;
private const decimal INTERNATIONAL_SHIPPING_COST = 35.00m;

public Order(Customer customer)
{
    Customer = customer;
    Products = new List<Product>();
}

public void AddProduct(Product product)
{
    Products.Add(product);
}

public string GetPackingLabel()
{
    string packingLabel = "Packing Label:\n";
    foreach (var product in Products)
    {
        packingLabel += $"- {product.GetProductInfo()}\n";
    }
    return packingLabel;
}

public string GetShippingLabel()
{
    return $"Shipping Label:\n{Customer.GetCustomerInfo()}";
}

public decimal GetTotalCost()
{
    decimal totalCost = 0;
    foreach (var product in Products)
    {
        totalCost += product.GetTotalCost();
    }
    if (Customer.IsUSA())
    {
        totalCost += USA_SHIPPING_COST;
    }
    else
    {
        totalCost += INTERNATIONAL_SHIPPING_COST;
    }
    return totalCost;
}

}

class Program
{
static void Main(string[] args)
{
// Create customers
var customer1 = new Customer("John Doe", new Address("123 Main St", "Anytown", "CA", "USA"));
var customer2 = new Customer("Jane Smith", new Address("456 Elm St", "Othertown", "NY", "USA"));
var customer3 = new Customer("Bob Johnson", new Address("789 Oak St", "Thirdtown", "ON", "Canada"));

    // Create products
    var product1 = new Product("Product 1", "P001", 19.99m, 2);
    var product2 = new Product("Product 2", "P002", 9.99m, 3);
    var product3 = new Product("Product 3", "P003", 29.99m, 1);

    // Create orders
    var order1 = new Order(customer1);
    order1.AddProduct(product1);
    order1.AddProduct(product2);

    var order2 = new Order(customer2);
    order2.AddProduct(product2);
    order2.AddProduct(product3);

    var order3 = new Order(customer3);
    order3.AddProduct(product1);
    order3.AddProduct(product3);

    // Display order information
    Console.WriteLine(order1.GetPackingLabel());
    Console.WriteLine(order1.GetShippingLabel());
    Console.WriteLine($"Total Cost: {order1.GetTotalCost():C}\n");

    Console.WriteLine(order2.GetPackingLabel());
    Console.WriteLine(order2.GetShippingLabel());
    Console.WriteLine($"Total Cost: {order2.GetTotalCost():C}\n");

    Console.WriteLine(order3.GetPackingLabel());
    Console.WriteLine(order3.GetShippingLabel());
    Console.WriteLine($"Total Cost: {order3.GetTotalCost():C}\n");
}

}