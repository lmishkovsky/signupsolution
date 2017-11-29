using System;
using System.Linq;
using brightsoftsignupService.DataObjects;

namespace brightsoftsignupService.Extensions
{
    /// <summary>
    /// Signup extensions.
    /// </summary>
    public static class SignupExtensions
    {
        /// <summary>
        /// Pers the event filter.
        /// </summary>
        /// <returns>The event filter.</returns>
        /// <param name="query">Query.</param>
        /// <param name="groupCode">Group code.</param>
        /// <param name="dtEventDate">Dt event date.</param>
        public static IQueryable<SignupItem> PerEventFilter(this IQueryable<SignupItem> query, string groupCode, DateTime dtEventDate)
		{
            return query.Where(item => item.GroupCode.Equals(groupCode) && item.EventDate.Equals((dtEventDate)));
		}

        /// <summary>
        /// Pers the event and user filter.
        /// </summary>
        /// <returns>The event and user filter.</returns>
        /// <param name="query">Query.</param>
        /// <param name="groupCode">Group code.</param>
        /// <param name="dtEventDate">Dt event date.</param>
        /// <param name="userID">User identifier.</param>
		public static IQueryable<SignupItem> PerEventAndUserFilter(this IQueryable<SignupItem> query, string groupCode, DateTime dtEventDate, string userID)
		{
            return query.Where(item => item.GroupCode.Equals(groupCode) && item.EventDate.Equals(dtEventDate) && item.UserID.Equals(userID));
		}

		public static IQueryable<ForumItem> PerEventMessageFilter(this IQueryable<ForumItem> query, string groupCode, DateTime dtEventDate)
		{
			return query.Where(item => item.GroupCode.Equals(groupCode) && item.EventDate.Equals((dtEventDate)));
		}
    }
}
