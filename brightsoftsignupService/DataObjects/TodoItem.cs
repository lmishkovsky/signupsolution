using Microsoft.Azure.Mobile.Server;

namespace brightsoftsignupService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public string GroupCode { get; set; }
    }
}