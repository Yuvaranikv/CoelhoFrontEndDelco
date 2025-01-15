using System;

namespace TURN.Models
{
    public class ClientModel
    {
        public int? ClientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
