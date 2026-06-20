namespace LegacyCode;

using System.Net.Http;
using System.Text.Json;

public class Order
{
    private readonly HttpClient _httpClient;

    public Order(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public OrderData? GetOrder(int orderId)
    {
        var url = $"https://codemanship.co.uk/api/orders.php?orderId={orderId}";

        var response = _httpClient
            .GetAsync(url)
            .GetAwaiter()
            .GetResult();

        response.EnsureSuccessStatusCode();

        var json = response.Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var order = JsonSerializer.Deserialize<OrderData>(json, options);
        return order;
    }
}
