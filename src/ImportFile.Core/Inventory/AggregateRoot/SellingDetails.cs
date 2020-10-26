namespace ImportFile.Core.Inventory.AggregateRoot
{
    public class SellingDetails
    {
        public SellingDetails(decimal price, decimal discount)
        {
            Price = price;
            Discount = discount;
        }

        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }
    }
}