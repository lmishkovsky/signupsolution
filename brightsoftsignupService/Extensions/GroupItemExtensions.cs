using System;
using System.Linq;
using brightsoftsignupService.DataObjects;

namespace brightsoftsignupService.Extensions
{
    /// <summary>
    /// Group item extensions.
    /// </summary>
    public static class GroupItemExtensions
    {
        /// <summary>
        /// Pers the user filter.
        /// </summary>
        /// <returns>The user filter.</returns>
        /// <param name="query">Query.</param>
        /// <param name="groupCode">GroupCode.</param>
		public static IQueryable<GroupItem> PerGroupCodeFilter(this IQueryable<GroupItem> query, string groupCode)
		{
            return query.Where(item => item.GroupCode.Equals(groupCode));
		}
    }
}
