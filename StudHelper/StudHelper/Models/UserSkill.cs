using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class UserSkill
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SkillId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual User User { get; set; }
    }
}
