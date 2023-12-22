// using System.Linq.Expressions;
// using DatingApp.Domain.Common.Database;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using System.Reflection;
//
// namespace DatingApp.Infrastructure.Persistence.Configurations.InterfaceConfigurations.EntityTrackers;
//
//
//
// public class TrackDeleteConfig : IDbPropertyConfiguration<ITrackDelete>
// {
//     public const string IsDeletedPropertyName = "IsDeleted";
//     public const string DeletedDateTimePropertyName = "DeleteDateTime";
//     public const string DeletedByPropertyName = "DeletedBy";
//
//     public void Build<TEntity>(ModelBuilder modelBuilder)
//         where TEntity : class
//     {
//         modelBuilder.Entity<TEntity>()
//             .Property<DateTime?>(DeletedDateTimePropertyName)
//             .IsRequired(false);
//
//         modelBuilder.Entity<TEntity>()
//             .Property<string?>(DeletedByPropertyName)
//             .IsRequired(false)
//             .IsUnicode(false)
//             .HasMaxLength(50);
//
//         modelBuilder.Entity<TEntity>()
//             .Property<bool>(IsDeletedPropertyName)
//             .HasDefaultValue(false)
//             .IsRequired();
//
//         modelBuilder.Entity<TEntity>()
//             .HasIndex(IsDeletedPropertyName)
//             .HasDatabaseName(IsDeletedPropertyName + "Index_" + modelBuilder.Entity<TEntity>().Metadata.ClrType.Name);
//     }
//
//     public static void UpdateEntries(EntityEntry e, string trackerName)
//     {
//         if (e.Entity is ITrackDelete && e.Metadata.FindProperty(DeletedDateTimePropertyName) != null)
//         {
//             e.Property(DeletedDateTimePropertyName).CurrentValue = DateTime.Now;
//             e.Property(DeletedByPropertyName).CurrentValue = trackerName;
//             e.Property(IsDeletedPropertyName).CurrentValue = true;
//             e.State = EntityState.Unchanged;
//             foreach (var entity in e.References.Where(r =>
//                          r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() &&
//                          r.TargetEntry.State == EntityState.Deleted))
//                 if (entity.TargetEntry != null)
//                     entity.TargetEntry.State = EntityState.Modified;
//         }
//         //else if (e.Entity is IDbModel && (whiteList == null || !whiteList(e.Entity)) && e.Entity is not IDeletable)
//         //    throw new DataException(
//         //        $"The entity [{e.Entity.GetType().FullName}]  delete Interfaces");
//     }
//
//     /// <summary>
//     /// IsDeleted Query Filter
//     /// </summary>
//     public static void BuildQueryFilter(ModelBuilder modelBuilder, Type[]? exceptions = null)
//     {
//         var efPropertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Public | BindingFlags.Static)?.MakeGenericMethod(typeof(bool));
//         if (efPropertyMethod == null)
//             throw new EntryPointNotFoundException("Method 'Ef.Property<bool>(string)' not found");
//
//         //Global FilterQuery(Filter Deleted Records)
//         var registeredTypes = modelBuilder.Model.GetEntityTypes()
//             .Where(e => e.ClrType.GetInterfaces(false)
//                 .Any(type => type == typeof(ITrackDelete))).ToArray();
//         if (!exceptions.IsNullOrEmpty())
//             registeredTypes = registeredTypes.Where(x => !exceptions.Contains(x.ClrType)).ToArray();
//         foreach (var type in registeredTypes)
//             modelBuilder.Entity(type.ClrType)
//                 .HasQueryFilter(BuildIsDeletedExpression(type.ClrType));
//
//         LambdaExpression BuildIsDeletedExpression(Type entityType)
//         {
//             var efEntity = Expression.Parameter(entityType, "e");
//             var body = Expression.Not(Expression.Call(efPropertyMethod, efEntity, Expression.Constant(IsDeletedPropertyName)));
//             return Expression.Lambda(body, efEntity);
//         }
//     }
//
// }