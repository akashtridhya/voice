namespace voice.models
{
    public class UserUpdateRequest : BaseIdRequest
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public int HouseNumber { get; set; }

        public int Postcode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }
        public string FbId { get; set; }
    }
}