// Ignore Spelling: Queryable

using System.Linq.Expressions;

namespace MoneyMaster.Common.Repositories.Extensions
{
    public static class QueryableExtensions
    {
        // Метод для пагинации
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Метод для сортировки коллекции по указанному свойству.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции, которая будет отсортирована.</typeparam>
        /// <param name="query">Коллекция, которую необходимо отсортировать (IQueryable).</param>
        /// <param name="sortBy">Имя свойства, по которому будет осуществляться сортировка.</param>
        /// <param name="isDescending">Флаг, указывающий, должна ли сортировка быть по убыванию (если <c>true</c>, то сортировка по убыванию).</param>
        /// <returns>Отсортированная коллекция (IQueryable).</returns>
        /// <exception cref="ArgumentException">Бросается, если свойство, указанное в <paramref name="sortBy"/>, не существует в типе <typeparamref name="T"/>.</exception>
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string? sortBy, bool isDescending) where T : class
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            // Проверяем, существует ли свойство указанное в sortBy у T
            var propertyInfo = typeof(T).GetProperty(sortBy);
            if (propertyInfo == null)
                throw new ArgumentException($"Свойство '{sortBy}' не существует для данного типа. '{typeof(T).Name}'.");

            // Создаёт параметр выражения, который будет представлять элемент коллекции T (например, User в коллекции IQueryable<User>).
            var parameter = Expression.Parameter(typeof(T), "e");

            // Создаёт выражение, которое указывает, какое свойство объекта T будет использоваться.
            var property = Expression.Property(parameter, propertyInfo.Name);

            // Создаёт лямбда-выражение из свойства и параметра.
            var lambda = Expression.Lambda(property, parameter);

            // Выбираем метод (OrderBy или OrderByDescending)
            var methodName = isDescending ? "OrderByDescending" : "OrderBy";
            var genericMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            // Выполняем сортировку
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda })!;

        }
    }
}
