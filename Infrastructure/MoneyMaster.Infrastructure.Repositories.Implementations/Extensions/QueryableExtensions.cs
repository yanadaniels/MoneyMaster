using System.Linq.Expressions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Extensions
{
    public static class QueryableExtensions
    {
        // Метод для пагинации
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        // Метод для сортировки
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string? sortBy, bool isDescending )
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda(property, parameter);

            var method = isDescending ? "OrderByDescending" : "OrderBy";
            var genericMethod = typeof(Queryable).GetMethods()
                .First(m => m.Name == method && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return genericMethod.Invoke(null, new object[] { query, lambda }) as IQueryable<T> ?? query;
        }

    }
}
