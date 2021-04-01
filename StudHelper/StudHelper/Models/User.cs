using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class User : IdentityUser
    {
        public User() : base()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Offers = new HashSet<Offer>();
            TaskEmployees = new HashSet<Task>();
            TaskEmployers = new HashSet<Task>();
            UserSkills = new HashSet<UserSkill>();
        }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Task> TaskEmployees { get; set; }
        public virtual ICollection<Task> TaskEmployers { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
