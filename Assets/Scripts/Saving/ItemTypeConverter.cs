using Newtonsoft.Json;
using System;

public class ItemTypeConverter : JsonConverter<ItemType>
{
    public override ItemType ReadJson(JsonReader reader, Type objectType, ItemType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return DataLoader.itemTypeLoader.GetItemType((string)reader.Value);
    }

    public override void WriteJson(JsonWriter writer, ItemType value, JsonSerializer serializer)
    {
        writer.WriteValue(value.name);
    }
}
