using MessagePack;
using Newtonsoft.Json;
using System.Net.Http.Headers;

var product = new Product();
product.Name = "Apple";
product.ExpiryDate = new DateTime(2023, 01, 01);
product.Sizes = new List<string>() { "small", "medium" };
product.Price = 3.99M;

var product2 = new Product();
product2.Name = "Apple";
product2.ExpiryDate = new DateTime(2023, 01, 01);
product2.Sizes = new List<string>() { "small", "medium" };
product2.Price = 4.99M;

var list = new List<Product>()
{ product,product2};

byte[] bytes = MessagePackSerializer.Serialize(list);
Console.WriteLine(bytes.Length);

var result = MessagePackSerializer.Deserialize<List<Product>>(bytes);

foreach (var pr in result)
{
    Console.WriteLine(pr);
}

var json = MessagePackSerializer.SerializeToJson(list);
Console.WriteLine(json);

[MessagePackObject]
public record Product
{
    [Key(0)]
    public string Name { get; set; }
    [Key(1)]
    public DateTime ExpiryDate { get; set; }
    [Key(2)]
    public decimal Price { get; set; }
    [Key(3)]
    public List<string> Sizes { get; set; }
}