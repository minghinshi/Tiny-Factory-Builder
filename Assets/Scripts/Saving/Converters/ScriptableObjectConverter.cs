using Newtonsoft.Json;
using System;
using UnityEngine;

public class ScriptableObjectConverter : JsonConverter<ScriptableObject>
{
    public override ScriptableObject ReadJson(JsonReader reader, Type objectType, ScriptableObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, ScriptableObject value, JsonSerializer serializer)
    {
        writer.WriteValue(value.name);
    }
}