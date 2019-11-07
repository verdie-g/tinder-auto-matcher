using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tinder.Models.Converters
{
    /// <summary>
    /// Convert a json value to null if false or to T if the value is an object.
    /// </summary>
    class JsonFalseOrObjectConverter<T> : JsonConverter<T?> where T : class
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.False)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<T>(ref reader);
            }
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteBooleanValue(false);
            }
            else
            {
                JsonSerializer.Serialize<T>(writer, value);
            }
        }
    }
}