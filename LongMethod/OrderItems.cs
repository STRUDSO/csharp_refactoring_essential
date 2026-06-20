namespace LongMethod;

public class OrderItems
{
    public OrderItems(List<OrderItem> items)
    {
        Items = items;
    }

    public List<OrderItem> Items { get; }

    public void ValidateItems()
    {
        if (Items == null)
        {
            throw new InvalidOperationException("Items cannot be null");
        }

        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain items");
        }
    }

    public double Subtotal()
    {
        return Items.Sum(item => item.Price * item.Quantity);
    }
}