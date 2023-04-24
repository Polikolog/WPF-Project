using Kyrsach_core.Infrastructur.Entity;

namespace Kyrsach_core.Model.Other
{
    public class UserComment
    {
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
