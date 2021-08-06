using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserGetByIdResponse
    {
        public Guid? Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Mobile { get; set; }
        
        public string ProfileUrl { get; set; }

        public string DmaCode { get; set; }

        public string UserIdForDmaStaff { get; set; }

        public DateTime Created { get; set; }
        
        public DateTime Modified { get; set; }

    }
}