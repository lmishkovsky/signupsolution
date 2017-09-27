using System;
using Microsoft.Azure.Mobile.Server;

namespace SignUp.Backend.DataObjects
{
    /// <summary>
    /// Signup group.
    /// </summary>
    public class GroupItem : EntityData
    {
        public string GroupCode { get; set; }
    }
}
