using System.Linq;

namespace Helpers.SearchModels
{
    public class SearchResult<TEntity>
    {
        public IQueryable<TEntity> Body { get; set; }
        public int Count { get; set; }
    }
}
