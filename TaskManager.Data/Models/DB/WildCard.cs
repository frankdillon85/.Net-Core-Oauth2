using System;
using System.Collections.Generic;

namespace TaskManager.Data.DB.Models
{
    public partial class WildCard
    {
        public Guid WildcardId { get; set; }
        public int Id { get; set; }
        public string WildcardName { get; set; }
    }
}
