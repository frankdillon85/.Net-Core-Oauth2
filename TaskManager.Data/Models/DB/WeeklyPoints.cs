using System;
using System.Collections.Generic;

namespace TaskManager.Data.DB.Models
{
    public partial class WeeklyPoints
    {
        public Guid WeeklyPointsId { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? Points { get; set; }
        public int? WeekNumber { get; set; }
        public int Wildcard { get; set; }
    }
}
