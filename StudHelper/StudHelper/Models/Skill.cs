using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class Skill
    {
        public Skill()
        {
            TaskSkills = new HashSet<TaskSkill>();
            UserSkills = new HashSet<UserSkill>();
        }

        public string Id { get; set; }
        public string SkillName { get; set; }

        public virtual ICollection<TaskSkill> TaskSkills { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
