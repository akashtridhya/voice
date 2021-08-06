using System;
using System.Text.Json.Serialization;

namespace voice.models.Requests.UserManagement
{
    public class DeleteRequest
    {
        public Guid? Id { get; set; }

        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}