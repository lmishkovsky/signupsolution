using System;
using SignUp.Abstractions;

namespace SignUp.Models
{
    /// <summary>
    /// Signup item.
    /// </summary>
    public class SignupItem : RowData
    {
        public string GroupCode { get; set; }

        public DateTimeOffset EventDate { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
