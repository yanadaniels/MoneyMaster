namespace MoneyMaster.Common.Models
{
    /// <summary>
    /// Модель ответа при запросе отсортированных и разбитых на страницы данных
    /// </summary>
    public class PaginationResponseModel<T>
    {
        /// <summary> Общее количество страниц </summary>
        public int TotalCount { get; set; }
        /// <summary> Номер текущей страницы </summary>
        public int PageNumber { get; set; }
        /// <summary> Колличество элементов на странице </summary>
        public int PageSize { get; set; }
        /// <summary> По полю сортировать </summary>
        public string? SortBy { get; set; }
        /// <summary> Порядок сортировки (по возрастанию или убыванию) </summary>
        public bool IsDescending { get; set; }
        /// <summary> Разбитые и отсортированные данные </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
