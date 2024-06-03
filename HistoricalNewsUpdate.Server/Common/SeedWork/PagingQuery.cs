using System.ComponentModel.DataAnnotations;

namespace HistoricalNewsUpdate.Common.SeedWork
{
    public class PagingQuery
    {
        /// <summary>
        /// Init
        /// </summary>
        public PagingQuery()
        {
            PageIndex = 1;
            PageSize = 10;
        }


        /// <summary>
        /// Limit
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page size is positive number only")]
        public int PageSize { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page Index is positive number only")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Sort field
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetFieldMapping()
        {
            return new Dictionary<string, string>();
        }
    }
}
