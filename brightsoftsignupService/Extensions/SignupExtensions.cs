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
        public static IQueryable<SignupItem> PerEventFilter(this IQueryable<SignupItem> query, string groupCode, DateTime dtEventDate)
		{
            return query.Where(item => item.GroupCode.Equals(groupCode) && item.EventDate.Equals((dtEventDate)));
		}
    }
}
