using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class Furniture
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Length { get; set; }
        public string? Color { get; set; }
        public string? Category { get; set; }
        public string? LivingSector { get; set; }
        public string? Material { get; set; }
        public int? Availability { get; set; }
        public decimal? Price { get; set; }
        public int? Rating { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? ImageIcon { get; set; }
        public List<BasketFurniture> BasketFurnitures { get; set; }
        public List<LikeFurniture> LikeFurnitures { get; set; }
        public List<OrderFurniture> OrderFurnitures { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
