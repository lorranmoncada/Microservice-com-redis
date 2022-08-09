using System;

namespace ApiPublicadora
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    public class DebitoConta
    {
        public decimal Valor { get; set; }

        public decimal FormatarValor()
        {
           return  Valor * 2;
        }
    }
}
