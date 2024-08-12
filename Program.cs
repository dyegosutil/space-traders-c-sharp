using System;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;

class Program
{
    public class WeatherResponse
    {
        public Main2 Main { get; set; }
        public string Name { get; set; }
    }

    public class ErrorResponse
{
    public int Cod { get; set; }
    public string Message { get; set; }
}

    public class Main2
    {
        public float Temp { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
    }

    static async Task Main(string[] args)
    {
        var client = new RestClient("http://api.openweathermap.org/data/2.5/");
        var request = new RestRequest("weather", Method.Get);
        request.AddParameter("q", "London");
        request.AddParameter("appid", "your_api_key");
        request.AddParameter("units", "metric"); // Optional: get temperature in Celsius
        
        var response = await client.ExecuteAsync<WeatherResponse>(request);

        if (response.IsSuccessful)
        {
            var weather = response.Data;
            Console.WriteLine($"City: {weather.Name}");
            Console.WriteLine($"Temperature: {weather.Main.Temp}°C");
            Console.WriteLine($"Pressure: {weather.Main.Pressure} hPa");
            Console.WriteLine($"Humidity: {weather.Main.Humidity}%");
        }
        else
        {
            string pira = response.ErrorMessage;
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response.Content);
                Console.WriteLine($"Error: {errorResponse.Message} (Code: {errorResponse.Cod})");
        }
    }
}
