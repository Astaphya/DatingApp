namespace DatingApp.Domain.Common.Extensions;
using System.Reflection;


public static class TypeExtensions
{
    /// <summary>
    /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
    /// or complex (i.e. custom class with public properties and methods).
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
    public static bool IsSimpleType(
        this Type type)
    {
        return
            type.IsValueType ||
            type.IsPrimitive ||
            new[]
            {
                typeof(String), typeof(Decimal), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan),
                typeof(Guid)
            }.Contains(type) ||
            (Convert.GetTypeCode(type) != TypeCode.Object);
    }

    public static Type GetUnderlyingType(this MemberInfo member)
    {
        switch (member.MemberType)
        {
            case MemberTypes.Event:
                return ((EventInfo)member).EventHandlerType;
            case MemberTypes.Field:
                return ((FieldInfo)member).FieldType;
            case MemberTypes.Method:
                return ((MethodInfo)member).ReturnType;
            case MemberTypes.Property:
                return ((PropertyInfo)member).PropertyType;
            default:
                throw new ArgumentException
                (
                    "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                );
        }
    }
    /// <summary>
    /// Get All Constants values in class type
    /// </summary>
    public static T[] GetAllPublicConstantValues<T>(this Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(fi => fi.FieldType == typeof(T))
            .Select(x => (T?)x.GetValue(x)!)
            .ToArray();


    /// <summary>
    /// Get All Interfaces of a Type [Extension for Built-in method]
    /// </summary>
    /// <param name="type">the type you want to know its interfaces</param>
    /// <param name="includeInherited">want the interfaces implemented in the parent or not</param>
    public static IEnumerable<Type> GetInterfaces(this Type type, bool includeInherited) =>
        includeInherited || type.BaseType == null ?
            type.GetInterfaces() :
            type.GetInterfaces().Except(type.BaseType.GetInterfaces());
}