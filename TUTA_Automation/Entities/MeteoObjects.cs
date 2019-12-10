using System.Collections.Generic;

namespace TUTA_Automation
{
    class MeteoObject
    {
        internal object result; 
        public ForecastTimestamp forecastTimestamp { get; set; }

        public Error error { get; set; }
        public Place place { get; set; }

        public class Error
        {
            public int code { get; set; }
            public string message { get; set; }
        }


        public class Coordinates
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Place
        {
            public string code { get; set; }
            public string name { get; set; }
            public string administrativeDivision { get; set; }
            public string country { get; set; }
            public string countryCode { get; set; }
            public Coordinates coordinates { get; set; }
        }

        public class ForecastTimestamp
        {
            public string forecastTimeUtc { get; set; }
            public double airTemperature { get; set; }
            public int windSpeed { get; set; }
            public int windGust { get; set; }
            public int windDirection { get; set; }
            public int cloudCover { get; set; }
            public int seaLevelPressure { get; set; }
            public double totalPrecipitation { get; set; }
            public string conditionCode { get; set; }
        }

        public class RootObject
        {
            public Place place { get; set; }
            public string forecastType { get; set; }
            public string forecastCreationTimeUtc { get; set; }
            public List<ForecastTimestamp> forecastTimestamps { get; set; }
        }

    }
}