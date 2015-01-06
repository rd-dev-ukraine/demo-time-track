using System;
using System.Globalization;
using Newtonsoft.Json;

namespace LanceTrack.Domain.Infrastructure
{
    public class DateConverter: JsonConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dt = (DateTime)value;
            writer.WriteValue(dt.ToString(DateTimeFormat));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = Convert.ToString(existingValue);
            return DateTime.ParseExact(value, DateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}