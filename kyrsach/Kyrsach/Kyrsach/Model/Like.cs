using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Model
{
    internal class Like
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
    }
}
