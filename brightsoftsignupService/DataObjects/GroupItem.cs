using System;
using Microsoft.Azure.Mobile.Server;

namespace brightsoftsignupService.DataObjects
{
    /// <summary>
    /// Group item.
    /// </summary>
    public class GroupItem : EntityData
    {
        /// <summary>
        /// The group identifier users will be entering
        /// </summary>
        /// <value>The group code.</value>
        public string GroupCode { get; set; }
    }
}
