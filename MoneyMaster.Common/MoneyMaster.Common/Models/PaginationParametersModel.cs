using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MoneyMaster.Common.Models
{
    /// <summary>
    /// Параметры для пагинации и фильтрации
    /// </summary>
    public class PaginationParametersModel
    {
        /// <summary> Ограничение максимального колличества подгружаемых элементов</summary>
        private const int MaxPageSize = 20;

        /// <summary> Номер страницы </summary>
        [Required(ErrorMessage = "Номер страницы обязателен.")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер страницы должен быть больше 0.")]
        public int PageNumber { get; set; } = 1;

        private int _pageSize { get; set; } = 10;

        /// <summary> Элементов на одной странице </summary>
        [Required(ErrorMessage = "Размер страницы обязателен.")]
        [Range(1, MaxPageSize, ErrorMessage = $"Размер страницы должен быть от 1 до 20.")]
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary> Поле для сортировки (например, "Name" или "Balance") </summary>
        public string? SortBy { get; set; } = null;

        /// <summary> Порядок сортировки (по возрастанию или убыванию) </summary>
        public bool IsDescending { get; set; } = false;

        /// <summary>
        /// Метод для генерации строки запроса.
        /// </summary>
        public string ToQueryString()
        {
            var queryParams = new Dictionary<string, string>
        {
            { "pageNumber", PageNumber.ToString() },
            { "pageSize", PageSize.ToString() },
            { "isDescending", IsDescending.ToString() }
        };

            // Добавляем параметр сортировки только если он не null
            if (!string.IsNullOrEmpty(SortBy))
            {
                queryParams.Add("sortBy", SortBy);
            }

            return string.Join("&", queryParams.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
        }
    }
}
