using System;
using System.Collections.Generic;
using System.Text;

namespace Imhauser.ObjectMapper
{
    public class Copy
    {
        public static TTarget To<TSource,TTarget>(TSource source) where TSource : class
        {
            TTarget target = (TTarget)Activator.CreateInstance(typeof(TTarget));

            var sourceProperties = source.GetType().GetProperties();
            var targetProperties = target.GetType().GetProperties();


            foreach (var parentProperty in sourceProperties)
            {
                foreach (var childProperty in targetProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(target, parentProperty.GetValue(source));
                        break;
                    }
                }
            }

            return target;
        }
    }
}
