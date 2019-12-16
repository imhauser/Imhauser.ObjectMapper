using System;
using System.Collections.Generic;
using System.Linq;


namespace Imhauser.ObjectMapper
{
    /// <summary>
    /// Static class implementing copy methods of the Imhauser.ObjectMapper
    /// </summary>
    public static class Copy
    {
        /// <summary>
        /// Copy all common properties from a source object to a new target object<br/>
        /// Only properties with same name and type are copied
        /// </summary>
        /// <typeparam name="TSource">Type of the source object</typeparam>
        /// <typeparam name="TTarget">Type of the target object</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>The newly creadted target object</returns>
        public static TTarget To<TSource,TTarget>(TSource source) where TSource : class
        {
            if (source == null)
                return default;

            TTarget target = (TTarget)Activator.CreateInstance(typeof(TTarget));
            return To<TSource, TTarget>(source, target);
        }

        /// <summary>
        /// Copy all common properties from a source object to an existing target object.<br/>
        /// Only properties with same name and type are copied
        /// </summary>
        /// <typeparam name="TSource">Type of the source object</typeparam>
        /// <typeparam name="TTarget">Type of the target object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="target">Target object</param>
        /// <returns>The target object</returns>
        public static TTarget To<TSource, TTarget>(TSource source, TTarget target) where TSource : class
        {
            if (source == null)
                return default;

            if (target == null)
                return To<TSource, TTarget>(source);


            var sourceProperties = source.GetType().GetProperties();
            var targetProperties = target.GetType().GetProperties();

            foreach (var parentProperty in sourceProperties)
            {
                foreach (var childProperty in targetProperties)
                {
                    if (parentProperty.Name == childProperty.Name)
                    {
                        if (parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            childProperty.SetValue(target, parentProperty.GetValue(source));
                            break;
                        }
                        if (childProperty.PropertyType.Name == "Nullable`1" && parentProperty.PropertyType == Nullable.GetUnderlyingType(childProperty.PropertyType))
                        {
                            childProperty.SetValue(target, parentProperty.GetValue(source));
                            break;
                        }

                    }

                }
            }
            return target;
        }

        /// <summary>
        /// Copy all common properties from a list of source objects to new list of objects with the target type.<br/>
        /// Only properties with same name and type are copied
        /// </summary>
        /// <typeparam name="TSource">Type of the source object</typeparam>
        /// <typeparam name="TTarget">Type of the target object</typeparam>
        /// <param name="source">IEnumerable list of source objects</param>
        /// <returns>IEnumerable list of target objects</returns>
        public static IEnumerable<TTarget> ToList<TSource, TTarget>(IEnumerable<TSource> source) where TSource : class
        {
            return source.Select(item => ObjectMapper.Copy.To<TSource, TTarget>(item));
        }


    }
}
