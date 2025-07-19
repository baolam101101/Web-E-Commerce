namespace Web_E_Commerce.DTOs.Shared
{
    public class PaginationWrapper<T>(int page, int pageSize, int totalItems, IEnumerable<T> items)
    {
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public int TotalItems { get; set; } = totalItems;
        public IEnumerable<T> Items { get; set; } = items;

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}