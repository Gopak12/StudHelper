using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class TaskSkill
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string SkillId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Task Task { get; set; }
    }
}
