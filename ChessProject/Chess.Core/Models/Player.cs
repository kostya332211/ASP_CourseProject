using System;

namespace Chess.Core.Models
{
    public class Player : Entity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public Guid PlayerDetailsId { get; set; }

        public int StatusId { get; set; }

        public int RoleId { get; set; }

        #region Navigation properties

        public Status Status { get; set; }

        public Role Role { get; set; }

        public PlayerDetails PlayerDetails { get; set; }

        #endregion
    }
}
