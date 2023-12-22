using DatingApp.Domain.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using MethodInfo = System.Reflection.MethodInfo;

namespace DatingApp.Infrastructure.Persistence.Configurations.InterfaceConfigurations;

public interface IDbPropertyConfiguration
{
    void Build<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class;
}

public interface IDbPropertyConfiguration<TConfigInterface> : IDbPropertyConfiguration
{

}


public class DbPropertyExtensions : IDisposable
{
    private readonly Dictionary<Type, (IDbPropertyConfiguration property, MethodInfo method)> _objects = new();


    // public void AddInterfaceConfiguration<T, TControl>()
    //     where TControl : IDbPropertyConfiguration
    // {
    //     var obj = Activator.CreateInstance(typeof(TControl));
    //     if (obj is TControl instance)
    //     {
    //         var method = obj.GetType().GetMethod(nameof(IDbPropertyConfiguration.Build));
    //         if (method != null)
    //             _objects.Add(typeof(T), (instance, method));
    //     }
    //     else
    //         throw new ArgumentNullException(nameof(obj),
    //             $"Cannot create instance from type [{typeof(TControl).FullName}]");
    // }

    public void AddInterfaceConfiguration(Type interfaceType, Type controlType)
    {
        var obj = Activator.CreateInstance(controlType);
        if (obj is IDbPropertyConfiguration instance)
        {
            var method = obj.GetType().GetMethod(nameof(IDbPropertyConfiguration.Build));
            if (method != null)
                _objects.Add(interfaceType, (instance, method));
        }
        else
            throw new ArgumentNullException(nameof(obj),
                $"Cannot create instance from type [{controlType.FullName}]");
    }

    public void Dispose() => _objects.Clear();

    public void ApplyConfiguration(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            BuildControlConfiguration(entityType.ClrType, modelBuilder);
    }

    private void BuildControlConfiguration(Type entity, ModelBuilder modelBuilder)
    {

        foreach (var type in entity.GetInterfaces(false))
            if (_objects.TryGetValue(type, out var value))
                value.method.MakeGenericMethod(entity)
                    .Invoke(value.property, new object[] { modelBuilder });
    }


}