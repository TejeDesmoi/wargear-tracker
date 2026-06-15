using System;
using System.Collections.Generic;
using System.Text;

namespace WargearTracker.Core
{
    public class Army
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Faction { get; set; } = string.Empty;
        public string Game { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? PublicSlug { get; set;
        }
        public bool IsPublic { get; set; } = false;
    }
}
