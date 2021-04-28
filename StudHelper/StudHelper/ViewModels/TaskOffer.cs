using StudHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudHelper.ViewModels
{
    public class TaskOffer
    {
        public Models.Task Task { get; set; }
        public Offer Offer { get; set; }
    }
}
