namespace DotNetRuleEngine.Test.Models
{
    public class Product
    {
        public Product()
        {
            Name = "Desktop Computer";
            Price = 999.99m;
            Description = "";
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
