using System;

namespace voice.models
{
    public class UsersSelectRequest : SearchParamRequest
    {
        public Guid? UserId { get; set; }
    }

    public class UsersSelectWithRoleRequest : UsersSelectRequest
    {
        public string Role { get; set; }
    }
}