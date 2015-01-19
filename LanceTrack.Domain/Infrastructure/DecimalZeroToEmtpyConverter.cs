using System;
using Newtonsoft.Json;

namespace LanceTrack.Domain.Infrastructure
{
    public class DecimalZeroToEmptyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue(String.Empty);
            }
            else
            {
                var val = (decimal)value;
                writer.WriteValue(val == 0 ? String.Empty : val.ToString("0.##").Replace(",", "."));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof (decimal?) && existingValue == null)
                return null;

            var value = Convert.ToString(existingValue);
            return String.IsNullOrWhiteSpace(value) ? 0M : Math.Round(Decimal.Parse(value.Replace(",", ".")), 2);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) ||
                objectType == typeof(decimal?);
        }
    }
}