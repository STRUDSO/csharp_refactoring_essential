namespace Duplication;

using System;
using System.Collections.Generic;
using System.Globalization;

public class WeatherReport
{
    public void FormatDailyReport(List<Forecast> forecasts, List<string> output)
    {
        foreach (Forecast forecast in forecasts)
        {
            string temperature = forecast.GetTemperature().ToString("0.0", CultureInfo.InvariantCulture);

            if (forecast.IsMorning())
            {
                string line = "Morning: " + temperature + "°C, "
                              + forecast.GetCondition() + ", wind " + forecast.GetWindSpeed() + "km/h";
                output.Add(line);
            }

            if (forecast.IsAfternoon())
            {
                string line = "Afternoon: " + temperature + "°C, "
                              + forecast.GetCondition() + ", wind " + forecast.GetWindSpeed() + "km/h";
                output.Add(line);
            }

            if (forecast.IsEvening())
            {
                string line = "Evening: " + temperature + "°C, "
                              + forecast.GetCondition() + ", wind " + forecast.GetWindSpeed() + "km/h";
                output.Add(line);
            }

            if (forecast.IsNight())
            {
                string line = "Night: " + temperature + "°C, "
                              + forecast.GetCondition() + ", wind " + forecast.GetWindSpeed() + "km/h";
                output.Add(line);
            }
        }
    }
}

public class Forecast
{
    private readonly string period; // "morning", "afternoon", "evening", "night"
    private readonly double temperature;
    private readonly string condition;
    private readonly int windSpeed;

    public Forecast(string period, double temperature, string condition, int windSpeed)
    {
        this.period = period;
        this.temperature = temperature;
        this.condition = condition;
        this.windSpeed = windSpeed;
    }

    public double GetTemperature()
    {
        return temperature;
    }

    public string GetCondition()
    {
        return condition;
    }

    public int GetWindSpeed()
    {
        return windSpeed;
    }

    public bool IsMorning()
    {
        return period == "morning";
    }

    public bool IsAfternoon()
    {
        return period == "afternoon";
    }

    public bool IsEvening()
    {
        return period == "evening";
    }

    public bool IsNight()
    {
        return period == "night";
    }
}
