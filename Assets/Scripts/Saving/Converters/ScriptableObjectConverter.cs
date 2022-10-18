using Newtonsoft.Json;
using System;
using UnityEngine;

public class ScriptableObjectConverter : JsonConverter<ScriptableObject>
{
    public override ScriptableObject ReadJson(JsonReader reader, Type objectType, ScriptableObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        DataLoaderAttribute attribute = (DataLoaderAttribute)Attribute.GetCustomAttribute(objectType, typeof(DataLoaderAttribute));
        return attribute.GetObject((string)reader.Value);
    }

    public override void WriteJson(JsonWriter writer, ScriptableObject value, JsonSerializer serializer)
    {
        writer.WriteValue(value.name);
    }
}