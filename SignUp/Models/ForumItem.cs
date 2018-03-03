using System;
using Newtonsoft.Json;
using SignUp.Abstractions;

namespace SignUp.Models
{
    public class ForumItem : RowData
    {
        /// <summary>
        /// "https://scontent.xx.fbcdn.net/v/t1.0-1/c33.33.414.414/s200x200/309820_10150436546956385_1524143185_n.jpg?oh=c79858eece7e33e96f577ac9a9cf3d40&oe=5A93D59F"
        /// </summary>
        public string ImageUrl  { get; set; }

        public string GroupCode { get; set; }

        public DateTime EventDate { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        // properties below are used strictly for display purposes

        [JsonIgnore]
        public string UpdatedAtAsString { get; set; }

        [JsonIgnore]
        public string Index { get; set; }
    }
}
