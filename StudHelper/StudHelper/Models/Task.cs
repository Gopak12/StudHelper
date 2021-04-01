using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class Task
    {
        public Task()
        {
            Docs = new HashSet<Doc>();
            Offers = new HashSet<Offer>();
            TaskSkills = new HashSet<TaskSkill>();
        }

        public string Id { get; set; }
        public string EmployerId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int Status { get; set; }
        public string EmployerReviewDescr { get; set; }
        public string EmployeeReviewDescr { get; set; }
        public int? EmployerRating { get; set; }
        public int? EmployeeRating { get; set; }

        public virtual User Employee { get; set; }
        public virtual User Employer { get; set; }
        public virtual ICollection<Doc> Docs { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<TaskSkill> TaskSkills { get; set; }
    }
}
