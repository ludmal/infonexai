namespace BlazorApp1.Data;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public class Chatinfo
{
    public string Message { get; set; }
    public string User { get; set; }
}

public class SearchInfo
{
    public string SearchText { get; set; }
    
}