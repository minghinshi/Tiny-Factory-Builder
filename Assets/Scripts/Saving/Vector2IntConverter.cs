using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class Vector2IntConverter : JsonConverter<Vector2Int>
{
    public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        return new Vector2Int((int)jsonObject["x"], (int)jsonObject["y"]);
    }

    public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
    {
        JObject jsonObject = new()
        {
            { "x", value.x },
            { "y", value.y }
        };
        jsonObject.WriteTo(writer);
    }
}
