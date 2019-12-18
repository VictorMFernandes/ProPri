using Rise.Core.Extensions;
using Rise.Core.Helpers;

namespace Rise.Users.Domain.Filters
{
    public class UserFilter
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }
        public EActiveFilter ActiveFilter { get; }

        public UserFilter(int pageNumber, int pageSize, string searchString, EActiveFilter? activeFilter)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ActiveFilter = activeFilter?? EActiveFilter.All;
            SearchString = searchString?.ToNeutral() ?? string.Empty;
        }
    }
}