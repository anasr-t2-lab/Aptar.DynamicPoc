using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aptar.DynamicPoc.Data.Extensions
{
    public static class PropertyBuilderExtensions
    {
        private static JsonSerializerSettings _privateSetterAndCtorSerializerSettings = new JsonSerializerSettings()
        {
            //ContractResolver = new DefaultContractResolver()
            //{
            //    NamingStrategy = new CamelCaseNamingStrategy()
            //},
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None
        };

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver()
        };
        private static JsonSerializer JsonSerializer =>
            JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.None
            });

        public static PropertyBuilder<T> HasTypeHandlingJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class
        {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                value => JsonConvert.SerializeObject(value, _privateSetterAndCtorSerializerSettings),
                value => JsonConvert.DeserializeObject<T>(value, _privateSetterAndCtorSerializerSettings)
            );

            ValueComparer<T> comparer = new ValueComparer<T>
            (
                (left, right) => JsonConvert.SerializeObject(left, _privateSetterAndCtorSerializerSettings) == JsonConvert.SerializeObject(right, _privateSetterAndCtorSerializerSettings),
                value => value == null ? 0 : JsonConvert.SerializeObject(value, _privateSetterAndCtorSerializerSettings).GetHashCode(),
                value => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value, _privateSetterAndCtorSerializerSettings), _privateSetterAndCtorSerializerSettings)
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }

        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                value => Serialize(value),
                value => Deserialize<T>(value) ?? new T()
            );

            ValueComparer<T> comparer = new ValueComparer<T>
            (
                (left, right) => Serialize(left) == Serialize(right),
                value => value == null ? 0 : Serialize(value).GetHashCode(),
                value => Deserialize<T>(Serialize(value))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }

        private static string Serialize(object obj)
        {
            var jObject = JObject.FromObject(obj, JsonSerializer);
            jObject.Add("payloadType", obj.GetType().FullName);
            return jObject.ToString(Formatting.None);
        }

        private static T Deserialize<T>(string json)
            where T : class
        {
            var jObject = JObject.Parse(json);
            var payloadType = GetType(jObject["payloadType"].Value<string>());

            return JsonConvert.DeserializeObject(json, payloadType) as T;
        }

        private static Type GetType(string typeFullName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.FullName.StartsWith("Neurocrine."))
                    continue;

                var type = assembly.GetType(typeFullName);
                if (type != null)
                    return type;
            }

            return null;
        }

        public static PropertyBuilder<List<T>> HasListJsonConversion<T>(this PropertyBuilder<List<T>> propertyBuilder)
        {
            ValueConverter<List<T>, string> converter = new ValueConverter<List<T>, string>
            (
                value => SerializeArray(value),
                value => DeserializeArray<T>(value)
            );

            ValueComparer<List<T>> comparer = new ValueComparer<List<T>>
            (
                (left, right) => SerializeArray(left) == SerializeArray(right),
                value => value == null ? 0 : SerializeArray(value).GetHashCode(),
                value => DeserializeArray<T>(SerializeArray(value))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }

        private static string SerializeArray<T>(T objects) where T : IEnumerable
        {
            var jArray = JArray.FromObject(objects, JsonSerializer);
            return jArray.ToString(Formatting.None);
        }

        private static List<T> DeserializeArray<T>(string json)
        {
            return JsonConvert.DeserializeObject(json, typeof(List<T>), _serializerSettings) as List<T>;
        }

        public static PropertyBuilder<Guid> HasGuidConversion(this PropertyBuilder<Guid> propertyBuilder)
        {
            ValueConverter<Guid, string> converter = new ValueConverter<Guid, string>
            (
                to => to.ToString("N"),
                from => Guid.Parse(from)
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            return propertyBuilder;
        }
    }
}
