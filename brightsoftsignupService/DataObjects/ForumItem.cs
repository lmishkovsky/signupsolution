﻿using System;
using Microsoft.Azure.Mobile.Server;

namespace brightsoftsignupService.DataObjects
{    
    /// <summary>
    /// Forum item.
    /// </summary>
    public class ForumItem : EntityData
    {
		public string GroupCode { get; set; }

        public DateTime EventDate { get; set; }

		public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string ImageUrl { get; set; }
    }
}
