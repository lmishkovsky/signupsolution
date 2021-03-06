﻿using System;
using Newtonsoft.Json;
using SignUp.Abstractions;

namespace SignUp.Models
{
    /// <summary>
    /// Signup item.
    /// </summary>
    public class SignupItem : RowData
    {
        public string GroupCode { get; set; }

        public DateTime EventDate { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        // properties below are used strictly for display purposes

        [JsonIgnore]
        public string UpdatedAtAsString { get; set; }

        [JsonIgnore]
        public string Index { get; set; }
    }
}
