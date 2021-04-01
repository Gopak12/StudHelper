using System;
using System.Collections.Generic;

#nullable disable

namespace StudHelper.Models
{
    public partial class Doc
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string DocPath { get; set; }

        public virtual Task Task { get; set; }
    }
}
