using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rise.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<PropertyInfo> GetCollections(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var type = obj.GetType();
            var result = new List<PropertyInfo>();

            foreach (var prop in type.GetProperties())
            {
                if (!typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)) continue;
                var get = prop.GetGetMethod();
                if (get.IsStatic || get.GetParameters().Length != 0) continue;
                if (prop.PropertyType == typeof(string)) continue;

                result.Add(prop);
            }
            return result;
        }
    }
}