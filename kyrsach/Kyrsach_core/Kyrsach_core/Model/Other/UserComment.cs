using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model.Other
{
    public class UserComment
    {
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
