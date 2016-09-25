using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MSniperService.Models
{
    //public class EncounterIdConverter : JsonConverter
    //{
    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        JToken jt = JToken.ReadFrom(reader);
    //        return jt.ToString();
    //    }

    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(ulong) == objectType;
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        serializer.Serialize(writer, value.ToString());
    //    }
    //}
}