using System;
using Microsoft.Azure.Mobile.Server;

namespace brightsoftsignupService.DataObjects
{
    /// <summary>
    /// Signup item.
    /// </summary>
    public class SignupItem : EntityData
    {
		public string GroupCode { get; set; }

        public DateTimeOffset EventDate { get; set; }

		public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
