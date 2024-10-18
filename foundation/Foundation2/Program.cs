using System;
using System.Collections.Generic;

// Product Class
public class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    // Constructor
    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    // Get the total cost of the product
    public double GetTotalCost()
    {
        return price * quantity;
    }

    // Getters for product details
    public string GetName() => name;
    public string GetProductId() => productId;
}

// Address Class
public class Address
{
    private string street;
    private string city;
    private string stateOrProvince;
    private string country;

    // Constructor
    public Address(string street, string city, string stateOrProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateOrProvince = stateOrProvince;
        this.country = country;
    }

    // Check if the address is in the USA
    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    // Get the address as a formatted string
    public string GetAddressString()
    {
        return $"{street}\n{city}, {stateOrProvince}\n{country}";
    }
}

// Customer Class
public class Customer
{
    private string name;
    private Address address;

    // Constructor
    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    // Check if the customer lives in the USA
    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    // Getters for customer details
    public string GetName() => name;
    public Address GetAddress() => address;
}

// Order Class
public class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    // Constructor
    public Order(Customer customer)
    {
        this.customer = customer;
    }

    // Add a product to the order
    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    // Calculate the total price (including shipping cost)
    public double CalculateTotalPrice()
    {
        double total = 0;
        foreach (Product product in products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost based on customer location
        total += customer.IsInUSA() ? 5 : 35;
        return total;
    }

    // Get the packing label
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    // Get the shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetAddressString()}";
    }
}

// Main Program
public class Program
{
    public static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Elm St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Oak Ave", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create products
        Product product1 = new Product("Laptop", "P001", 1200.50, 1);
        Product product2 = new Product("Mouse", "P002", 25.99, 2);
        Product product3 = new Product("Keyboard", "P003", 75.50, 1);

        // Create orders and add products
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);

        // Display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.CalculateTotalPrice():0.00}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.CalculateTotalPrice():0.00}\n");
    }
}
