using System.Text.Json;
using System.Text.Json.Serialization;

namespace Educationalcenter.Converters
{
    public class NullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
    {
        public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out DateTime dateTime))
                return TimeOnly.FromDateTime(dateTime);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(((TimeOnly)value).ToString("O"));
        }
    }
}
