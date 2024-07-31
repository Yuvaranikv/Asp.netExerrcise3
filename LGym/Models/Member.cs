namespace LGym.Models
{
    /// <summary>
    /// Member model
    /// </summary>
    public class Member
    {
        /// <summary>
        /// /the id of the e member
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// The name of the member
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The lastname of the member
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email of the member
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Join Date of member
        /// </summary>
        public DateTime JoinDate { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
