using System;

namespace Oauth.Data.DB.Models
{
    public partial class WildCard
    {
        public Guid WildcardId { get; set; }
        public int Id { get; set; }
        public string WildcardName { get; set; }
    }
}
