namespace MoneyMaster.Common
{
    public class PaginationParameters
    {
        /// <summary> Номер страницы </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary> Элементов на одной странице </summary>
        public int PageSize { get; set; } = 10;
        /// <summary> Поле для сортировки (например, "Name" или "Balance") </summary>
        public string? SortBy { get; set; }
        /// <summary> Порядок сортировки (по возрастанию или убыванию) </summary>
        public bool IsDescending { get; set; }
    }
}
