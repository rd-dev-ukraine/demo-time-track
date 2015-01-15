using System;
using System.Globalization;
using System.Security;
using Newtonsoft.Json;

namespace LanceTrack.Domain.Infrastructure
{
    public class DecimalZeroToEmptyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = (decimal)value;
            writer.WriteValue(val == 0 ? String.Empty : val.ToString("0.##"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = Convert.ToString(existingValue);
            return String.IsNullOrWhiteSpace(value) ? 0M : Decimal.Parse(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }
    }
}