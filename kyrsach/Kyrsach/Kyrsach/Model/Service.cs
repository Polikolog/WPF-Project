using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Model
{
    internal class Service
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Rating { get; set; }
        public string Descripion { get; set; }
        public byte?[] Date { get; set; }
    }
}
