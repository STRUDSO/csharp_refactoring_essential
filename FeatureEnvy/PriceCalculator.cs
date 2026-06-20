namespace FeatureEnvy;

public class PriceCalculator
{
    public double CalculateFinalPrice(Product product)
    {
        double price = product.GetPrice();

        if (product.IsOnSale())
        {
            price *= 0.8;
        }

        return price;
    }
}