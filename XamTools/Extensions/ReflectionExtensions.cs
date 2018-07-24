using System;
using System.Linq.Expressions;
using System.Reflection;

namespace XamTools.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Given a lambda expression that contains a single reference to a public property, retrieves the property's setter accessor.
        /// </summary>
        /// <typeparam name="TProperty">Data type of property</typeparam>
        /// <param name="propertyExpression">A lambda expression in the form of <code>() => PropertyName</code></param>
        /// <returns></returns>
        public static Action<object, TProperty> ExtractPropertySetter<TProperty>(this Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");

            var setMethod = property.SetMethod;

            if (setMethod == null)
                throw new ArgumentException("The referenced property does not have a set method.", "propertyExpression");

            if (setMethod.IsStatic)
                throw new ArgumentException("The referenced property is a static property.", "propertyExpression");

            Action<object, TProperty> action = (obj, val) => setMethod.Invoke(obj, new object[] { val });
            return action;
        }

        /// <summary>
        /// Extracts the property name from the property expression.
        /// 
        /// Implementation borrowed from Jounce MVVM framework.
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyExpression">A lambda expression in the form of <code>() => PropertyName</code></param>
        /// <param name="failSilently">When 'true' causes this method to return null results instead of throwing 
        /// an exception whenever a problem occurs while probing the type information.</param>
        /// <returns>The property name</returns>
        public static string ExtractPropertyName<TObject, TProperty>(this Expression<Func<TObject, TProperty>> propertyExpression, bool failSilently = false)
        {
            if (propertyExpression == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
            }

            var getMethod = property.GetMethod;

            if (getMethod == null)
            {
                // this shouldn't happen - the expression would reject the property before reaching this far
                if (failSilently)
                    return null;
                throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
            }

            if (getMethod.IsStatic)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Extracts the property name from the property expression.
        /// 
        /// Implementation borrowed from Jounce MVVM framework.
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyExpression">A lambda expression in the form of <code>() => PropertyName</code></param>
        /// <param name="failSilently">When 'true' causes this method to return null results instead of throwing 
        /// an exception whenever a problem occurs while probing the type information.</param>
        /// <returns>The property name</returns>
        public static string ExtractPropertyName<T>(this Expression<Func<T>> propertyExpression, bool failSilently = false)
        {
            if (propertyExpression == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
            }

            var getMethod = property.GetMethod;

            if (getMethod == null)
            {
                // this shouldn't happen - the expression would reject the property before reaching this far
                if (failSilently)
                    return null;
                throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
            }

            if (getMethod.IsStatic)
            {
                if (failSilently)
                    return null;
                throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Inspects a type to see if it defines a property with the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="obj">An instance of an object being inspected, or a variable of the corresponding class type (can be null).</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>Returns 'true' if the property is found.</returns>
        public static bool HasProperty<T>(this T obj, string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                return false;

            var type = Equals(obj, default(T)) ? typeof(T) : obj.GetType();

            // Verify that the property name matches a realinstance property on this object.
            return type.GetRuntimeProperty(propertyName) != null;
        }
          public static IEnumerable<PropertyInfo> GetProperties(this Type type) => type.GetRuntimeProperties();

        public static bool IsAssignableFrom(this Type type, Type typeInfo) => type.GetTypeInfo().IsAssignableFrom(typeInfo.GetTypeInfo());

        public static Type BaseType(this Type type) => type.GetTypeInfo().BaseType;

        public static bool IsPrimitive(this Type type) => type.GetTypeInfo().IsPrimitive;

        public static bool IsGenericType(this Type type) => type.GetTypeInfo().IsGenericType;

        public static bool IsInterface(this Type type) => type.GetTypeInfo().IsInterface;

        public static bool IsAbstract(this Type type) => type.GetTypeInfo().IsAbstract;

        public static bool IsValueType(this Type type) => type.GetTypeInfo().IsValueType;

        public static bool IsEnum(this Type type) => type.GetTypeInfo().IsEnum;

        public static bool IsGenericTypeDefinition(this Type type) => type.GetTypeInfo().IsGenericTypeDefinition;

        public static string GetCulture(this CultureInfo info) => info.ToString();

        public static IEnumerable<MethodInfo> GetMethods(this Type type) => type.GetTypeInfo().DeclaredMethods;

        public static IEnumerable<MemberInfo> GetMember(this Type type, string name) => type.GetTypeInfo().DeclaredMembers;

        public static MethodInfo GetMethod(this Type type, string name, Type[] types)
        {
            var results = from m in type.GetTypeInfo().DeclaredMethods
                          where m.Name == name
                          let methodParameters = m.GetParameters().Select(_ => _.ParameterType).ToArray()
                          where (methodParameters.Length == types.Length) && !methodParameters.Except(types).Any()
                              && !types.Except(methodParameters).Any()
                          select m;

            return results.FirstOrDefault();
        }

        public static PropertyInfo GetProperty(this Type type, string name) => type.GetRuntimeProperty(name);

        public static Type[] GetGenericArguments(this Type type) => type.GetTypeInfo().GenericTypeArguments;

        public static bool GetIsDynamicBound(this IEnumerable collection)
        {
            var enumerator = collection.GetEnumerator();

            if (!enumerator.MoveNext())
                return false;

            var record = enumerator.Current;

            if (record != null)
            {
                var isDynamicBound = DynamicHelper.CheckIsDynamicObject(record.GetType());
                return isDynamicBound;
            }

            return false;
        }
    
    }

}