using System;
using SignUp.Abstractions;

namespace SignUp.Models
{
    /// <summary>
    /// Group item.
    /// </summary>
    public class GroupItem : RowData
    {
        public string GroupCode { get; set; }

        public string Schedule { get; set; }
    }
}
