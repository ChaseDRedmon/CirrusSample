using System.Text.Json.Serialization;

namespace CirrusTest;

public class TestModel
{
    // These fields below are fields that I have personally retrieved from my Ambient Weather Station (WS-2902A/B)

        /// <summary>
        /// Epoch (Unix) time from 1/1/1970 (measured in milliseconds according to ambient weather docs).
        /// </summary>
        [JsonPropertyName("dateutc")]
        public long? EpochMilliseconds { get; init; }

        /// <summary>
        /// Indoor Temperature in Fahrenheit reported by the Base Station.
        /// </summary>
        [JsonPropertyName("tempinf")]
        public double? IndoorTemperatureFahrenheit { get; init; }

        /// <summary>
        /// Indoor Humidity reported by the Base Station.
        /// </summary>
        [JsonPropertyName("humidityin")]
        public int? IndoorHumidity { get; init; }

        /// <summary>
        /// Relative Barometric Pressure in inches of mercury (in-HG) reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("baromrelin")]
        public double? RelativeBarometricPressure { get; init; }

        /// <summary>
        /// Absolute Barometric Pressure in inches of mercury (in-HG) reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("baromabsin")]
        public double? AbsoluteBarometricPressure { get; init; } 

        /// <summary>
        /// Outdoor Temperature in Fahrenheit reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("tempf")]
        public double? OutdoorTemperatureFahrenheit { get; init; }

        /// <summary>
        /// A battery indicator reported by the Outdoor Sensor Array.
        /// A value of 1 represents an 'OK' battery level.
        /// A value of 0 represents a 'low' battery level.
        ///
        /// For Meteobridge Users: the above value are flipped. See below.
        /// A value of 0 represents an 'OK' battery level.
        /// A value of 1 represents a 'low' battery level.
        /// </summary>
        [JsonPropertyName("battout")]
        public int? BatteryLowIndicator { get; init; }

        /// <summary>
        /// The outdoor humidity reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("humidity")]
        public int? OutdoorHumidity { get; init; }

        /// <summary>
        /// Wind Direction reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("winddir")]
        public int? WindDirection { get; init; }

        /// <summary>
        /// Wind Speed in Miles Per Hour reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("windspeedmph")]
        public double? WindSpeedMph { get; init; }

        /// <summary>
        /// Wind Gust in Miles Per Hour reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("windgustmph")]
        public double? WindGustMph { get; init; }

        /// <summary>
        /// The maximum windspeed from a wind gust for that day.
        /// </summary>
        [JsonPropertyName("maxdailygust")]
        public double? MaxDailyGust { get; init; }

        /// <summary>
        /// Hourly Rainfall in Inches.
        /// </summary>
        [JsonPropertyName("hourlyrainin")]
        public double? HourlyRainfall { get; init; }

        /// <summary>
        /// Current event's rainfall in inches.
        /// </summary>
        [JsonPropertyName("eventrainin")]
        public double? EventRainfall { get; init; }

        /// <summary>
        /// Daily Rainfall in Inches.
        /// </summary>
        [JsonPropertyName("dailyrainin")]
        public double? DailyRainfall { get; init; }

        /// <summary>
        /// Weekly rainfall in inches.
        /// </summary>
        [JsonPropertyName("weeklyrainin")]
        public double? WeeklyRainfall { get; init; }

        /// <summary>
        /// Monthly rainfall in inches.
        /// </summary>
        [JsonPropertyName("monthlyrainin")]
        public double? MonthlyRainfall { get; init; }

        /// <summary>
        /// Yearly rainfall in inches.
        /// </summary>
        [JsonPropertyName("yearlyrainin")]
        public double? YearlyRainfall { get; init; }

        /// <summary>
        /// Total rainfall recorded by sensor in inches.
        /// </summary>
        [JsonPropertyName("totalrainin")]
        public double? TotalRainfall { get; init; }

        /// <summary>
        /// Solar Radiation measured in Watts Per Meter^2 (W/m^2) reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("solarradiation")]
        public double? SolarRadiation { get; init; }

        /// <summary>
        /// Ultra-violet radiation index reported by the Outdoor Sensor Array.
        /// </summary>
        [JsonPropertyName("uv")]
        public int? UltravioletRadiationIndex { get; init; }

        /// <summary>
        /// Feels Like Temperature.
        /// If temperature is less than 50ºF => Wind Chill;
        /// If temperature is greater than 68ºF => Heat Index (calculated on server).
        /// </summary>
        [JsonPropertyName("feelsLike")]
        public double? OutdoorFeelsLikeTemperatureFahrenheit { get; init; }

        /// <summary>
        /// Dew Point Temperature in Fahrenheit.
        /// </summary>
        [JsonPropertyName("dewPoint")]
        public double? DewPointFahrenheit { get; init; }

        /// <summary>
        /// Indoor Feels Like Temperature in Fahrenheit reported by the Base Station.
        /// </summary>
        [JsonPropertyName("feelsLikein")]
        public double? IndoorFeelsLikeTemperatureFahrenheit { get; init; }

        /// <summary>
        /// Indoor Dew Point Temperature in Fahrenheit reported by the Base Station.
        /// </summary>
        [JsonPropertyName("dewPointin")]
        public double? IndoorDewPointTemperatureFahrenheit { get; init; }

        /// <summary>
        /// Last DateTime recorded where <see cref="HourlyRainfall"/> was > 0 inches.
        /// </summary>
        [JsonPropertyName("lastRain")]
        public DateTimeOffset LastRain { get; init; }

        /// <summary>
        /// Unknown value? Probably something to do with Ambient Weathers Databases/Servers?
        /// </summary>`
        [JsonPropertyName("loc")]
        public string? Loc { get; init; }

        /// <summary>
        /// DateTime version of <see cref="EpochMilliseconds"/>.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTimeOffset? UtcDate { get; init; }
}