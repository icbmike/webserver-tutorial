namespace WebServerTutorial;

public class WeatherService
{
    public string GetForecast()
    {
        var dateIsEven = DateTime.Now.Date.Day % 2 == 0;

        return dateIsEven ? "Sunny" : "Overcast";
    }
}