using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class Offer
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public decimal ProposedPrice { get; set; }

        public virtual User Employee { get; set; }
        public virtual Task Task { get; set; }
    }
}
